using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidRemoteCamera.Extensions
{
    public static partial class Extensions
    {
        public static Task<TcpClient> AcceptTcpClientAsync(this TcpListener listener, CancellationToken token)
        {
            try
            {
                return listener.AcceptTcpClientAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}