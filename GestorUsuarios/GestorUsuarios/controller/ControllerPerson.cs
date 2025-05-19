using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorUsuarios.models;
using GestorUsuarios.services;

namespace GestorUsuarios.controller
{
    internal class ControllerPerson
    {
        ServicesPersons servicesPersons;

        public ControllerPerson(ServicesPersons servicesPersons)
        {
            this.servicesPersons = servicesPersons;
        }

        public Person GetPerson(int? id, string name = null)
        {
            return servicesPersons.GetPerson(id, name);
        }
        public List<Person> ListUsers()
        {
            return servicesPersons.ListUsers();
        }
        public void AddPerson(Person person)
        {
            servicesPersons.AddPerson(person);
        }
    }
}
