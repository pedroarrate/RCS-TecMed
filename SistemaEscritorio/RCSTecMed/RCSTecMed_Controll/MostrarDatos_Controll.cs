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
        
        private RCSTecMed_Entities db = new RCSTecMed_Entities();
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
            return db.SOCIO.Count();
        }

        public int TotalCentrosAcademicosRegistrados()
        {
            return db.CENTRO_ACADEMICO.Count();        
        }

        public int TotalTipoCertificacionRegistrados()
        {
            return db.CERTIFICACION.Count();
        }

        public int TotalEstablecimientosRegistrados()
        {
            return db.ESTABLECIMIENTO.Count();
        }
    }
}
