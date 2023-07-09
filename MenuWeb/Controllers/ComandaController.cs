using Application.Interface;
using Application.Responsive;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;

namespace MenuWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IServicesComanda _service;
        private readonly IServicesMercaderia _servicemer;
        public ComandaController(IServicesComanda service, IServicesMercaderia servicemer)
        {
            _service = service;
            _servicemer = servicemer;
        }

        [HttpGet("/api/v1/Comanda")]
        public async Task<ActionResult> GetSerach(string? fecha)
        {

            if (!fecha.IsNullOrEmpty() && !DateTime.TryParse(fecha, out _))
            {
                
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }
            try
            {
                var result = await _service.GetSearchId(fecha);
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("/api/v1/Comanda")]
        public async Task<ActionResult<ComandaResponse>> Post(ComandaRequest request)
        {            
            if (request.FormaEntrega <= 0 || request.FormaEntrega > 3)
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }           
            try{    
                foreach(var num in request.Mercaderias)
                {
                    var a =await _servicemer.GetMerId(num);
                    if (a == null) { return BadRequest(new { message = "No se proporcionó una id válida" });}                                                  
                }     
                var result = await _service.InsertCom(request);
                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("/api/v1/Comanda/{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            if(id.Equals(Guid.Empty))
            {
                return BadRequest(new { message = "No se proporcionó una solicitud válida" });
            }
            try
            {
                var result = await _service.GetSearchId(id);
                if (result != null)
                {
                    return new JsonResult(result){ StatusCode = 200};
                }
                else
                {
                    return NotFound(new { message = "NO se encontro el elemento" });
                }
            }       
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
