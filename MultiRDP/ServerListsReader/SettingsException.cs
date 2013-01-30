using System;
using System.Collections.Generic;
using System.Text;

namespace ServerListXMLReader
{
    public class SettingsException : Exception
    {
        public enum ExceptionTypes
        {
            DUPLICATE_ENTRY
        }

        ExceptionTypes _exTypes;

        public SettingsException(ExceptionTypes exception_type)
        {
            this._exTypes = exception_type;
        }

        public ExceptionTypes ExceptionType
        {
            get
            {
                return this._exTypes;
            }
            set
            {
                this._exTypes = value;
            }
        }
    }
}
