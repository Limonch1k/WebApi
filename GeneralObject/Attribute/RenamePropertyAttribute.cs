using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneralObject.MyCustomAttribute;

[AttributeUsage(AttributeTargets.Property)]

public class RenamePropertyAttribute : Attribute
{
    public string PropertyName { get; set; }
}

