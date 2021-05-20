using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    class IllegalRequestException: Exception
    {
        public IllegalRequestException(string i_Subject) : base(i_Subject){ }

    }
}
