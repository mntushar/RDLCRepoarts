using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RDLCRepoarts.Controllers
{
    public class ReportController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;

        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            string mimtype = "";
            int extention = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("rp1", "Product RDLC report");
            LocalReport localReport = new LocalReport(path);
            var result = localReport.Execute(RenderType.Pdf, extention, parametrs, mimtype);

            return File(result.MainStream, "application/pdf");
        }
    }
}
