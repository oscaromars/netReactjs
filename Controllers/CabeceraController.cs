namespace Formaapp.Controllers // Ajusta según tu espacio de nombres
{
    using Microsoft.AspNetCore.Mvc;
    using Formaapp.Models; // Ajusta según tu espacio de nombres
    using Formaapp.Services; // Ajusta según tu espacio de nombres

    [ApiController]
    [Route("api/[controller]")]
    public class CabeceraController : ControllerBase
    {
        private readonly CabeceraService _cabeceraService;

        public CabeceraController(CabeceraService cabeceraService)
        {
            _cabeceraService = cabeceraService;
        }

        [HttpGet]
        public IActionResult GetCabeceras()
        {
            var cabeceras = _cabeceraService.GetAll();
            return Ok(cabeceras);
        }

        [HttpGet("{id}")]
        public IActionResult GetCabecera(int id)
        {
            var cabecera = _cabeceraService.GetById(id);
            if (cabecera == null) return NotFound();
            return Ok(cabecera);
        }



        [HttpPost]
        public IActionResult CreateCabecera([FromBody] Cabecera cabecera)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

             // Maneja solo el cabeceraId en los detalles
                foreach (var detalle in cabecera.Detalles)
                    {
                            detalle.CabeceraId = cabecera.Id; // Asigna el ID de la cabecera
                    }
            
            _cabeceraService.Create(cabecera);
            return CreatedAtAction(nameof(GetCabecera), new { id = cabecera.Id }, cabecera);
        }
        

        [HttpPut("{id}")]
        public IActionResult UpdateCabecera(int id, [FromBody] Cabecera cabecera)
        {
            if (id != cabecera.Id) return BadRequest("ID del recurso no coincide con el ID del objeto.");

            try
            {
                _cabeceraService.Update(cabecera);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCabecera(int id)
        {
            try
            {
                _cabeceraService.Delete(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
