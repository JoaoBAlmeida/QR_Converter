using QR_Converter.Strategy;
using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Converter.Controller
{
    public class Code_Converter
    {
        private IConv_Sys conv_Sys;

        public Code_Converter(string type)
        {
            if (type == "Rio das Ostras") conv_Sys = new RDO_Sys();
            else throw new Exception("Type not predefined");
        }
        public string ConvertCode(string code)
        {
            return conv_Sys.ConvertData(code);
        }
    }
}
