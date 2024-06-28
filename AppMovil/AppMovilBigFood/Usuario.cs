using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMovilBigFood
{
    internal class Usuario
    {
        public string Login { get; set; }
        public string Password { get; set; }
        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
        public char Estado { get; set; }
        public bool ConfirmarPassword(string pw)
        {
            bool correcto = false;
            if (string.IsNullOrEmpty(pw))
            {
                correcto = false;
            }
            if (this.Password.Equals(pw))
            {
                correcto = true;
            }
            return correcto;
        }


    }
}
