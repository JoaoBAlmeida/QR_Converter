using System;
using System.Collections.Generic;
using System.Text;

namespace QR_Converter.Strategy
{
    public class RDO_Sys : IConv_Sys
    {
        public string ConvertData(string raw)
        {
            //First remove the uncessary beggining (17 chars)
            raw = raw.Remove(0, 17);

            //Create list to hold codes
            List<string> codes = new List<string>();

            //Loop to separate code by code
            while(raw.Length > 0)
            {
                //Collect the last five numbers
                codes.Add(raw.Substring(raw.Length - 5, 5));

                //Update raw without those numbers
                raw = raw.Remove(raw.Length - 5, 5);
            }

            //String to return
            string converted = string.Empty;

            //Loop to populate returnable string
            for(int x = 0; x < codes.Count; x++)
            {
                if (x == 0) converted = codes[x];
                else converted += "," + codes[x];
            }

            //Return string
            return converted;
        }
    }
}
