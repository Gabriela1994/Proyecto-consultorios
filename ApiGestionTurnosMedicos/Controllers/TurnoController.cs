using ApiGestionTurnosMedicos.CustomModels;
using ApiGestionTurnosMedicos.Services;
using ApiGestionTurnosMedicos.Validations;
using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models.CustomModels;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGestionTurnosMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public TurnoController(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/<TurnoController>
        [HttpGet]
        public List<VwTurno> Get()
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.ShiftList();   
        }

        // GET api/<TurnoController>/5
        [HttpGet("{id}")]
        public VwTurno Get(int id)
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.GetShiftForId(id);
        }

        // POST api/<TurnoController>
        [HttpPost]
        public IActionResult Post([FromBody] TurnoCustom oShift)
        {
            const int EstadoActivo = 1;

            ValidationsMethodPost validations = new ValidationsMethodPost(_context);
            ValidationsMethodPost validationResult = validations.ValidationsMethodPostShift(oShift);

            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            try
            {
                TurnoLogic sLogic = new TurnoLogic(_context);
                TurnoCustom shiftCustom = new TurnoCustom();
                Turno shift = new Turno();

                shift.MedicoId = oShift.MedicoId;
                shift.PacienteId = oShift.PacienteId;
                shift.Fecha = shiftCustom.ModifyDate(oShift.Fecha);
                shift.Hora = shiftCustom.ModifyHour(oShift.Hora);
                shift.EstadoId = oShift.EstadoId; //EstadoActivo;
                shift.Observaciones = oShift.Observaciones;

                sLogic.CreateShift(shift);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTurno(int id, [FromBody] TurnoCustom turnoUpdate)
        {
            if (turnoUpdate == null)
            {
                return BadRequest(new { Message = "Los datos del turno no pueden ser nulos." });
            }

            // Verificar si el turno existe
            var turno = _context.Turnos.Find(id);
            if (turno == null)
            {
                return NotFound(new { Message = "Turno no encontrado." });
            }

            // Verificar si el médico existe
            var medicoExistente = _context.Medicos.Find(turnoUpdate.MedicoId);
            if (medicoExistente == null)
            {
                return BadRequest(new { Message = "Médico no encontrado." });
            }

            // Validar datos de entrada
            var validations = new ValidationsMethodPut(_context);
            var validationResult = validations.ValidationsMethodPutShift(turnoUpdate);
            if (!validationResult.IsValid)
            {
                return BadRequest(new { Message = validationResult.ErrorMessage });
            }

            // Intentar convertir Fecha y Hora
            if (!DateTime.TryParse(turnoUpdate.Fecha, out DateTime fecha) ||
                !TimeSpan.TryParse(turnoUpdate.Hora, out TimeSpan hora))
            {
                return BadRequest(new { Message = "Formato de fecha u hora inválido." });
            }

            // Actualizar campos del turno
            turno.Fecha = fecha;
            turno.Hora = hora;
            turno.MedicoId = turnoUpdate.MedicoId;
            turno.PacienteId = turnoUpdate.PacienteId;
            turno.EstadoId = turnoUpdate.EstadoId;
            turno.Observaciones = turnoUpdate.Observaciones;

            // Guardar cambios en la base de datos
            _context.SaveChanges();

            return Ok(new { Message = "Turno actualizado correctamente." });
        }



        [HttpPut("set-turno-status/{id}")]
        public IActionResult Put(int id, [FromQuery] int st)
        {

            Estado oStatus = new ();

            oStatus.Id = st;

            ValidationsMethodPut validations = new ValidationsMethodPut(_context);
            ValidationsMethodPut validationResult = validations.ValidationMethodPutStatus(oStatus);

            // Para que el FrontEnd pueda mostrar un mensaje más específico se
            // retorna un estatus HTML 400 con el mensaje de error que viene del
            // validador.
            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            TurnoLogic sLogic = new TurnoLogic(_context);
            sLogic.UpdateShiftStatus(id, oStatus.Id);

            return Ok();
        }

        // DELETE api/<TurnoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            sLogic.DeleteShift(id);
        }

        [HttpGet("get-turnos-for-doctor")]
        public List<HorarioTurnos> GetBusyShiftsGroupedByDay(int idDoctor)
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.ListOfShiftsGroupedByDay(idDoctor);
        }

        [HttpGet("get-turnos-of-patient/{idPaciente}")]
        public List<Turno> GetListOfShiftsByPatient(int idPaciente)
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.ListOfShiftsByPatient(idPaciente);
        }

        [HttpGet("get-turnos-of-doctor/{idMedico}")]
        public List<Turno> GetListOfShiftsByDoctor(int idMedico)
        {
            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.ListOfShiftsByDoctor(idMedico);
        }

        // Este no es usado
        [HttpGet("get-turnos-disponibles")]
        public List<HorarioTurnos> GetListOfAvailableShifts(int idDoctor)
        {
            // Este no es usado

            TurnoLogic sLogic = new TurnoLogic(_context);
            return sLogic.ListOfAvailableShifts(idDoctor);
        }

        [HttpGet("get-turnos-of-date/{fecha}")]
        public List<VwTurno> GetShiftsOfDate(DateTime fecha)
        {
            TurnoLogic sLogic = new (_context);
            return sLogic.ListOfShiftsOfDate(fecha);
        }

    }
}
