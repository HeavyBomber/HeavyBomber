using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserInterface.UI
{
    public interface IMenuForm
    {
        void AddMenuEntry(string entry, Delegate callback);
    }
}
