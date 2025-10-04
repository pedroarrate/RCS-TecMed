using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ESTABLECIMIENTOACTUAL
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdEstablecimientoActual { get; set; }
        public int IdEstablecimiento { get; set; }
        public int Rut { get; set; }
        public DateTime FechaDesde { get; set; }
        public Nullable<DateTime> FechaHasta { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreEstablecimiento;
        public string _nombreEstablecimiento { get { return NombreEstablecimiento; } }
        string NombreSocio;
        public string _nombreSocio { get { return NombreSocio; } }
        public string FechaEstablecimientoActualDesde {  get; set; }
        public string FechaEstablecimientoActualHasta { get; set; }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdEstablecimientoActual = 0;
            IdEstablecimiento = 0;
            Rut = 0;
            FechaDesde = DateTime.Today;
            FechaHasta = null;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreEstablecimiento = string.Empty;
            NombreSocio = string.Empty;
            FechaEstablecimientoActualDesde = string.Empty;
            FechaEstablecimientoActualHasta = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_ESTABLECIMIENTOACTUAL() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerNombreEstablecimiento()
        {
            var est = new Controll_ESTABLECIMIENTO { IdEstablecimiento = IdEstablecimiento };
            NombreEstablecimiento = est.ReadId() ? est.NombreEstablecimiento ?? string.Empty : string.Empty;
        }

        private void ObtenerNombreSocio()
        {
            var sc = new Controll_SOCIO { Rut = Rut };
            NombreSocio = sc.ReadId() ? $"{sc.ApellidoPaterno} {sc.ApellidoMaterno} {sc.Nombres}" ?? string.Empty : string.Empty;
        }

        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                ESTABLECIMIENTO_ACTUAL ea = new ESTABLECIMIENTO_ACTUAL();
                try
                {
                    CommonDB.Synchronize(this, ea);
                    db.ESTABLECIMIENTO_ACTUAL.Add(ea);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadId() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DEL ID
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTABLECIMIENTO_ACTUAL ea = db.ESTABLECIMIENTO_ACTUAL.FirstOrDefault(x => x.IdEstablecimientoActual == IdEstablecimientoActual);
                    if (ea == null)
                        return false;

                    CommonDB.Synchronize(ea, this);
                    ObtenerNombreEstablecimiento();
                    ObtenerNombreSocio();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Update() //ACTUALIZA UN REGISTRO EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTABLECIMIENTO_ACTUAL ea = db.ESTABLECIMIENTO_ACTUAL.FirstOrDefault(x => x.IdEstablecimientoActual == IdEstablecimientoActual);
                    if (ea == null)
                        return false;

                    CommonDB.Synchronize(this, ea);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Delete() //ELIMINA UN REGISTRO DE LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTABLECIMIENTO_ACTUAL ea = db.ESTABLECIMIENTO_ACTUAL.FirstOrDefault(x => x.IdEstablecimientoActual == IdEstablecimientoActual);
                    if (ea == null)
                        return false;

                    db.ESTABLECIMIENTO_ACTUAL.Remove(ea);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /*METODOS DE LISTADOS*/
        public List<Controll_ESTABLECIMIENTOACTUAL> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTABLECIMIENTO_ACTUAL> listaDatos = db.ESTABLECIMIENTO_ACTUAL.ToList<ESTABLECIMIENTO_ACTUAL>();
                List<Controll_ESTABLECIMIENTOACTUAL> listaEstablecimientoActual = GenerarLista(listaDatos);
                return listaEstablecimientoActual;
            }
            catch (Exception)
            {
                return new List<Controll_ESTABLECIMIENTOACTUAL>();
            }
        }
        
        private List<Controll_ESTABLECIMIENTOACTUAL> GenerarLista(List<ESTABLECIMIENTO_ACTUAL> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ESTABLECIMIENTOACTUAL> listaEstablecimientoActual = new List<Controll_ESTABLECIMIENTOACTUAL>();
            foreach (ESTABLECIMIENTO_ACTUAL data in dataList)
            {
                Controll_ESTABLECIMIENTOACTUAL ea = new Controll_ESTABLECIMIENTOACTUAL();
                CommonDB.Synchronize(data, ea);
                ea.FechaEstablecimientoActualDesde = data.FechaDesde.ToString("dd-MM-yyyy");
                ea.FechaEstablecimientoActualHasta = data.FechaHasta.HasValue? data.FechaHasta.Value.ToString("dd-MM-yyyy") : string.Empty;
                ea.ObtenerNombreEstablecimiento();
                ea.ObtenerNombreSocio();
                ea.ObtenerUsuario();
                listaEstablecimientoActual.Add(ea);
            }
            return listaEstablecimientoActual;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ESTABLECIMIENTO_ACTUAL
                    .OrderByDescending(x => x.IdEstablecimientoActual)
                    .Select(x => x.IdEstablecimientoActual)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
