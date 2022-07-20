using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Model
{
    public class EventData
    {

        public int id { get; set; }
        public string eventDateTime { get; set; }
        public string eventTime { get; set; }
        public string eventDate { get; set; }
        public string eventCode { get; set; }
        public string userId { get; set; }
        public int deviceId { get; set; }
        public string systemDateTime { get; set; }
        public int funcCode { get; set; }
        public int doorStatus { get; set; }
        public int moduleAddr { get; set; }
        public int operationMode { get; set; }
        public int readerAddr { get; set; }
        public bool sync { get; set; }

        public EventData() {
            //
        }

        public override string ToString()
        {
            //return "Id: " + this.id + " - Name: " + this.name;
            return this.eventDate + " " + this.eventTime + " " + this.eventCode + " " + this.userId + " " + this.deviceId + " " + this.funcCode;
        }

    }
}
