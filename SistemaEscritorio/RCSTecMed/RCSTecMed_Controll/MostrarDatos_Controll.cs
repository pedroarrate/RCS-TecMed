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
        
        private readonly RCSTecMed_Entities db = new RCSTecMed_Entities();
        public MostrarDatos_Controll() { }
        
        public string MostrarUsuario(int id)
        {
            Controll_USUARIO cu = new Controll_USUARIO() { IdUsuario = id };
            
            return cu.ReadId()
                ? $"{cu.ApellidoPaterno} {cu.Nombre}"
                : "Usuario No encontrado";
        }

        public int IdRegionxComuna(string comuna)
        {
            var reg = new Controll_COMUNA { IdComuna = comuna };
            return reg.ReadId() ? reg.IdRegion : 0;

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

        public int TotalEstablecimientosRegistradosPorRegion(int reg)
        {
            return db.ESTABLECIMIENTO.Count(x => x.Region == reg);
        }

        public int TotalEstablecimientosRegistradosPorRegionComuna(string com)
        {
            return db.ESTABLECIMIENTO.Count(x => x.IdComuna == com);
        }

        public int TotalFormaPagoRegistrado()
        {
            return db.FORMAPAGO.Count();
        }

        public int TotalFechaEstimaPagoRegistrado()
        {
            return db.ESTIMADOPAGO.Count();
        }

        public int TotalNombreCertificaciones()
        {
            return db.CERTIFICACIONES.Count();
        }
    }
}
