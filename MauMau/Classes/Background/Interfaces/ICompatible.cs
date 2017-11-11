using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Background.Interfaces
{
    interface ICompatible
    {
         bool Compatible(ICompatible card);
    }
}
