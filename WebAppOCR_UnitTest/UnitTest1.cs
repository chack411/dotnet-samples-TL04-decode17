using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppOCR.Controllers;
using WebAppOCR.Models;

namespace UnitTest_WebAppOCR
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HomeTest()
        {
            var home = new HomeController();
            Assert.IsNotNull(home);
        }

        [TestMethod]
        public void IndexTest()
        {
            var home = new HomeController();
            Assert.IsNotNull(home);
            var result = home.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AboutTest()
        {
            var home = new HomeController();

            Assert.IsNotNull(home);
            var result = home.About() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OcrViewTest()
        {
            var home = new HomeController();

            Assert.IsNotNull(home);
            var result = home.Ocr() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void OcrTest()
        {
            var home = new HomeController();
            Assert.IsNotNull(home);

            var imageData = new Image();
            imageData.Id = 0;
            imageData.ImageUrl = "https://chack.blob.core.windows.net/images/Azure_Book.jpg";

            var result = home.Ocr(imageData) as ViewResult;
            Assert.IsNotNull(result);

            Image model = (Image)result.ViewData.Model;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Result);
            Assert.AreNotEqual("", model.Result);
        }
    }
}
