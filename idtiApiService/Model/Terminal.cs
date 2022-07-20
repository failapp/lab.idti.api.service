using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using idtiApiService.Sdk;

namespace idtiApiService.Model
{
    public class Terminal
    {

        public int id { get; set; }
        public string name { get; set; }
        public string ipAddr { get; set; }
        public int tcpPort { get; set; }
        public int status { get; set; }
        public int controllerModel { get; set; }  // modelo  .. 
        public int commType { get; set; }         // TCPIP o Serial ..
        public int commReadTimeOut { get; set; }
        public string commSerialPort { get; set; }
        public int commSerialBPS { get; set; }
        public int protocolVersion { get; set; }
        public bool polling { get; set; }

        public Terminal() {
            //
        }

        public Terminal(int id, string name, string ipAddr, int tcpPort) {

            this.id = id;
            this.name = name;
            this.ipAddr = ipAddr;
            this.tcpPort = tcpPort;
            this.controllerModel = (int)isldev.clsDevParams.ControllerType.BSC_201;
            this.commType = (int)clsGlobal.enumCommunicationIndex.TCPIP;
            this.commReadTimeOut = 3000;
            this.commSerialPort = "COM1";
            this.commSerialBPS = 19200;
            this.protocolVersion = (int)isldev.clsDevParams.ProtocolVersion.VersionTwo;
            this.polling = false;

        }

        public override string ToString() {

            return "Id: " + this.id + " - Name: " + this.name + " - Ip Addr: " +
                    this.ipAddr + " - tcpPort: " + this.tcpPort + " - Model: " + ((isldev.clsDevParams.ControllerType)this.controllerModel).ToString();

        }


    }
}
