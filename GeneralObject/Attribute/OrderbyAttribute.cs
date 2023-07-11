using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneralObject.MyCustomAttribute;

[AttributeUsage(AttributeTargets.Property)]

public class OrderbyAttribute : Attribute
{
    public int Order { get; set; }
}

