using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
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
            var dt = new DataTable();
            dt = GetStudentList();

            string mimtype = "";
            int PageNumer = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";
            Dictionary<string, string> parametrs = new Dictionary<string, string>();
            parametrs.Add("rp1", "Student RDLC report");

            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("StudentDs", dt);

            var result = localReport.Execute(RenderType.Pdf, PageNumer, parametrs, mimtype);

            return File(result.MainStream, "application/pdf");
        }

        public DataTable GetStudentList()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Department");

            DataRow row;
            for(int i=101; i<= 140; i++)
            {
                row = dt.NewRow();
                row["Id"] = i;
                row["Name"] = "Tushar" + i;
                row["Department"] = "Cse";

                dt.Rows.Add(row);

            }

            return dt;
        }
    }
}
