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

        public string MostrarNombreSocio(int rut)
        {
            Controll_SOCIO soc = new Controll_SOCIO() { Rut = rut };

            return soc.ReadId()
                ? $"{soc.ApellidoPaterno} {soc.ApellidoMaterno} {soc.Nombres}"
                : "Socio No encontrado";
        }

        public string MostrarEstablecimiento(int id)
        {
            Controll_ESTABLECIMIENTO es = new Controll_ESTABLECIMIENTO() { IdEstablecimiento = id };

            return es.ReadId()
                ? $"{es.NombreEstablecimiento}"
                : "Establecimiento No encontrado";
        }

        public string MostrarComuna(string id)
        {
            Controll_COMUNA com = new Controll_COMUNA() { IdComuna = id };

            return com.ReadId()
                ? $"{com.NombreComuna}"
                : "Comuna No encontrado";
        }

        public string MostrarComunaPorIdEstablecimiento(int id)
        {
            var est = new Controll_ESTABLECIMIENTO { IdEstablecimiento = id };

            if (!est.ReadId())
                return "Establecimiento no encontrado";

            var comuna = new Controll_COMUNA { IdComuna = est.IdComuna };

            return comuna.ReadId()
                ? comuna.NombreComuna
                : "Comuna no encontrada";
        }

        public string MostrarRegionPorIdEstablecimiento(int id)
        {
            var est = new Controll_ESTABLECIMIENTO { IdEstablecimiento = id };

            if (!est.ReadId())
                return "Establecimiento no encontrado";

            var comuna = new Controll_COMUNA { IdComuna = est.IdComuna };

            if (!comuna.ReadId())
                return "Comuna no encontrada";

            var region = new Controll_REGION { IdRegion = comuna.IdRegion };
            return region.ReadId()
                ? region.NombreRegion
                : "Región no encontrada";
        }

        public string MostrarRegion(int id)
        {
            
            var region = new Controll_REGION { IdRegion = id };
            return region.ReadId()
                ? region.NombreRegion
                : "Región no encontrada";
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

        public int TotalEstablecimientoActualRegistrados()
        {
            return db.ESTABLECIMIENTO_ACTUAL.Count();
        }

        public int TotalEstablecimientoActualPorRutRegistrados(int rut)
        {
            return db.ESTABLECIMIENTO_ACTUAL.Count(x => x.Rut == rut);
        }
    }
}
