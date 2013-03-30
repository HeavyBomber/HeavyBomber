using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateManagement
{
    public class ScreenChangeEventArgs : EventArgs
    {
        public string StateName { get; set; }
    }
}
