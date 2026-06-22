
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace TiendaCursos.API.Helpers
{
    public class FicheroAlmacenamiento : IFicheroAlmacenamiento
    {
        private readonly string cadenaConexion;

        public FicheroAlmacenamiento(IConfiguration configuration)
        {
            cadenaConexion = configuration.GetConnectionString("AzureConexion")!;
        }
        public async Task BorrarFicheroAsync(string ruta, string nombreContenedor)
        {
            var cliente = new BlobContainerClient(cadenaConexion, nombreContenedor);
            await cliente.CreateIfNotExistsAsync();
            var fileName = Path.GetFileName(ruta);
            var blob = cliente.GetBlobClient(fileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> GuardarFicheroAsync(byte[] content, string extension, string nombreContenedor)
        {
            var cliente = new BlobContainerClient(cadenaConexion, nombreContenedor);
            await cliente.CreateIfNotExistsAsync();
            cliente.SetAccessPolicy(PublicAccessType.Blob);
            var nombreFichero = $"{Guid.NewGuid()}{extension}";
            var blob = cliente.GetBlobClient(nombreFichero);
            using (var ms = new MemoryStream(content))
            {
                await blob.UploadAsync(ms);
            }
            return blob.Uri.ToString();
        }
    }
}
