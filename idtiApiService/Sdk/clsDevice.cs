using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace idtiApiService.Sdk
{
    class clsDevice
    {
        public enum UserCommand
        {
            UserCount,
            UserList,
            UserReceive,
            UserSend,
            UserCheckIsExist,
            UserDelete,
            UserReset
        }

        public clsDevice()
        { 
        }

        public string[] GetControllerProtocol()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.ProtocolVersion)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.ProtocolVersion)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetControllerType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.ControllerType)).Length - 1];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.ControllerType)))
            {
                if (!value.Equals("None"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetEventIndexType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.EventIndexType)).Length - 1];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.EventIndexType)))
            {
                if (!value.Equals("None")) {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetUserLevel()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.UserLevel)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.UserLevel)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetValidation()
        {
            string[] args = new string[(int)isldev.clsDevParams.ValidationCode.Max + 1];            

            for (int i = (int)isldev.clsDevParams.ValidationCode.Min; i <= (int)isldev.clsDevParams.ValidationCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args; 
        }

        public string[] GetValidationWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.ValidationCode.Max];

            for (int i = (int)isldev.clsDevParams.ValidationCode.Min + 1; i <= (int)isldev.clsDevParams.ValidationCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetTimezone()
        {
            string[] args = new string[(int)isldev.clsDevParams.TimezoneCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.TimezoneCode.Min; i <= (int)isldev.clsDevParams.TimezoneCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetTimezoneWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.TimezoneCode.Max];

            for (int i = (int)isldev.clsDevParams.TimezoneCode.Min + 1; i <= (int)isldev.clsDevParams.TimezoneCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetCanteenGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.CanteenGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.CanteenGroupCode.Min; i <= (int)isldev.clsDevParams.CanteenGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetCanteenGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.CanteenGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.CanteenGroupCode.Min + 1; i <= (int)isldev.clsDevParams.CanteenGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetUserGeneralGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.UserGeneralGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.UserGeneralGroupCode.Min; i <= (int)isldev.clsDevParams.UserGeneralGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetUserGeneralGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.UserGeneralGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.UserGeneralGroupCode.Min + 1; i <= (int)isldev.clsDevParams.UserGeneralGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetElevatorCtrlAModeLevel()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.ElevatorControlAPolicyLevel)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.ElevatorControlAPolicyLevel)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetElevatorCtrlAModeRelayOuputCode()
        {
            string[] args = new string[(int)isldev.clsDevParams.ElevatorControlARelayOutputScope.Max + 1];

            for (int i = (int)isldev.clsDevParams.ElevatorControlARelayOutputScope.Min; i <= (int)isldev.clsDevParams.ElevatorControlARelayOutputScope.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetUserGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.UserGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.UserGroupCode.Min; i <= (int)isldev.clsDevParams.UserGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetUserGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.UserGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.UserGroupCode.Min + 1; i <= (int)isldev.clsDevParams.UserGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetDeviceGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.DeviceGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.DeviceGroupCode.Min; i <= (int)isldev.clsDevParams.DeviceGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetDeviceGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.DeviceGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.DeviceGroupCode.Min + 1; i <= (int)isldev.clsDevParams.DeviceGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetAccessGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.AccessGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.AccessGroupCode.Min; i <= (int)isldev.clsDevParams.AccessGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetAccessGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.AccessGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.AccessGroupCode.Min + 1; i <= (int)isldev.clsDevParams.AccessGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetElevatorControlGroup()
        {
            string[] args = new string[(int)isldev.clsDevParams.ElevatorControlGroupCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.ElevatorControlGroupCode.Min; i <= (int)isldev.clsDevParams.ElevatorControlGroupCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetElevatorControlGroupWithoutNone()
        {
            string[] args = new string[(int)isldev.clsDevParams.ElevatorControlGroupCode.Max];

            for (int i = (int)isldev.clsDevParams.ElevatorControlGroupCode.Min + 1; i <= (int)isldev.clsDevParams.ElevatorControlGroupCode.Max; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetElevatorControlGroupSector()
        {
            const int elvCtrlGroupSectorCodeMin = 1;
            const int elvCtrlGroupSectorCodeMax = 15;

            string[] args = new string[elvCtrlGroupSectorCodeMax];

            for (int i = elvCtrlGroupSectorCodeMin; i <= elvCtrlGroupSectorCodeMax; i += 1)
            {
                args[i - 1] = i.ToString();
            }

            return args;
        }

        public string[] GetDeviceLevel()
        {
            string[] args = new string[(int)isldev.clsDevParams.DeviceLevelCode.Max + 1];

            for (int i = (int)isldev.clsDevParams.DeviceLevelCode.Min; i <= (int)isldev.clsDevParams.DeviceLevelCode.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetMultiLanguage()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.MultiLanguage)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.MultiLanguage)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetOperationMode(int controllerType)
        {
            string[] args = null;

            if (controllerType >= (int)isldev.clsDevParams.ControllerType.ISC_101 && controllerType <= (int)isldev.clsDevParams.ControllerType.ISC_201A)
            {
                // ISC
                args = new string[6];
                args[0] = isldev.clsDevParams.OperationMode.MODE_ANY_POSSIBLE_ONE.ToString();
                args[1] = isldev.clsDevParams.OperationMode.MODE_CARD.ToString();
                args[2] = isldev.clsDevParams.OperationMode.MODE_ID_OR_CARD.ToString();
                args[3] = isldev.clsDevParams.OperationMode.MODE_ID_AND_CARD.ToString();
                args[4] = isldev.clsDevParams.OperationMode.MODE_OPEN.ToString();
                args[5] = isldev.clsDevParams.OperationMode.MODE_CLOSED.ToString();
            }
            else if (controllerType >= (int)isldev.clsDevParams.ControllerType.BSC_101 && controllerType <= (int)isldev.clsDevParams.ControllerType.BSC_201S)
            {
                // BSC
                args = new string[12];
                args[0] = isldev.clsDevParams.OperationMode.MODE_ANY_POSSIBLE_ONE.ToString();
                args[1] = isldev.clsDevParams.OperationMode.MODE_FP.ToString();
                args[2] = isldev.clsDevParams.OperationMode.MODE_ID_AND_FP.ToString();
                args[3] = isldev.clsDevParams.OperationMode.MODE_CARD_OR_FP.ToString();
                args[4] = isldev.clsDevParams.OperationMode.MODE_CARD_AND_FP.ToString();
                args[5] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_IDandCARD.ToString();
                args[6] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_CARDandFP.ToString();
                args[7] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_CARD.ToString();
                args[8] = isldev.clsDevParams.OperationMode.MODE_IDandCARD_OR_CARDandFP.ToString();
                args[9] = isldev.clsDevParams.OperationMode.MODE_ID_AND_CARD_AND_FP.ToString();
                args[10] = isldev.clsDevParams.OperationMode.MODE_OPEN.ToString();
                args[11] = isldev.clsDevParams.OperationMode.MODE_CLOSED.ToString();
            }
            else
            {
                // SSC
                args = new string[15];
                args[0] = isldev.clsDevParams.OperationMode.MODE_ANY_POSSIBLE_ONE.ToString();
                args[1] = isldev.clsDevParams.OperationMode.MODE_CARD.ToString();
                args[2] = isldev.clsDevParams.OperationMode.MODE_ID_OR_CARD.ToString();
                args[3] = isldev.clsDevParams.OperationMode.MODE_ID_AND_CARD.ToString();
                args[4] = isldev.clsDevParams.OperationMode.MODE_FP.ToString();
                args[5] = isldev.clsDevParams.OperationMode.MODE_ID_AND_FP.ToString();
                args[6] = isldev.clsDevParams.OperationMode.MODE_CARD_OR_FP.ToString();
                args[7] = isldev.clsDevParams.OperationMode.MODE_CARD_AND_FP.ToString();
                args[8] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_IDandCARD.ToString();
                args[9] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_CARDandFP.ToString();
                args[10] = isldev.clsDevParams.OperationMode.MODE_IDandFP_OR_CARD.ToString();
                args[11] = isldev.clsDevParams.OperationMode.MODE_IDandCARD_OR_CARDandFP.ToString();
                args[12] = isldev.clsDevParams.OperationMode.MODE_ID_AND_CARD_AND_FP.ToString();
                args[13] = isldev.clsDevParams.OperationMode.MODE_OPEN.ToString();
                args[14] = isldev.clsDevParams.OperationMode.MODE_CLOSED.ToString();
            }

            return args;
        }

        public string[] GetDoorMode()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DoorMode)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DoorMode)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetDoorModeWithoutNone()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DoorMode)).Length - 1];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DoorMode)))
            {
                if (!value.Equals("None"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetDoorForcedOpenMode()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DoorForcedOpenMode)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DoorForcedOpenMode)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetSummerTimeDifferenceTimeType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.SummerTimeDifferenceTimeType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.SummerTimeDifferenceTimeType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetHolidayType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.HolidayType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.HolidayType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetDuressType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DuressType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DuressType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetFunctionKey()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.FunctionKey)).Length - 1];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.FunctionKey)))
            {
                if (!value.Equals("None"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetInputApplicationType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.InputApplicationType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.InputApplicationType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetInputActiveType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.InputActiveType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.InputActiveType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetInputLineFault()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.InputLineFault)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.InputLineFault)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetOutputActiveType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.OutputActiveType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.OutputActiveType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetBiometricRescanningCount()
        {
            string[] args = new string[(int)isldev.clsDevParams.BiometricRescanningCount.Max + 1];

            for (int i = (int)isldev.clsDevParams.BiometricRescanningCount.Min; i <= (int)isldev.clsDevParams.BiometricRescanningCount.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetBiometricScanningLevel()
        {
            string[] args = new string[(int)isldev.clsDevParams.BiometricScanningLevel.Max + 1];

            for (int i = (int)isldev.clsDevParams.BiometricScanningLevel.Min; i <= (int)isldev.clsDevParams.BiometricScanningLevel.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetBiometricTemplateCount()
        {
            string[] args = new string[Enum.GetNames(typeof(islbio.clsBioParams.TemplateCount)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islbio.clsBioParams.TemplateCount)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetCallServerNetworkMode()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.CallServerNetworkMode)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.CallServerNetworkMode)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetLCDBackLightType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.LCDBackLightType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.LCDBackLightType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetLCDDateFormatType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.LCDDateFormatType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.LCDDateFormatType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetAlarmActiveType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.AlarmActiveType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.AlarmActiveType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetDeviceUserBinObject()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserBinObject)).Length - 1];

            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserBinObject)))
            {
                if (!value.Equals("None"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetDeviceUserObject(int controllerType, int userCommandType)
        {
            string[] args = null;

            if (controllerType >= (int)isldev.clsDevParams.ControllerType.ISC_101 && controllerType <= (int)isldev.clsDevParams.ControllerType.ISC_201A)
            {
                if (userCommandType != (int)UserCommand.UserDelete && userCommandType != (int)UserCommand.UserReset)
                {
                    // ISC & Not Delete, Reset
                    args = new string[5];

                    args[0] = isldev.clsDevCommand.DeviceUserObject.Info.ToString();
                    args[1] = isldev.clsDevCommand.DeviceUserObject.InfoName.ToString();
                    args[2] = isldev.clsDevCommand.DeviceUserObject.InfoCard.ToString();
                    args[3] = isldev.clsDevCommand.DeviceUserObject.InfoCardName.ToString();
                    args[4] = isldev.clsDevCommand.DeviceUserObject.InfoRestriction.ToString();
                }
                else
                {
                    // ISC & Delete, Reset
                    args = new string[2];

                    args[0] = isldev.clsDevCommand.DeviceUserDeleteObject.Card.ToString();
                    args[1] = isldev.clsDevCommand.DeviceUserDeleteObject.All.ToString();
                }                
            }
            else
            {
                if (userCommandType != (int)UserCommand.UserDelete && userCommandType != (int)UserCommand.UserReset)
                {
                    // Not ISC & Not Delete, Reset
                    args = new string[Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserObject)).Length - 1];
                    int i = 0;

                    foreach (string value in Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserObject)))
                    {
                        if (!value.Equals("None"))
                        {
                            args[i] = value;
                            i++;
                        }
                    }
                }
                else
                {
                    // Not ISC & Delete, Reset
                    args = new string[Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserDeleteObject)).Length - 1];
                    int i = 0;

                    foreach (string value in Enum.GetNames(typeof(isldev.clsDevCommand.DeviceUserDeleteObject)))
                    {
                        if (!value.Equals("None"))
                        {
                            args[i] = value;
                            i++;
                        }
                    }
                }
            }            

            return args;
        }

        public string[] GetProximityType()
        {
            string[] args = new string[Enum.GetNames(typeof(islprox.clsProxParams.ProximityType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islprox.clsProxParams.ProximityType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetProximityWiegand_E()
        {
            string[] args = new string[Enum.GetNames(typeof(islprox.clsProxParams.Proximity_E_Wiegand)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islprox.clsProxParams.Proximity_E_Wiegand)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetProximityWiegand_H()
        {
            string[] args = new string[Enum.GetNames(typeof(islprox.clsProxParams.Proximity_H_Wiegand)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islprox.clsProxParams.Proximity_H_Wiegand)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetProximityWiegand_M()
        {
            string[] args = new string[Enum.GetNames(typeof(islprox.clsProxParams.Proximity_M_Wiegand)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islprox.clsProxParams.Proximity_M_Wiegand)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetBiometricType()
        {
            string[] args = new string[Enum.GetNames(typeof(islbio.clsBioParams.TemplateType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islbio.clsBioParams.TemplateType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetBiometricTypeSub_S()
        {
            string[] args = new string[Enum.GetNames(typeof(islbio.clsBioParams.Template_S_SubType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islbio.clsBioParams.Template_S_SubType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetBiometricTypeSub_IDTi()
        {
            string[] args = new string[Enum.GetNames(typeof(islbio.clsBioParams.Template_IDTi_SubType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islbio.clsBioParams.Template_IDTi_SubType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }
        // 2016.02 edit
        public string[] GetBiometricTypeSub_JPx00()
        {
            string[] args = new string[Enum.GetNames(typeof(islbio.clsBioParams.Template_JPx00_SubType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(islbio.clsBioParams.Template_JPx00_SubType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public int GetUserLevelValue(int userLevel)
        {
            int retVal = (int)isldev.clsDevParams.UserLevel.User_31;

            int userMin = (int)isldev.clsDevParams.UserLevel.User_14;
            int userMax = (int)isldev.clsDevParams.UserLevel.User_31;
            int adminRMin = (int)isldev.clsDevParams.UserLevel.User_31 + 1;
            int adminRMax = (int)isldev.clsDevParams.UserLevel.AdminRegular;
            int adminAMin = (int)isldev.clsDevParams.UserLevel.AdminRegular + 1;
            int adminAMax = (int)isldev.clsDevParams.UserLevel.AdminAuthority;
            int adminSMin = (int)isldev.clsDevParams.UserLevel.AdminAuthority + 1;
            int adminSMax = (int)isldev.clsDevParams.UserLevel.AdminSystem;

            if (userLevel > userMin && userLevel <= userMax)
            {
                retVal = (int)isldev.clsDevParams.UserLevel.User_31;
            }
            else if (userLevel >= adminRMin && userLevel <= adminRMax)
            {
                retVal = (int)isldev.clsDevParams.UserLevel.AdminRegular;
            }
            else if (userLevel >= adminAMin && userLevel <= adminAMax)
            {
                retVal = (int)isldev.clsDevParams.UserLevel.AdminAuthority;
            }
            else if (userLevel >= adminSMin && userLevel <= adminSMax)
            {
                retVal = (int)isldev.clsDevParams.UserLevel.AdminSystem;
            }
            else
            {
                retVal = userLevel; 
            }

            return retVal;
        }

        public string GetProximityWiegandName(int proximityType, int proximityWiegand)
        {
            string retVal = "None";

            if (proximityType > 0)
            {

                switch (proximityType)
                {
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_E:
                        retVal = ((islprox.clsProxParams.Proximity_E_Wiegand)proximityWiegand).ToString();
                        break;
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_H:
                        retVal = ((islprox.clsProxParams.Proximity_H_Wiegand)proximityWiegand).ToString();
                        break;
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_M:
                        retVal = ((islprox.clsProxParams.Proximity_M_Wiegand)proximityWiegand).ToString();
                        break;
                    default:
                        break;
                }
            }

            return retVal;
        }

        public int GetProximityWiegandValue(string proximityTypeName, string proximityWiegandName)
        {
            int retVal = 0;

            if ((int)Enum.Parse(typeof(islprox.clsProxParams.ProximityType), proximityTypeName) > 0)
            {
                switch ((int)Enum.Parse(typeof(islprox.clsProxParams.ProximityType), proximityTypeName))
                {
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_E:
                        retVal = (int)Enum.Parse(typeof(islprox.clsProxParams.Proximity_E_Wiegand), proximityWiegandName);
                        break;
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_H:
                        retVal = (int)Enum.Parse(typeof(islprox.clsProxParams.Proximity_H_Wiegand), proximityWiegandName);
                        break;
                    case (int)islprox.clsProxParams.ProximityType.IntelliScan_M:
                        retVal = (int)Enum.Parse(typeof(islprox.clsProxParams.Proximity_M_Wiegand), proximityWiegandName);
                        break;
                    default:
                        break;
                }
            }

            return retVal;
        }

        public string GetBiometricSubTypeName(int biometricType, int biometricSubType)
        {
            string retVal = "None";

            if (biometricType > 0)
            {

                switch (biometricType)
                {
                    case (int)islbio.clsBioParams.TemplateType.BioScan_S:
                        retVal = ((islbio.clsBioParams.Template_S_SubType)biometricSubType).ToString();
                        break;
                    case (int)islbio.clsBioParams.TemplateType.BioScan_IDTi:
                        retVal = ((islbio.clsBioParams.Template_IDTi_SubType)biometricSubType).ToString();
                        break;
                    // 2016.01 edit
                    case (int)islbio.clsBioParams.TemplateType.BioScan_JPx00:
                        retVal = ((islbio.clsBioParams.Template_JPx00_SubType)biometricSubType).ToString();
                        break;
                    default:
                        break;
                }
            }

            return retVal;
        }        

        public int GetBiometricSubTypeValue(string biometricTypeName, string biometricSubTypeName)
        {
            int retVal = 0;

            if ((int)Enum.Parse(typeof(islbio.clsBioParams.TemplateType), biometricTypeName) > 0)
            {
                switch ((int)Enum.Parse(typeof(islbio.clsBioParams.TemplateType), biometricTypeName))
                {
                    case (int)islbio.clsBioParams.TemplateType.BioScan_S:
                        retVal = (int)Enum.Parse(typeof(islbio.clsBioParams.Template_S_SubType), biometricSubTypeName);
                        break;
                    case (int)islbio.clsBioParams.TemplateType.BioScan_IDTi:
                        retVal = (int)Enum.Parse(typeof(islbio.clsBioParams.Template_IDTi_SubType), biometricSubTypeName);
                        break;
                    // 2016.01 edit
                    case (int)islbio.clsBioParams.TemplateType.BioScan_JPx00:
                        retVal = (int)Enum.Parse(typeof(islbio.clsBioParams.Template_JPx00_SubType), biometricSubTypeName);
                        break;
                    default:
                        break;
                }
            }

            return retVal;
        }

        public int GetCommBPSName(int commBaudRateValue)
        {
            switch (commBaudRateValue)
            {
                case (int)isldev.clsDevParams.CommBaudRate.br4800:
                    return 4800;
                case (int)isldev.clsDevParams.CommBaudRate.br9600:
                    return 9600;
                case (int)isldev.clsDevParams.CommBaudRate.br19200:
                    return 19200;
                case (int)isldev.clsDevParams.CommBaudRate.br38400:
                    return 38400;
                case (int)isldev.clsDevParams.CommBaudRate.br57600:
                    return 57600;
                case (int)isldev.clsDevParams.CommBaudRate.br115200:
                    return 115200;
                default:
                    return 19200;
            }
        }

        public int GetCommBPSValue(int commBaudRateName)
        {
            switch (commBaudRateName)
            {
                case 4800:
                    return (int)isldev.clsDevParams.CommBaudRate.br4800;
                case 9600:
                    return (int)isldev.clsDevParams.CommBaudRate.br9600;
                case 19200:
                    return (int)isldev.clsDevParams.CommBaudRate.br19200;
                case 38400:
                    return (int)isldev.clsDevParams.CommBaudRate.br38400;
                case 57600:
                    return (int)isldev.clsDevParams.CommBaudRate.br57600;
                case 115200:
                    return (int)isldev.clsDevParams.CommBaudRate.br115200;
                default:
                    return 19200;
            }
        }

        public string[] GetUserAccessRestrictType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.UserAccessRestrictType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.UserAccessRestrictType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }       

        public string[] GetUserAccessRestrictDayLimitType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.UserAccessRestrictLimitType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.UserAccessRestrictLimitType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetUserAccessRestrictDayLimitCount()
        {
            string[] args = new string[(int)isldev.clsDevParams.UserAccessRestrictLimitCount.Max + 1];

            for (int i = (int)isldev.clsDevParams.UserAccessRestrictLimitCount.Min; i <= (int)isldev.clsDevParams.UserAccessRestrictLimitCount.Max; i += 1)
            {
                args[i] = i.ToString();
            }

            return args;
        }

        public string[] GetIOBoardTCPIPConnCount()
        {
            string[] args = new string[(int)isldev.clsDevParams.IOBoardTCPIPConnCount.Max - (int)isldev.clsDevParams.IOBoardTCPIPConnCount.Min + 1];

            for (int i = (int)isldev.clsDevParams.IOBoardTCPIPConnCount.Min; i <= (int)isldev.clsDevParams.IOBoardTCPIPConnCount.Max; i += 1)
            {
                args[i - (int)isldev.clsDevParams.IOBoardTCPIPConnCount.Min] = i.ToString();
            }

            return args;
        }

        public string[] GetUserImageFormat()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DevImageFormat)).Length - 1];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DevImageFormat)))
            {
                if (!value.Equals("Unknown"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

        public string[] GetUserImageRotatedType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DevImageRotatedType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DevImageRotatedType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }

        public string[] GetDeviceCommBinaryFrameSizeType()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.DevCommBinaryFrameSizeType)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.DevCommBinaryFrameSizeType)))
            {
                args[i] = value;
                i++;
            }

            return args;
        }
        // Brian
        public string[] GetLanSpeed()
        {
            string[] args = new string[Enum.GetNames(typeof(isldev.clsDevParams.ControllerLanSpeed)).Length];
            int i = 0;

            foreach (string value in Enum.GetNames(typeof(isldev.clsDevParams.ControllerLanSpeed)))
            {
                if (!value.Equals("None"))
                {
                    args[i] = value;
                    i++;
                }
            }

            return args;
        }

    }
}
