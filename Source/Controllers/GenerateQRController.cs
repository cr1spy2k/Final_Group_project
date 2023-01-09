using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
using AFakeProductIdentificationSystem.Models;

namespace AFakeProductIdentificationSystem.Controllers
{
    public class GenerateQRController : Controller
    {
        public ActionResult Index()
        {
            if (!HomeController.isLoaded)
            {
                HomeController.Load();
                HomeController.isLoaded = true;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string qrText)
        {
            if (!HomeController.isLoaded)
            {
                HomeController.Load();
                HomeController.isLoaded = true;
            }

            string link = "https://localhost:44308/CheckQR/Index/";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(link + qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return View(BitmapToBytes(qrCodeImage));
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

    }
}