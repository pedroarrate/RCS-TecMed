using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_FORMAPAGO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdFormaPagoCuota { get; set; }
        public string DescripcionFormaPagoCuota { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string _nombreUsuario { get {  return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdFormaPagoCuota = 0;
            DescripcionFormaPagoCuota = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_FORMAPAGO() { Init(); } //CONSTRUCTOR DE LA CLASE

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
                FORMAPAGO fp = new FORMAPAGO();
                try
                {
                    CommonDB.Synchronize(this, fp);
                    db.FORMAPAGO.Add(fp);
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
                    FORMAPAGO fp = db.FORMAPAGO.FirstOrDefault(x => x.IdFormaPagoCuota == IdFormaPagoCuota);
                    if (fp == null)
                        return false;

                    CommonDB.Synchronize(fp, this);
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
                    FORMAPAGO fp = db.FORMAPAGO.First(x => x.DescripcionFormaPagoCuota == DescripcionFormaPagoCuota);
                    if (fp == null)
                        return false;

                    CommonDB.Synchronize(fp, this);
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
                    FORMAPAGO fp = db.FORMAPAGO.FirstOrDefault(x => x.IdFormaPagoCuota == IdFormaPagoCuota);
                    if (fp == null)
                        return false;

                    CommonDB.Synchronize(this, fp);
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
                    FORMAPAGO fp = db.FORMAPAGO.FirstOrDefault(x => x.IdFormaPagoCuota == IdFormaPagoCuota);
                    if (fp == null)
                        return false;

                    db.FORMAPAGO.Remove(fp);
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
        public List<Controll_FORMAPAGO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<FORMAPAGO> listaDatos = db.FORMAPAGO.ToList<FORMAPAGO>();
                List<Controll_FORMAPAGO> listaFormaPago = GenerarLista(listaDatos);
                return listaFormaPago;
            }
            catch (Exception)
            {
                return new List<Controll_FORMAPAGO>();
            }
        }

        public List<Controll_FORMAPAGO> ReadAllOrdenadoDesc() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<FORMAPAGO> listaDatos = db.FORMAPAGO.ToList<FORMAPAGO>();
                List<Controll_FORMAPAGO> listaFormaPago = GenerarLista(listaDatos);
                listaFormaPago = listaFormaPago.OrderBy(x => x.DescripcionFormaPagoCuota).ToList();
                return listaFormaPago;
            }
            catch (Exception)
            {
                return new List<Controll_FORMAPAGO>();
            }
        }

        private List<Controll_FORMAPAGO> GenerarLista(List<FORMAPAGO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_FORMAPAGO> listaFormaPago = new List<Controll_FORMAPAGO>();
            foreach (FORMAPAGO data in dataList)
            {
                Controll_FORMAPAGO fp = new Controll_FORMAPAGO();
                CommonDB.Synchronize(data, fp);
                fp.ObtenerUsuario();
                listaFormaPago.Add(fp);
            }
            return listaFormaPago;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.FORMAPAGO
                    .OrderByDescending(x => x.IdFormaPagoCuota)
                    .Select(x => x.IdFormaPagoCuota)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
