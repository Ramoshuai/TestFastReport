using FastReport;
using FastReport.Export.Html;
using FastReport.Export.Image;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestFastReport.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("/TestReport")]
        public string TestPrint(int quality=100)
        {

            using Report report = new Report();
            
            report.Load("2.frx");

            #region byte
            using FileStream fs = new FileStream("1.png", FileMode.Open, FileAccess.Read);
            byte[] buffur = new byte[fs.Length];
            fs.Read(buffur, 0, (int)fs.Length);
            fs.Close();
            var data = new List<object>() { new { Image = buffur } };
            #endregion

            #region Image
            //Image picture = Image.FromFile("1.png");
            //var data = new List<object>() { new { Image = picture } }; 
            #endregion

            using ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            image.ResolutionX = image.ResolutionY = image.Resolution = 300;
            image.JpegQuality = quality;

            report.RegisterData(data, "Test");
            report.Prepare();

            report.Export(image,"export_"+DateTime.Now.ToString("HHmmssfff")+".jpeg" );
            Count += 1;

            return Count.ToString()+"_"+ quality;
        }

        private static int Count = 0;
    }
}
