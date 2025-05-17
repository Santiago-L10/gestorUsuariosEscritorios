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
        public Person(int idPerson, string namePerson, string lastnamesPerson, int age, string emailPerson)
        {
            IdPerson = idPerson;
            NamesPerson = namePerson;
            LastNamesPerson = lastnamesPerson;
            AgePerson = age;
            EmailPerson = emailPerson;
        }

    }

    
}
