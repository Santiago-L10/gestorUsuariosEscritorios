using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorUsuarios.models;

namespace GestorUsuarios.services
{
    public static class SessionUser
    {
        public static User currentUser { get; private set; }

        public static void Login(User user) {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            currentUser = user;
        }

        
    }
}
