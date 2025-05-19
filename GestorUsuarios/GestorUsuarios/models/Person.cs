namespace GestorUsuarios.models
{
    class Person
    {
        public int IdPerson { get; set; }
        public string NamesPerson { get; set; }
        public string LastNamesPerson { get; set; }
        public int AgePerson { get; set; }
        public string emailPerson { get; set; }

        public Person() { }
        public Person(string namesPerson, string lastNamesPerson, int agePerson, string emailPerson)
        {
            NamesPerson = namesPerson;
            LastNamesPerson = lastNamesPerson;
            AgePerson = agePerson;
            EmailPerson = emailPerson;
        }
    }
}
