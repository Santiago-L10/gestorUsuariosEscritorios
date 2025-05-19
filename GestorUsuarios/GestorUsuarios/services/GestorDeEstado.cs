using GestorUsuarios.controller;
using GestorUsuarios.models;
using GestorUsuarios.models.conex;
using GestorUsuarios.services;
using System;
using System.Threading.Tasks;
using System.Windows;
class GestorDeEstado
{
    public static async Task TemporizadorUsuario(User user, int tiempo)
    {
        ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));

        if (tiempo > 10000)
        {
            await Task.Delay(tiempo - 10000); // Espera hasta que queden 10 segundos
            //MessageBox.Show("El usuario se inactivará en 10 segundos");
            await Task.Delay(10000); // Espera los últimos 10 segundos
        }
        else
        {
            //MessageBox.Show($"El usuario se inactivará en {tiempo / 1000} segundos");
            await Task.Delay(tiempo); // Espera el tiempo restante
        }

        // Cambiar estado del usuario y actualizar en la base de datos
        user.CambioDeEstado(false);
        
        controllerUser.updateUser(user);

        MessageBox.Show("El usuario ha sido inactivado correctamente.");
    }


    public static void ActiveUser(User user){
        ControllerUser controllerUser = new ControllerUser(new ServicesUsers(new DBUser(), new DBPerson()));
        user.CambioDeEstado(true);
        user.Estado = true;
        controllerUser.updateUser(user);
        TemporizadorUsuario(user, 60000); // Se restablece 1 minuto el tiempo
    }
}
