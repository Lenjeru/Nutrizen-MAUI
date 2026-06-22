namespace TiendaCursos.API.Helpers
{
    public interface IFicheroAlmacenamiento
    {
        Task<string> GuardarFicheroAsync(byte[] content, string extension, string nombreContenedor);
        Task BorrarFicheroAsync(string path, string nombreContenedor);
        async Task<string> EditarFicheroAsync(byte[] content, string extension, string nombreContenedor, string ruta)
        {
            if (ruta is not null)
            {
                await BorrarFicheroAsync(ruta, nombreContenedor);
            }
            return await GuardarFicheroAsync(content, extension, nombreContenedor);
        }
    }
}
