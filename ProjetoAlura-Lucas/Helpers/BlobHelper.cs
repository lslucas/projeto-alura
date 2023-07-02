using Azure.Storage.Blobs;

namespace ProjetoAlura_Lucas.Helpers
{
    public static class BlobHelper
    {
        public static async Task<string> UploadFile(IFormFile file)
        {
            if (file == null)
            {
                return null;
            }

            string connectionString = "DefaultEndpointsProtocol=https;AccountName=lslucas;AccountKey=hCPsx6GtF3a7AytibIeH87xLoTDPTHjKK0q4xdWSiYladGkOzzNIEnBv+h6Z4rpkTOF8IsnuERps+ASt7OvKxQ==;EndpointSuffix=core.windows.net";

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

        public static async Task DeleteBlob(string blobUrl)
        {
            // Retrieve the connection string for your Azure Storage account
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=lslucas;AccountKey=hCPsx6GtF3a7AytibIeH87xLoTDPTHjKK0q4xdWSiYladGkOzzNIEnBv+h6Z4rpkTOF8IsnuERps+ASt7OvKxQ==;EndpointSuffix=core.windows.net";

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
