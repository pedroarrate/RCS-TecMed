using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class MostrarDatos_Controll
    {
        
        public MostrarDatos_Controll() { }
        
        public string MostrarUsuario(int id)
        {
            Controll_USUARIO cu = new Controll_USUARIO();
            cu.IdUsuario = id;

            return cu.ReadId()
                ? $"{cu.ApellidoPaterno} {cu.Nombre}"
                : "Usuario No encontrado";
        }

        public int TotalSociosRegistrados()
        {
            using (var db = new RCSTecMed_Entities())
            {
                return db.SOCIO.Count();
            }

        }
    }
}
