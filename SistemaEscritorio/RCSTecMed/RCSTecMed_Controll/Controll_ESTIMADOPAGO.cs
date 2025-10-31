using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ESTIMADOPAGO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdEstimadoPago { get; set; }
        public string DescripcionEstimadoPago { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/

        string NombreUsuario;
        public string UsuarioNombre { get { return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            IdEstimadoPago = 0;
            DescripcionEstimadoPago = string.Empty;
            IdUsuario = 0;

            NombreUsuario = string.Empty;
        }

        public Controll_ESTIMADOPAGO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerUsuario()
        {
            var eu = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = eu.ReadId() ? eu.UserName ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                ESTIMADOPAGO ep = new ESTIMADOPAGO();
                try
                {
                    CommonDB.Synchronize(this, ep);
                    db.ESTIMADOPAGO.Add(ep);
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
                    ESTIMADOPAGO ep = db.ESTIMADOPAGO.FirstOrDefault(x => x.IdEstimadoPago == IdEstimadoPago);
                    if (ep == null)
                        return false;

                    CommonDB.Synchronize(ep, this);
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadDesc() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ESTIMADOPAGO ep = db.ESTIMADOPAGO.First(x => x.DescripcionEstimadoPago == DescripcionEstimadoPago);
                    if (ep == null)
                        return false;

                    CommonDB.Synchronize(ep, this);
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
                    ESTIMADOPAGO ep = db.ESTIMADOPAGO.FirstOrDefault(x => x.IdEstimadoPago == IdEstimadoPago);
                    if (ep == null)
                        return false;

                    CommonDB.Synchronize(this, ep);
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
                    ESTIMADOPAGO ep = db.ESTIMADOPAGO.FirstOrDefault(x => x.IdEstimadoPago == IdEstimadoPago);
                    if (ep == null)
                        return false;

                    db.ESTIMADOPAGO.Remove(ep);
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
        public List<Controll_ESTIMADOPAGO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTIMADOPAGO> listaDatos = db.ESTIMADOPAGO.ToList<ESTIMADOPAGO>();
                List<Controll_ESTIMADOPAGO> listaEstimadoPago = GenerarLista(listaDatos);
                return listaEstimadoPago;
            }
            catch (Exception)
            {
                return new List<Controll_ESTIMADOPAGO>();
            }
        }

        public List<Controll_ESTIMADOPAGO> ListaEstimadoPagoOrdenado() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA POR DESCRIPCION
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTIMADOPAGO> listaDatos = db.ESTIMADOPAGO.ToList<ESTIMADOPAGO>();
                List<Controll_ESTIMADOPAGO> listaEstimadoPago = GenerarLista(listaDatos);
                listaEstimadoPago = listaEstimadoPago.OrderBy(x => x.DescripcionEstimadoPago).ToList();
                return listaEstimadoPago;
            }
            catch (Exception)
            {
                return new List<Controll_ESTIMADOPAGO>();
            }
        }

        public List<Controll_ESTIMADOPAGO> ListaEstimadoPagoId(int id) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA POR DESCRIPCION
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTIMADOPAGO> listaDatos = db.ESTIMADOPAGO.Where(x => x.IdEstimadoPago == id).ToList<ESTIMADOPAGO>();
                List<Controll_ESTIMADOPAGO> listaEstimadoPago = GenerarLista(listaDatos);
                listaEstimadoPago = listaEstimadoPago.OrderBy(x => x.DescripcionEstimadoPago).ToList();
                return listaEstimadoPago;
            }
            catch (Exception)
            {
                return new List<Controll_ESTIMADOPAGO>();
            }
        }

        public List<Controll_ESTIMADOPAGO> ListaEstimadoPagoDesc(string desc) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA POR DESCRIPCION
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTIMADOPAGO> listaDatos = db.ESTIMADOPAGO.Where(x => x.DescripcionEstimadoPago == desc).ToList<ESTIMADOPAGO>();
                List<Controll_ESTIMADOPAGO> listaEstimadoPago = GenerarLista(listaDatos);
                listaEstimadoPago = listaEstimadoPago.OrderBy(x => x.DescripcionEstimadoPago).ToList();
                return listaEstimadoPago;
            }
            catch (Exception)
            {
                return new List<Controll_ESTIMADOPAGO>();
            }
        }
        private List<Controll_ESTIMADOPAGO> GenerarLista(List<ESTIMADOPAGO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ESTIMADOPAGO> listaEstimadoPago = new List<Controll_ESTIMADOPAGO>();
            foreach (ESTIMADOPAGO data in dataList)
            {
                Controll_ESTIMADOPAGO es = new Controll_ESTIMADOPAGO();
                CommonDB.Synchronize(data, es);
                es.ObtenerUsuario();

                listaEstimadoPago.Add(es);
            }
            return listaEstimadoPago;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ESTIMADOPAGO
                    .OrderByDescending(x => x.IdEstimadoPago)
                    .Select(x => x.IdEstimadoPago)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }
    }
}
