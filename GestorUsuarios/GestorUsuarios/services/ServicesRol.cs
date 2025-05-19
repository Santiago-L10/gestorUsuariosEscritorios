using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;

namespace GestorUsuarios.services
{
    internal class ServicesRol
    {
        DBRol dBRol;

        public ServicesRol(DBRol dbRol)
        {
            this.dBRol = dbRol;
        }

        public List<Rol> GetRoles()
        {
            return dBRol.GetListRols();
        }
    }
}
