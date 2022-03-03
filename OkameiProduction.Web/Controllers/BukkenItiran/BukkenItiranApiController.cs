using System;
using System.Web.Http;
using Models;
using OkameiProduction.BL;

namespace OkameiProduction.Web.Controllers
{
    public class BukkenItiranApiController : BaseApiController
    {
        [HttpPost]
        public string ExistsDisplayResult([FromBody] BukkenItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new BukkenItiranBL();
            if (bl.ExistsDisplayResult(model))
            {
                return GetSuccessResult();
            }
            else
            {
                return GetErrorResult("S013");
            }
           
        }
        public string CSVFormExport([FromBody] BukkenItiranModel model)
        {
            if (model == null) return GetBadRequestResult();

            var bl = new BukkenItiranBL();
            var dt = bl.GetCSVResult(model);

            try
            {


                BukkenItiran exportExcel = new BukkenItiran();
                string SavePath = System.Web.Hosting.HostingEnvironment.MapPath("~/output/project");
                string InitializerPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Initializer/CSVForm.csv");

                exportExcel.CSVFormExport(SavePath + "\\" + model.FileName, InitializerPath, dt);
                return GetSuccessResult();


            }
            catch (Exception ex)
            {

                return GetBadRequestResult();
            }

        }


    }
}  

