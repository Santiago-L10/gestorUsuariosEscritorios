using System;
using System.Threading.Tasks;
class ManagerState
{
    public static async void StartInactivityTimer(User user, int time){
        while (time > 0){
            await Task.Delay(time);
            time -= time;
            if (time <= 10000){
                Console.WriteLine("El usuario se inactivara en 10 segundos");
                await Task.Delay(10000);
                user.ChangeStateActivite(false);
            }    
        }
    }

    public static void ActiveUser(User user){
        user.ChangeStateActivite(true);
        StartInactivityTimer(user, 60000); // Se debe resatablecer el timer
    }
}