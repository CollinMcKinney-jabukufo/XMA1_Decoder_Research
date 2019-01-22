using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jabukufo.Common
{
    [MetaData()]
    public class Test : MetaData
    {

    }

    public class MetaData
    {
        private readonly Dictionary<string, Param> Parameters = new Dictionary<string, Param> { };

        protected MetaData() { }


        protected class MetaDataAttribute : Attribute
        {
            public MetaDataAttribute(
                (Type, string, object) p0 = (null, null, null),
                (Type, string, object) p1 = (null, null, null),
                (Type, string, object) p2 = (null, null, null),
                (Type, string, object) p3 = (null, null, null),
                (Type, string, object) p4 = (null, null, null),
                (Type, string, object) p5 = (null, null, null),
                (Type, string, object) p6 = (null, null, null),
                (Type, string, object) p7 = (null, null, null),
                (Type, string, object) p8 = (null, null, null),
                (Type, string, object) p9 = (null, null, null))
            {

            }
        }
    }

    public class Param
    {
        public readonly Type Type;
        public readonly string Name;
        public readonly object Value;

        public Param(Type type, string name, object value)
        {
            this.Type = type;
            this.Name = name;
            this.Value = value;
        }

        public static implicit operator Param((Type, string, object) parameter)
            => new Param(parameter.Item1, parameter.Item2, parameter.Item3);
    }
}
