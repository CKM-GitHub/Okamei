using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
namespace OkameiProduction.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InputBukkenShousai_PurecattoJissekiExport pu = new InputBukkenShousai_PurecattoJissekiExport();
            //pu.Export(System.Web.Hosting.HostingEnvironment.MapPath("~/fonts/"));
        }
    }
}