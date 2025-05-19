namespace GestorUsuarios.models
{
    public class Rol
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
