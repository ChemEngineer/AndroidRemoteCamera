using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndroidRemoteCamera.Servers
{
    [Serializable]
    public class TcpServerData : INotifyPropertyChanged, ISerializable
    {
        public TcpServerData()
        {
            Initialize();
        }

        private void Initialize() { }

        #region PropertyChanged Pattern

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion


        private string _ipAddress;
        public string ipAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                OnPropertyChanged("ipAddress");
            }
        }

        private int _port;
        public int port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged("port");
            }
        }

        private bool _isConnected;
        public bool isConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged("isConnected");
            }
        }



        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ipAddress", _ipAddress, typeof(string));
            info.AddValue("port", _port, typeof(int));
            info.AddValue("isConnected", _isConnected, typeof(bool));
        }
        public TcpServerData(SerializationInfo info, StreamingContext context)
        {
            _ipAddress = (string)info.GetValue("ipAddress", typeof(string));
            _port = (int)info.GetValue("port", typeof(int));
            _isConnected = (bool)info.GetValue("isConnected", typeof(bool));
        }
    }

    [Serializable]
    public class TcpServerDataList : List<TcpServerData>, ISerializable
    {
        public TcpServerDataList() { }

       

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("lst", this, this.GetType());
        }

        public TcpServerDataList(SerializationInfo info, StreamingContext context)
        {

        }

        #endregion
    }
}