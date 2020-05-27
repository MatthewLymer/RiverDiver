using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RiverDiver.Web.App.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
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
        private readonly ILogger<WaiverController> _logger;

        public WaiverController(IHostingEnvironment hostingEnvironment, ILogger<WaiverController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(WaiverModel waiver)
        {
            if (!ModelState.IsValid)
            {
                return View(waiver);
            }
            
            var stream = CreateWaiverStream(waiver);
            
            var apiKey = Environment.GetEnvironmentVariable("RD_SENDGRID_KEY");
            var waiverToEmail = Environment.GetEnvironmentVariable("RD_WAIVER_TO_EMAIL");
            var waiverFromEmail = Environment.GetEnvironmentVariable("RD_WAIVER_FROM_EMAIL");
            
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(waiverFromEmail);
            var subject = $"Waiver for passenger {waiver.FirstName} {waiver.LastName}";
            var to = new EmailAddress(waiverToEmail);
            var plainTextContent = $"Please see attached the waiver for passenger {waiver.FirstName} {waiver.LastName}";
            var htmlContent = "<p>" + HttpUtility.HtmlEncode(plainTextContent) + "</p>"
                + "<pre>" + HttpUtility.HtmlEncode(JsonConvert.SerializeObject(waiver)) + "</pre>";
            
            var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            using (var base64Stream = new CryptoStream(stream, new ToBase64Transform(), CryptoStreamMode.Read))
            using (var reader = new StreamReader(base64Stream))
            {
                var waiverFileName = $"{waiver.FirstName} {waiver.LastName} waiver.pdf";
                var base64Content = reader.ReadToEnd();
                message.AddAttachment(waiverFileName, base64Content, "application/pdf");
            }
            
            var response = await client.SendEmailAsync(message);

            if ((int) response.StatusCode >= 400)
            {
                var content = await response.Body.ReadAsStringAsync();
                
                _logger.LogError("Failed to send email. (statusCode={0}, content={1})", response.StatusCode, content);

                TempData["Errors"] = new []
                {
                    "There was a problem on our end sending your waiver, try downloading the waiver instead."
                };
                
                return View(waiver);
            }

            return Redirect("~/waiver/thankyou");
        }

        [HttpGet]
        public IActionResult ThankYou()
        {
            return View();
        }

        private Stream CreateWaiverStream(WaiverModel waiver)
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

                return memoryStream;
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

        private static PdfImage CreatePdfImage(string signature)
        {
            var base64 = GetBase64Image(signature);

            var binary = Convert.FromBase64String(base64);

            using (var stream = new MemoryStream(binary))
            {
                return PdfImage.FromStream(stream);
            }
        }

        private static string GetBase64Image(string signature)
        {
            const string expectedPrefix = "data:image/png;base64,";

            if (!signature.StartsWith(expectedPrefix))
            {
                throw new ArgumentException("Must be a base-64 png image", nameof(signature));
            }

            return signature.Substring(expectedPrefix.Length);
        }
    }
}