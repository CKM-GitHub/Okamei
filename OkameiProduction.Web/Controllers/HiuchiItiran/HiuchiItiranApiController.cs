using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Models;
using OkameiProduction.BL;
namespace OkameiProduction.Web.Controllers
{
    public class HiuchiItiranApiController : BaseApiController
    { 
       
        [HttpPost]
        public string ExistsDisplayResult([FromBody] HiuchiItiranModel model)
        {
            if (model == null) return GetBadRequestResult();
            var bl = new HiuchiItiranBL();
            if (bl.GetDisplayResult(model).Rows.Count > 0)
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult("S013");
            }
        }
        [HttpPost]
        public string PDF( )
        {
            HttpResponseMessage result = null;
            result = Request.CreateResponse(HttpStatusCode.OK);
            FileStream stream = File.OpenRead( (@"D:\Projects\Okame\Okamei\OkameiProduction.Web\output\project\") + "123.txt");
            byte[] fileBytes = new byte[stream.Length];
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            result.Content = new ByteArrayContent(fileBytes);
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = "FileName.txt";
            
            result.Content.Dispose();
           
            return GetSuccessResult();
        }
        [HttpPost]
        public HttpResponseMessage GetFile( )
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            byte[] bytes = File.ReadAllBytes((@"D:\Projects\Okame\Okamei\OkameiProduction.Web\output\project\") + "123.txt");

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "123.txt";

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping("123.txt"));
         
            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new StreamContent(new FileStream((@"D:\Projects\Okame\Okamei\OkameiProduction.Web\output\project\") + "123.txt", FileMode.Open, FileAccess.Read));
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "ptk.txt";
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            return response;
        }

        [HttpPost]
        public HttpResponseMessage PdfExport(HiuchiItiranModel model)
        {
            HiuchiItiran.HiuchiItiranController PdfCaller = new HiuchiItiran.HiuchiItiranController();
            PdfCaller.ExportHiuchiPdf(model);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/output/project/"+ model.FileName);//
            byte[] bytes = File.ReadAllBytes(filePath); 
            response.Content = new ByteArrayContent(bytes); 
            response.Content.Headers.ContentLength = bytes.LongLength; 
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = model.FileName; 
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(model.FileName)); 
            return response;
        }


    }
}
