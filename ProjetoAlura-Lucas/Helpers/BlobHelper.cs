using Azure.Storage.Blobs;

namespace ProjetoAlura_Lucas.Helpers
{
    public static class BlobHelper
    {
        public static async Task<string> UploadFile(IFormFile file, string connectionString)
        {
            if (file == null)
            {
                return null;
            }

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("data");

            string photoName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            BlobClient blobClient = containerClient.GetBlobClient(photoName);

            // max 2mb
            int maxSizeBytes = 2 * 1024 * 1024;
            if (file.Length > maxSizeBytes)
            {
                throw new Exception("The photo size exceeds the maximum limit of 2MB.");
            }

            // Upload blob
            using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // retorna uri do arquivo gravado na azure
            return blobClient.Uri.ToString();
        }

        public static async Task DeleteBlob(string blobUrl, string connectionString)
        {
            // Create a BlobServiceClient object using the connection string
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("data");

            // Get the name of the blob from the URL
            Uri uri = new Uri(blobUrl);
            string blobName = Path.GetFileName(uri.LocalPath);

            // Get a reference to the blob
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            // Delete the blob
            await blobClient.DeleteAsync();
        }
    }
}
