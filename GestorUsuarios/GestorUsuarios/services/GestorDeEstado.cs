using System;
using System.Threading.Tasks;
class GestorDeEstado
{
    public static async void TemporizadorUsuario(User user, int tiempo){
        while (tiempo > 0){
            await Task.Delay(tiempo);
            tiempo -= tiempo;
            if (tiempo <= 10000){
                Console.WriteLine("El usuario se inactivara en 10 segundos");
                await Task.Delay(10000);
                user.CambioDeEstado(false);
            }    
        }
    }

    public static void ActiveUser(User user){
        user.CambioDeEstado(true);
        TemporizadorUsuario(user, 60000); // Se restablece 1 minuto el tiempo
    }
}
