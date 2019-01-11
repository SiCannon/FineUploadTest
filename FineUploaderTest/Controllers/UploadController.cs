using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using FineUploader;

namespace FineUploaderTest.Controllers
{
    [RoutePrefix("api/upload")]
    public class UploadController : ApiController
    {
        public IHttpActionResult GetTest()
        {
            return Ok(new List<string> { "one", "two", "three", "four" });
        }

        [HttpPost]
        [Route("send")]
        public IHttpActionResult Send(string qquuid, string qqfilename, int qqtotalfilesize, byte[] qqfile)
        {
            return Ok("done!");
        }

        [HttpPost]
        public FineUploaderResult PostUploadFile(FineUpload upload)
        {
            // asp.net mvc will set extraParam1 and extraParam2 from the params object passed by Fine-Uploader

            var dir = @"c:\temp";
            var filePath = Path.Combine(dir, upload.Filename);
            try
            {
                upload.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }

            // the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = 12345 });
        }
    }
}
