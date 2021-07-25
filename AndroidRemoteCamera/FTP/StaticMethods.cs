
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Communicator.FTP
{
    public class StaticMethods
    {

        public static string GetFilePath()
        {
          
            return  Path.Combine( "wwwroot", "bmp.jpg");
        }

        //public static string GetFilePath()
        //{
        //    var vv = AppContext.BaseDirectory.Split("\\", StringSplitOptions.RemoveEmptyEntries);
        //    string p = "";
        //    foreach (var v in vv)
        //    {
        //        if (v == "Communicator")
        //        {
        //            p = Path.Combine(p, v);
        //            var res = Directory.EnumerateFiles(p).Where(x => x.Contains(".pfx"));
        //            if (res.Count() > 0)
        //                break;
        //        }
        //        else
        //        {
        //            p = Path.Combine(p, v);
        //        }
        //    }

        //    p = Path.Combine(p, "wwwroot", "bmp.jpg");
        //    Console.WriteLine(p);
        //    return p;
        //}

        public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }
            return result;
        }

        public static void Upload(Bitmap bitmap)
        {

            using (WebClient client = new WebClient())
            using (var ms = new MemoryStream())
            {

                try
                {
                    Console.WriteLine("Resizing Bitmap");
                    bitmap = ResizeBitmap(bitmap, 320, 240);
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    client.Credentials = new NetworkCredential("pi", "raspberry");
                    Console.WriteLine("Beginning upload to FTP");
                    client.UploadData("ftp://EntropyHome.Asuscomm.com:21/files/bmp2.jpg", ms.ToArray());
                    Console.WriteLine("Upload Complete");
                    //client.UploadDataTaskAsync("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg", ms.ToArray());
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void UploadBitmap(byte[] b)
        {

            using (WebClient client = new WebClient())
            using (var ms = new MemoryStream())
            {

                try
                {
                    Bitmap bitmap;
                    using (var ms1 = new MemoryStream(b))
                    {
                        bitmap = new Bitmap(ms1);
                    }
                    Console.WriteLine("Resizing Bitmap");
                    bitmap = ResizeBitmap(bitmap, 320, 240);
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    client.Credentials = new NetworkCredential("pi", "raspberry");
                    Console.WriteLine("Beginning upload to FTP");
                    client.UploadData("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg", ms.ToArray());
                    Console.WriteLine("Upload Complete");
                    //client.UploadDataTaskAsync("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg", ms.ToArray());
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
        }


        //public static void Upload(string filename)
        //{
        //    // Get the object used to communicate with the server.
        //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg");
        //    request.Method = WebRequestMethods.Ftp.UploadFile;

        //    // This example assumes the FTP site uses anonymous logon.
        //    request.Credentials = new NetworkCredential("pi", "raspberry");

        //    // Copy the contents of the file to the request stream.
        //    byte[] fileContents;
        //    using (StreamReader sourceStream = new StreamReader(filename))
        //    {
        //        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
        //    }

        //    request.ContentLength = fileContents.Length;

        //    using (Stream requestStream = request.GetRequestStream())
        //    {
        //        requestStream.Write(fileContents, 0, fileContents.Length);
        //        requestStream.Flush();
        //    }
        //}

        public static void Download()
        {
            try
            {
                Console.WriteLine("Creating Download client");
                WebClient ftpClient = new WebClient();
                ftpClient.Credentials = new NetworkCredential("pi", "raspberry");
                Console.WriteLine("Beginning download from FTP");
                byte[] imageByte = ftpClient.DownloadData("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg");
                Console.WriteLine("Download Complete");
                string p = GetFilePath();
                Console.WriteLine(p);
                File.WriteAllBytes(p, imageByte);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public static long DownloadFileSize()
        {
            long FileSize = 0;
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://EntropyHome.Asuscomm.com:21/files/bmp.jpg");
                request.Credentials = new NetworkCredential("pi", "raspberry");
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.UseBinary = true;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                FileSize = response.ContentLength;

                response.Close();
                ftpStream.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return FileSize;
        }
    }
}
