using System;

class Program
{
    static void Main(string[] args)
    {
        //Define the content and file path for the QR code
        string content = "https://www.churchofjesuschrist.org/?lang=eng";
         string path = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "qrcode.png"        
            );

        QRData dados = new QRData { Content = content, FilePath = path };

        QRGeneratorBase gerador = new PNGQRGenerator();
        gerador.Generate(dados);

        Console.WriteLine("Program finished. Press any key to exit.");
        Console.ReadKey();
    }
}
