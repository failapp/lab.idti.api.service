using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Sdk
{
    public class clsProximity : BaseResource
    {

        public enum enumProximityISUReaderType {
            Nothing = 0,
            ISU_USBSingleReader_Mifare = 1,            // ps/2 Å¸ÀÔ, Mifare Only
            ISU_SerialMultipleReader_ISR100 = 2     // rs232 Å¸ÀÔ, Mifare, EM, HID
        }

        // Not Used
        //public enum enumISUSerialReaderSubType
        //{
        //    ISU_SerialReader_ISF_E,
        //    ISU_SerialReader_ISF_H,
        //    ISU_SerialReader_ISF_M
        //}

        public enum enumProximityType {
            Nothing = 0,
            IntelliScan_E = 1,
            IntelliScan_H = 2,
            IntelliScan_M = 3
        }

        public enum enumWiegandFormatEM {
            ISF_E_26BITSTANDARD = 1
        }

        public enum enumWiegandFormatHID {
            ISF_H_26BITSTANDARD = 1,
            ISF_H_26BITFULLBINARY = 2,
            ISF_H_34BITIDTi = 5,
            ISF_H_36BITSTX = 6,
            H_37bits = 3,               //Not Use
            H_Corporates1000 = 4        //Not Use
        }

        public enum enumWiegandFormatMifare
        {
            ISF_M_32BITUID = 1,
            ISF_M_32BITUIDFULLHEX = 9,
            ISF_M_34BITIDTi = 6,
            ISF_M_34BITIDTiREVERSE = 7,
            ISF_M_64BITUID = 8,
            ISF_M_64BITFULLHEX = 5,
            ISF_M_32BITDEC = 2,
            ISF_M_64BITFULLHEX_KLICENSE = 3,
            ISF_M_32BITUIDREVERSE = 10,
            ISF_M_32BITDECREVERSE = 11,
            //M_Data = 2,                 //Not Use
            //M_LG = 3,                   //Not Use
            M_WOORI_BANK = 4            //Not Use
        }

        private bool _disposed = false;

        public clsProximity()
        {
        }

        protected override void Dispose(bool isDisposing) {
            // ¿©·¯¹øÀÇ dispose¸¦ ¼öÇàÇÏÁö ¾Êµµ·Ï ÇÑ´Ù. 
            if (_disposed)
                return;

            if (isDisposing) {
                // ÇØ¾ßÇÒ ÀÏ : managed ¸®¼Ò½º¸¦ ÇØÁ¦ÇÑ´Ù.
            }
            // ÇØ¾ßÇÒ ÀÏ : unmanaged ¸®¼Ò½º¸¦ ÇØÁ¦ÇÑ´Ù. 

            // ±â¹Ý class°¡ ÀÚ½ÅÀÇ ¸®¼Ò½º¸¦ ÇØÁ¦ÇÒ¼ö ÀÖµµ·Ï ÇÑ´Ù. 
            // ±â¹Ý class´Â
            // GC.SuppressFinalize()¸¦ È£ÃâÇÒ Ã¥ÀÓÀÌ ÀÖµû. 
            base.Dispose(isDisposing);
            // ÇÏÀ§ classÀÇ disposeÇÃ·¡±×¸¦ ¼³¤¸¾îÇÑ´Ù. 
            _disposed = true;
        }




        /// ////////////////////////////////////////////
    }
}
