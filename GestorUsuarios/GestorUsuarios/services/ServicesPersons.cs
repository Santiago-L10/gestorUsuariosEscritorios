using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;

namespace GestorUsuarios.services
{
    internal class ServicesPersons
    {
        DBPerson dBPerson;
        public ServicesPersons(DBPerson dBPerson)
        {
            this.dBPerson = dBPerson;
        }
        public Person GetPerson(int? id, string name = null)
        {
            return dBPerson.GetPerson(id, name);
        }

        public List<Person> ListUsers()
        {
            return dBPerson.listUsers();
        }

        public void AddPerson(Person person)
        {
            dBPerson.SetPerson(person);
        }
    }
}
