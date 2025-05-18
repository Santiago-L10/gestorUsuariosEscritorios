using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorUsuarios.models
{
    class Rol
    {
        public int IdRol { get; set; }
        public string NameRol { get; set; }

        public Rol() { }
        public Rol (string name)
        {
            this.NameRol = name;
        }
    }


}
