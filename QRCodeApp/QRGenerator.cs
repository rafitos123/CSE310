using System;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

//struct to hold QR code data
public struct QRData
{
    public string Content;
    public string FilePath;
}

// Abstract class
public abstract class QRGeneratorBase
{
    public abstract void Generate(QRData datas);
}

//Concrete class that generates a PNG QR code
public class PNGQRGenerator : QRGeneratorBase
{
    public override void Generate(QRData data)
    {
        if (string.IsNullOrWhiteSpace(data.Content))
        {
            Console.WriteLine("Content is empty. Cannot generate QR code.");
            return;
        }

        //Color combinations for QR code
        (Color backgroundColor, Color PixelColor)[] styles = new (Color, Color)[]
        {
            (Color.White, Color.Black),
            (Color.LightYellow, Color.DarkBlue),
            (Color.LightPink, Color.DarkRed),
            (Color.LightGray, Color.DarkGreen)
        };

        //Randomly select a color style for the QR code
        Random rnd = new Random();
        (Color backgroundColor, Color PixelColor) chosen = styles[0];

        //Loop through each style and generate a QR code image
        foreach (var style in styles)
        {   
            //chose one color style randomly
            if (rnd.Next(0, styles.Length) == 0) 
            
            {
                chosen = style;
            }
        }

        //Generate a unique file name based on the chosen pixel color
        string nomeArquivo = $"qrcode_{chosen.PixelColor.Name}.png";
        string path = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            nomeArquivo
        );

        GenerateImage(data.Content, path, chosen.backgroundColor, chosen.PixelColor);
    }

    //Method to generate QR code image with specified colors
    private void GenerateImage(string Content, string path, Color backgroundColor, Color PixelColor)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(Content, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);

        using (Bitmap qrCodeImage = qrCode.GetGraphic(20, PixelColor, backgroundColor, true))
        {
            qrCodeImage.Save(path, ImageFormat.Png);
        }

        Console.WriteLine($"QR Code generated in: {path}");
    }
}

