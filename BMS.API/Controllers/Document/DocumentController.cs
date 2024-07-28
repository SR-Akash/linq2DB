using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Polly;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BMS.API.Controllers.Document
{
    [Route("bms/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse("BlobEndpoint=https://manageirum.blob.core.windows.net?sp=rw&st=2024-06-01T05:12:22Z&se=2025-06-01T13:12:22Z&spr=https&sv=2022-11-02&sr=c&sig=H5l5adMcXB6DKNnC163aVUx0ZbZOfJTI%2Fpk7xFM20Bg%3D");

        public class FileValidationError
        {
            public string FileName { get; set; }
            public string Message { get; set; }
        }
        public class FileResponse
        {
            public string id { get; set; }
            public string fileName { get; set; }
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string id)
        {
            CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("mgm");

            try
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(id);

                await blockBlob.FetchAttributesAsync();
                string contentType = blockBlob.Properties.ContentType;

                MemoryStream ms = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(ms);

                if (ms.ToArray().Count() > 0 && contentType != null)
                {
                    return File(ms.ToArray(), contentType);
                }
                else
                {
                    blockBlob = container.GetBlockBlobReference(id);
                    await blockBlob.FetchAttributesAsync();

                    contentType = blockBlob.Properties.ContentType;
                    await blockBlob.DownloadToStreamAsync(ms);

                    return File(ms.ToArray(), contentType);

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        [Route("UploadFileNew")]
        public async Task<IActionResult> UploadFileNew(IFormFile file)
        {
            try
            {
                string filename = string.Empty;
                long maxFileSize = (1 * 1024 * 1024); // 1 MB

                if (file.Length > maxFileSize)
                {
                    long fileSize = file.Length / (1024 * 1024);
                    FileValidationError error = new FileValidationError
                    {
                        FileName = file.Name,
                        Message = "Maximum File Size Allowed (1MB) Your File Size (" + fileSize + "MB)"
                    };
                    return NotFound(error);
                }

                string extension = System.IO.Path.GetExtension(file.FileName);

                if (extension != ".jpeg" && extension != ".jpg" && extension != ".png" && extension != ".xlsx" && extension != ".pdf")
                {
                    throw new Exception("Invalid Image Format");
                }

                filename = DateTime.Now.Ticks.ToString() + "_" + (file.FileName == "blob" ? "blob.jpg" : file.FileName);

                var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
                var cloudBlobContainer = cloudBlobClient.GetContainerReference("mgm");

                var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filename);
                cloudBlockBlob.Properties.ContentType = file.ContentType;

                await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(filename);
                await blockBlob.FetchAttributesAsync();


                FileResponse response = new FileResponse
                {
                    id = blockBlob.Name,
                    fileName = file.FileName
                };
                return Ok(response);


            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
