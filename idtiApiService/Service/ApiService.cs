using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


using idtiApiService.Model;
using idtiApiService.Impl;
using idtiApiService.Utils;


namespace idtiApiService.Service
{
    public class ApiService : IApiService {

        private readonly IConfiguration _configuration;
        private readonly ILogger<ApiService> _logger;
        private string _connStr;
        private int _commRetry = 1;

        public ApiService(IConfiguration configuration, ILogger<ApiService> logger) {
            _configuration = configuration;
            _logger = logger;
            _connStr = _configuration["ConnectionStrings:Oracle"];
            _commRetry = int.Parse(this._configuration["CommInterval:commRetry"]);
        }


        public void loadDevices() {

            string msg = "servicio integracion sistema control acceso iniciado .. ";
            _logger.LogInformation(msg);
            Console.WriteLine(msg);

            int pollingDelay = int.Parse(_configuration["CommInterval:pollingDelay"]);
            Console.WriteLine($"polling delay : ({pollingDelay} seconds) ");


            // sincronizar dispositivos sistema Oracle ..
            List<Terminal> terminalList = new List<Terminal>();

            using (OracleConnection con = new OracleConnection(this._connStr)) {
                using (OracleCommand cmd = con.CreateCommand()) {
                    try {

                        con.Open();
                        cmd.CommandText = "SELECT ID, NAME, STATUS, DEVICE_TYPE, IP_ADDR, MAC_ADDR, TCP_PORT, COMM_TIMEOUT FROM DEVICE";
                        OracleDataReader reader = cmd.ExecuteReader();

                        while (reader.Read()) {

                            try {

                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);
                                string ipAddr = reader.GetString(4);
                                int tcpPort = reader.GetInt32(6);

                                Terminal terminal = new Terminal(id, name, ipAddr, tcpPort);

                                terminal.status = reader.GetInt32(2);
                                terminal.commReadTimeOut = reader.GetInt32(7);
                                terminal.controllerModel = reader.GetInt32(3);
                                terminal.polling = (terminal.status == 1) ? true : false;

                                terminalList.Add(terminal);

                            } catch (Exception ex) {
                                _logger.LogWarning(ex.Message);
                                continue;
                            }

                        }
                        reader.Dispose();
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            } // fin using oracle devices ..


            
            using (var db = new IntegrationContext()) {

                foreach (Terminal terminal in terminalList) {

                    Terminal t = db.terminals.Find(terminal.id);
                    if (t == null) {
                        db.terminals.Add(terminal);
                    } else {
                        t.name = terminal.name;
                        t.ipAddr = terminal.ipAddr;
                        t.tcpPort = terminal.tcpPort;
                        t.status = terminal.status;
                        t.polling = (t.status == 1) ? true : false;
                        db.terminals.Update(t);
                    }
                }

                int cnt = db.SaveChanges();
                //Console.WriteLine("{0} records saved to database", cnt);

                msg = "Dispositivos de control acceso activos: ";

                _logger.LogInformation(msg);
                Console.WriteLine(msg);

                foreach (Terminal terminal in db.terminals) {
                    msg = "terminal -> id: " + terminal.id + " - ipAddr: " + terminal.ipAddr + " - port: " + terminal.tcpPort + " - name: " +  terminal.name;
                    _logger.LogInformation(msg);
                    Console.WriteLine(msg);
                }

            }
            


         }


        public List<Terminal> findAllDevices() {

            List<Terminal> deviceList = new List<Terminal>();
            using (var db = new IntegrationContext()) {
                deviceList = db.terminals.ToList();
            }

            return deviceList;

        }


        public List<UserDev> fetchUsersDevice(int deviceId) {

            List<UserDev> userDevList = new List<UserDev>();

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return userDevList;

            // validacion ping ..
            if (!Util.pingDevice(terminal.ipAddr)) return userDevList;

            SdkService sdkService = new SdkService();
            int userCount = 0;
            int loop = 5; //nro de intentos de comunicacion ..
            int intent = 0;
            do {
                userCount = sdkService.UserCountMethod(terminal);
                intent++;
                if (userCount > 0) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);
            if (userCount == 0) return userDevList;

            

            int increaseCount = isldev.clsDevParams.UserBlockCount;  /// Bloques de 50 ..
            for (int i = 1; i <= userCount; i += increaseCount) {

                int indexStartBlock = i;
                int indexEndBlock = i + increaseCount - 1;
                if (indexEndBlock > userCount) { indexEndBlock = userCount; }
                int indexLastBlock = userCount;

                //isldev.clsDevUserInfo[] devUsersInfo = sdkService.UserListMethod(indexStartBlock, indexEndBlock, indexLastBlock, terminal);
                isldev.clsDevUserInfo[] devUsersInfo = null;
                loop = 5; //nro de intentos de comunicacion ..
                intent = 0;
                do {
                    devUsersInfo = sdkService.UserListMethod(indexStartBlock, indexEndBlock, indexLastBlock, terminal);
                    intent++;
                    
                    if (isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None)) {
                        intent = loop + 1;
                    } else {
                        Task.Delay(1000);
                    }
                } while (intent < loop);


                if (isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None)) {

                    foreach (isldev.clsDevUserInfo userInfo in devUsersInfo) {

                        //Console.WriteLine("id:" + userInfo.UserID + " - level:" + userInfo.Level + " - expire-date:" + userInfo.AccessExpiredDate + " - prox-type:" + userInfo.ProximityType + " - wiegand:" + userInfo.ProximityWiegand);

                        string dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString();

                        UserDev userDev = new UserDev(userInfo.UserID, "");
                        //isldev.clsDevUserInfo devUserInfo = sdkService.UserReceiveMethod(dataInfo, userDev, terminal);

                        isldev.clsDevUserInfo devUserInfo = null;
                        int _loop = 5;
                        int _intent = 0;
                        do {
                            devUserInfo = sdkService.UserReceiveMethod(dataInfo, userDev, terminal);
                            _intent++;
                            if (devUserInfo != null) _intent = _loop + 1; else Task.Delay(1000);
                        } while (intent < _loop);


                        if (isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None)) {

                            if (devUserInfo.DevUserLCDData != null) userDev.displayName = devUserInfo.DevUserLCDData.UserLCDInitials;
                            if (devUserInfo.DevUserProxData != null) {
                                userDev.proximity.CardID = devUserInfo.DevUserProxData.UserProximityCardID;
                                userDev.proximity.FacilityCode = devUserInfo.DevUserProxData.UserProximityFacilityCode;
                            }

                            if (devUserInfo.DevUserTemplateData != null) {
                                userDev.biometric.templateCount = devUserInfo.TemplateCount;

                                //userDev.biometric.templateData = devUserInfo.DevUserTemplateData.UserTemplate;

                                byte[] template = devUserInfo.DevUserTemplateData.UserTemplate;
                                userDev.biometric.templateData = Convert.ToBase64String(template);

                            }

                            if (userDev.proximity.CardID == null) userDev.proximity.CardID = "";
                            if (userDev.proximity.FacilityCode == null) userDev.proximity.FacilityCode = "";

                            userDevList.Add(userDev);

                        }

                    }

                } else {
                    string msjError = "User Receive: " + ((isldev.clsDevMessage.MessageCodes)isldev.clsDevMessage.getInstance().MessageCode).ToString();
                    Console.WriteLine(msjError);
                }

            } // fin for ..

            
            terminal = null;
            sdkService = null;

            return userDevList;

        }


        public UserDev fetchUserDevice(int deviceId, string userId) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return null;

            // validacion ping ..
            if (!Util.pingDevice(terminal.ipAddr)) return null;

            if (!Util.IsNumeric(userId.Trim())) return null;
            userId = userId.PadLeft(16, '0');


            UserDev userDev = new UserDev(userId, "");


            SdkService sdkService = new SdkService();
            string dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString();

            int loop = this._commRetry; //nro de intentos de comunicacion ..
            int intent = 0;

            /// validacion existencia de usuario ..
            int ack = 0;
            do {
                ack = sdkService.UserIsExistMethod(userDev, terminal);
                intent++;
                if (ack == 1) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);
            if (ack == 0 || ack == 2) return null;


            isldev.clsDevUserInfo devUserInfo = null;
            loop = this._commRetry;
            intent = 0;
            do {
                devUserInfo = sdkService.UserReceiveMethod(dataInfo, userDev, terminal);
                intent++;
                if (devUserInfo != null) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);

            

            if (isldev.clsDevMessage.getInstance().MessageCode.Equals((int)isldev.clsDevMessage.MessageCodes.None)) {

                if (devUserInfo.DevUserLCDData != null) userDev.displayName = devUserInfo.DevUserLCDData.UserLCDInitials;
                if (devUserInfo.DevUserProxData != null) {
                    userDev.proximity.CardID = devUserInfo.DevUserProxData.UserProximityCardID;
                    userDev.proximity.FacilityCode = devUserInfo.DevUserProxData.UserProximityFacilityCode;
                }

                if (devUserInfo.DevUserTemplateData != null) {
                    userDev.biometric.templateCount = devUserInfo.TemplateCount;
                    byte[] template = devUserInfo.DevUserTemplateData.UserTemplate;
                    userDev.biometric.templateData = Convert.ToBase64String(template);
                }

                if (userDev.proximity.CardID == null) userDev.proximity.CardID = "";
                if (userDev.proximity.FacilityCode == null) userDev.proximity.FacilityCode = "";

            }

            if (devUserInfo == null) return null;

            
            terminal = null;
            sdkService = null;

            return userDev;

        }


        public List<UserDev> sendUsersDevice(int deviceId, List<UserDev> userDevList) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return null;


            List<UserDev> userList = new List<UserDev>();
            foreach (UserDev userDev in userDevList) {
                UserDev u = this.sendUserDevice(terminal, userDev);
                if (u != null) userList.Add(u);
            }

            return userList;

        }


        private UserDev sendUserDevice(Terminal terminal, UserDev userDev) {

            // validar información de usuario ..
            if (!Util.IsNumeric(userDev.userID.Trim())) return null;
            string userId = userDev.userID.PadLeft(16, '0');
            userDev.userID = userId;
            if (userDev.displayName.Trim() == string.Empty) userDev.displayName = int.Parse(userDev.userID.Trim()).ToString();


            string dataInfo = this.dataInfoCheck(userDev);

            SdkService sdkService = new SdkService();
            int ack = 0;


            int loop = this._commRetry; //nro de intentos de comunicacion ..
            int intent = 0;
            do
            {
                ack = sdkService.UserSendMethod(dataInfo, userDev, terminal);
                intent++;
                if (ack > 0) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);

            
            terminal = null;
            sdkService = null;

            if (ack != 1) return null;

            return userDev;

        }


        private string dataInfoCheck(UserDev userDev) {

            string dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString();

            try {
                if (userDev.biometric != null) {
                    if (userDev.biometric.templateData.Trim() == string.Empty || userDev.biometric.templateCount == 0) {
                        dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString();
                    }
                } else {
                    dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString();
                }
            } catch (Exception ex) {
                dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString();
                Console.WriteLine(ex.Message);
            }

            try {
                if (userDev.proximity != null)
                {
                    if (userDev.proximity.CardID.Trim() == string.Empty) {
                        if (dataInfo == isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) {
                            dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameFinger.ToString();
                        } else {
                            dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoName.ToString();
                        }
                    }
                } else {
                    if (dataInfo == isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) {
                        dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameFinger.ToString();
                    } else {
                        dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoName.ToString();
                    }
                }
            } catch (Exception ex) {
                if (dataInfo == isldev.clsDevCommand.DeviceUserObject.InfoNameCardFinger.ToString()) {
                    dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoNameFinger.ToString();
                } else {
                    dataInfo = isldev.clsDevCommand.DeviceUserObject.InfoName.ToString();
                }
                Console.WriteLine(ex.Message);
            }

            return dataInfo;
        }


        public int deleteUserDevice(int deviceId, string userId) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return 0;

            SdkService sdk = new SdkService();

            string dataInfo = isldev.clsDevCommand.DeviceUserDeleteObject.All.ToString();

            int ack = 0;

            int loop = this._commRetry;
            int intent = 0;
            do {
                ack = sdk.UserDeleteMethod(dataInfo, userId, terminal);
                intent++;
                if (ack == 1) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);

            if (ack == 2) ack = 0;

            terminal = null;
            sdk = null;

            return ack;

        }


        public int resetUsersDevice(int deviceId) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return 0;

            SdkService sdk = new SdkService();

            int ack = 0;
            int loop = this._commRetry;
            int intent = 0;
            do {
                ack = sdk.UserResetMethod(terminal);
                intent++;
                if (ack == 1) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);


            Task.Delay(200);
            terminal = null;
            sdk = null;

            return ack;

        }


        public int resetEventsDevice(int deviceId) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return 0;


            SdkService sdk = new SdkService();


            int ack = 0;
            int loop = this._commRetry;
            int intent = 0;
            do {
                ack = sdk.EventResetMethod(terminal);
                intent++;
                if (ack == 1) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);


            Task.Delay(200);
            terminal = null;
            sdk = null;

            return ack;

        }



        public string sendDateTimeDevice(int deviceId, string dateTime) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return "";

            if (!Util.IsDate(dateTime)) return "";

            DateTime dt = DateTime.Parse(dateTime);

            SdkService sdk = new SdkService();
            int loop = this._commRetry; //nro de intentos de comunicacion ..
            int intent = 0;
            do {
                int ack = sdk.DateTimeSendMethod(dt, terminal);
                intent++;
                if (ack > 0) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);

            sdk = null;

            return dateTime;

        }



        public string fetchDateTimeDevice(int deviceId) {

            Terminal terminal = null;
            using (var db = new IntegrationContext()) {
                terminal = db.terminals.Find(deviceId);
            }
            if (terminal == null) return "";

            SdkService sdk = new SdkService();
            string dateTime = "";

            int loop = this._commRetry; //nro de intentos de comunicacion ..
            int intent = 0;
            do {
                dateTime = sdk.DateTimeReceiveMethod(terminal);
                intent++;
                if (dateTime.Trim() != string.Empty) intent = loop + 1; else Task.Delay(1000);
            } while (intent < loop);


            dateTime = (dateTime.Trim() != string.Empty) ? Util.dateTimeFormat(dateTime.Trim()) : "";

            terminal = null;
            sdk = null;

            return dateTime;

        }



        public void setFlagDevice(int deviceId, bool flag) {

            using (var db = new IntegrationContext()) {
                Terminal terminal = db.terminals.Find(deviceId);
                terminal.polling = flag;
                db.SaveChanges();
            }

            int commDelay = int.Parse(this._configuration["CommInterval:commDelay"]);

            Task.Delay(commDelay);

        }



        public void pollingDevice() {

            try
            {
                List<Terminal> deviceList = new List<Terminal>();
                using (var db = new IntegrationContext()) {
                    deviceList = db.terminals.Where(x => x.polling).ToList();
                }
                if (deviceList == null || deviceList.Count() <= 0) return;


                SdkService sdkService = new SdkService();
                List<List<EventData>> eventDataList = new List<List<EventData>>();
                foreach (Terminal device in deviceList) {

                    // re-evaluacion de estado de dispositivos en cada iteracion ..
                    using (var db = new IntegrationContext()) {
                        List<Terminal> deviceEnabledList = db.terminals.Where(x => x.polling).ToList();
                        Terminal deviceCheck = deviceEnabledList.Find(x => x.id == device.id);
                        //Console.WriteLine("dispositivo id: " + device.id + " - activo: " + (deviceCheck != null));
                        if (deviceCheck == null) break;
                    }

                    List<EventData> eventList = sdkService.getEventData(device);
                    eventDataList.Add(eventList);

                }

                int cntLocal = this.persistSQLite(eventDataList);
                int cntImpl = this.persistOracle();

                eventDataList = null;
                sdkService = null;

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                _logger.LogWarning(ex.Message);
            }

        }


        private int persistSQLite(List<List<EventData>> eventDataList) {

            int cnt = 0;
            try {
                using (var db = new IntegrationContext()) {
                    foreach (List<EventData> eventList in eventDataList) {
                        foreach (EventData eventData in eventList) {
                            int filter = db.eventlogs.Where(x => x.eventDateTime.Trim() == eventData.eventDateTime.Trim() && x.userId.Trim() == eventData.userId.Trim()).Count();
                            if (filter == 0) {
                                eventData.sync = false;
                                db.eventlogs.Add(eventData);
                            }
                        }
                    }
                    cnt = db.SaveChanges();
                    //Console.WriteLine("{0} records saved to database", cnt);
                }
            } catch (Exception ex) {
                _logger.LogWarning(ex.Message);
            }
            return cnt;
        }



        private int persistOracle() {

            int cnt = 0;

            try {

                using (OracleConnection con = new OracleConnection(this._connStr)) {
                    using (OracleCommand cmd = con.CreateCommand()) {

                        con.Open();
                        using (var db = new IntegrationContext()) {
                            foreach (EventData eventData in db.eventlogs.Where(x => !x.sync).ToList()) {

                                cmd.CommandText = "INSERT INTO EVENTLOG " +
                                                          "(EVENT_DATETIME, EVENT_DATE, EVENT_TIME, EVENT_CODE, USER_ID, DEVICE_ID, SYSTEM_DATETIME, FUNC_CODE, DOOR_STATUS, MODULE_ADDR, OPERATION_MODE, READER_ADDR) " +
                                                          "VALUES (" +
                                                          "'" + eventData.eventDateTime + "', " +
                                                          "'" + eventData.eventDate + "', " +
                                                          "'" + eventData.eventTime + "', " +
                                                          "'" + eventData.eventCode + "', " +
                                                          "'" + eventData.userId + "', " +
                                                          " " + eventData.deviceId + ", " +
                                                          "'" + eventData.systemDateTime + "', " +
                                                          " " + eventData.funcCode + ", " +
                                                          " " + eventData.doorStatus + ", " +
                                                          " " + eventData.moduleAddr + ", " +
                                                          " " + eventData.operationMode + ", " +
                                                          " " + eventData.readerAddr + ")";

                                int success = cmd.ExecuteNonQuery();
                                //Console.WriteLine("oracle -> success -> " + success);
                                if (success > 0) {
                                    cnt++;
                                    eventData.sync = true;
                                    db.Update(eventData);

                                    _logger.LogInformation(eventData.ToString());
                                    Console.WriteLine(eventData.ToString());

                                }
                            } // fin foreach eventdata ..

                            int ok = db.SaveChanges();
                            if (ok > 0) Console.WriteLine("{0} records saved to database", ok);

                        } // fin using SQLite ..
                    }
                } // fin using Oracle ..

            } catch (Exception ex) {
                _logger.LogWarning(ex.Message);
            }

            return cnt;

        }






        /// ///////////////////////////////////////////////////////////////////////////////////
    }
}
