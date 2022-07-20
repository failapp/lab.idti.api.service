using System;
using System.Collections.Generic;
using System.Text;
using Indy.Sockets.Units;

namespace idtiApiService.Sdk
{
    class clsIndySock
    {
        public enum enumTCPPort
        {
            p1004 = 1004
        }

        public enum enumConnMode
        {
            Client,
            Server
        }

        public enum enumReadTimeoutScope
        {
            MinValue = 100,
            MaxValue = 20000,
            CountValue = 100,
            DefaultValue = 7000
        }

        public enum enumConnTimeoutScope
        {
            MinValue = 100,
            MaxValue = 20000,
            CountValue = 100,
            //DefaultValue = 1000
            DefaultValue = 10000
        }

        private Indy.Sockets.TCPClient tcpClient = null;
        private string tcpName = string.Empty;
        private int tcpTag = 0;
        private string tcpHost = string.Empty;
        private int tcpPort = (int)enumTCPPort.p1004;
        private int tcpReadTimeout = (int)enumReadTimeoutScope.DefaultValue;
        private int tcpConnectTimeout = (int)enumConnTimeoutScope.DefaultValue;
        private Indy.Sockets.IndyIPVersion tcpIPVersion = Indy.Sockets.IndyIPVersion.Id_IPv4;

        public string TcpHost
        {
            set { this.tcpHost = value; }
        }

        public int TcpPort
        {
            set { this.tcpPort = value; }
        }

        public int TcpReadTimeout
        {
            set { this.tcpReadTimeout = value; }
        }

        private bool IsCreated
        {
            get 
            {
                if (this.tcpClient != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsConnected
        {
            get 
            {
                if (this.tcpClient != null && this.tcpClient.Connected())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public clsIndySock()
        { 
        }

        public int[] GetReadTimeoutList()
        {
            int[] argi = new int[((int)enumReadTimeoutScope.MaxValue - (int)enumReadTimeoutScope.MinValue) / (int)enumReadTimeoutScope.CountValue + 1];

            for (int i = (int)enumReadTimeoutScope.MinValue, j = 0; i <= (int)enumReadTimeoutScope.MaxValue; i += (int)enumReadTimeoutScope.CountValue, j++)
            {
                argi[j] = i;
            }

            return argi;
        }

        public int[] GetTCPPortList()
        {
            int[] argi = new int[Enum.GetValues(typeof(enumTCPPort)).Length];
            int i = 0;

            foreach (int value in Enum.GetValues(typeof(enumTCPPort)))
            {
                argi[i] = value;
                i++;
            }

            return argi;
        }

        public bool CreateTcpClient()
        {
            bool retVal = false;

            try
            {
                this.tcpClient = new Indy.Sockets.TCPClient();
                this.tcpClient.Name = this.tcpHost;
                this.tcpClient.Tag = this.tcpTag;
                this.tcpClient.Host = this.tcpHost;
                this.tcpClient.Port = this.tcpPort;
                this.tcpClient.ReadTimeout = this.tcpReadTimeout;
                this.tcpClient.ConnectTimeout = this.tcpConnectTimeout;
                this.tcpClient.IPVersion = this.tcpIPVersion;

                retVal = true;
            }
            catch (Exception ex)
            {
                clsCommError.getInstance().CommErrorMessage = ex.Message;
                return retVal;
            }

            return retVal;
        }

        public void DestroyTcpClient()
        {
            if (this.IsCreated)
            {
                try
                {
                    if (this.IsConnected) { this.tcpClient.Disconnect(); }

                    this.tcpClient.Destroy();
                    this.tcpClient.Dispose();
                    this.tcpClient = null;
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                }
            }
        }

        public bool ConnectTcpClient()
        {
            bool retVal = false;

            if (this.IsCreated && !IsConnected)
            {
                try
                {
                    this.tcpClient.Connect(this.tcpHost, this.tcpPort);

                    retVal = true;
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                    return retVal;
                }
            }

            return retVal;
        }

        public void DisconnectTcpClient()
        {
            if (this.IsConnected)
            {
                try
                {
                    this.tcpClient.Disconnect();
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                }
            }
        }

        public bool WriteBuffer(byte[] argb)
        {
            bool retVal = false;

            if (this.IsConnected)
            {
                try
                {
                    this.tcpClient.IOHandler.Write(argb);

                    retVal = true;
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                    return false;
                } 
            }           

            return retVal;
        }

        public byte[] ReadBuffer()
        {
            // 나중에 패킷 헤더 정의 후 꼭 수정할 것.
            int lengthToPacketLenBytes = 3;
            int offsetPacketLenFirst = 1;
            int offsetPacketLenSecond = 2;            

            byte[] argb = null;

            if (this.IsConnected)
            {
                try
                {
                    // 패킷 헤더에서 패킷길이의 값을 가지고 있는 바이트를 먼저 가져 옴. (STX[0] + LENGTH[1..2])
                    this.tcpClient.IOHandler.ReadBytes(ref argb, lengthToPacketLenBytes, true);

                    // LENGTH 계산
                    int quotient = Convert.ToInt32(argb[offsetPacketLenFirst]) * (Convert.ToInt32(byte.MaxValue) + 1);
                    int remainder = Convert.ToInt32(argb[offsetPacketLenSecond]);
                    int length = quotient + remainder;

                    // LENGTH 값만큼 먼저 받은 STX[0] + LENGTH[1..2] 를 제외한 나머지 바이트를 가져 옴. 
                    this.tcpClient.IOHandler.ReadBytes(ref argb, length - lengthToPacketLenBytes, true);
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                    return argb;
                }
            }

            return argb;
        }
    }
}
