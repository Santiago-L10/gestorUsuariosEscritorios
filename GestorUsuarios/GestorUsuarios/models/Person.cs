namespace GestorUsuarios.models
{
    class Person
    {
        public int IdPerson { get; set; }
        public string NamesPerson { get; set; }
        public string LastNamesPerson { get; set; }
        public int AgePerson { get; set; }
        public string EmailPerson { get; set; }

        public Person() { }
        public Person(int idPerson, string namesPerson, string lastNamesPerson, int agePerson, string emailPerson)
        {
            IdPerson = idPerson;
            NamesPerson = namesPerson;
            LastNamesPerson = lastNamesPerson;
            AgePerson = agePerson;
            EmailPerson = emailPerson;
        }
    }
}
