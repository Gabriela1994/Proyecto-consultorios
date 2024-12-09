using BusinessLogic.AppLogic;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiGestionTurnosMedicos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public EstadoController(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/estado
        [HttpGet]
        public List<Estado> Get()
        {
            EstadoLogic estadoLogic = new EstadoLogic(_context);
            return estadoLogic.GetAllEstados();
        }

        // GET api/estado/5
        [HttpGet("{id}")]
        public Estado Get(int id)
        {
            EstadoLogic estadoLogic = new EstadoLogic(_context);
            return estadoLogic.GetEstadoById(id);
        }

        // POST api/estado
        [HttpPost]
        public IActionResult Post([FromBody] Estado estado)
        {
            try
            {
                EstadoLogic estadoLogic = new EstadoLogic(_context);
                estadoLogic.CreateEstado(estado);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/estado/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Estado estado)
        {
            try
            {
                EstadoLogic estadoLogic = new EstadoLogic(_context);
                estadoLogic.UpdateEstado(id, estado);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/estado/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            EstadoLogic estadoLogic = new EstadoLogic(_context);
            estadoLogic.DeleteEstado(id);
        }
    }
}
