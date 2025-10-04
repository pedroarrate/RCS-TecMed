using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_COMUNA
    {
        /*ATRIBUTOS DE LA CLASE*/
        public string IdComuna { get; set; }
        public string NombreComuna { get; set; }
        public int IdRegion { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreRegion;
        public string _nombreRegion { get { return NombreRegion; } }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdComuna = string.Empty;
            NombreComuna = string.Empty;
            IdRegion = 0;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreRegion = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_COMUNA() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerRegion()
        {
            var re = new Controll_REGION { IdRegion = IdRegion };
            NombreRegion = re.ReadId() ? re.NombreRegion ?? string.Empty : string.Empty;
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
                COMUNA com = new COMUNA();
                try
                {
                    CommonDB.Synchronize(this, com);
                    db.COMUNA.Add(com);
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
                    COMUNA com = db.COMUNA.FirstOrDefault(x => x.IdComuna == IdComuna);
                    if (com == null)
                        return false;

                    CommonDB.Synchronize(com, this);
                    ObtenerRegion();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadComuna() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    COMUNA com = db.COMUNA.First(x => x.NombreComuna == NombreComuna);
                    if (com == null)
                        return false;

                    CommonDB.Synchronize(com, this);
                    ObtenerRegion();
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
                    COMUNA com = db.COMUNA.FirstOrDefault(x => x.IdComuna == IdComuna);
                    if (com == null)
                        return false;

                    CommonDB.Synchronize(this, com);
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
                    COMUNA com = db.COMUNA.FirstOrDefault(x => x.IdComuna == IdComuna);
                    if (com == null)
                        return false;

                    db.COMUNA.Remove(com);
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
        public List<Controll_COMUNA> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<COMUNA> listaDatos = db.COMUNA.ToList<COMUNA>();
                List<Controll_COMUNA> listaComuna = GenerarLista(listaDatos);
                return listaComuna;
            }
            catch (Exception)
            {
                return new List<Controll_COMUNA>();
            }
        }

        public List<Controll_COMUNA> ReadAllOrdenadoComuna() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<COMUNA> listaDatos = db.COMUNA.ToList<COMUNA>();
                List<Controll_COMUNA> listaComuna = GenerarLista(listaDatos);
                listaComuna = listaComuna.OrderBy(x => x.NombreComuna).ToList();
                return listaComuna;
            }
            catch (Exception)
            {
                return new List<Controll_COMUNA>();
            }
        }

        private List<Controll_COMUNA> GenerarLista(List<COMUNA> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_COMUNA> listaComuna = new List<Controll_COMUNA>();
            foreach (COMUNA data in dataList)
            {
                Controll_COMUNA com = new Controll_COMUNA();
                CommonDB.Synchronize(data, com);
                com.ObtenerRegion();
                com.ObtenerUsuario();
                listaComuna.Add(com);
            }
            return listaComuna;
        }

    }
}
