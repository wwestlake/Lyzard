/* 
 * Lyzard Modeling and Simulation System
 * 
 * Copyright 2019 William W. Westlake Jr.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lyzard.IDE.ViewModels
{
    public class PropertyBase : ViewModelBase
    {
        private string _name;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }

        public Type Type { get; set; }
    }

    public class StringProperty : PropertyBase
    {
        private string _value;

        public string Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }

    public class IntProperty : PropertyBase
    {
        private int _value;

        public int Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }

    public class FloatProperty : PropertyBase
    {
        private float _value;

        public float Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }

    public class DoubleProperty : PropertyBase
    {
        private double _value;

        public double Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }

    public class BooleanProperty : PropertyBase
    {
        private bool _value;

        public bool Value { get => _value; set { _value = value; OnPropertyChanged(); } }
    }

    public class EnumProperty : PropertyBase
    {
        private Enum _value;

        public Enum Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        public List<string> Values
        {
            get
            {
                return Enum.GetNames(this.Type).ToList();
            }
        }
    }




}
