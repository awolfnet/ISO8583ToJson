using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{
    public static class FieldExtensions
    {
        public static string FieldNumber(this ISO8583.Field fieldName)
        {
            return ((int)fieldName).ToString();
        }
    }
}
