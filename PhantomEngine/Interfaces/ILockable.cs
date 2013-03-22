using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhantomEngine.Interfaces
{
    public interface ILockable
    {
        bool IsLocked();
        void Lock();
        void Unlock();
    }
}
