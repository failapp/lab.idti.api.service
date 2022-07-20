using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
//using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using isldev;

namespace idtiApiService.Sdk
{
    class clsGlobal
    {
        public enum enumCommunicationIndex
        {
            None,
            TCPIP,
            Serial,
            Server
        }       

        public clsGlobal()
        {
        }

        //public void FillComboBox(ComboBox cbo, string[] args)
        //{
        //    ArrayList cboList = new ArrayList();

        //    int i = 0;

        //    if (args != null)
        //    {
        //        foreach (string s in args)
        //        {
        //            cboList.Add(new ComboData(i.ToString(), s));
        //            i += 1;
        //        }
        //    }
        //    cbo.DataSource = null;
        //    cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        //    cbo.DisplayMember = "Text";
        //    cbo.ValueMember = "Value";
        //    cbo.DataSource = cboList;
        //}

        //public void FillComboBox(ComboBox cbo, int[] argi)
        //{
        //    ArrayList cboList = new ArrayList();

        //    int j = 0;

        //    if (argi != null)
        //    {
        //        foreach (int i in argi)
        //        {
        //            cboList.Add(new ComboData(j.ToString(), i.ToString()));
        //            j += 1;
        //        }
        //    }
        //    cbo.DataSource = null;
        //    cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        //    cbo.DisplayMember = "Text";
        //    cbo.ValueMember = "Value";
        //    cbo.DataSource = cboList;
        //}

        public DateTime ConvertDateTimeToDateTime(string datetime)
        {
            DateTime retVal = DateTime.Now;

            datetime = datetime.Substring(0, 4) + "-" +
                       datetime.Substring(4, 2) + "-" +
                       datetime.Substring(6, 2) + " " +
                       datetime.Substring(8, 2) + ":" +
                       datetime.Substring(10, 2) + ":" +
                       datetime.Substring(12, 2);

            retVal = Convert.ToDateTime(datetime);

            return retVal;
        }

        public DateTime ConvertDateToDateTime(string date)
        {
            DateTime retVal = DateTime.Now;

            date = date.Substring(0, 4) + "-" +
                   date.Substring(4, 2) + "-" +
                   date.Substring(6, 2);

            retVal = Convert.ToDateTime(date);

            return retVal;
        }

        public DateTime ConvertHourMinToDateTime(string hourmin)
        {
            DateTime retVal = DateTime.Now;

            hourmin = hourmin.Substring(0, 2) + ":" +
                      hourmin.Substring(2, 2);

            retVal = Convert.ToDateTime(hourmin);

            return retVal;
        } 

        // Create Folder.        
        public void CreateFolder(string s)
        {
            if (!System.IO.Directory.Exists(s)) { System.IO.Directory.CreateDirectory(s); }
        }

        public byte[] CalcBoolArrayToByte(bool[] value)
        {
            const int lengthCalcBoolArrayBasicLength = 8;

            byte[] retVal = null;

            if (value != null && (value.Length % lengthCalcBoolArrayBasicLength).Equals(0))
            {
                retVal = new byte[value.Length / lengthCalcBoolArrayBasicLength];

                for (int i = 0; i < retVal.Length; i++)
                {
                    BitArray bits = new System.Collections.BitArray(lengthCalcBoolArrayBasicLength);

                    for (int j = 0; j < bits.Length; j++)
                    {
                        bits[j] = value[(i * lengthCalcBoolArrayBasicLength) + j];
                        //bits[j] = value[((i + 1) * lengthCalcBoolArrayBasicLength) - j - 1];
                    }
                    bits.CopyTo(retVal, retVal.Length - i - 1);
                }
            }

            return retVal;
        }

        public ImageFormat GetSystemImageFormat(int devImageFormat)
        {
            ImageFormat retVal = ImageFormat.Bmp;

            switch (devImageFormat)
            {
                case (int)isldev.clsDevParams.DevImageFormat.Bmp:
                    retVal = ImageFormat.Bmp;
                    break;
                case (int)isldev.clsDevParams.DevImageFormat.Gif:
                    retVal = ImageFormat.Gif;
                    break;
                case (int)isldev.clsDevParams.DevImageFormat.Jpeg:
                    retVal = ImageFormat.Jpeg;
                    break;
                case (int)isldev.clsDevParams.DevImageFormat.Png:
                    retVal = ImageFormat.Png;
                    break;
            }

            return retVal;
        }


        public Bitmap SetImageRotate(Bitmap bmImage, int devImageRotate)
        {
            Bitmap retVal = bmImage;

            switch (devImageRotate)
            {
                case (int)clsDevParams.DevImageRotatedType.None:
                    break;
                case (int)clsDevParams.DevImageRotatedType.Dig90:
                    retVal.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case (int)clsDevParams.DevImageRotatedType.Dig180:
                    retVal.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case (int)clsDevParams.DevImageRotatedType.Dig270:
                    retVal.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }

            return retVal;
        }


    }

    public class ComboData
    {
        readonly string value = string.Empty;
        readonly string text = string.Empty;

        public ComboData(string value, string text)
        {
            this.value = value;
            this.text = text;
        }

        public string Value
        {
            get { return this.value; }
        }

        public string Text
        {
            get { return this.text; }
        }
    }
}
