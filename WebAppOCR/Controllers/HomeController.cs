using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppOCR.Models;
using ChackLib;

namespace WebAppOCR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Sample Application for .NET Standard, .NET Core and Cognitive Services.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Home/Ocr
        public ActionResult Ocr()
        {
            return View();
        }

        // POST: Home/Ocr
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ocr(Image imageData)
        {
            try
            {
                Uri image = new Uri(imageData.ImageUrl);

                // Setup for OcrLib
                // 1) Go to https://www.microsoft.com/cognitive-services/en-us/computer-vision-api 
                //    Sign up for computer vision api
                // 2) Add environment variable "Vision_API_Subscription_Key" and set Computer vision key as value
                //    e.g. Vision_API_Subscription_Key=123456789abcdefghijklmnopqrstuvw

                Task<string> task = Task.Run(() => OcrLib.DoOcr(image));
                task.Wait();

                imageData.Result = task.Result;

                return View("OcrView", imageData);
            }
            catch
            {
                return View();
            }
        }
    }
}
