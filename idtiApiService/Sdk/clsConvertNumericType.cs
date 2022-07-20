using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace idtiApiService.Sdk
{
    public class clsConvertNumericType : BaseResource {

        #region Public Enum
        public enum enumNumericType
        {
            decimalType,
            binaryType,
            hexaType
        }

        public enum enumDecimalType
        {
            bitType8,
            bitType16,
            bitType32,
            bitType64,
            ubitType16,
            ubitType32,
            ubitType64,
        }
        #endregion //Public Enum

        #region Declaration
        public static readonly double binaryNumber = 2;
        public static readonly double hexaNumber = 16;

        public static readonly int bittype8_binary_mincount = 0;
        public static readonly int bittype8_binary_maxcount = 8;
        public static readonly int bittype16_binary_mincount = 0;
        public static readonly int bittype16_binary_maxcount = 16;
        public static readonly int bittype32_binary_mincount = 0;
        public static readonly int bittype32_binary_maxcount = 32;
        public static readonly int bittype64_binary_mincount = 0;
        public static readonly int bittype64_binary_maxcount = 64;
        public static readonly int ubittype16_binary_mincount = 0;
        public static readonly int ubittype16_binary_maxcount = 16;
        public static readonly int ubittype32_binary_mincount = 0;
        public static readonly int ubittype32_binary_maxcount = 32;
        public static readonly int ubittype64_binary_mincount = 0;
        public static readonly int ubittype64_binary_maxcount = 64;

        public static readonly int bittype8_hexa_mincount = 0;
        public static readonly int bittype8_hexa_maxcount = 2;
        public static readonly int bittype16_hexa_mincount = 0;
        public static readonly int bittype16_hexa_maxcount = 4;
        public static readonly int bittype32_hexa_mincount = 0;
        public static readonly int bittype32_hexa_maxcount = 8;
        public static readonly int bittype64_hexa_mincount = 0;
        public static readonly int bittype64_hexa_maxcount = 16;
        public static readonly int ubittype16_hexa_mincount = 0;
        public static readonly int ubittype16_hexa_maxcount = 4;
        public static readonly int ubittype32_hexa_mincount = 0;
        public static readonly int ubittype32_hexa_maxcount = 8;
        public static readonly int ubittype64_hexa_mincount = 0;
        public static readonly int ubittype64_hexa_maxcount = 16;

        private string decimalValue = string.Empty;
        private string binaryValue = string.Empty;
        private string hexaValue = string.Empty;

        private int numericType = (int)enumNumericType.decimalType;
        private int decimalType = (int)enumDecimalType.bitType8;

        private string exMessage = string.Empty;
        private bool _disposed = false;
        #endregion //Declaration

        #region Constructor
        public clsConvertNumericType()
        {
        }

        public clsConvertNumericType(int numType, int decType, string strValue)
        {
            this.numericType = numType;
            this.decimalType = decType;

            switch (this.numericType)
            {
                case (int)enumNumericType.decimalType:
                    {
                        this.decimalValue = strValue;
                        this.binaryValue = this.GetBinarybyDecimal();
                        this.hexaValue = this.GetHexabyDecimal();
                    }
                    break;
                case (int)enumNumericType.binaryType:
                    {
                        this.binaryValue = strValue;
                        this.decimalValue = this.GetDecimalbyBinary();
                        this.hexaValue = this.GetHexabyBinary();
                    }
                    break;
                case (int)enumNumericType.hexaType:
                    {
                        this.hexaValue = strValue;
                        this.decimalValue = this.GetDecimalbyHexa();
                        this.binaryValue = this.GetBinarybyHexa();
                    }
                    break;
                default:
                    this.decimalValue = string.Empty;
                    this.binaryValue = string.Empty;
                    this.hexaValue = string.Empty;
                    break;
            }
        }
        #endregion //Constructor

        #region Properity
        public string DecimalValue
        {
            get { return this.decimalValue; }
        }

        public string BinaryValue
        {
            get { return this.binaryValue; }
        }

        public string HexaValue
        {
            get { return this.hexaValue; }
        }
        #endregion

        #region Methods
        protected override void Dispose(bool isDisposing)
        {
            // ¿©·¯¹øÀÇ dispose¸¦ ¼öÇàÇÏÁö ¾Êµµ·Ï ÇÑ´Ù. 
            if (_disposed)
                return;

            if (isDisposing)
            {
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
        #endregion //Methods

        #region ConvertToDecimalbyBinary Methods
        private string GetDecimalbyBinary()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_BinaryToDecimal();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_BinaryToDecimal();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty && (this.binaryValue.Length > bittype8_binary_mincount && this.binaryValue.Length <= bittype8_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            byte nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (byte)(Convert.ToByte(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType16_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype16_binary_mincount && this.binaryValue.Length <= bittype16_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int16 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (Int16)(Convert.ToInt16(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType32_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype32_binary_mincount && this.binaryValue.Length <= bittype32_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int32 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (Int32)(Convert.ToInt32(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType64_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype64_binary_mincount && this.binaryValue.Length <= bittype64_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int64 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (Int64)(Convert.ToInt64(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType16_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype16_binary_mincount && this.binaryValue.Length <= ubittype16_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt16 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (UInt16)(Convert.ToUInt16(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType32_BinaryToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype32_binary_mincount && this.binaryValue.Length <= ubittype32_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt32 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (UInt32)(Convert.ToUInt32(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType64_BinaryToDecimal()
        {
            string retVal = string.Empty;

            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype64_binary_mincount && this.binaryValue.Length <= ubittype64_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt64 nDecimal = 0;
            for (int i = (this.binaryValue.Length - 1); i >= 0; i--)
            {
                nDecimal += (UInt64)(Convert.ToUInt64(this.binaryValue.Substring(i, 1)) * (Math.Pow(binaryNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }
        #endregion //ConvertToDecimal Methods

        #region ConvertToDecimalbyHexa Methods
        private string GetDecimalbyHexa()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_HexaToDecimal();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_HexaToDecimal();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_HexaToDecimal();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_HexaToDecimal();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_HexaToDecimal();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_HexaToDecimal();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_HexaToDecimal();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > bittype8_hexa_mincount && this.hexaValue.Length <= bittype8_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return retVal;


            //2. Çüº¯È¯
            double j = 0;
            byte nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (byte)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (byte)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (byte)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (byte)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (byte)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (byte)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (byte)(Convert.ToByte(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }
            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType16_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > bittype16_hexa_mincount && this.hexaValue.Length <= bittype16_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int16 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (Int16)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (Int16)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (Int16)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (Int16)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (Int16)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (Int16)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (Int16)(Convert.ToInt16(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType32_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > bittype32_hexa_mincount && this.hexaValue.Length <= bittype32_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int32 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (Int32)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (Int32)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (Int32)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (Int32)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (Int32)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (Int32)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (Int32)(Convert.ToInt32(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string bitType64_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > bittype64_hexa_mincount && this.hexaValue.Length <= bittype64_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            Int64 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (Int64)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (Int64)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (Int64)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (Int64)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (Int64)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (Int64)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (Int64)(Convert.ToInt64(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType16_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > ubittype16_hexa_mincount && this.hexaValue.Length <= ubittype16_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt16 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (UInt16)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (UInt16)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (UInt16)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (UInt16)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (UInt16)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (UInt16)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (UInt16)(Convert.ToUInt16(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType32_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > ubittype32_hexa_mincount && this.hexaValue.Length <= ubittype32_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt32 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (UInt32)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (UInt32)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (UInt32)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (UInt32)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (UInt32)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (UInt32)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (UInt32)(Convert.ToUInt32(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }

        private string ubitType64_HexaToDecimal()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Hexa°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length > ubittype64_hexa_mincount && this.hexaValue.Length <= ubittype64_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }
                }
            }
            else
                return string.Empty;

            //2. Çüº¯È¯
            double j = 0;
            UInt64 nDecimal = 0;
            for (int i = (this.hexaValue.Length - 1); i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                    nDecimal += (UInt64)(10 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                    nDecimal += (UInt64)(11 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                    nDecimal += (UInt64)(12 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                    nDecimal += (UInt64)(13 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                    nDecimal += (UInt64)(14 * (Math.Pow(hexaNumber, j)));
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                    nDecimal += (UInt64)(15 * (Math.Pow(hexaNumber, j)));
                else
                    nDecimal += (UInt64)(Convert.ToUInt64(this.hexaValue.Substring(i, 1)) * (Math.Pow(hexaNumber, j)));
                j++;
            }

            retVal = nDecimal.ToString();
            return retVal;
        }
        #endregion //ConvertToDecimalbyHexa Methods

        #region ConvertToBinarybyDecimal Methods
        private string GetBinarybyDecimal()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_DecimalToBinary();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_DecimalToBinary();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_DecimalToBinary();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_DecimalToBinary();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_DecimalToBinary();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_DecimalToBinary();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_DecimalToBinary();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_DecimalToBinary()
        {
            string retVal = string.Empty;
            byte nDecimal = 0;
            try
            {
                nDecimal = byte.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(bittype8_binary_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (byte)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    nDecimal -= (byte)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string bitType16_DecimalToBinary()
        {
            string retVal = string.Empty;
            Int16 nDecimal = 0;
            try
            {
                nDecimal = Int16.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt16 unDecimal = (UInt16)nDecimal;
            for (double i = (double)(bittype16_binary_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt16)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    unDecimal -= (UInt16)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string bitType32_DecimalToBinary()
        {
            string retVal = string.Empty;
            Int32 nDecimal = 0;
            try
            {
                nDecimal = Int32.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt32 unDecimal = (UInt32)nDecimal;
            for (double i = (double)(bittype32_binary_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt32)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    unDecimal -= (UInt32)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string bitType64_DecimalToBinary()
        {
            string retVal = string.Empty;
            Int64 nDecimal = 0;
            try
            {
                nDecimal = Int64.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt64 unDecimal = (UInt64)nDecimal;
            for (double i = (double)(bittype64_binary_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt64)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    unDecimal -= (UInt64)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType16_DecimalToBinary()
        {
            string retVal = string.Empty;
            UInt16 nDecimal = 0;
            try
            {
                nDecimal = UInt16.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype16_binary_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt16)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    nDecimal -= (UInt16)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType32_DecimalToBinary()
        {
            string retVal = string.Empty;
            UInt32 nDecimal = 0;
            try
            {
                nDecimal = UInt32.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype32_binary_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt32)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    nDecimal -= (UInt32)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType64_DecimalToBinary()
        {
            string retVal = string.Empty;
            UInt64 nDecimal = 0;
            try
            {
                nDecimal = UInt64.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype64_binary_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt64)(Math.Pow(binaryNumber, i)))
                {
                    retVal += "1";
                    nDecimal -= (UInt64)(Math.Pow(binaryNumber, i));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }
        #endregion //ConvertToBinarybyDecimal Methods

        #region ConvertToBinarybyHexa Methods
        private string GetBinarybyHexa()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_HexaToBinary();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_HexaToBinary();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_HexaToBinary();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_HexaToBinary();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_HexaToBinary();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_HexaToBinary();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_HexaToBinary();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_HexaToBinary()
        {
            string retVal = string.Empty;

            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= bittype8_hexa_mincount && this.hexaValue.Length <= bittype8_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < bittype8_hexa_maxcount)
            {
                for (int i = 0; i < (bittype8_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }

            return retVal;
        }

        private string bitType16_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= bittype16_hexa_mincount && this.hexaValue.Length <= bittype16_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < bittype16_hexa_maxcount)
            {
                for (int i = 0; i < (bittype16_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        private string bitType32_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= bittype32_hexa_mincount && this.hexaValue.Length <= bittype32_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < bittype32_hexa_maxcount)
            {
                for (int i = 0; i < (bittype32_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        private string bitType64_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= bittype64_hexa_mincount && this.hexaValue.Length <= bittype64_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < bittype64_hexa_maxcount)
            {
                for (int i = 0; i < (bittype64_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 0; j < 4; j++)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal += "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal += "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        private string ubitType16_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= ubittype16_hexa_mincount && this.hexaValue.Length <= ubittype16_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < ubittype16_hexa_maxcount)
            {
                for (int i = 0; i < (ubittype16_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        private string ubitType32_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= ubittype32_hexa_mincount && this.hexaValue.Length <= ubittype32_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < ubittype32_hexa_maxcount)
            {
                for (int i = 0; i < (ubittype32_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        private string ubitType64_HexaToBinary()
        {
            string retVal = string.Empty;
            if (this.hexaValue != string.Empty &&
                (this.hexaValue.Length >= ubittype64_hexa_mincount && this.hexaValue.Length <= ubittype64_hexa_maxcount))
            {
                foreach (char ch in this.hexaValue)
                {
                    if (ch != '0' && ch != '1' && ch != '2' && ch != '3' && ch != '4' && ch != '5' && ch != '6' && ch != '7' && ch != '8' && ch != '9'
                        && ch != 'a' && ch != 'b' && ch != 'c' && ch != 'd' && ch != 'e' && ch != 'f' && ch != 'A' && ch != 'B' &&
                        ch != 'C' && ch != 'D' && ch != 'E' && ch != 'F')
                    {
                        return retVal;
                    }

                }
            }

            if (this.hexaValue.Length < ubittype64_hexa_maxcount)
            {
                for (int i = 0; i < (ubittype64_hexa_maxcount - this.hexaValue.Length); i++)
                {
                    retVal += "0000";
                }
            }

            for (int i = this.hexaValue.Length - 1; i >= 0; i--)
            {
                if (this.hexaValue.Substring(i, 1) == "a" || this.hexaValue.Substring(i, 1) == "A")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 10);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "b" || this.hexaValue.Substring(i, 1) == "B")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 11);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "c" || this.hexaValue.Substring(i, 1) == "C")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 12);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "d" || this.hexaValue.Substring(i, 1) == "D")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 13);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "e" || this.hexaValue.Substring(i, 1) == "E")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 14);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else if (this.hexaValue.Substring(i, 1) == "f" || this.hexaValue.Substring(i, 1) == "F")
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * 15);
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
                else
                {
                    byte nDecimal = (byte)((Math.Pow(hexaNumber, 1)) * Convert.ToByte(this.hexaValue.Substring(i, 1)));
                    for (double j = 3; j >= 0; j--)
                    {
                        if (nDecimal >= (byte)(Math.Pow(binaryNumber, j)))
                        {
                            retVal = retVal + "1";
                            nDecimal -= (byte)(Math.Pow(binaryNumber, j));
                        }
                        else
                            retVal = retVal + "0";
                    }
                }
            }
            return retVal;
        }

        #endregion //ConvertToBinarybyHexa Methods

        #region ConvertToHexabyDecimal Methods
        private string GetHexabyDecimal()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_DecimalToHexa();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_DecimalToHexa();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_DecimalToHexa();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_DecimalToHexa();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_DecimalToHexa();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_DecimalToHexa();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_DecimalToHexa();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_DecimalToHexa()
        {
            string retVal = string.Empty;

            byte nDecimal = 0;
            try
            {
                nDecimal = byte.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(bittype8_hexa_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (byte)(Math.Pow(hexaNumber, i)))
                {
                    byte nHexaRet = (byte)(nDecimal / (byte)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    nDecimal -= (byte)(nHexaRet * (byte)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }

            return retVal;
        }

        private string bitType16_DecimalToHexa()
        {
            string retVal = string.Empty;
            Int16 nDecimal = 0;
            try
            {
                nDecimal = Int16.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt16 unDecimal = (UInt16)nDecimal;
            for (double i = (double)(bittype16_hexa_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt16)(Math.Pow(hexaNumber, i)))
                {
                    UInt16 nHexaRet = (UInt16)(unDecimal / (UInt64)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    unDecimal -= (UInt16)(nHexaRet * (UInt16)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }

            return retVal;
        }

        private string bitType32_DecimalToHexa()
        {
            string retVal = string.Empty;
            Int32 nDecimal = 0;
            try
            {
                nDecimal = Int32.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt32 unDecimal = (UInt32)nDecimal;
            for (double i = (double)(bittype32_hexa_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt32)(Math.Pow(hexaNumber, i)))
                {
                    UInt32 nHexaRet = (UInt32)(unDecimal / (UInt32)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    unDecimal -= (UInt32)(nHexaRet * (UInt32)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string bitType64_DecimalToHexa()
        {
            string retVal = string.Empty;
            Int64 nDecimal = 0;
            try
            {
                nDecimal = Int64.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            UInt64 unDecimal = (UInt64)nDecimal;
            for (double i = (double)(bittype64_hexa_maxcount - 1); i >= 0; i--)
            {
                if (unDecimal >= (UInt64)(Math.Pow(hexaNumber, i)))
                {
                    UInt64 nHexaRet = (UInt64)(unDecimal / (UInt64)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    unDecimal -= (UInt64)(nHexaRet * (UInt64)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType16_DecimalToHexa()
        {
            string retVal = string.Empty;
            UInt16 nDecimal = 0;
            try
            {
                nDecimal = UInt16.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype16_hexa_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt16)(Math.Pow(hexaNumber, i)))
                {
                    UInt16 nHexaRet = (UInt16)(nDecimal / (UInt16)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    nDecimal -= (UInt16)(nHexaRet * (UInt16)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType32_DecimalToHexa()
        {
            string retVal = string.Empty;
            UInt32 nDecimal = 0;
            try
            {
                nDecimal = UInt32.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype32_hexa_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt32)(Math.Pow(hexaNumber, i)))
                {
                    UInt32 nHexaRet = (UInt32)(nDecimal / (UInt32)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    nDecimal -= (UInt32)(nHexaRet * (UInt32)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        private string ubitType64_DecimalToHexa()
        {
            string retVal = string.Empty;
            UInt64 nDecimal = 0;
            try
            {
                nDecimal = UInt64.Parse(this.decimalValue);
            }
            catch (OverflowException ex)
            {
                this.exMessage = ex.Message;
                return retVal;
            }

            for (double i = (double)(ubittype64_hexa_maxcount - 1); i >= 0; i--)
            {
                if (nDecimal >= (UInt16)(Math.Pow(hexaNumber, i)))
                {
                    UInt64 nHexaRet = (UInt64)(nDecimal / (UInt64)(Math.Pow(hexaNumber, i)));
                    if (nHexaRet == 10)
                        retVal += "A";
                    else if (nHexaRet == 11)
                        retVal += "B";
                    else if (nHexaRet == 12)
                        retVal += "C";
                    else if (nHexaRet == 13)
                        retVal += "D";
                    else if (nHexaRet == 14)
                        retVal += "E";
                    else if (nHexaRet == 15)
                        retVal += "F";
                    else
                        retVal += nHexaRet.ToString();

                    nDecimal -= (UInt64)(nHexaRet * (UInt64)(Math.Pow(hexaNumber, i)));
                }
                else
                    retVal += "0";
            }
            return retVal;
        }

        #endregion //ConvertToHexabyDecimal Methods

        #region ConvertToHexabyBinary Methods
        private string GetHexabyBinary()
        {
            string retVal = string.Empty;
            switch (this.decimalType)
            {
                case (int)enumDecimalType.bitType8:
                    retVal = this.bitType8_BinaryToHexa();
                    break;
                case (int)enumDecimalType.bitType16:
                    retVal = this.bitType16_BinaryToHexa();
                    break;
                case (int)enumDecimalType.bitType32:
                    retVal = this.bitType32_BinaryToHexa();
                    break;
                case (int)enumDecimalType.bitType64:
                    retVal = this.bitType64_BinaryToHexa();
                    break;
                case (int)enumDecimalType.ubitType16:
                    retVal = this.ubitType16_BinaryToHexa();
                    break;
                case (int)enumDecimalType.ubitType32:
                    retVal = this.ubitType32_BinaryToHexa();
                    break;
                case (int)enumDecimalType.ubitType64:
                    retVal = this.ubitType64_BinaryToHexa();
                    break;
                default:
                    retVal = string.Empty;
                    break;
            }
            return retVal;
        }

        private string bitType8_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty && (this.binaryValue.Length > bittype8_binary_mincount && this.binaryValue.Length <= bittype8_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < bittype8_binary_maxcount)
            {
                for (int i = 0; i < (bittype8_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < bittype8_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }

            return retVal;
        }

        private string bitType16_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype16_binary_mincount && this.binaryValue.Length <= bittype16_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < bittype16_binary_maxcount)
            {
                for (int i = 0; i < (bittype16_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < bittype16_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        private string bitType32_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype32_binary_mincount && this.binaryValue.Length <= bittype32_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < bittype32_binary_maxcount)
            {
                for (int i = 0; i < (bittype32_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < bittype32_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        private string bitType64_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > bittype64_binary_mincount && this.binaryValue.Length <= bittype64_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < bittype64_binary_maxcount)
            {
                for (int i = 0; i < (bittype64_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < bittype64_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        private string ubitType16_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype16_binary_mincount && this.binaryValue.Length <= ubittype16_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < ubittype16_binary_maxcount)
            {
                for (int i = 0; i < (ubittype16_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < ubittype16_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        private string ubitType32_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype32_binary_mincount && this.binaryValue.Length <= ubittype32_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < ubittype32_binary_maxcount)
            {
                for (int i = 0; i < (ubittype32_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < ubittype32_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        private string ubitType64_BinaryToHexa()
        {
            string retVal = string.Empty;
            //1. À¯È¿ÇÑ Binary°ª / ¹üÀ§¸¦ È®ÀÎÇÑ´Ù.
            if (this.binaryValue != string.Empty &&
                (this.binaryValue.Length > ubittype64_binary_mincount && this.binaryValue.Length <= ubittype64_binary_maxcount))
            {
                foreach (char singlechar in this.binaryValue)
                {
                    if (singlechar != '0' && singlechar != '1')
                        return string.Empty;
                }
            }
            else
                return string.Empty;

            string tempbinaryValue = string.Empty;
            if (this.binaryValue.Length < ubittype64_binary_maxcount)
            {
                for (int i = 0; i < (ubittype64_binary_maxcount - this.binaryValue.Length); i++)
                    tempbinaryValue += "0";
            }

            tempbinaryValue += this.binaryValue;

            for (int i = 0; i < ubittype64_hexa_maxcount; i++)
            {
                string strSingleHexValue = tempbinaryValue.Substring(i * 4, 4);
                byte nSingleHexValue = 0;
                double k = 3;
                for (int j = 0; j < 4; j++)
                {
                    nSingleHexValue += (byte)(Math.Pow(binaryNumber, k) * Convert.ToByte(strSingleHexValue.Substring(j, 1)));
                    k--;
                }

                if (nSingleHexValue == 10)
                    retVal += "A";
                else if (nSingleHexValue == 11)
                    retVal += "B";
                else if (nSingleHexValue == 12)
                    retVal += "C";
                else if (nSingleHexValue == 13)
                    retVal += "D";
                else if (nSingleHexValue == 14)
                    retVal += "E";
                else if (nSingleHexValue == 15)
                    retVal += "F";
                else
                    retVal += nSingleHexValue.ToString();
            }
            return retVal;
        }

        #endregion //ConvertToHexabyBinary Methods





        /// //////////////////////////////////////////
    }
}
