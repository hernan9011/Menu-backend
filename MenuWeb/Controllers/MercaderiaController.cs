using Application.Interface;
using Application.Responsive;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MenuWeb.Controllers
{
    [Route("/api/[controler]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IServicesMercaderia _service;
        public MercaderiaController(IServicesMercaderia service)
        {
            _service = service;
        }

        [HttpGet("/api/v1/Mercaderia")]
        public async Task<ActionResult> GetSerach(int? tipo, string? nombre , string? orden)
        {
            if (!orden.IsNullOrEmpty() && orden.ToLower() != "asc" && orden.ToLower() != "desc" || tipo < 1 || tipo > 10)
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }
            try
            {
                var result = await _service.GetBuscar(tipo,nombre,orden);
                return new JsonResult(result){ StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("/api/v1/Mercaderia")]
        public async Task<ActionResult> Post(MercaderiaRequest request)
        {
            if ( request.Tipo < 1 || request.Tipo > 10 || request.Precio < 1                            
                || request.Nombre.IsNullOrEmpty() || request.Nombre.Length > 50
                || request.Preparacion.IsNullOrEmpty() || request.Preparacion.Length > 255
                || request.Ingredientes.IsNullOrEmpty() || request.Ingredientes.Length > 255
                || request.Imagen.IsNullOrEmpty() || request.Imagen.Length > 255)                          
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }
            try
            {
                var name =await _service.Search(request.Nombre);
                if (name != null)
                {
                    return Conflict(new { message = "El nombre esta siendo usado" });
                }
                var result = await _service.InsertMer(request);
                return new JsonResult(result) { StatusCode = 201};
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        } 
   
        [HttpGet("/api/v1/Mercaderia/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }
            try
            {
                var search = await _service.GetMerId(id);
                if (search == null)
                {
                    return NotFound(new { message = "No se proporcionó una id válida" }); 
                }
                var result = await _service.GetMerId(id);
                return new JsonResult(result) { StatusCode = 200};
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        } 
        
        [HttpPut("/api/v1/Mercaderia/{id}")]
        public async Task<IActionResult> PutId(int id,MercaderiaRequest request)
        {         
            if (request.Tipo < 1 || request.Tipo > 10 || request.Precio < 1
              || request.Nombre.IsNullOrEmpty() || request.Nombre.Length > 50
              || request.Preparacion.IsNullOrEmpty() || request.Preparacion.Length > 255
              || request.Ingredientes.IsNullOrEmpty() || request.Ingredientes.Length > 255
              || request.Imagen.IsNullOrEmpty() || request.Imagen.Length > 255)
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }          
            try
            {  
                var name = await _service.Search(request.Nombre);
                if (name != null)
                {
                    return Conflict(new { message = "El nombre esta siendo usado" });
                }
                var search = await _service.GetMerId(id);
                if (search == null)
                {
                    return NotFound(new { message = "No se encontro la id" });
                }
                var result = await _service.PutMerId(id,request);
                return new JsonResult(result) { StatusCode = 200};
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
       
        [HttpDelete("/api/v1/Mercaderia/{id}")]  
        public async Task<IActionResult> DeleteId(int id)
        {
            if (id < 1)
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }          
            try
            {
                var search = await _service.GetMerId(id);
                if(search == null)
                {
                    return NotFound(new { message = "No se encontro la id" });
                }
                var result = await _service.RemoveMer(id);   
                if (result == null)
                {
                    return Conflict(new { message = "El elemento esta siendo usado" });
                }
                else
                {
                    return new JsonResult(result) { StatusCode = 200 };
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
   
    }
}
