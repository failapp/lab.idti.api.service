using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace idtiApiService.Sdk
{
    class clsConfig
    {
        private int targetCommType;
        private string targetCommTCPIPAddr;
        private int targetCommTCPPort;
        private string targetCommSerialPortName;
        private int targetCommSerialBPS;
        private int targetCommReadTimeout;
        private int targetDevType;
        private int targetDevProtocol;
        private bool targetFrameOptIsRequestAck;
        private bool targetFrameOptIsExcludeDevStatus;
        private bool targetFrameOptIsPassword;
        private string targetFrameOptPasswordValue;

        public int TargetCommType
        {
            get { return this.targetCommType; }
            set { this.targetCommType = value; }
        }

        public string TargetCommTCPIPAddr
        {
            get { return this.targetCommTCPIPAddr; }
            set { this.targetCommTCPIPAddr = value; }
        }

        public int TargetCommTCPPort
        {
            get { return this.targetCommTCPPort; }
            set { this.targetCommTCPPort = value; }
        }

        public string TargetCommSerialPortName
        {
            get { return this.targetCommSerialPortName; }
            set { this.targetCommSerialPortName = value; }
        }

        public int TargetCommSerialBPS
        {
            get { return this.targetCommSerialBPS; }
            set { this.targetCommSerialBPS = value; }
        }

        public int TargetCommReadTimeout
        {
            get { return this.targetCommReadTimeout; }
            set { this.targetCommReadTimeout = value; }
        }

        public int TargetDevType
        {
            get { return this.targetDevType; }
            set { this.targetDevType = value; }
        }

        public int TargetDevProtocol
        {
            get { return this.targetDevProtocol; }
            set { this.targetDevProtocol = value; }
        }

        public bool TargetFrameOptIsRequestAck
        {
            get { return this.targetFrameOptIsRequestAck; }
            set { this.targetFrameOptIsRequestAck = value; }
        }

        public bool TargetFrameOptIsExcludeDevStatus
        {
            get { return this.targetFrameOptIsExcludeDevStatus; }
            set { this.targetFrameOptIsExcludeDevStatus = value; }
        }

        public bool TargetFrameOptIsPassword
        {
            get { return this.targetFrameOptIsPassword; }
            set { this.targetFrameOptIsPassword = value; }
        }

        public string TargetFrameOptPasswordValue
        {
            get { return this.targetFrameOptPasswordValue; }
            set { this.targetFrameOptPasswordValue = value; }
        }

        public clsConfig ()
        { 
        }

        // Get Config Item
        public string GetAppConfig (string sKey)
        {
            return ConfigurationManager.AppSettings[sKey];
        }

        // Set Config Item
        public void SetAppConfig (string sKey, string sValue)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            currentConfig.AppSettings.Settings[sKey].Value = sValue;
            currentConfig.Save();
            ConfigurationManager.RefreshSection("appSettings");   // 내용 갱신  
        }

        // Get All
        public void GetAllAppConfig()
        {
            this.targetCommType = Convert.ToInt32(this.GetAppConfig("TargetCommType"));
            this.targetCommTCPIPAddr = this.GetAppConfig("TargetCommTCPIPAddr");
            this.targetCommTCPPort = Convert.ToInt32(this.GetAppConfig("TargetCommTCPPort"));
            this.targetCommSerialPortName = this.GetAppConfig("TargetCommSerialPortName");
            this.targetCommSerialBPS = Convert.ToInt32(this.GetAppConfig("TargetCommSerialBPS"));
            this.TargetCommReadTimeout = Convert.ToInt32(this.GetAppConfig("TargetCommReadTimeout"));
            this.targetDevType = Convert.ToInt32(this.GetAppConfig("TargetDevType"));
            this.targetDevProtocol = Convert.ToInt32(this.GetAppConfig("TargetDevProtocol"));
            this.targetFrameOptIsRequestAck = Convert.ToBoolean(this.GetAppConfig("TargetFrameOptIsRequestAck"));
            this.targetFrameOptIsExcludeDevStatus = Convert.ToBoolean(this.GetAppConfig("TargetFrameOptIsExcludeDevStatus"));
            this.targetFrameOptIsPassword = Convert.ToBoolean(this.GetAppConfig("TargetFrameOptIsPassword"));
            this.targetFrameOptPasswordValue = this.GetAppConfig("TargetFrameOptPasswordValue");
        }
    }
}
