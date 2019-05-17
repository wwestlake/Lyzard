using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyzard.IDE.ViewModels
{
    public class PropertyBase : ViewModelBase
    {
        private string _name;

        public string Name { get => _name; set { _name = value; FirePropertyChanged(); } }

        public Type Type { get; set; }
    }

    public class StringProperty : PropertyBase
    {
        private string _value;

        public string Value { get => _value; set { _value = value; FirePropertyChanged(); } }
    }

    public class IntProperty : PropertyBase
    {
        private int _value;

        public int Value { get => _value; set { _value = value; FirePropertyChanged(); } }
    }

    public class FloatProperty : PropertyBase
    {
        private float _value;

        public float Value { get => _value; set { _value = value; FirePropertyChanged(); } }
    }

    public class DoubleProperty : PropertyBase
    {
        private double _value;

        public double Value { get => _value; set { _value = value; FirePropertyChanged(); } }
    }

    public class BooleanProperty : PropertyBase
    {
        private bool _value;

        public bool Value { get => _value; set { _value = value; FirePropertyChanged(); } }
    }

    public class EnumProperty : PropertyBase
    {
        private Enum _value;

        public Enum Value { get => _value; set { _value = value; FirePropertyChanged(); } }

        public List<string> Values
        {
            get
            {
                return Enum.GetNames(this.Type).ToList();
            }
        }
    }




}
