using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiGestionTurnosMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadController : ControllerBase
    {
        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public EspecialidadController(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion


        // GET: api/<EspecialidadesController>
        [HttpGet]
        public List<Especialidad> Get()
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            return eLogic.SpecialtyList();
        }

        // GET api/<EspecialidadesController>/5
        [HttpGet("{id}")]
        public Especialidad Get(int id)
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            return eLogic.GetSpecialtyForId(id);
        }

        // POST api/<EspecialidadesController>
        [HttpPost]
        public IActionResult Post([FromBody] Especialidad oEspecialidad)
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            eLogic.CreateSpecialty(oEspecialidad);
            return Ok();
        }

        // PUT api/<EspecialidadesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Especialidad oEspecialidad)
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            eLogic.UpdateSpecialty(id, oEspecialidad);
            return Ok();
        }

        // DELETE api/<EspecialidadesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            eLogic.DeleteSpecialty(id);
        }

        [HttpGet("list-covered-specialty")]
        public List<Especialidad> GetListCoveredSpecialty()
        {
            EspecialidadLogic eLogic = new EspecialidadLogic(_context);
            return eLogic.CoveredSpecialtyList();
        }
    }
}
