//see https://docs.microsoft.com/en-us/azure/storage/files/storage-dotnet-how-to-use-files
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.Azure.Storage; // Namespace for Storage Client Library
using Microsoft.Azure.Storage.File; // Namespace for Azure Files
using System;
using System.Diagnostics;
using System.IO;
using System.Web.Mvc;


namespace Azure.Filestorage.MVCApp.Demo.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method to process all the files selected
        /// </summary>
        [HttpPost]
        public ActionResult UploadFile()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                Stream inputstream = Request.Files[i].InputStream;
                if (inputstream.Length > 0)
                    {
                        string filename = Request.Files[i].FileName;
                        CopyToFileShare(inputstream, filename);
                    }
            }
            return View("Index");
        }

        /// <summary>
        /// Method to Copy file to Azure File share
        /// </summary>
        /// <param name="finputstream">source file inputstream</param>
        /// <param name="filename">source filename</param>
        private void CopyToFileShare(Stream finputstream, string filename)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
                
                // Create a CloudFileClient object for credentialed access to Azure Files.
                CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
                
                // Get a reference to the file share .
                CloudFileShare share = fileClient.GetShareReference(CloudConfigurationManager.GetSetting("ShareDirectoryName"));
                
                // Ensure that the share exists.
                if (share.Exists())
                {
                    // Get a reference to the root directory for the share.
                    CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                    // Get a reference to the destination file.
                    CloudFile destFile = rootDir.GetFileReference(filename);
                    // Start the copy operation.
                    destFile.UploadFromStream(finputstream);
                    finputstream.Dispose();
                }
            }
            catch (Exception ex)
            {
                Write(string.Format("message: {0} stacktrace: {1}",ex.Message,ex.StackTrace));
            }
        }

        /// <summary>
        /// Method to write to Eventlog
        /// </summary>
        /// <param name="message">The event message</param>
        private void Write(string message)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry(message, EventLogEntryType.Error, 101, 1);
            }
        }
    }
}