using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using MVC_ComponentesCodeFirst.Logging;

namespace MVC_ComponentesCodeFirst.Logging
{
    public class LoggerManagerAzure : ILoggerManager
    {

        private readonly string _storageConnectionString;
        private readonly string _fileName = DateTime.Today.ToString("yyyy-MM-dd") + "_logfile.txt";
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
            var blobServiceClient = new BlobServiceClient(_storageConnectionString);

            // COPY EXAMPLE CODE BELOW HERE
            //Create a unique name for the container
            string containerName = "logsobdulio";

            // Create the container and return a container client object
            try { 
                _containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName); 
            }
            catch(Exception ex)
            {
                _containerClient =  blobServiceClient.GetBlobContainerClient(containerName);
            }
        }
        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new();
            var writer = new StreamWriter(stream);
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
            //using (Stream stream = blobClient.OpenWrite(true))
            //{
            Stream writtenStream = await blobClient.OpenWriteAsync(true);
            uploadFileStream.CopyTo(writtenStream);
            writtenStream.Close();
            await blobClient.UploadAsync(writtenStream, true);
                uploadFileStream.Close();
                writtenStream.Close();
            //}
            await _containerClient.DeleteAsync();
        }
        public async  void LogDebug(string message)
        {
            await WriteLogToContainerFileAsync(DateTime.UtcNow + " DEBUG " + message + "\n");
        }

        public async void LogError(string message)
        {
            await WriteLogToContainerFileAsync(DateTime.UtcNow + " ERROR " + message + "\n");
        }

        public async void LogInfo(string message)
        {
            await WriteLogToContainerFileAsync(DateTime.UtcNow + " INFO " + message + "\n");
        }

        public async void LogWarn(string message)
        {
            await WriteLogToContainerFileAsync(DateTime.UtcNow + " WARN " + message + "\n");
        }
    }
}
