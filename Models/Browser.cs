using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flow.Models
{
    class Browser
    {
        private string _path;
        private string _name;

        public string Path { get; set; }
        public string Name { get; set; }
        public Browser(string path)
        {
            _path = path;
        }

        public Browser(string path, string name)
        {
            _path = path;
            _name = name;
        }
    }
}
