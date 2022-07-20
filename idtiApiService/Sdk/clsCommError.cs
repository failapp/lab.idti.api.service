using System;
using System.Collections.Generic;
using System.Text;

namespace idtiApiService.Sdk
{
    class clsCommError
    {
        private static clsCommError instance;               
                
        private string commErrorMessage = string.Empty;      

        public static clsCommError getInstance()
        {
            if (instance == null)
            {
                instance = new clsCommError();
            }

            return instance;
        } 

        public string CommErrorMessage
        {
            get { return this.commErrorMessage; }

            set { this.commErrorMessage = value; }
        }
    }
}
