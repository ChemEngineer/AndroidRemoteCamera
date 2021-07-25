using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AndroidRemoteCamera.Servers
{
    public  class TcpImageServer : AsyncTcpServer<byte[]>
    {

        public TcpImageServer(string ip, int port, Action action = null) : base(ip, port, action)
        {
            
        }


       
    }
}