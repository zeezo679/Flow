using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class Validation
    {
        public static int ValidateURL(string url)
        {
            if(String.IsNullOrWhiteSpace(url))
            {
                return 0;
            }
            return 1;
        }
    }
}
