using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RiverDiver.Web.App.Models;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;
using TimeZoneConverter;

namespace RiverDiver.Web.App.Controllers
{
    public sealed class WaiverController : Controller
    {
        private const string WaiversDirectory = "forms";
        private const string WaiverFileName = "river-diver-waiver-form.pdf";
        private const string DefaultTimeZone = "America/Toronto";
        
        private readonly IHostingEnvironment _hostingEnvironment;

        public WaiverController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WaiverModel waiver)
        {
            return ModelState.IsValid 
                ? CreateWaiverActionResult(waiver) 
                : View(waiver);
        }

        private IActionResult CreateWaiverActionResult(WaiverModel waiver)
        {
            var fullName = $"{waiver.FirstName} {waiver.LastName}";
            var hasAllergies = !string.IsNullOrWhiteSpace(waiver.Allergies);
            var hasMedications = !string.IsNullOrWhiteSpace(waiver.Medications);

            var timeZoneInfo = TZConvert.GetTimeZoneInfo(DefaultTimeZone);
            var now = DateTimeOffset.UtcNow.ToOffset(timeZoneInfo.BaseUtcOffset);

            var addressAndCity = $"{waiver.StreetAddress}, {waiver.City}";
            
            const int signatureWidth = 155;
            
            using (var doc = new PdfDocument())
            {
                doc.LoadFromFile(Path.Join(_hostingEnvironment.WebRootPath, WaiversDirectory, WaiverFileName));

                var form = (PdfFormWidget) doc.Form;
                var page = doc.Pages[0];
                
                ((PdfTextBoxFieldWidget) form.FieldsWidget["FullName1"]).Text = fullName;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["FullName2"]).Text = fullName;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["FullName3"]).Text = fullName;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Address"]).Text = addressAndCity;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Allergies"]).Text = waiver.Allergies ?? string.Empty;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Country"]).Text = waiver.Country;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Email"]).Text = waiver.Email;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Medications"]).Text = waiver.Medications ?? string.Empty;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Phone"]).Text = waiver.Phone;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Province"]).Text = waiver.Province;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["CertificationNumber"]).Text = waiver.CertificationNumber;
                ((PdfTextBoxFieldWidget) form.FieldsWidget["PostalCode"]).Text = waiver.PostalCode;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Angina"]).Checked = waiver.Angina;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Asthma"]).Checked = waiver.Asthma;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Bronchitis"]).Checked = waiver.Bronchitis;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Diabetes"]).Checked = waiver.Diabetes;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Emphysema"]).Checked = waiver.Emphysema;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Seizures"]).Checked = waiver.Seizures;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["Stroke"]).Checked = waiver.Stroke;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["HeartAttack"]).Checked = waiver.HeartAttack;
                ((PdfCheckBoxWidgetFieldWidget) form.FieldsWidget["HighBloodPressure"]).Checked = waiver.HighBloodPressure;
                ((PdfRadioButtonListFieldWidget) form.FieldsWidget["HasAllergies"]).SelectedValue = hasAllergies ? "Yes" : "No";
                ((PdfRadioButtonListFieldWidget) form.FieldsWidget["HasMedications"]).SelectedValue = hasMedications ? "Yes" : "No";
                ((PdfTextBoxFieldWidget) form.FieldsWidget["Date"]).Text = string.Format("{0:MMMM d}{1}, {0:yyyy}", now, GetOrdinal(now.Day));
                
                foreach (var field in form.FieldsWidget.OfType<PdfTextBoxFieldWidget>())
                {
                    field.Flatten = true;
                }

                foreach (var field in form.FieldsWidget.OfType<PdfCheckBoxWidgetFieldWidget>())
                {
                    field.Flatten = true;
                }

                foreach (var field in form.FieldsWidget.OfType<PdfRadioButtonListFieldWidget>())
                {
                    field.ReadOnly = true;
                }

                var signature = CreatePdfImage(waiver.Signature);

                var ratio = signatureWidth / (float) signature.Width;
                
                page.Canvas.DrawImage(signature, 68, 667, signatureWidth, ratio * signature.Height);
                
                var memoryStream = new MemoryStream();

                doc.SaveToStream(memoryStream);

                memoryStream.Position = 0;

                return File(memoryStream, "application/pdf", WaiverFileName);
            }
        }

        private static string GetOrdinal(int value)
        {
            if (value >= 4 && value <= 20)
            {
                return "th";
            }

            switch (value % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }

        private static PdfImage CreatePdfImage(string base64Image)
        {
            const string expectedPrefix = "data:image/png;base64,";

            if (!base64Image.StartsWith(expectedPrefix))
            {
                throw new ArgumentException("Must be a base-64 png image", nameof(base64Image));
            }

            var binary = Convert.FromBase64String(base64Image.Substring(expectedPrefix.Length));

            using (var stream = new MemoryStream(binary))
            {
                return PdfImage.FromStream(stream);
            }
        }
    }
}