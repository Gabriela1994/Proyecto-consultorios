using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using ApiGestionTurnosMedicos.CustomModels;
using DataAccess.Repository;
using ApiGestionTurnosMedicos.Validations;
using System.ComponentModel.DataAnnotations;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGestionTurnosMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public MedicoController(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/<MedicoController>
        [HttpGet]
        public List<Medico> Get()
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            return dLogic.DoctorList();
        }

        // GET api/<MedicoController>/5
        [HttpGet("{id}")]
        public Medico Get(int id)
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            return dLogic.GetDoctorForId(id);
        }

        // POST api/<MedicoController>
        [HttpPost]
        public IActionResult Post([FromBody] MedicoCustom oDoctor)
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            Medico medico = new Medico();
            MedicoCustom medicoCustom = new MedicoCustom();
            ValidationsMethodPost validations = new ValidationsMethodPost(_context);
            ValidationsMethodPost validationResult = validations.ValidationsMethodPostDoctor(oDoctor);

            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            medico.Nombre= oDoctor.Nombre;
            medico.Apellido = oDoctor.Apellido;
            medico.EspecialidadId = oDoctor.EspecialidadId;
            medico.FechaAltaLaboral = oDoctor.FechaAltaLaboral;
            medico.Direccion = oDoctor.Direccion;
            medico.Dni = oDoctor.Dni;
            medico.Telefono = oDoctor.Telefono;
            medico.HorarioAtencionInicio = medicoCustom.ModifyStartTime(oDoctor.HorarioAtencionInicio);
            medico.HorarioAtencionFin = medicoCustom.ModifyEndTime(oDoctor.HorarioAtencionFin);

            dLogic.CreateDoctor(medico);
            return Ok();
        }

        // PUT api/<MedicoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MedicoCustom oDoctor)
        {
            ValidationsMethodPut validations = new ValidationsMethodPut(_context);
            ValidationsMethodPut validationResult = validations.ValidationsMethodPutDoctor(oDoctor);

            if (validationResult.IsValid == false) return BadRequest(new { validationResult.ErrorMessage });

            MedicoLogic dLogic = new MedicoLogic(_context);
            dLogic.UpdateDoctor(id, oDoctor);
            return Ok();
        }

        // DELETE api/<MedicoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            dLogic.DeleteDoctor(id);
        }

        [HttpGet("list-for-specialty/{id}")]
        public List<Medico> GetListDoctorForSpecialty(int id)
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            return dLogic.FindDoctorForSpecialty(id);
        }
        
        [HttpGet("get-all-doctors")]
        public List<MedicoCustom> ReturnAllDoctorsWithOurSpecialty()
        {
            MedicoLogic dLogic = new MedicoLogic(_context);
            return dLogic.ReturnAllDoctorsWithOurSpecialty();
        }
    }
}
