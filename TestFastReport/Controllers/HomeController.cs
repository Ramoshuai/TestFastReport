using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Image;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestFastReport.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("/TestReport")]
        public string TestPrint(string type)
        {

            using Report report = new Report();
            report.Load("3.frx");

            using FileStream fs = new FileStream("1.png", FileMode.Open, FileAccess.Read);
            byte[] buffur = new byte[fs.Length];
            fs.Read(buffur, 0, (int)fs.Length);
            fs.Close();

            using ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            image.Resolution = 300;

            var data = new List<object>() { new { Image = buffur } };
            report.RegisterData(data, "Test");
            report.Prepare();
            using MemoryStream reportImageStream = new MemoryStream();
            report.Export(image, reportImageStream);

            return Convert.ToBase64String(reportImageStream.ToArray());
        }
    }
}
