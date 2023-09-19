using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Security.Policy;

namespace TiendaOrdenadoresAPI.Logging
{
    public class LoggerManagerAzure : ILoggerManager
    {

        private readonly string _storageConnectionString;
        private readonly string _fileName = "${ shortdate} _logfile.txt";
        private BlobContainerClient _containerClient;

        public LoggerManagerAzure()
        {
            var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _storageConnectionString = MyConfig.GetValue<string>("AppSettings:storageConnectionString");
            CreateBlobContainerAsync().GetAwaiter().GetResult();
        }
        public async Task CreateBlobContainerAsync()
        {
            // Create a client that can authenticate with a connection string
            BlobServiceClient blobServiceClient = new(_storageConnectionString);

            // COPY EXAMPLE CODE BELOW HERE
            //Create a unique name for the container
            string containerName = "logs_ObdulioAPI";

            // Create the container and return a container client object
             _containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
        }
        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new();
            StreamWriter writer = new(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public async Task WriteLogToContainerFileAsync(string message) 
        { 

            // Get a reference to the blob
            BlobClient blobClient = _containerClient.GetBlobClient(_fileName);
            Stream uploadFileStream = GenerateStreamFromString(message);
            Stream writtenStream = await blobClient.OpenWriteAsync(true);
            uploadFileStream.CopyTo(writtenStream);
            writtenStream.Close();
            // Open the file and upload its data
                await blobClient.UploadAsync(writtenStream);
                uploadFileStream.Close();
        }
        public void LogDebug(string message)
        {
            WriteLogToContainerFileAsync("[DEBUG] " + message + "\n").GetAwaiter().GetResult();
        }

        public void LogError(string message)
        {
            WriteLogToContainerFileAsync("[ERROR] " + message + "\n").GetAwaiter().GetResult();
        }

        public void LogInfo(string message)
        {
            WriteLogToContainerFileAsync("[INFO] " + message + "\n").GetAwaiter().GetResult();
        }

        public void LogWarn(string message)
        {
            WriteLogToContainerFileAsync("[WARN] " + message + "\n").GetAwaiter().GetResult();
        }
    }
}
