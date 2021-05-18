using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MaxValue { get; }
        public float MinValue { get; }
        public ValueOutOfRangeException(string i_Subject, float i_MaxValue) : base(i_Subject)
        {
            MaxValue = i_MaxValue;
            MinValue = 0f;
        }
    }
}