using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicIterfaces.UserInterface
{
    public interface IButton
    {
        event EventHandler Click;
    }
}
