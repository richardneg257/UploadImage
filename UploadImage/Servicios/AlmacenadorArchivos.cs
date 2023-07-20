namespace UploadImage.Servicios;

public class AlmacenadorArchivos : IAlmacenadorArchivos
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AlmacenadorArchivos(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> GuardarArchivo(byte[] contenido, string fileName, string contenedor, string contentType)
    {
        var nombreArchivo = fileName;
        string folder = Path.Combine(_env.WebRootPath, contenedor);

        if(!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        string ruta = Path.Combine(folder, nombreArchivo);

        await File.WriteAllBytesAsync(ruta, contenido);

        var urlBase = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";
        var urlCompleta = Path.Combine(urlBase, contenedor, nombreArchivo).Replace("\\", "/") ;

        return urlCompleta;
    }
}
