using FlightsExample.Core.Services;
using SkiaSharp;
using SkiaSharp.QrCode;
using System.Reflection;

namespace FlightsExample.Services.Services
{
    public class QRCodeService : IQRCodeService
    {
        public string Create(string passengerCode)
        {
            using var generator = new SkiaSharp.QrCode.QRCodeGenerator();
            var qr = generator.CreateQrCode(passengerCode, ECCLevel.L);
            var info = new SKImageInfo(512, 512);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Render(qr, info.Width, info.Height, SKColors.White, SKColors.Black);
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            //TODO: testing qr - remove
            //var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location + @"\\test.png");
            //using var stream = File.OpenWrite(filePath);
            //data.SaveTo(stream);
            return Convert.ToBase64String(data.ToArray());
        }
    }
}