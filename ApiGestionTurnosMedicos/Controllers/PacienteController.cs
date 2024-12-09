using ApiGestionTurnosMedicos.Validations;
using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGestionTurnosMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public PacienteController(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/<PacientesController>
        [HttpGet]
        public List<Paciente> Get()
        {
            PacienteLogic pLogic = new PacienteLogic(_context);
            return pLogic.PatientsList();
        }

        // GET api/<PacientesController>/5
        [HttpGet("{id}")]
        public Paciente Get(int id)
        {
            PacienteLogic pLogic = new PacienteLogic(_context);
            return pLogic.GetPatientForId(id);            
        }

        // POST api/<PacientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Paciente oPatient)
        {
            ValidationsMethodPost validations = new ValidationsMethodPost(_context);
            ValidationsMethodPost validationResult = validations.ValidationsMethodPostPatient(oPatient);

            // Para que el FrontEnd pueda mostrar un mensaje más específico se
            // retorna un estatus HTML 400 con el mensaje de error que viene del
            // validador.
            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            PacienteLogic pLogic = new PacienteLogic(_context);
            pLogic.CreateAPatient(oPatient);

            return Ok();
        }

        // PUT api/<PacientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Paciente oPatient)
        {
            ValidationsMethodPut validations = new ValidationsMethodPut(_context);
            ValidationsMethodPut validationResult = validations.ValidationsMethodPutPatient(oPatient);

            // Para que el FrontEnd pueda mostrar un mensaje más específico se
            // retorna un estatus HTML 400 con el mensaje de error que viene del
            // validador.
            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            PacienteLogic pLogic = new PacienteLogic(_context);
            pLogic.UpdatePatient(id, oPatient);

            return Ok();
        }

        // DELETE api/<PacientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PacienteLogic pLogic = new PacienteLogic(_context);
            pLogic.DeletePatient(id);
        }

        [HttpGet("get-dni")]
        public ActionResult<Paciente> GetPatientForDni(string dni)
        {
            try {
                PacienteLogic pLogic = new PacienteLogic(_context);
                return pLogic.GetPatientForDNI(dni);

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
