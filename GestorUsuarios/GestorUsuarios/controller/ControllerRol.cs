using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorUsuarios.models;
using GestorUsuarios.services;

namespace GestorUsuarios.controller
{
    internal class ControllerRol
    {
        ServicesRol servicesRol;
        public ControllerRol(ServicesRol servicesRol)
        {
            this.servicesRol = servicesRol;
        }

        public List<Rol> GetRoles()
        {
            return servicesRol.GetRoles();
        }
    }
}
