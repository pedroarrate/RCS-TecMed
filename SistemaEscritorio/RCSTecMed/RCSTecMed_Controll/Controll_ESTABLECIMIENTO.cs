using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ESTABLECIMIENTO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdEstablecimiento { get; set; }
        public string NombreEstablecimiento { get; set; }
        public string IdComuna { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Telefono { get; set; }
        public string Email { get; set; }
        public string NombreContacto { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreComuna;
        public string _nombreComuna { get { return NombreComuna; } }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CALSE*/
            IdEstablecimiento = 0;
            NombreEstablecimiento = string.Empty;
            IdComuna = string.Empty;
            Direccion = string.Empty;
            Telefono = null;
            Email = string.Empty;
            NombreContacto = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreComuna = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_ESTABLECIMIENTO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerComuna()
        {
            var com = new Controll_COMUNA { IdComuna = IdComuna };
            NombreComuna = com.ReadId() ? com.NombreComuna ?? string.Empty : string.Empty;
        }

        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        public bool ReadId() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DEL ID
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTABLECIMIENTO est = db.ESTABLECIMIENTO.FirstOrDefault(x => x.IdEstablecimiento == IdEstablecimiento);
                    if (est == null)
                        return false;

                    CommonDB.Synchronize(est, this);
                    ObtenerComuna();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadEstablecimiento() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTABLECIMIENTO est = db.ESTABLECIMIENTO.First(x => x.NombreEstablecimiento == NombreEstablecimiento);
                    if (est == null)
                        return false;

                    CommonDB.Synchronize(est, this);
                    ObtenerComuna();
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
                    ESTABLECIMIENTO est = db.ESTABLECIMIENTO.FirstOrDefault(x => x.IdEstablecimiento == IdEstablecimiento);
                    if (est == null)
                        return false;

                    CommonDB.Synchronize(this, est);
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
                    ESTABLECIMIENTO est = db.ESTABLECIMIENTO.FirstOrDefault(x => x.IdEstablecimiento == IdEstablecimiento);
                    if (est == null)
                        return false;

                    db.ESTABLECIMIENTO.Remove(est);
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
        public List<Controll_ESTABLECIMIENTO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTABLECIMIENTO> listaDatos = db.ESTABLECIMIENTO.ToList<ESTABLECIMIENTO>();
                List<Controll_ESTABLECIMIENTO> listaEstablecimiento = GenerarLista(listaDatos);
                return listaEstablecimiento;
            }
            catch (Exception)
            {
                return new List<Controll_ESTABLECIMIENTO>();
            }
        }

        public List<Controll_ESTABLECIMIENTO> ReadAllOrdenadoEstablecimiento() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTABLECIMIENTO> listaDatos = db.ESTABLECIMIENTO.ToList<ESTABLECIMIENTO>();
                List<Controll_ESTABLECIMIENTO> listaEstablecimiento = GenerarLista(listaDatos);
                listaEstablecimiento = listaEstablecimiento.OrderBy(x => x.NombreEstablecimiento).ToList();
                return listaEstablecimiento;
            }
            catch (Exception)
            {
                return new List<Controll_ESTABLECIMIENTO>();
            }
        }

        private List<Controll_ESTABLECIMIENTO> GenerarLista(List<ESTABLECIMIENTO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ESTABLECIMIENTO> listaEstablecimiento = new List<Controll_ESTABLECIMIENTO>();
            foreach (ESTABLECIMIENTO data in dataList)
            {
                Controll_ESTABLECIMIENTO est = new Controll_ESTABLECIMIENTO();
                CommonDB.Synchronize(data, est);
                est.ObtenerComuna();
                est.ObtenerUsuario();
                listaEstablecimiento.Add(est);
            }
            return listaEstablecimiento;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ESTABLECIMIENTO
                    .OrderByDescending(x => x.IdEstablecimiento)
                    .Select(x => x.IdEstablecimiento)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
