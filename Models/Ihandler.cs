using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    interface Ihandler
    {
        void HandleRemove(string FileName);
        void HandleAdd(string FileName);
        void HandleShowList(string FileName);
    }
}
