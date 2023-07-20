using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadImage.Servicios;

namespace UploadImage.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GatosController : ControllerBase
{
    private readonly IAlmacenadorArchivos _almacenadorArchivos;
    private readonly string contenedor = "gatos";

    public GatosController(IAlmacenadorArchivos almacenadorArchivos)
    {
        _almacenadorArchivos = almacenadorArchivos;
    }

    [HttpPost("upload")]
    public async Task<ActionResult<string>> Upload([FromForm] IFormFile archivo)
    {
        string nombreArchivo = string.Empty;
        if (archivo is null) return BadRequest();

        using(var ms = new MemoryStream())
        {
            await archivo.CopyToAsync(ms);
            var arregloBytes = ms.ToArray();
            var extension = Path.GetExtension(archivo.FileName);
            nombreArchivo = await _almacenadorArchivos.GuardarArchivo(arregloBytes, archivo.FileName, contenedor, archivo.ContentType);
        }

        return nombreArchivo;
    }
}
