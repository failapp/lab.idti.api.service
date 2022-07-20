using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Configuration;
using System.Threading;

using idtiApiService.Sdk;
using idtiApiService.Model;
using idtiApiService.Utils;

using isldev;
using islbio;
using islprox;


namespace idtiApiService.Service
{
    public class SdkService
    {

        private static readonly string folderPathTemplate = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Biometria\Template";
        private static readonly string folderPathImage = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Biometria\Image";
        private static readonly string folderPathBinary = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Biometria\Binary";

        public string getTemplateFolder() {
            return folderPathTemplate;
        }

        private clsGlobal Global = new clsGlobal();
        private clsConfig Config = new clsConfig();
        private clsIndySock IndySock = new clsIndySock();
        private clsSerialPort SerialPort = new clsSerialPort();
        private clsDevice Device = new clsDevice();

        private clsDevData devData = new isldev.clsDevData();
        private clsDevImage devImage = new isldev.clsDevImage();
        private clsDevCommand devCommand;
        private clsDevFrame devFrame;
        private clsBioTemplateFrame devBioTemplateFrame = new clsBioTemplateFrame();
        private clsDevCommon devCommon = new isldev.clsDevCommon();


        public void sendDateTime(Terminal terminal) {

            int dataSend = this.DateTimeSendMethod(DateTime.Now, terminal);

            if (isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None)) {
                Console.WriteLine("Data Send: " + ((isldev.clsDevParams.AckResult)dataSend).ToString());
            } else {
                Console.WriteLine("Data Send: " + ((isldev.clsDevMessage.MessageCodes)isldev.clsDevMessage.getInstance().MessageCode).ToString());
            }

        }


        public List<EventData> getEventData(Terminal terminal) {

            List<EventData> lsData = new List<EventData>();

            try {

                // validacion ping ..
                if (!Util.pingDevice(terminal.ipAddr)) return lsData;

                isldev.clsDevEventInfo[] devEventInfo = this.EventDataMethod(terminal);

                if ( isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None) ) {


                    foreach (isldev.clsDevEventInfo eventInfo in devEventInfo) {
                        
                        EventData data = new EventData();

                        data.eventDate = eventInfo.DateTime.Substring(0,8);
                        data.eventTime = eventInfo.DateTime.Substring(8,6);
                        data.eventDateTime = eventInfo.DateTime;
                        data.eventCode = eventInfo.EventCode;
                        data.userId = eventInfo.AccessID;
                        data.deviceId = terminal.id;
                        data.systemDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                        data.moduleAddr = eventInfo.ModuleAddress;
                        data.operationMode = eventInfo.OperationMode; 
                        data.doorStatus = eventInfo.DoorStatus; 
                        data.funcCode = eventInfo.FuncCode; 

                        lsData.Add(data);

                    } // fin foreach ..

                } else {
                    //Console.WriteLine("Request Event Data: " + ((isldev.clsDevMessage.MessageCodes)isldev.clsDevMessage.getInstance().MessageCode).ToString());
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return lsData;

        }




        /// ///////////////////////////////////////////////////////////////

        private void InitCommErrorMessage() {
            clsCommError.getInstance().CommErrorMessage = string.Empty;
            string msj = clsCommError.getInstance().CommErrorMessage;
            Console.WriteLine(msj);
        }


        private void SetCommErrorMessage() {
            if (clsCommError.getInstance().CommErrorMessage != string.Empty) {
                string msj = "Message : " + clsCommError.getInstance().CommErrorMessage;
                Console.WriteLine(msj);
            }
        }


        public int DateTimeSendMethod(DateTime fechaHora, Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            DateTime currentDateTime = this.Global.ConvertDateTimeToDateTime(fechaHora.ToString("yyyyMMddHHmmss"));

            bData = this.devData.BuildDateTimeWithDayOfWeekByte(currentDateTime.ToString("yyyyMMddHHmmss"), (int)currentDateTime.DayOfWeek);

            this.devCommand = new isldev.clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.DeviceDateTimeChange);
            this.devFrame = new isldev.clsDevFrame(terminal.protocolVersion, false, bData);
            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;

            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = ""; 
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();
            clsCommInfo info = new clsCommInfo();

            info.CommType = terminal.commType;             
            info.CommReadTimeout = terminal.commReadTimeOut; 
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);
            bReturn = comm.ReceiveReturnFrameByte();
            retVal = this.devFrame.GetDeviceDataResult(bReturn);

            return retVal;

        }



        public string DateTimeReceiveMethod(Terminal terminal) {

            string retVal = string.Empty;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            this.devCommand = new isldev.clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.DeviceDateTimeCheck);

            this.devFrame = new isldev.clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1; 
            this.devFrame.AddressDestModule = 0;


            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();
            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();


            /*
            if (!this.chkFrameOptIsExcludeDeviceStatus.Checked)
            {
                if (this.currProtocolVersion.Equals((int)isldev.clsDevParams.ProtocolVersion.VersionOne))
                {
                    // Version One
                    isldev.clsDevStatusVerOneInfo devStatusInfo = this.devFrame.GetDeviceStatusObjectVersionOne(bReturn);
                    this.SetDeviceStatus(devStatusInfo);

                    if (devStatusInfo != null && devStatusInfo.DeviceDateTime.Length > 0) { retVal = devStatusInfo.DeviceDateTime; }
                }
                else if (this.currProtocolVersion.Equals((int)isldev.clsDevParams.ProtocolVersion.VersionTwo))
                {
                    // Version Two
                    isldev.clsDevStatusVerTwoInfo devStatusInfo = this.devFrame.GetDeviceStatusObjectVersionTwo(bReturn);
                    this.SetDeviceStatus(devStatusInfo);

                    if (devStatusInfo != null && devStatusInfo.DeviceDateTime.Length > 0) { retVal = devStatusInfo.DeviceDateTime; }
                }
                
            }
            */

            clsDevStatusVerTwoInfo devStatusInfo = this.devFrame.GetDeviceStatusObjectVersionTwo(bReturn);
            if (devStatusInfo != null && devStatusInfo.DeviceDateTime.Length > 0) { retVal = devStatusInfo.DeviceDateTime; }


            return retVal;

        }



        private clsDevEventInfo[] EventDataMethod(Terminal terminal) {

            isldev.clsDevEventInfo[] retVal = null;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            /// auto-sincronizacion fecha hora ..
            bData = this.devData.BuildDateTimeWithDayOfWeekByte(DateTime.Now.ToString("yyyyMMddHHmmss"), (int)DateTime.Now.DayOfWeek);

            try {

                this.devCommand = new isldev.clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.EventReceive);
                
                this.devFrame = new isldev.clsDevFrame(terminal.protocolVersion, false, bData);

                this.devFrame.AddressDestController = 1;
                this.devFrame.AddressDestModule = 0;
                this.devFrame.FrameOptIsRequestAck = true;
                this.devFrame.FrameOptIsExcludeDeviceStatus = false;
                this.devFrame.FrameOptIsTimeSync = true;
                this.devFrame.FrameOptIsReRequestEvent = false;
                this.devFrame.FrameOptIsBlocking = true;
                this.devFrame.FrameOptIsEventImage = false;
                this.devFrame.FrameOptIsPassword = false;
                this.devFrame.Password = "";

                this.devFrame.Command = this.devCommand.CommandCode;
                this.devFrame.CommandSub = this.devCommand.CommandSubCode;
                this.devFrame.ObjCode = this.devCommand.ObjectCode;
                this.devFrame.FrameOptIsTCP = true;

                bFrame = this.devFrame.BuildFrameByte();

                clsCommInfo info = new clsCommInfo();

                info.CommType = terminal.commType;
                info.CommReadTimeout = terminal.commReadTimeOut;
                info.CommTCPIPAddr = terminal.ipAddr;
                info.CommTCPPort = terminal.tcpPort;
                info.CommSerialPort = terminal.commSerialPort;
                info.CommSerialBPS = terminal.commSerialBPS;

                /// 2016.02 edit
                info.CommDelay = false;

                clsComm comm = new clsComm(info, bFrame);
                bReturn = comm.ReceiveReturnFrameByte();
                retVal = this.devFrame.GetEventInfoObjectAll(bReturn);

            } catch (Exception ex) {
                Console.WriteLine("!!!! ERROR SOCKET !!! -> " + ex.Message);
            }

            return retVal;

        }



        public clsDevUserInfo UserReceiveMethod(string infoData, UserDev userDev, Terminal terminal)
        {

            clsDevUserInfo retVal = null;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            bData = this.devData.BuildUserRequestIDByte(userDev.userID, "00000001");

            this.devCommand = new clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.UserReceive);

            this.devCommand.SetUserObject((int)Enum.Parse(typeof(clsDevCommand.DeviceUserObject), infoData));

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;    
            this.devFrame.AddressDestModule = 0;        
            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();

            info.CommType = terminal.commType; 
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            // 2016.02 edit
            info.CommDelay = false;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserInfoObjectAll(bReturn,
                                                        (int)Enum.Parse(typeof(clsDevCommand.DeviceUserObject), infoData),
                                                        (int)Enum.Parse(typeof(clsDevParams.MultiLanguage), "English"));

            /// forzar data proximity para tarjetas HID / ICLASS ..
            if (retVal != null)
            {
                if (retVal.DevUserProxData == null)
                {

                    try
                    {

                        int mifare = (int)clsProxParams.ProximityType.IntelliScan_M;
                        int wiegand = (int)clsProxParams.Proximity_M_Wiegand.ISF_M_64bitFullHex;

                        if (userDev.proximity.proximityType == mifare && userDev.proximity.proximityWiegand == wiegand)
                        {

                            byte[] bProxID = new byte[8];
                            bProxID[0] = 0;
                            bProxID[1] = 0;
                            bProxID[2] = 0;
                            bProxID[3] = bReturn[330];
                            bProxID[4] = bReturn[329];
                            bProxID[5] = bReturn[328];
                            bProxID[6] = bReturn[327];
                            bProxID[7] = bReturn[326];

                            clsELCardFormat.Instance.AnalCardData(Convert.ToInt32(userDev.proximity.proximityType), Convert.ToInt32(userDev.proximity.proximityWiegand), bProxID);

                            clsConvertNumericType objCardID = new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType,
                                (int)clsConvertNumericType.enumDecimalType.ubitType64, clsELCardFormat.Instance.CardID.ToString());

                            clsDevUserProxData userProxData = new clsDevUserProxData();
                            userProxData.UserProximityCardID = objCardID.HexaValue;
                            userProxData.UserProximityFacilityCode = "";
                            retVal.DevUserProxData = userProxData;

                        }

                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);

                    }
                }
            }

            return retVal;

        }



        public int UserCountMethod(Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            this.devCommand = new clsDevCommand((int)clsDevCommand.DeviceCommandType.UserCountCheck);

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;  
            this.devFrame.AddressDestModule = 0;

            this.devFrame.FrameOptIsRequestAck = true;            
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;  
            this.devFrame.FrameOptIsPassword = false;             
            this.devFrame.Password = "";                          
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.BlockStartDataIndex = (int)clsDevParams.FrameDataBlockIndex.Min;
            this.devFrame.BlockEndDataIndex = (int)clsDevParams.FrameDataBlockIndex.Max;
            this.devFrame.BlockCountDataIndex = (int)clsDevParams.FrameDataBlockIndex.Max;

            
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();

            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserCount(bReturn);

            return retVal;

        }



        public clsDevUserInfo[] UserListMethod(int blockStartIndex, int blockEndIndex, int userCount, Terminal terminal) {

            clsDevUserInfo[] retVal = null;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            this.devCommand = new clsDevCommand((int)clsDevCommand.DeviceCommandType.UserListReceive);

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;

            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;

            this.devFrame.FrameOptIsBlocking = true;

            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.BlockStartDataIndex = blockStartIndex;
            this.devFrame.BlockEndDataIndex = blockEndIndex;
            this.devFrame.BlockCountDataIndex = userCount;

            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();

            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            // 2016.02 edits
            info.CommDelay = false;
            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            if (bReturn == null) {
                Thread.Sleep(4000); //GSM TEST
                bReturn = comm.ReceiveReturnFrameByte();
            }

            retVal = this.devFrame.GetUserInfoListObject(bReturn);

            return retVal;

        }



        public int UserSendMethod(string infoData, UserDev userDev, Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            clsDevUserInfo devUserInfo = new clsDevUserInfo();

            devUserInfo.UserID = userDev.userID;
            devUserInfo.UserRevisionID = "00000001";
            devUserInfo.Level = userDev.level;
            devUserInfo.ValidationCode = 0;
            devUserInfo.TimezoneCode = 0;
            devUserInfo.CanteenCode = 0;
            devUserInfo.UserGeneralGroupCode = 0;
            devUserInfo.AccessPassword = "";
            devUserInfo.AccessExpiredDate = userDev.dateValidity;

            devUserInfo.AccessOptionIsArea = false;
            devUserInfo.AccessOptionIsOffline = false;
            devUserInfo.AccessOptionIsValidation = false;
            devUserInfo.AccessOptionIsTimezone = false;
            devUserInfo.AccessOptionIsAccessGroup = false;
            devUserInfo.AccessOptionIsAPB = false;

            devUserInfo.AccessOptionIsInterLock = false;
            devUserInfo.AccessOptionIsTrap = false;
            devUserInfo.AccessOptionIsEscort = false;
            devUserInfo.AccessOptionIsGuardTour = false;
            devUserInfo.AccessOptionIsDuress = false;
            devUserInfo.AccessOptionIsVisitor = false;
            devUserInfo.AccessOptionIsPatrol = false;
            devUserInfo.AccessOptionIsHandicap = false;
            devUserInfo.AccessOptionIsInactiveDoor = false;
            devUserInfo.AccessOptionIsTwoMan = false;
            devUserInfo.AccessOptionIsTwoReader = false;

            devUserInfo.AccessOptionIsPINPass = userDev.accessPIN;

            devUserInfo.AccessOptionIsCardPass = false;
            devUserInfo.AccessOptionIsLocalAPB = false;
            devUserInfo.AccessOptionIsGlobalAPB = false;

            devUserInfo.AccessOptionIsLCDInitials = true;
            devUserInfo.AccessOptionIsPassword = false;

            devUserInfo.BiometricType = userDev.biometric.biometricType;
            devUserInfo.BiometricTypeSub = userDev.biometric.biometricTypeSub;
            devUserInfo.TemplateCount = userDev.biometric.templateCount;


            devUserInfo.ProximityType = userDev.proximity.proximityType;
            devUserInfo.ProximityWiegand = userDev.proximity.proximityWiegand;
            devUserInfo.ProximityType2 = (int)Enum.Parse(typeof(clsProxParams.ProximityType), "None");
            devUserInfo.ProximityWiegand2 = this.Device.GetProximityWiegandValue("None", "None");

            if (infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoName.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoNameFinger.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoData.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoDataFinger.ToString()))
            {
                devUserInfo.DevUserLCDData = new isldev.clsDevUserLCDData();
                devUserInfo.DevUserLCDData.UserLCDInitials = userDev.displayName;
            }

            if (infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoCard.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoCardFinger.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoData.ToString()) ||
                infoData.Equals(isldev.clsDevCommand.DeviceUserObject.InfoDataFinger.ToString()))
            {
                devUserInfo.DevUserProxData = new isldev.clsDevUserProxData();
                devUserInfo.DevUserProxData.UserProximityFacilityCode = userDev.proximity.FacilityCode;
                devUserInfo.DevUserProxData.UserProximityCardID = userDev.proximity.CardID;
                // 2016.02 edit
                devUserInfo.DevUserProxData2 = new isldev.clsDevUserProxData();
                devUserInfo.DevUserProxData2.UserProximityFacilityCode = "";
                devUserInfo.DevUserProxData2.UserProximityCardID = "";

            }



            if (infoData.Equals(clsDevCommand.DeviceUserObject.InfoFinger.ToString()) ||
                infoData.Equals(clsDevCommand.DeviceUserObject.InfoCardFinger.ToString()) ||
                infoData.Equals(clsDevCommand.DeviceUserObject.InfoNameFinger.ToString()) ||
                infoData.Equals(clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) ||
                infoData.Equals(clsDevCommand.DeviceUserObject.InfoData.ToString()) ||
                infoData.Equals(clsDevCommand.DeviceUserObject.InfoDataFinger.ToString()))
            {

                byte[] bTemplate = Convert.FromBase64String(userDev.biometric.templateData);

                devUserInfo.DevUserTemplateData = new clsDevUserTemplateData();
                devUserInfo.DevUserTemplateData.UserTemplate = bTemplate;

                /*
                string templateName = userDev.userID + "00000001.DAT";
                string templateFileName = folderPathTemplate + @"\" + templateName;
                byte[] bTemplate = this.devBioTemplateFrame.OpenTemplateFileToByte(templateFileName);
                if (this.devBioTemplateFrame.IsValidTemplate(bTemplate,
                                                             devUserInfo.BiometricType,
                                                             devUserInfo.BiometricTypeSub,
                                                             devUserInfo.TemplateCount,
                                                             devUserInfo.UserID))
                {

                    devUserInfo.DevUserTemplateData = new clsDevUserTemplateData();
                    devUserInfo.DevUserTemplateData.UserTemplate = bTemplate;
                }
                */
            }

            /// ////////////           
            bData = this.devData.BuildUserInfoObjectAllByte(devUserInfo,
                                                            (int)Enum.Parse(typeof(isldev.clsDevCommand.DeviceUserObject), infoData),
                                                            (int)Enum.Parse(typeof(isldev.clsDevParams.MultiLanguage), "English"));
            /// ///////////

            this.devCommand = new isldev.clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.UserSend);
            this.devCommand.SetUserObject((int)Enum.Parse(typeof(isldev.clsDevCommand.DeviceUserObject), infoData));

            this.devFrame = new isldev.clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;



            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();
            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;
            // 2016.02 edit
            info.CommDelay = false;
            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserResult(bReturn);

            return retVal;

        }



        public int UserIsExistMethod(UserDev userDev, Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            bData = this.devData.BuildUserRequestIDByte(userDev.userID, "00000001");

            this.devCommand = new clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.UserRegisterCheck);

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;


            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();
            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserResult(bReturn);

            return retVal;

        }



        public int UserDeleteMethod(string infoData, string userId, Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            bData = this.devData.BuildUserRequestIDByte(userId, "00000001");

            this.devCommand = new clsDevCommand((int)isldev.clsDevCommand.DeviceCommandType.UserDelete);
            this.devCommand.SetUserDeleteObject((int)Enum.Parse(typeof(clsDevCommand.DeviceUserDeleteObject), infoData));

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;

            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;           
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();

            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserResult(bReturn);

            return retVal;

        }



        public int UserResetMethod(Terminal terminal) {
            int retVal = 0;

            string dataInfo = clsDevCommand.DeviceUserDeleteObject.All.ToString();

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            this.devCommand = new clsDevCommand((int)clsDevCommand.DeviceCommandType.UserReset);
            this.devCommand.SetUserDeleteObject((int)Enum.Parse(typeof(clsDevCommand.DeviceUserDeleteObject), dataInfo));

            this.devFrame = new isldev.clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;


            this.devFrame.FrameOptIsRequestAck = true; 
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false; 
            this.devFrame.Password = "";           
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;


            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();
            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut + 10000;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort;
            info.CommSerialBPS = terminal.commSerialBPS;
            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetUserResult(bReturn);

            return retVal;

        }



        public int EventResetMethod(Terminal terminal) {

            int retVal = 0;

            byte[] bData = null;
            byte[] bFrame = null;
            byte[] bReturn = null;

            this.devCommand = new clsDevCommand((int)clsDevCommand.DeviceCommandType.EventReset);

            this.devFrame = new clsDevFrame(terminal.protocolVersion, false, bData);

            this.devFrame.AddressDestController = 1;
            this.devFrame.AddressDestModule = 0;


            this.devFrame.FrameOptIsRequestAck = true;
            this.devFrame.FrameOptIsExcludeDeviceStatus = false;
            this.devFrame.FrameOptIsPassword = false;
            this.devFrame.Password = "";
            this.devFrame.Command = this.devCommand.CommandCode;
            this.devFrame.CommandSub = this.devCommand.CommandSubCode;
            this.devFrame.ObjCode = this.devCommand.ObjectCode;
            this.devFrame.FrameOptIsTCP = true;

            bFrame = this.devFrame.BuildFrameByte();

            clsCommInfo info = new clsCommInfo();
            info.CommType = terminal.commType;
            info.CommReadTimeout = terminal.commReadTimeOut; //+ 10000;
            info.CommTCPIPAddr = terminal.ipAddr;
            info.CommTCPPort = terminal.tcpPort;
            info.CommSerialPort = terminal.commSerialPort; 
            info.CommSerialBPS = terminal.commSerialBPS;

            clsComm comm = new clsComm(info, bFrame);

            bReturn = comm.ReceiveReturnFrameByte();

            retVal = this.devFrame.GetEventResetResult(bReturn);

            return retVal;

        }



        


        ///////////////////////////////////////////////////////////////////////////////////////////////////// ..
    }
}
