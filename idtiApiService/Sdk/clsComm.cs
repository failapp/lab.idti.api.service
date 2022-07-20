using System;
using System.Collections.Generic;
using System.Text;

namespace idtiApiService.Sdk
{
    class clsComm
    {
        private clsCommInfo CommInfo = new clsCommInfo();
        private byte[] bBuffer = null;

        public clsComm(clsCommInfo communicationInfo, byte[] bSendFrame)
        {
            this.CommInfo = communicationInfo;
            this.bBuffer = bSendFrame;
        }

        public byte[] ReceiveReturnFrameByte()
        {
            byte[] argb = null;
            //bool tcpconnectVal = false;

            switch (this.CommInfo.CommType)
            {
                case (int)clsGlobal.enumCommunicationIndex.TCPIP:
                    
                    if (this.CommInfo.CommDelay) { System.Threading.Thread.Sleep(4000); }

                    try
                    {

                        clsIndySock indySock = new clsIndySock();

                        //indySock.TcpReadTimeout = this.CommInfo.CommReadTimeout;
                        if (this.CommInfo.CommDelay) { indySock.TcpReadTimeout = this.CommInfo.CommReadTimeout + 4000; }
                        else { indySock.TcpReadTimeout = this.CommInfo.CommReadTimeout; }

                        indySock.TcpHost = this.CommInfo.CommTCPIPAddr;
                        indySock.TcpPort = this.CommInfo.CommTCPPort;
                        indySock.CreateTcpClient();
                        indySock.ConnectTcpClient();

                        //tcpconnectVal=indySock.ConnectTcpClient();
                        //if (tcpconnectVal == false)
                        //{
                        //    System.Threading.Thread.Sleep(4000); //GSM TEST
                        //    indySock.ConnectTcpClient();
                        //}

                        //System.Threading.Thread.Sleep(3000); //GSM TEST
                        System.Threading.Thread.Sleep(50); //GSM TEST
                        //System.Threading.Thread.Sleep(6000); //GSM TEST
                        indySock.WriteBuffer(this.bBuffer);
                        //System.Threading.Thread.Sleep(2000); //GSM TEST
                        //System.Threading.Thread.Sleep(4000); //GSM TEST
                        System.Threading.Thread.Sleep(200); //GSM TEST
                        argb = indySock.ReadBuffer();

                        indySock.DisconnectTcpClient();
                        indySock.DestroyTcpClient();

                        indySock = null;

                    } catch (Exception ex) {
                        Console.WriteLine("!!! INDY SOCKET ERROR !!!!");
                        Console.WriteLine(ex.Message);
                    }


                    
                    break;




                case (int)clsGlobal.enumCommunicationIndex.Serial:
                    clsSerialPort serialPort = new clsSerialPort();
                    serialPort.ReadTimeout = this.CommInfo.CommReadTimeout;
                    serialPort.PortName = this.CommInfo.CommSerialPort;
                    serialPort.BaudRate = this.CommInfo.CommSerialBPS;
                    serialPort.CreateComPort();
                    serialPort.OpenComPort();
                    serialPort.WriteBuffer(this.bBuffer);
                    //System.Threading.Thread.Sleep(300);
                    System.Threading.Thread.Sleep(550); // 2013.10.14 Brian 수정.
                    
                    argb = serialPort.ReadBuffer();

                    serialPort.CloseComPort();
                    serialPort.DestroyComPort();
                    serialPort = null;
                    break;
                default:
                    break;
            }

            return argb;
        }

        //private byte[] ReceiveReturnFrame(clsCommUnitInfo commInfo, byte[] bSendFrame)
        //{
        //    byte[] argb = null;

        //    this.commError = (int)clsCommunication.enumCommErrors.None;
        //    this.resposeTime = 0;
        //    int addWaitTime = 0;
        //    if (this.isEventBlockReceive) { addWaitTime = 0; }

        //    if (bSendFrame != null)
        //    {
        //        if (bSendFrame.Length > 0)
        //        {
        //            if (commInfo.NetType == (int)clsCommunication.enumCommType.Sock)
        //            {
        //                // Create TCP Socket
        //                clsIndySock objIndySock = this.CreateTCPSocket(commInfo);
        //                objIndySock.RequestFrameSize = objCommand.ReturnFrameLength; // 응답 패킷 사이즈를 부여함.                        

        //                // Connect TCP Socket
        //                bool connectResult = objIndySock.ConnectSock();

        //                if (connectResult)
        //                {
        //                    // Write (Send Bytes)
        //                    bool writeResult = objIndySock.WriteBuffer(bSendFrame);

        //                    if (writeResult)
        //                    {
        //                        //System.Threading.Thread.Sleep(124 + 30 + addWaitTime);

        //                        // Read (Receive Bytes)
        //                        long timeStamp = System.DateTime.Now.Ticks;
        //                        bool elapsedTime;
        //                        clsController objController = new clsController();

        //                        do
        //                        {
        //                            argb = objIndySock.ReadBuffer();
        //                            elapsedTime = System.DateTime.Now.Ticks - timeStamp > System.TimeSpan.TicksPerMillisecond * objController.AckDelay;

        //                        } while (!elapsedTime && argb == null);

        //                        if (argb != null)
        //                        {
        //                            this.resposeTime = objIndySock.ResponseTime;
        //                        }
        //                        else
        //                        {
        //                            this.commError = (int)clsCommunication.enumCommErrors.ReadError;
        //                        }

        //                        if (objController != null)
        //                        {
        //                            objController.Dispose();
        //                            objController = null;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        this.commError = (int)clsCommunication.enumCommErrors.WriteError;
        //                    }

        //                    // Disconnect TCP Socket
        //                    objIndySock.DisconnectSock();

        //                    // Remove TCP Socket
        //                    objIndySock.RemoveSock();
        //                    objIndySock.Dispose();
        //                    objIndySock = null;
        //                }
        //                else
        //                {
        //                    this.commError = (int)clsCommunication.enumCommErrors.ConnectError;
        //                }

        //            }
        //            else if (commInfo.NetType == (int)clsCommunication.enumCommType.Serial)
        //            {
        //                // Create Serial Port
        //                clsSerialPort objSerialPort = this.CreateSerialPort(commInfo);

        //                // Open Serial Port
        //                bool connectResult = objSerialPort.OpenPort();

        //                if (connectResult)
        //                {
        //                    // Write (Send Bytes)
        //                    bool writeResult = objSerialPort.WriteBuffer(bSendFrame);

        //                    if (writeResult)
        //                    {
        //                        //System.Threading.Thread.Sleep(124 + 30 + addWaitTime);

        //                        // Read (Receive Bytes)
        //                        long timeStamp = System.DateTime.Now.Ticks;
        //                        bool elapsedTime;
        //                        clsController objController = new clsController();

        //                        do
        //                        {
        //                            argb = objSerialPort.ReadBuffer();
        //                            elapsedTime = System.DateTime.Now.Ticks - timeStamp > System.TimeSpan.TicksPerMillisecond * objController.AckDelay;

        //                        } while (!elapsedTime && argb == null);

        //                        if (argb != null && argb.Length > 0)
        //                        {
        //                            this.resposeTime = objSerialPort.ResponseTime;
        //                        }
        //                        else
        //                        {
        //                            this.commError = (int)clsCommunication.enumCommErrors.ReadError;
        //                        }

        //                        if (objController != null)
        //                        {
        //                            objController.Dispose();
        //                            objController = null;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        this.commError = (int)clsCommunication.enumCommErrors.WriteError;
        //                    }

        //                    // Close Serial Port
        //                    objSerialPort.ClosePort();

        //                    // Remove Serial Port
        //                    objSerialPort.RemovePort();
        //                }
        //                else
        //                {
        //                    this.commError = (int)clsCommunication.enumCommErrors.ConnectError;
        //                }
        //            }
        //        }
        //    }

        //    return argb;
        //}
    }
}
