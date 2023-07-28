using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer.MVVM.Model
{
    internal class TreshFilterSettings
    {
        public double Value { get; set; }
        public double MaxValue { get; set; }
        public bool Active { get; set; }
        public TreshFilterSettings() { }
        public TreshFilterSettings(double value, double maxValue, bool active) {  
            Value = value; 
            MaxValue = maxValue;
            Active = active; 
        }
    }
}
