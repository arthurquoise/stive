using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stive.Data.Services
{
    interface ILogin
    {
        void SaveAccessToSession();
        bool IsLogin();

    }
}
