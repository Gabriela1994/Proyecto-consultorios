using ApiGestionTurnosMedicos.CustomModels;
using ApiGestionTurnosMedicos.Services;
using DataAccess.Data;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.AppLogic
{
    public class TurnoLogic
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public TurnoLogic(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        private readonly HttpClient _httpClient;

        public TurnoLogic(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<VwTurno> ShiftList()
        {
            TurnoRepository repShift = new TurnoRepository(_context);
            return repShift.GetAllShift();
        }

        public VwTurno GetShiftForId(int id)
        {
            if (id == 0) throw new ArgumentException("Id cannot be 0");

            try
            {
                TurnoRepository repShift = new TurnoRepository(_context);
                VwTurno oShiftFound = repShift.GetDisplayShiftById(id) ?? throw new ArgumentException("No shift was found with that id");
                return oShiftFound;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void CreateShift(Turno oShift)
        {

            #region Validations

            if (oShift.Observaciones == "" || oShift.Observaciones == "string")
            {
                oShift.Observaciones = "Sin observaciones agregadas";
            }

            #endregion

            try
            {
                TurnoRepository repShift = new TurnoRepository(_context);
                MedicoRepository repDoctor = new MedicoRepository(_context);
                PacienteRepository repPatient = new PacienteRepository(_context);


                Paciente patient = repPatient.GetPatientForId(oShift.PacienteId);
                MedicoConEspecialidad doctor = new MedicoConEspecialidad();
                MedicoConEspecialidad Odoctor = repDoctor.ReturnDoctorWithSpecialty(oShift.MedicoId);

                repShift.CreateShift(oShift);

                Message message = new Message();

                message.SendEmail("Constancia de turno",
                    $"<div>\r\n<h1>Reserva de turno</h1>\r\n  <h3>Gracias por confiar en Consultorios A.G.J.S! Te informamos que tu turno ha sido reservado con éxito</h3>\r\n\r\n" +
                    $"<h2 style=\"color: rgba(0, 128, 0, 0.751)\">Datos del paciente:</h2>\r\n <h4>Nombre y apellido: {patient.NombreCompletoPaciente()}</h4>\r\n   <h4>Documento: {patient.Dni}</h4>\r\n  <h4>Fecha de nacimiento: {patient.FechaNacimiento}</h4>\r\n <h4>Edad: {patient.EdadDelPaciente(patient.FechaNacimiento)} </h4>\r\n\r\n <h2 style=\"color: rgba(0, 128, 0, 0.751)\">Constancia del turno:</h2>\r\n " +
                    $"<h4>Profesional: {doctor.NombreCompletoMedico(Odoctor.Nombre, Odoctor.Apellido)}</h4>\r\n <h4>Especialidad: {Odoctor.Especialidad}</h4>\r\n <h4>Fecha y hora del turno: {oShift.Fecha.ToString(@"dd\-MM\-yyyy")} a las {oShift.Hora.ToString(@"hh\:mm") }hrs</h4>\r\n  <h4>Observaciones: {oShift.Observaciones}</h4>\r\n\r\n " +
                    "<h2 style=\"color: rgba(0, 128, 0, 0.751)\">Requisitos para la atencion: </h2>\r\n <h4 style=\"color: rgba(255, 0, 0, 0.824)\">No olvides traer tu DNI</h4>\r\n <h4>Es requisito indispensable para poder ser atendido</h4>\r\n </div>",
                    $"{patient.Email}");            

            }
            

            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void UpdateShift(int id, Turno oShift)
        {
            TurnoRepository repShift = new TurnoRepository(_context);

            try
            {
                Turno oShiftFound = repShift.GetShiftById(id) ?? throw new ArgumentException("No Shift was found with that id");
                oShiftFound.Hora = oShift.Hora;
                oShiftFound.Fecha = oShift.Fecha;
                oShiftFound.Observaciones = oShift.Observaciones;
                repShift.UpdateShift(oShiftFound);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void UpdateShiftStatus(int id, int status)
        {
            TurnoRepository repShift = new TurnoRepository(_context);

            try
            {
                Turno oShiftFound = repShift.GetShiftById(id) ?? throw new ArgumentException("No Shift was found with that id");
                oShiftFound.EstadoId = status;
                repShift.UpdateShift(oShiftFound);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void DeleteShift(int id)
        {
            TurnoRepository repShift = new TurnoRepository(_context);

            try
            {
                Turno oShiftFound = repShift.GetShiftById(id) ?? throw new ArgumentException("No shift was found with that id");
                repShift.DeleteShift(oShiftFound);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public List<HorarioTurnos> ListOfShiftsGroupedByDay(int idDoctor)
        //Devuelve la lista de turnos ocupados por dia
        {
            TurnoRepository repoTurno = new TurnoRepository(_context);
            return repoTurno.ListOfShiftsGroupedByDay(idDoctor);
        }

        public List<HorarioTurnos> ListOfAvailableShifts(int idDoctor)
        //Devuelve la lista de turnos disponibles por dia
        // Este no es usado
        {
            MedicoRepository repTurno = new MedicoRepository(_context);

            Medico medico = repTurno.GetDoctorForId(idDoctor, repTurno.Get_context());
            TimeSpan hora_llegada = medico.HorarioAtencionInicio;
            TimeSpan hora_salida = medico.HorarioAtencionFin;

            List<TimeSpan> horarios_disponibles = new List<TimeSpan>();

            TimeSpan _INTERVALO = TimeSpan.FromHours(1);

            for (TimeSpan hora = hora_llegada; hora < hora_salida; hora += _INTERVALO)
            {
                horarios_disponibles.Add(hora);
            }

            List<HorarioTurnos> turnos_ocupados = ListOfShiftsGroupedByDay(idDoctor);
            List<HorarioTurnos> turnos_disponibles = new List<HorarioTurnos>();

            foreach (var h in turnos_ocupados)
            {
                List<TimeSpan> horarios_libres = horarios_disponibles.Except(h.Hora_turno).ToList();

                HorarioTurnos busyShifts = new HorarioTurnos();
                busyShifts.Fecha_turno = h.Fecha_turno;
                busyShifts.Hora_turno = horarios_libres;

                turnos_disponibles.Add(busyShifts);
            }
            return turnos_disponibles;
        }

        public List<VwTurno> ListOfShiftsOfDate(DateTime fecha)
        //Devuelve la lista de turnos de una fecha
        {
            TurnoRepository repoTurno = new(_context);
            return repoTurno.GetShiftsOfDate(fecha);
        }

        public List<Turno> ListOfShiftsByPatient(int idPaciente)
        //Devuelve la lista de turnos de un paciente
        {
            TurnoRepository repoTurno = new(_context);
            return repoTurno.GetShiftsByPatient(idPaciente);
        }
        public List<Turno> ListOfShiftsByDoctor(int idMedico)
        //Devuelve la lista de turnos de un médico
        {
            TurnoRepository repoTurno = new(_context);
            return repoTurno.GetShiftsByDoctor(idMedico);
        }
    }
}
