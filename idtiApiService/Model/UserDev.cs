using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using isldev;
using islbio;
using islprox;

namespace idtiApiService.Model
{
    public class UserDev
    {

        public string userID { get; set; }
        public string displayName { get; set; }
        public string dateValidity { get; set; }
        public int level { get; set; }
        public bool accessPIN { get; set; }

        public UserDevProximity proximity;
        public UserDevBiometric biometric;

        public UserDev()
        {
            //
        }

        public UserDev(string id, string display)
        {

            this.userID = id;
            this.displayName = display;
            this.dateValidity = DateTime.Now.AddYears(50).ToString("yyyyMMdd");
            this.level = (int)clsDevParams.UserLevel.User_31;
            this.accessPIN = true;

            this.proximity = new UserDevProximity();
            this.proximity.proximityType = (int)clsProxParams.ProximityType.IntelliScan_M;
            this.proximity.proximityWiegand = (int)clsProxParams.Proximity_M_Wiegand.ISF_M_64bitFullHex;
            this.proximity.FacilityCode = "";
            this.proximity.CardID = "";

            this.biometric = new UserDevBiometric();
            this.biometric.biometricType = (int)clsBioParams.TemplateType.BioScan_IDTi;
            this.biometric.biometricTypeSub = (int)clsBioParams.Template_IDTi_SubType.BSU_D_FS11;
            this.biometric.templateCount = (int)clsBioParams.TemplateCount.None;
            this.biometric.templateData = "";  //null;

        }

        public override string ToString()
        {
            return "Id: " + this.userID + " - Name: " + this.displayName;
        }

    }


    public class UserDevProximity
    {

        public int proximityType { get; set; }
        public int proximityWiegand { get; set; }
        public string CardID { get; set; }
        public string FacilityCode { get; set; }

    }

    public class UserDevBiometric
    {

        public int templateCount { get; set; }
        public int biometricType { get; set; }
        public int biometricTypeSub { get; set; }
        //public byte[] templateData { get; set; }
        public string templateData { get; set; }

    }



}
