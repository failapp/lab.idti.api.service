using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Sdk
{

    public class clsELCardFormat
    {

        static clsELCardFormat instance = null;
        static readonly object syncRoot = new Object();

        private int cardReaderType = (int)clsProximity.enumProximityISUReaderType.ISU_USBSingleReader_Mifare;
        //private int cardReaderSubType = (int)clsProximity.enumISUSerialReaderSubType.ISU_SerialReader_ISF_E;          // Not Used

        private UInt64 cardID;
        private UInt64 cardFacility;

        private clsELCardFormat()
        {
            this.cardID = 0;
            this.cardFacility = 0;
        }

        public UInt64 CardID
        {
            get { return this.cardID; }
            set { this.cardID = value; }
        }

        public UInt64 CardFacility
        {
            get { return this.cardFacility; }
            set { this.cardFacility = value; }
        }

        public int CardReaderType
        {
            get { return this.cardReaderType; }
            set { this.cardReaderType = value; }
        }

        // Not Used
        //public int CardReaderSubType
        //{
        //    get { return this.cardReaderSubType; }
        //    set { this.cardReaderSubType = value; }
        //}

        public static clsELCardFormat Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new clsELCardFormat();
                    }
                }
                return instance;
            }
        }

        //-----------------------------------------------------------
        // Binary String -> Dec String
        //-----------------------------------------------------------
        public string BinaryToDec(string s)
        {
            string BinaryToDec = "";
            try
            {
                BinaryToDec = Convert.ToInt64(s, 2).ToString();
            }
            catch (Exception ex)
            {
                BinaryToDec = ex.Message.ToString();
            }
            return BinaryToDec;
        }


        //-----------------------------------------------------------
        // Dec String -> Binary String
        //-----------------------------------------------------------
        public string DecToBinary(string s, int nBitCnt)
        {
            string DecToBinary = string.Empty;
            string ZeroString = string.Empty;
            try
            {
                long decs = Int64.Parse(s);

                DecToBinary = Convert.ToString(decs, 2);

                int nLength = DecToBinary.Length;

                if (nLength < nBitCnt)
                {
                    for (int i = 0; i < (nBitCnt - nLength); i++)
                        ZeroString += "0";

                    DecToBinary = ZeroString + DecToBinary;
                }
            }
            catch (Exception ex)
            {
                DecToBinary = ex.Message.ToString();
            }
            return DecToBinary;
        }



        //-----------------------------------------------------------
        // Hex String -> Dec String
        //-----------------------------------------------------------
        public string HexToDec(string s)
        {
            string HexToDec = "";
            try
            {
                HexToDec = Convert.ToInt64(s, 16).ToString();
            }
            catch (Exception ex)
            {
                HexToDec = ex.Message.ToString();
            }
            return HexToDec;
        }


        //-----------------------------------------------------------
        // Dec String -> Hex String
        //-----------------------------------------------------------
        public string DecToHex(string s, int nBitCnt)
        {
            string DecToHex = string.Empty;
            string ZeroString = string.Empty;
            try
            {
                long decs = Int64.Parse(s);

                DecToHex = Convert.ToString(decs, 16);

                int nLength = DecToHex.Length;

                if (nLength < nBitCnt)
                {
                    for (int i = 0; i < (nBitCnt - nLength); i++)
                        ZeroString += "0";

                    DecToHex = ZeroString + DecToHex;
                }
            }
            catch (Exception ex)
            {
                DecToHex = ex.Message.ToString();
            }
            return DecToHex;
        }

        public byte[] MakeCardReaderDataToByte(int proximityType, int proximityMapType, UInt64 cardid, UInt64 cardfacility)
        {
            byte[] nCardID = null;
            ICardFormat cardformat = null;
            switch (proximityType)
            {
                case (int)clsProximity.enumProximityType.IntelliScan_E:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatEM.ISF_E_26BITSTANDARD:
                            cardformat = new clsEM26BitsStandard(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_H:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITSTANDARD:
                            cardformat = new clsHID26BitsStandard(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITFULLBINARY:
                            cardformat = new clsHID26BItsFullBynary(cardid, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_34BITIDTi:
                            cardformat = new clsHID34BitsIDTi(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_36BITSTX:
                            cardformat = new clsHID36BitsSTX(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_M:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTi:
                            cardformat = new clsMifareiClass34Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTiREVERSE:
                            cardformat = new clsMifareiClass34BitsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITUID:
                            cardformat = new clsMifareUID64Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUID:
                            cardformat = new clsMifareUID32Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDREVERSE:
                            cardformat = new clsMifareUID32BitsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDFULLHEX:
                            cardformat = new clsMifareUID32BitsFullHex(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX:
                            cardformat = new clsMifareIDTi071(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDEC:
                            cardformat = new clsMifare32bitDecimal(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDECREVERSE:
                            cardformat = new clsMifare32bitDecimalsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX_KLICENSE:
                            cardformat = new clsMifare64bitFullHexKoreaLicense(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.Nothing:
                default:
                    break;
            }

            if (cardformat != null)
            {
                nCardID = cardformat.CardData;
            }

            return nCardID;
        }

        public string MakeCardReaderDataToString(int proximityType, int proximityMapType, UInt64 cardid, UInt64 cardfacility)
        {
            string strCardData = null;
            ICardFormat cardformat = null;
            switch (proximityType)
            {
                case (int)clsProximity.enumProximityType.IntelliScan_E:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatEM.ISF_E_26BITSTANDARD:
                            cardformat = new clsEM26BitsStandard(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_H:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITSTANDARD:
                            cardformat = new clsHID26BitsStandard(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITFULLBINARY:
                            cardformat = new clsHID26BItsFullBynary(cardid, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_34BITIDTi:
                            cardformat = new clsHID34BitsIDTi(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_36BITSTX:
                            cardformat = new clsHID36BitsSTX(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_M:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTi:
                            cardformat = new clsMifareiClass34Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTiREVERSE:
                            cardformat = new clsMifareiClass34BitsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITUID:
                            cardformat = new clsMifareUID64Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUID:
                            cardformat = new clsMifareUID32Bits(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDREVERSE:
                            cardformat = new clsMifareUID32BitsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDFULLHEX:
                            cardformat = new clsMifareUID32BitsFullHex(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX:
                            cardformat = new clsMifareIDTi071(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDEC:
                            cardformat = new clsMifare32bitDecimal(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDECREVERSE:
                            cardformat = new clsMifare32bitDecimalsReverse(cardid, cardfacility, true);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX_KLICENSE:
                            cardformat = new clsMifare64bitFullHexKoreaLicense(cardid, cardfacility, true);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.Nothing:
                default:
                    break;
            }

            if (cardformat != null)
            {
                strCardData = cardformat.StrCardData;
            }

            return strCardData;
        }

        public string MakeCardData(int proximityType, int proximityMapType, UInt64 cardid, UInt64 cardfacility)
        {
            string strCardID = string.Empty;
            ICardFormat cardformat = null;
            switch (proximityType)
            {
                case (int)clsProximity.enumProximityType.IntelliScan_E:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatEM.ISF_E_26BITSTANDARD:
                            cardformat = new clsEM26BitsStandard(cardid, cardfacility, false);
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_H:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITSTANDARD:
                            cardformat = new clsHID26BitsStandard(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITFULLBINARY:
                            cardformat = new clsHID26BItsFullBynary(cardid, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_34BITIDTi:
                            cardformat = new clsHID34BitsIDTi(cardid, cardfacility, false);
                            break;
                        //case (int)clsProximity.enumWiegandFormatHID.ISF_H_36BITSTX:
                        //cardformat = new clsHID36BitsSTX(cardid, cardfacility, false);
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_37bits:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_Corporates1000:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_M:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTi:
                            cardformat = new clsMifareiClass34Bits(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTiREVERSE:
                            cardformat = new clsMifareiClass34BitsReverse(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITUID:
                            cardformat = new clsMifareUID64Bits(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUID:
                            cardformat = new clsMifareUID32Bits(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDREVERSE:
                            cardformat = new clsMifareUID32BitsReverse(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDFULLHEX:
                            cardformat = new clsMifareUID32BitsFullHex(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX:
                            cardformat = new clsMifareIDTi071(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDEC:
                            cardformat = new clsMifare32bitDecimal(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDECREVERSE:
                            cardformat = new clsMifare32bitDecimalsReverse(cardid, cardfacility, false);
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX_KLICENSE:
                            cardformat = new clsMifare64bitFullHexKoreaLicense(cardid, cardfacility, false);
                            break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_Data:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_WOORI_BANK:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_LG:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.Nothing:
                default:
                    break;
            }

            if (cardformat != null)
            {
                strCardID = cardformat.StrCardData;
            }
            return strCardID;
        }

        public bool AnalCardData(int proximityType, int proximityMapType, byte[] data)
        {
            ICardFormat cardformat = null;

            switch (proximityType)
            {
                case (int)clsProximity.enumProximityType.IntelliScan_E:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatEM.ISF_E_26BITSTANDARD:
                            cardformat = new clsEM26BitsStandard();
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_H:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITSTANDARD:
                            cardformat = new clsHID26BitsStandard();
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITFULLBINARY:
                            cardformat = new clsHID26BItsFullBynary();
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_34BITIDTi:
                            cardformat = new clsHID34BitsIDTi();
                            break;
                        //case (int)clsProximity.enumWiegandFormatHID.ISF_H_36BITSTX:
                        //cardformat = new clsHID36BitsSTX();
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_37bits:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_Corporates1000:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_M:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTi:
                            cardformat = new clsMifareiClass34Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTiREVERSE:
                            cardformat = new clsMifareiClass34BitsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITUID:
                            cardformat = new clsMifareUID64Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUID:
                            cardformat = new clsMifareUID32Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDREVERSE:
                            cardformat = new clsMifareUID32BitsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDFULLHEX:
                            cardformat = new clsMifareUID32BitsFullHex();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX:
                            cardformat = new clsMifareIDTi071();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDEC:
                            cardformat = new clsMifare32bitDecimal();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDECREVERSE:
                            cardformat = new clsMifare32bitDecimalsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX_KLICENSE:
                            cardformat = new clsMifare64bitFullHexKoreaLicense();
                            break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_Data:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_WOORI_BANK:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_LG:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.Nothing:
                default:
                    break;
            }

            if (cardformat != null)
            {
                if (cardformat.AnalCardData(data))
                {
                    this.CardID = cardformat.CardID;
                    this.CardFacility = cardformat.CardFacility;
                    return true;
                }
            }
            return false;
        }

        public bool AnalCardReaderData(int proximityType, int proximityMapType, byte[] data)
        {
            ICardFormat cardformat = null;

            switch (proximityType)
            {
                case (int)clsProximity.enumProximityType.IntelliScan_E:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatEM.ISF_E_26BITSTANDARD:
                            cardformat = new clsEM26BitsStandard();
                            break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_H:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITSTANDARD:
                            cardformat = new clsHID26BitsStandard();
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_26BITFULLBINARY:
                            cardformat = new clsHID26BItsFullBynary();
                            break;
                        case (int)clsProximity.enumWiegandFormatHID.ISF_H_34BITIDTi:
                            cardformat = new clsHID34BitsIDTi();
                            break;
                        //case (int)clsProximity.enumWiegandFormatHID.ISF_H_36BITSTX:
                        //cardformat = new clsHID36BitsSTX();
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_37bits:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatHID.H_Corporates1000:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.IntelliScan_M:
                    switch (proximityMapType)
                    {
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTi:
                            cardformat = new clsMifareiClass34Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_34BITIDTiREVERSE:
                            cardformat = new clsMifareiClass34BitsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITUID:
                            cardformat = new clsMifareUID64Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUID:
                            cardformat = new clsMifareUID32Bits();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDREVERSE:
                            cardformat = new clsMifareUID32BitsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITUIDFULLHEX:
                            cardformat = new clsMifareUID32BitsFullHex();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX:
                            cardformat = new clsMifareIDTi071();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDEC:
                            cardformat = new clsMifare32bitDecimal();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_32BITDECREVERSE:
                            cardformat = new clsMifare32bitDecimalsReverse();
                            break;
                        case (int)clsProximity.enumWiegandFormatMifare.ISF_M_64BITFULLHEX_KLICENSE:
                            cardformat = new clsMifare64bitFullHexKoreaLicense();
                            break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_Data:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_WOORI_BANK:
                        //break;
                        //case (int)clsProximity.enumWiegandFormatMifare.M_LG:
                        //break;
                        default:
                            break;
                    }
                    break;
                case (int)clsProximity.enumProximityType.Nothing:
                default:
                    break;
            }

            if (cardformat != null)
            {
                cardformat.AnalCardReaderData(data);
                this.CardID = cardformat.CardID;
                this.CardFacility = cardformat.CardFacility;
                return true;
            }

            return false;
        }
    }

    public enum enumBit
    {
        NonCheckBit,
        CheckBit
    }

    //=========================================================================
    // Name    : ICardFormat
    //
    // History : 2008.04.30 created by david
    //
    // Des     : Ä«µå Å¸ÀÔ ÇÁ·ÎÅäÅ¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    interface ICardFormat
    {
        UInt64 CardID
        {
            get;
            set;
        }

        UInt64 CardFacility
        {
            get;
            set;
        }

        byte[] CardData
        {
            get;
            set;
        }

        string StrCardData
        {
            get;
            set;
        }

        bool AnalCardData(byte[] data);
        bool AnalCardReaderData(byte[] data);
        void GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility);
        void GetCardData(out byte[] data);
    }

    //=========================================================================
    // Name    : clsEM26BitsStandard : ICardFormat
    //
    // History : 2008.04.30 created by david
    //
    // Des     : EM 26Bits Ä«µå Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsEM26BitsStandard : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 16.0;
        private double max_bit_cardfacility = 8.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardfacility) || value < 0)
                {
                    this._cardfacility = 0;
                    //throw new ArgumentException("cardfacility value is not correct");
                }
                else
                    this._cardfacility = value;
            }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsEM26BitsStandard()
        {
        }

        public clsEM26BitsStandard(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;
            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function
        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;

            objFacility.Dispose();
            objFacility = null;
            objCardID.Dispose();
            objCardID = null;
            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 4°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility + strCardID.Substring(0, 4);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardID.Substring(4, 12);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;

            objFacility.Dispose();
            objFacility = null;
            objCardID.Dispose();
            objCardID = null;
            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 4°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility + strCardID.Substring(0, 4);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardID.Substring(4, 12);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 40 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[5];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
            }

            clsConvertNumericType objFacility =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit)));
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit)));

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit))));
            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit))));
            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : string data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objFacility =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit)));
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit)));

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(15, Convert.ToInt32(this.MaxCardFacilityBit))));
            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(23, Convert.ToInt32(this.MaxCardIDBit))));
            return true;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }


        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }
        #endregion
    }


    //=========================================================================
    // Name    : clsHID26BitsStandard
    //
    // History : 2008.01.15 created by david - clsHID26Bits »ó¼Ó ¹æ½Ä
    //           2008.04.30 modified by david - Interface¹æ½ÄÀ¸·Î º¯°æ
    //
    // Des     : 
    //
    //           ºí·° ±¸¼º º¸´Â¹ý
    //              | ½ÇÁ¦ Ä«µå µ¥ÀÌÅÍ ¿µ¿ª |
    //              { Card Facility & Card ID ±¸ºÐ}
    //              [ Lowbit & Highbit ]
    //              / byte ´ÜÀ§ ºí·° /
    //
    //           00000000 / 000000 |0[{0/0000000}{0/000][00000/0000000}]0|
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsHID26BitsStandard : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 16.0;
        private double max_bit_cardfacility = 8.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardfacility) || value < 0)
                {
                    this._cardfacility = 0;
                    //throw new ArgumentException("cardfacility value is not correct");
                }
                else
                    this._cardfacility = value;
            }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsHID26BitsStandard()
        {
        }

        public clsHID26BitsStandard(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;
            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function
        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;

            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 4°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility + strCardID.Substring(0, 4);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardID.Substring(4, 12);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }


        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;

            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 4°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility + strCardID.Substring(0, 4);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardID.Substring(4, 12);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 40 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[5];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objFacility =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit)));
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit)));

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;

            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit))));
            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit))));
            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objFacility =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData.Substring(39, Convert.ToInt32(this.MaxCardFacilityBit)));
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData.Substring(47, Convert.ToInt32(this.MaxCardIDBit)));

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;

            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(15, Convert.ToInt32(this.MaxCardFacilityBit))));
            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(23, Convert.ToInt32(this.MaxCardIDBit))));
            return true;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }


        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsHID26BItsFullBynary
    //
    // History : 2008.01.21 created by david
    //         : 2008. 4.30 modified by david
    //
    // Des     : 
    //
    //           ºí·° ±¸¼º º¸´Â¹ý
    //              | ½ÇÁ¦ Ä«µå µ¥ÀÌÅÍ ¿µ¿ª |
    //              { Card Facility & Card ID ±¸ºÐ}
    //              [ Lowbit & Highbit ]
    //              / byte ´ÜÀ§ ºí·° /
    //
    //           00000000 / 000000{00 / 00000000 / 00000000 / 00000000}
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsHID26BItsFullBynary : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 26.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsHID26BItsFullBynary()
        {
        }

        public clsHID26BItsFullBynary(UInt64 cardid, bool bReader)
        {
            this.CardID = cardid;
            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //1. CardID¸¦ 40ÀÚ¸®¼öÀÇ 2Áø¼ö·Î º¯ÇüÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //2. 8°³´ÜÀ§·Î ³ª´©¾î¼­ CardData¿¡ ÀúÀåÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //1. CardID¸¦ 40ÀÚ¸®¼öÀÇ 2Áø¼ö·Î º¯ÇüÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            if (strCardID.Length > 40)
            {
                strCardID = strCardID.Substring(strCardID.Length - 40, 40);
            }

            this._carddata = new byte[5];

            //2. 8°³´ÜÀ§·Î ³ª´©¾î¼­ CardData¿¡ ÀúÀåÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));
            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));
            return true;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }


        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsHID34BitsIDTi
    //
    // History : 2008.01.22 created by david
    //           2008.03.11 modified by david - Ä«µå Å¸ÀÔ ¹Ì¼÷Áö·Î ÀÎÇÑ µ¥ÀÌÅÍ »ý¼º ¿À·ù ¼öÁ¤
    //           2008. 4.30 modified by david - interface ICardFormat¸¦ ÀÌ¿ë
    //
    // Des     : 34Bits IDTiÄ«µå Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsHID34BitsIDTi : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 16.0;
        private double max_bit_cardfacility = 16.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardfacility) || value < 0)
                {
                    this._cardfacility = 0;
                    //throw new ArgumentException("cardfacility value is not correct");
                }
                else
                    this._cardfacility = value;
            }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsHID34BitsIDTi()
        {
        }

        public clsHID34BitsIDTi(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;
            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function
        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility »óÀ§ 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 8°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility.Substring(0, 8) + strCardID.Substring(0, 8);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardFacility.Substring(8, 8) + strCardID.Substring(8, 8);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;
            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //Facility »óÀ§ 8°³ ºñÆ® ¿Í CardIDÀÇ »óÀ§ 8°³ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardFacility.Substring(0, 8) + strCardID.Substring(0, 8);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 12 ºñÆ® Ãëµæ
            string strLowBit = strCardFacility.Substring(8, 8) + strCardID.Substring(8, 8);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 40 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[5];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            string strCardID = strCardData.Substring(39, 8) + strCardData.Substring(55, 8);
            string strCardFacility = strCardData.Substring(31, 8) + strCardData.Substring(47, 8);

            clsConvertNumericType objFacility =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardFacility);
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardID));
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardFacility));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            string strCardID = strCardData.Substring(39, 8) + strCardData.Substring(55, 8);
            string strCardFacility = strCardData.Substring(31, 8) + strCardData.Substring(47, 8);

            clsConvertNumericType objFacility =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardFacility);
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;


            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardID));
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardFacility));
            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }


        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsHID36BitsSTX
    //
    // History : 2008.01.15 created by david
    //           2008. 4.30 modified by david
    //
    // Des     : 
    //
    //           36bits STX type Ä«µå´Â 36bits·Î ±¸¼ºµÇ¾î ÀÖÀ½.
    //           even parity 1bit + card facility 16 bit + card id 18 bit + odd parity 1bit = 36bit·Î ±¸¼º µÇ¾î ÀÖÀ½.
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsHID36BitsSTX : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 18.0;
        private double max_bit_cardfacility = 16.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = value;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardfacility) || value < 0)
                {
                    this._cardfacility = 0;
                    //throw new ArgumentException("cardfacility value is not correct");
                }
                else
                    this._cardfacility = value;
            }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsHID36BitsSTX()
        {
        }

        public clsHID36BitsSTX(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue.Substring(14, (int)max_bit_cardid);
            objCardID.Dispose();
            objCardID = null;

            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //CardID »óÀ§ 8°³ ºñÆ® + CardFacilityÀÇ »óÀ§ 8°³ ºñÆ® + CardIDÀÇ 9¹øÂ° ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardID.Substring(0, 8) + strCardFacility.Substring(0, 8) + strCardID.Substring(8, 1);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 9°³ºñÆ® + CardFacilityÀÇ ÇÏÀ§ 8 ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strLowBit = strCardID.Substring(9, 9) + strCardFacility.Substring(8, 8);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            // CardFacility¿Í CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objFacility =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType16,
                this.CardFacility.ToString());
            string strCardFacility = objFacility.BinaryValue;
            objFacility.Dispose();
            objFacility = null;

            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue.Substring(14, (int)max_bit_cardid);
            objCardID.Dispose();
            objCardID = null;

            //string strCardFacility = clsELCardFormat.Instance.DecToBinary(this.CardFacility.ToString(), Convert.ToInt32(this.MaxCardFacilityBit));
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            //1. »óÀ§ºñÆ®
            //CardID »óÀ§ 8°³ ºñÆ® + CardFacilityÀÇ »óÀ§ 8°³ ºñÆ® + CardIDÀÇ 9¹øÂ° ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strHighBit = strCardID.Substring(0, 8) + strCardFacility.Substring(0, 8) + strCardID.Substring(8, 1);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 9°³ºñÆ® + CardFacilityÀÇ ÇÏÀ§ 8 ºñÆ®¸¦ ÇÕÄ£´Ù.
            string strLowBit = strCardID.Substring(9, 9) + strCardFacility.Substring(8, 8);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();
            int nZeroCnt = 40 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            this._carddata = new byte[5];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                    data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            string strCardID = strCardData.Substring(29, 8) + strCardData.Substring(45, 10);
            string strCardFacility = strCardData.Substring(37, 8) + strCardData.Substring(55, 8);

            clsConvertNumericType objFacility =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardFacility);
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardID));
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardFacility));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            string strCardID = strCardData.Substring(29, 8) + strCardData.Substring(45, 10);
            string strCardFacility = strCardData.Substring(37, 8) + strCardData.Substring(55, 8);

            clsConvertNumericType objFacility =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardFacility);
            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardID);

            this.CardFacility = Convert.ToUInt64(objFacility.DecimalValue);
            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objFacility.Dispose();
            objFacility = null;
            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardID));
            //this.CardFacility = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardFacility));

            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }


        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareUID32Bits
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare UID 32Bits Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareUID32Bits : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 32.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareUID32Bits()
        {
        }

        public clsMifareUID32Bits(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 32 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[4];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 4; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }
        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += this.Reverse(clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8));
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareUID32BitsReverse
    //
    // History : 2009. 11. 20 created by david
    //
    // Des     : Mifare UID 32Bits Reverse Å¸ÀÔ
    //
    // (c) 2009. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareUID32BitsReverse : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 32.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareUID32BitsReverse()
        {
        }

        public clsMifareUID32BitsReverse(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";

            string tempCardID = strZero + strCardID;
            strCardID = string.Empty;
            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    strCardID += tempCardID.Substring(i * 8, 8);
                }
                else
                    strCardID = "00000000" + strCardID;
            }

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 32 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            string tempCardID = strZero + strCardID;
            strCardID = string.Empty;
            for (int i = 3; i >= 0; i--)
            {
                strCardID += tempCardID.Substring(i * 8, 8);
            }

            this._carddata = new byte[4];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ Reverse ÇÑ°ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 4; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(this.Reverse(objCardData.DecimalValue));
                objCardData.Dispose();
                objCardData = null;
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }
        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    clsConvertNumericType objPartCardData =
                       new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                       data[i].ToString());
                    strCardData += objPartCardData.BinaryValue;
                    objPartCardData.Dispose();
                    objPartCardData = null;
                }
                else
                    strCardData = "00000000" + strCardData;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    clsConvertNumericType objPartCardData =
                      new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                      data[i].ToString());

                    strCardData += this.Reverse(objPartCardData.BinaryValue);
                    objPartCardData.Dispose();
                    objPartCardData = null;
                }
                else
                    strCardData = "00000000" + strCardData;

                //strCardData += this.Reverse(clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8));
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareUID32BitsFullHex
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare UID 32Bits FullHex Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareUID32BitsFullHex : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 32.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareUID32BitsFullHex()
        {
        }

        public clsMifareUID32BitsFullHex(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 32 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[4];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 4; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += this.Reverse(clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8));
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareiClass34Bits
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare iClass 34 Bits Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareiClass34Bits : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 34.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = value;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareiClass34Bits()
        {
        }

        public clsMifareiClass34Bits(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;
            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue.Substring(30, (int)this.max_bit_cardid);
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 32);
            objCardID.Dispose();
            objCardID = null;

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue.Substring(30, (int)this.max_bit_cardid);
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 32);
            objCardID.Dispose();
            objCardID = null;

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[5];

            //6. 5Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;
            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }


        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardID = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardID += this.Reverse(objPartCardData.BinaryValue);
                //strCardID += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardID += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            //1. »óÀ§ºñÆ®
            //CardID »óÀ§ 16°³ ºñÆ®
            string strHighBit = strCardID.Substring(32, 16);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //  - CardID ÇÏÀ§ 16°³ºñÆ®
            string strLowBit = strCardID.Substring(48, 16);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();

            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);
            clsConvertNumericType objCardID =
                 new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                 strCardData);

            this.CardID = Convert.ToUInt64(objCardID.DecimalValue);
            objCardID.Dispose();
            objCardID = null;
            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareiClass34BitsReverse iclass
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare iClass 34Bits Reverse Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareiClass34BitsReverse : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 34.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareiClass34BitsReverse()
        {
        }

        public clsMifareiClass34BitsReverse(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue.Substring(30, (int)this.max_bit_cardid);

            objCardID.Dispose();
            objCardID = null;

            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 32);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue.Substring(30, (int)this.max_bit_cardid);

            objCardID.Dispose();
            objCardID = null;

            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 32);

            int nZeroCnt = 40 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[5];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 5; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }


        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardID = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardID += this.Reverse(objPartCardData.BinaryValue);
                //strCardID += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardID += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            //1. »óÀ§ºñÆ®
            //CardID »óÀ§ 16°³ºñÆ®
            string strHighBit = strCardID.Substring(56, 8) + strCardID.Substring(48, 8);

            //2. ÇÏÀ§ ºñÆ® »ý¼º
            //CardID ÇÏÀ§ 16°³ºñÆ®
            string strLowBit = strCardID.Substring(40, 8) + strCardID.Substring(32, 8);

            //3. EvenParity°ª ¼³Á¤ : È¦¼ö ÀÏ°æ¿ì Even Parity °ª ¼³Á¤
            if (this.GetBitCount(strHighBit) % 2 != 0)
                this.EvenParity = (byte)enumBit.CheckBit;
            else
                this.EvenParity = (byte)enumBit.NonCheckBit;

            //4. OddParity°ª ¼³Á¤ : Â¦¼ö ÀÏ°æ¿ì Odd Parity °ª ¼³Á¤
            if (this.GetBitCount(strLowBit) % 2 == 0)
                this.OddParity = (byte)enumBit.CheckBit;
            else
                this.OddParity = (byte)enumBit.NonCheckBit;

            //5. ½ÇÁ¦ »ç¿ëµÉ CardData »ý¼º
            string strCardData = this.EvenParity.ToString() + strHighBit + strLowBit + this.OddParity.ToString();

            int nZeroCnt = 64 - strCardData.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardData = strZero + strCardData;

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardData.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardData.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardData);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardData), 16);

            clsConvertNumericType objCardID =
                 new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                 strCardData);

            this.CardID = Convert.ToUInt64(objCardID.DecimalValue);
            objCardID.Dispose();
            objCardID = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareUID64Bits iclass
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare UID 64Bits Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareUID64Bits : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 64.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardfacility value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareUID64Bits()
        {
        }

        public clsMifareUID64Bits(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;

            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;

            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }


        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += this.Reverse(clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8).ToString());
            }

            clsConvertNumericType objCardID =
                 new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                 strCardData);

            this.CardID = Convert.ToUInt64(objCardID.DecimalValue);
            objCardID.Dispose();
            objCardID = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));
            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareIDTi071 iclass
    //
    // History : 2008. 8. 8 created by david
    //
    // Des     : Mifare IDTi 071Å¸ÀÔ
    //           Ä«µåÇí»ç°ªÀÌ ±×´ë·Î »ç¿ëµÊ.
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifareIDTi071 : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 64.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifareIDTi071()
        {
        }

        public clsMifareIDTi071(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }


        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;


                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardID =
                 new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                 strCardData);

            this.CardID = Convert.ToUInt64(objCardID.DecimalValue);
            objCardID.Dispose();
            objCardID = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));
            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }

    //=========================================================================
    // Name    : clsMifareUID32Bits
    //
    // History : 2008. 4. 30 created by david
    //
    // Des     : Mifare UID 32Bits Å¸ÀÔ
    //
    // (c) 2008. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifare32bitDecimal : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 32.0;
        private double max_bit_cardfacility = 0.0;
        #endregion //Declaration

        #region Constructor
        public clsMifare32bitDecimal()
        {
        }

        public clsMifare32bitDecimal(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion //Constructor

        #region Properity
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion //Properity

        #region Member Function
        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());

            string strCardID = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                strCardID += this.Reverse(objCardID.BinaryValue.PadLeft(64, '0').Substring(i * 8, 8));
            }

            objCardID.Dispose();
            objCardID = null;

            /*string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;*/
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 32 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[4];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 4; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }
        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += this.Reverse(clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8));
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion //Member Function
    }

    //=========================================================================
    // Name    : clsMifare32bitDecimalsReverse
    //
    // History : 2009. 11. 20 created by david
    //
    // Des     : Mifare UID 32Bits Decimal Reverse Å¸ÀÔ
    //
    // (c) 2009. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifare32bitDecimalsReverse : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 32.0;
        private double max_bit_cardfacility = 0.0;
        #endregion //Declaration

        #region Constructor
        public clsMifare32bitDecimalsReverse()
        {
        }

        public clsMifare32bitDecimalsReverse(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion //Constructor

        #region Properity
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion //Properity

        #region Member Function
        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());

            string strCardID = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                strCardID += this.Reverse(objCardID.BinaryValue.PadLeft(64, '0').Substring(i * 8, 8));
            }

            objCardID.Dispose();
            objCardID = null;

            /*string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;*/
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            string tempCardID = strZero + strCardID;
            strCardID = string.Empty;
            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    strCardID += tempCardID.Substring(i * 8, 8);
                }
                else
                    strCardID = "00000000" + strCardID;
            }

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                      new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                      strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
                new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType32,
                this.CardID.ToString());
            string strCardID = objCardID.BinaryValue;
            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), Convert.ToInt32(this.MaxCardIDBit));

            int nZeroCnt = 32 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            string tempCardID = strZero + strCardID;
            strCardID = string.Empty;
            for (int i = 3; i >= 0; i--)
            {
                strCardID += tempCardID.Substring(i * 8, 8);
            }

            this._carddata = new byte[4];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 4; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    clsConvertNumericType objPartCardData =
                       new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                       data[i].ToString());
                    strCardData += this.Reverse(objPartCardData.BinaryValue);
                    //strCardData += objPartCardData.BinaryValue;
                    objPartCardData.Dispose();
                    objPartCardData = null;
                }
                else
                    strCardData = "00000000" + strCardData;
                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 7; i >= 0; i--)
            {
                if (i >= 4)
                {
                    clsConvertNumericType objPartCardData =
                      new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                      data[i].ToString());

                    strCardData += objPartCardData.BinaryValue;
                    objPartCardData.Dispose();
                    objPartCardData = null;
                }
                else
                {
                    strCardData = "00000000" + strCardData;
                }
            }

            clsConvertNumericType objCardData =
                    new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                    strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 4. 30 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion //Member Function
    }

    //=========================================================================
    // Name    : clsMifare64bitFullHex - Korea License iclass
    //
    // History : 2009. 10. 27 created by david
    //
    // Des     : Mifare 64bit Ç®ÇÙ»ç ¹öÀü2 - ÇÑ±¹ ÁÖ¹Îµî·Ï¹øÈ£
    //           Ä«µåÇí»ç°ªÀÌ ±×´ë·Î »ç¿ëµÊ.
    //
    // (c) 2009. IDTi Corproation, Inc.  All Rights Reserved
    //=========================================================================
    class clsMifare64bitFullHexKoreaLicense : ICardFormat
    {
        #region Declaration
        private UInt64 _cardid;
        private UInt64 _cardfacility;
        private byte[] _carddata = null;
        private string _strcarddata = string.Empty;

        private byte _evenparity;
        private byte _oddparity;
        private double max_bit_cardid = 64.0;
        private double max_bit_cardfacility = 0.0;
        #endregion

        #region Property
        public UInt64 CardID
        {
            get { return this._cardid; }
            set
            {
                if (value > Math.Pow(2.0, max_bit_cardid) || value < 0)
                {
                    this._cardid = 0;
                    //throw new ArgumentException("cardid value is not correct");
                }
                else
                    this._cardid = value;
            }
        }

        public UInt64 CardFacility
        {
            get { return this._cardfacility; }
            set { this._cardfacility = value; }
        }

        public string StrCardData
        {
            get { return this._strcarddata; }
            set { this._strcarddata = value; }
        }

        public byte[] CardData
        {
            get { return this._carddata; }
            set { this._carddata = value; }
        }

        private byte EvenParity
        {
            get { return this._evenparity; }
            set { this._evenparity = value; }
        }

        private byte OddParity
        {
            get { return this._oddparity; }
            set { this._oddparity = value; }
        }

        private double MaxCardIDBit
        {
            get { return this.max_bit_cardid; }
        }

        private double MaxCardFacilityBit
        {
            get { return this.max_bit_cardfacility; }
        }
        #endregion

        #region Constructor
        public clsMifare64bitFullHexKoreaLicense()
        {
        }

        public clsMifare64bitFullHexKoreaLicense(UInt64 cardid, UInt64 cardfacility, bool bReader)
        {
            this.CardID = cardid;
            this.CardFacility = cardfacility;

            if (bReader)
                this.MakeCardReaderData();
            else
                this.MakeCardData();
        }
        #endregion

        #region Member Function

        //===================================================================
        // Function name   : MakeCardData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private void MakeCardData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : MakeCardReaderData
        // Description     : Card Data Create
        // Return type     : void 
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private void MakeCardReaderData()
        {
            //CardID¸¦ 2Áø¼ö·Î º¯È¯ÇÑ´Ù.
            clsConvertNumericType objCardID =
               new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
               this.CardID.ToString());

            string strCardID = objCardID.BinaryValue;

            objCardID.Dispose();
            objCardID = null;
            //string strCardID = clsELCardFormat.Instance.DecToBinary(this.CardID.ToString(), 64);

            int nZeroCnt = 64 - strCardID.Length;
            string strZero = string.Empty;
            for (int i = 0; i < nZeroCnt; i++)
                strZero += "0";
            strCardID = strZero + strCardID;

            this._carddata = new byte[8];

            //6. 8Á¶°¢À¸·Î Àß¶ó¼­ CardData¿¡ °ªÀ» ¼³Á¤ÇÑ´Ù.
            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  strCardID.Substring(i * 8, 8));
                this.CardData[i] = Convert.ToByte(objCardData.DecimalValue);
                objCardData.Dispose();
                objCardData = null;

                //this.CardData[i] = Convert.ToByte(clsELCardFormat.Instance.BinaryToDec(strCardID.Substring(i * 8, 8)));
            }

            clsConvertNumericType objStrCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                  strCardID);
            this.StrCardData = objStrCardData.HexaValue;
            objStrCardData.Dispose();
            objStrCardData = null;

            //this.StrCardData = clsELCardFormat.Instance.DecToHex(clsELCardFormat.Instance.BinaryToDec(strCardID), 16);
        }

        //===================================================================
        // Function name   : GetBitCount
        // Description     : Get BitCount
        // Return type     : int 
        // Argument        : string strNum
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        private int GetBitCount(string strNum)
        {
            int sum = 0;
            for (int i = 0; i < strNum.Length; i++)
            {
                string strBit = strNum.Substring(i, 1);
                if (strBit == "1")
                    sum++;
            }
            return sum;
        }

        public string Reverse(string str)
        {
            string str1 = string.Empty;
            for (int i = str.Length; i > 0; i--)
            {
                str1 += str.Substring(i - 1, 1);
            }
            return str1;
        }


        //===================================================================
        // Function name   : ICardFormat.AnalCardData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        bool ICardFormat.AnalCardData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                   data[i].ToString());
                strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;


                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardData =
                   new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                   strCardData);

            this.CardID = Convert.ToUInt64(objCardData.DecimalValue);

            objCardData.Dispose();
            objCardData = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));

            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.AnalCardReaderData
        // Description     : CardData Analizer
        // Return type     : 
        // Argument        : byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        bool ICardFormat.AnalCardReaderData(byte[] data)
        {
            if (data == null || data.Length < 8)
                return false;

            string strCardData = string.Empty;

            this._carddata = new byte[data.Length];
            Array.Copy(data, this._carddata, data.Length);

            for (int i = 0; i < 8; i++)
            {
                clsConvertNumericType objPartCardData =
                  new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.decimalType, (int)clsConvertNumericType.enumDecimalType.bitType8,
                  data[i].ToString());

                strCardData += this.Reverse(objPartCardData.BinaryValue);
                //strCardData += objPartCardData.BinaryValue;
                objPartCardData.Dispose();
                objPartCardData = null;

                //strCardData += clsELCardFormat.Instance.DecToBinary(data[i].ToString(), 8);
            }

            clsConvertNumericType objCardID =
                 new clsConvertNumericType((int)clsConvertNumericType.enumNumericType.binaryType, (int)clsConvertNumericType.enumDecimalType.ubitType64,
                 strCardData);

            this.CardID = Convert.ToUInt64(objCardID.DecimalValue);
            objCardID.Dispose();
            objCardID = null;

            //this.CardID = Convert.ToUInt64(clsELCardFormat.Instance.BinaryToDec(strCardData));
            return true;
        }

        //===================================================================
        // Function name   : ICardFormat.GetCardIDnFacility
        // Description     : CardID & CardFacility Get
        // Return type     : 
        // Argument        : out UInt64 cardid
        // Argument        : out UInt64 cardfacility
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        void ICardFormat.GetCardIDnFacility(out UInt64 cardid, out UInt64 cardfacility)
        {
            cardid = this.CardID;
            cardfacility = this.CardFacility;
        }


        //===================================================================
        // Function name   : ICardFormat.GetCardData
        // Description     : CardData Get
        // Return type     : 
        // Argument        : out byte[] data
        // Date            : 2008. 8. 8 created by david
        //===================================================================
        void ICardFormat.GetCardData(out byte[] data)
        {
            data = this.CardData;
        }
        #endregion
    }



    /// /////////////////////////////////////////////////////////////
}
