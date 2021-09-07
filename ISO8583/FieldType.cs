using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{
    public static class FieldType
    {
        /// <summary>
        /// Fixed length field of 2 digits
        /// </summary>
        public const string n2 = "00";

        /// <summary>
        /// Fixed length field of 4 digits
        /// </summary>
        public const string n4 = "0000";

        /// <summary>
        /// Fixed length field of 4 digits Month and Day
        /// </summary>
        public const string n4_date = "MMdd";

        /// <summary>
        /// Fixed length field of 6 digits
        /// </summary>
        public const string n6 = "000000";

        /// <summary>
        /// Fixed length field of 6 digits Hours Minutes and Seconds
        /// </summary>
        public const string n6_time = "HHmmss";

        /// <summary>
        /// Fixed length field of 10 digits date and time
        /// </summary>
        public const string n10_datetime = "MMddHHmmss";

        /// <summary>
        /// Fixed length field of 12 digits
        /// </summary>
        public const string n12 = "000000000000";
    }
}
