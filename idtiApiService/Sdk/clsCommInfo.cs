using System;
using System.Collections.Generic;
using System.Text;

namespace idtiApiService.Sdk
{
    class clsCommInfo    
    {
        private int commType;
        private int commReadTimeout;
        private string commTCPIPAddr;
        private int commTCPPort;
        private string commSerialPort;
        private int commSerialBPS;
        // 2016.02 edit
        private bool commDelay;

        public int CommType
        {
            get { return this.commType; }
            set { this.commType = value; }
        }

        public int CommReadTimeout
        {
            get { return this.commReadTimeout; }
            set { this.commReadTimeout = value; }
        }

        public string CommTCPIPAddr
        {
            get { return this.commTCPIPAddr; }
            set { this.commTCPIPAddr = value; }
        }

        public int CommTCPPort
        {
            get { return this.commTCPPort; }
            set { this.commTCPPort = value; }
        }

        public string CommSerialPort
        {
            get { return this.commSerialPort; }
            set { this.commSerialPort = value; }
        }

        public int CommSerialBPS
        {
            get { return this.commSerialBPS; }
            set { this.commSerialBPS = value; }
        }
        // 2016.02 edit
        public bool CommDelay
        {
            get { return this.commDelay; }
            set { this.commDelay = value; }
        }

        public clsCommInfo()
        { 
        }


    }
}
