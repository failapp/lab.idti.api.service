using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

//using System.IO.

namespace idtiApiService.Sdk
{
    class clsSerialPort
    {       
        public enum enumReadTimeoutScope
        {
            MinValue = 100,
            MaxValue = 20000,
           // MaxValue = 30000,
            CountValue = 100,
            //DefaultValue = 3000
            DefaultValue = 5000
        }

        public enum enumWriteTimeoutScope
        {
            MinValue = 100,
            MaxValue = 10000,
            CountValue = 100,
            DefaultValue = 1000
        }

        public enum enumBaudrate
        {
            //br2400 = 2400,
            br4800 = 4800,
            br9600 = 9600,
            br19200 = 19200,
            br38400 = 38400,
            br57600 = 57600,
            br115200 = 115200,
            br230400 = 230400,
            br460800 = 460800,
        }

        private SerialPort comPort = null;
        private string portName = "COM1";
        private int baudRate = 19200;
        private Parity parity = Parity.None;
        private int dataBits = 8;
        private StopBits stopBits = StopBits.One;
        private Handshake handShake = Handshake.None;
        private int readTimeout = (int)enumReadTimeoutScope.DefaultValue;
        private int writeTimeout = (int)enumWriteTimeoutScope.DefaultValue;

        private byte[] readBytes = null;
        private bool isReceiveCompleted = true;

        public string PortName
        {
            set { this.portName = value; }
        }

        public int BaudRate
        {
            set { this.baudRate = value; }
        }

        public int ReadTimeout
        {
            set { this.readTimeout = value; }
        }

        private bool IsCreated
        {
            get
            {
                if (this.comPort != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsOpen
        {
            get
            {
                if (this.comPort != null && this.comPort.IsOpen)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        

        public clsSerialPort()
        { 
        }

        public string[] GetSystemPortsName()
        {
            return SerialPort.GetPortNames();
        }

        public int[] GetBaudrateList()
        {
            int[] argi = new int[Enum.GetValues(typeof(enumBaudrate)).Length];
            int i = 0;

            foreach (int value in Enum.GetValues(typeof(enumBaudrate)))
            {
                argi[i] = value;
                i++;
            }

            return argi;
        }

        private void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 나중에 패킷 헤더 정의 후 꼭 수정할 것.
            int lengthToPacketLenBytes = 3;
            int offsetPacketLenFirst = 1;
            int offsetPacketLenSecond = 2;

            if (this.IsOpen)
            {
                this.isReceiveCompleted = false;

                int lengthReceived = this.comPort.BytesToRead;
                byte[] argb = new byte[lengthReceived];

                this.comPort.Read(argb, 0, lengthReceived);

                if (this.readBytes != null)
                {
                    // 계속 받기
                    byte[] oldBytes = this.readBytes;
                    byte[] newBytes = new byte[this.readBytes.Length + argb.Length];

                    Array.Copy(oldBytes, 0, newBytes, 0, oldBytes.Length);
                    Array.Copy(argb, 0, newBytes, oldBytes.Length, argb.Length);

                    this.readBytes = newBytes;
                }
                else
                {
                    // 최초 받기
                    this.readBytes = argb;
                }

                // 패킷 헤더에서 패킷길이의 값을 가지고 있는 바이트가 받아졌는지 확인 (STX[0] + LENGTH[1..2])
                if (this.readBytes != null && this.readBytes.Length > lengthToPacketLenBytes)
                {
                    // LENGTH 계산
                    int quotient = Convert.ToInt32(readBytes[offsetPacketLenFirst]) * (Convert.ToInt32(byte.MaxValue) + 1);
                    int remainder = Convert.ToInt32(readBytes[offsetPacketLenSecond]);
                    int length = quotient + remainder;

                    // LENGTH 값만큼 다 받았는지 확인해 처리
                    if (this.readBytes.Length.Equals(length)) { this.isReceiveCompleted = true; }
                }
            }
        }

        public bool CreateComPort()
        {
            bool retVal = false;

            try
            {
                this.comPort = new SerialPort();
                this.comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

                this.comPort.PortName = this.portName;
                this.comPort.BaudRate = this.baudRate;
                this.comPort.Parity = this.parity;
                this.comPort.DataBits = this.dataBits;
                this.comPort.StopBits = this.stopBits;
                this.comPort.Handshake = this.handShake;
                this.comPort.ReadTimeout = this.readTimeout;
                this.comPort.WriteTimeout = this.writeTimeout;
                //this.comPort.ReadBufferSize = 65534;
                //this.comPort.WriteBufferSize = 65534;

                retVal = true;
            }
            catch (Exception ex)
            {
                clsCommError.getInstance().CommErrorMessage = ex.Message;
                return retVal;
            }

            return retVal;
        }

        public void DestroyComPort()
        {
            if (this.IsCreated)
            {
                try
                {
                    if (IsOpen) { this.comPort.Close(); }
                    this.comPort.Dispose();
                    this.comPort = null;
                }
                catch (Exception ex)
                {
                    clsCommError.getInstance().CommErrorMessage = ex.Message;
                }
            }
        }

        public bool OpenComPort()
        {
            bool retVal = false;

            if (this.IsCreated && !this.IsOpen)
            {
                try
                {
                    this.comPort.Open();

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

        public void CloseComPort()
        {
            if (this.IsOpen)
            {
                try
                {
                    this.comPort.Close();
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

            if (this.IsOpen)
            {
                try
                {
                    this.comPort.Write(argb, 0, argb.Length);

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

        public byte[] ReadBuffer()
        {
            byte[] argb = null;
            /////////////////////////////////////////////////////////////////////////////////
            if (this.readBytes == null) { this.isReceiveCompleted = false; } // 2015.12.14
            /////////////////////////////////////////////////////////////////////////////////

            if (IsOpen)
            {
                try
                {
                    // ReadTimeout 
                    long startStamp = System.DateTime.Now.Ticks;                            
                    bool elapsedTime = false;

                    while (!this.isReceiveCompleted && !elapsedTime)
                    {
                        System.Threading.Thread.Sleep(100);
                        elapsedTime = System.DateTime.Now.Ticks - startStamp > System.TimeSpan.TicksPerMillisecond * this.comPort.ReadTimeout;
                    }

                    argb = this.readBytes;    
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
