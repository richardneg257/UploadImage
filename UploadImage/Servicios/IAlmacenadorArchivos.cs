namespace UploadImage.Servicios;

public interface IAlmacenadorArchivos
{
    Task<string> GuardarArchivo(byte[] contenido, string fileName, string contenedor, string contentType);
}
