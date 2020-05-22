using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using RiverDiver.Web.App.Models;
using Spire.Pdf;
using Spire.Pdf.HtmlConverter.Qt;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class WaiverController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WaiverModel waiver)
        {
            if (ModelState.IsValid)
            {
                var doc = new PdfDocument();
                
                var memoryStream = new MemoryStream();
                
                doc.SaveToStream(memoryStream);
                
                memoryStream.Position = 0;

                return File(memoryStream, "application/pdf");
            }
            
            return View(waiver);
        }
    }
}