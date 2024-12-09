using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TurnoRepository
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public TurnoRepository(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public List<VwTurno> GetAllShift()
        {
            using (_context)
            {
                return _context.VwTurnos.ToList();
            }
        }

        public List<VwTurno> GetShiftsOfDate(DateTime fecha)
        {
            using (_context)
            {
                return _context.VwTurnos.Where(t => t.Fecha == fecha).ToList();
            }
        }


        public Turno GetShiftById(int id)
        {
            return _context.Turnos.Find(id);
        }

        public VwTurno GetDisplayShiftById(int id)
        {
            return _context.VwTurnos.Where(t => t.Id == id).FirstOrDefault();
        }


        public void CreateShift(Turno oShift)
        {
            using (_context)
            {
                _context.Turnos.Add(oShift);
                _context.SaveChanges();
            }
        }

        public void UpdateShift(Turno oShift)
        {
            using (_context)
            {
                _context.Entry(oShift).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteShift(Turno oShift)
        {
            using (_context)
            {
                _context.Turnos.Remove(oShift);
                _context.SaveChanges();
            }
        }
        public List<HorarioTurnos> ListOfShiftsGroupedByDay(int id_doctor)
            // Lista de turnos agrupados por dia.
        {
            TurnoCustom turnoCustom = new TurnoCustom();
            List<HorarioTurnos> horario = new List<HorarioTurnos>();
            const int estado_cancelado = 2;
            DateTime fecha_actual = DateTime.Now;

            horario = (from t in _context.Turnos
                       join m in _context.Medicos on t.MedicoId equals m.Id
                       where m.Id == id_doctor
                       where t.EstadoId != estado_cancelado
                       where t.Fecha >= fecha_actual
                       group t by t.Fecha into g
                       select new HorarioTurnos
                       {
                           Fecha_turno = g.Key,
                           Hora_turno = g.Select(t => t.Hora).ToList()
                       }).ToList();

            return horario;
        }

        public bool VerifyIfShiftExist(int medicoId, DateTime fecha, TimeSpan hora, int? turnoId = null)
        {
            // Busca si existe algún turno en la misma fecha y hora, pero excluye el turno con el ID actual
            var turnoExistente = _context.Turnos
                .Where(t => t.MedicoId == medicoId
                            && t.Fecha == fecha
                            && t.Hora == hora
                            && (!turnoId.HasValue || t.Id != turnoId)) // Excluir el turno actual que se está modificando
                .FirstOrDefault();

            // Retorna true si encuentra un turno, false si no lo encuentra
            return turnoExistente != null;
        }

        public List<Turno> GetShiftsByPatient(int idPaciente)
        {
            using (_context)
            {
                return _context.Turnos.Where(t => t.PacienteId == idPaciente).ToList();
            }
        }

        public List<Turno> GetShiftsByDoctor(int idMedico)
        {
            using (_context)
            {
                return _context.Turnos.Where(t => t.MedicoId == idMedico).ToList();
            }
        }
    }
}
