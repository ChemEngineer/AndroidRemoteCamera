using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidRemoteCamera.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AndroidRemoteCamera
{
   
    public class db
    {
        //Device to recieve image
        public static string remoteIp = "192.168.1.73";
        public static int remotePort = 8011;

        //This server to send image
        public static string thisIp = "192.168.1.99";
        public static int serverPort = 8010;

        public static  int imageScale = 5;

        public static string path { get; set; }


        private static byte[] _lastImage;
        public static byte[] lastImage
        {
            get { return _lastImage; }
            set { 
                _lastImage = value;
                    db.SendImageTcp();

            }
        }


        private static TcpImageServer _server;
        public static TcpImageServer server
        {
            get { return _server; }
            set
            {
                _server = value;         
            }
        }

        public db()
        {
          
        }

        public static void SendImageTcp()
        {
            TcpClient c = new TcpClient();
            c.Connect(remoteIp, remotePort);
            var res = Services.ItemResizerService.ResizeImageAndroid(lastImage,   (1920 / imageScale), (1080 / imageScale));
            c.Client.Send(res);
            c.Close();
        }

    }
}