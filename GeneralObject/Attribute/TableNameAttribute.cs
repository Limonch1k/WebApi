using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralObject.MyCustomAttribute;

    [AttributeUsage(AttributeTargets.All)]
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }
    }

