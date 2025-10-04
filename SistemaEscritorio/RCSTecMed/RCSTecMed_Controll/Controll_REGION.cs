using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_REGION
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public int IdUsuario { get; set; }

        /*VARIBLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdRegion = 0;
            NombreRegion = string.Empty;
            IdUsuario = 0;

            /*VARIBALES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_REGION() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
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
                REGION re = new REGION();
                try
                {
                    CommonDB.Synchronize(this, re);
                    db.REGION.Add(re);
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
                    REGION re = db.REGION.FirstOrDefault(x => x.IdRegion == IdRegion);
                    if (re == null)
                        return false;

                    CommonDB.Synchronize(re, this);
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadRegion() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    REGION re = db.REGION.First(x => x.NombreRegion == NombreRegion);
                    if (re == null)
                        return false;

                    CommonDB.Synchronize(re, this);
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
                    REGION re = db.REGION.FirstOrDefault(x => x.IdRegion == IdRegion);
                    if (re == null)
                        return false;

                    CommonDB.Synchronize(this, re);
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
                    REGION re = db.REGION.FirstOrDefault(x => x.IdRegion == IdRegion);
                    if (re == null)
                        return false;

                    db.REGION.Remove(re);
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
        public List<Controll_REGION> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<REGION> listaDatos = db.REGION.ToList<REGION>();
                List<Controll_REGION> listaRegion = GenerarLista(listaDatos);
                return listaRegion;
            }
            catch (Exception)
            {
                return new List<Controll_REGION>();
            }
        }

        public List<Controll_REGION> ReadAllOrdenadoRegion() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<REGION> listaDatos = db.REGION.ToList<REGION>();
                List<Controll_REGION> listaRegion = GenerarLista(listaDatos);
                listaRegion = listaRegion.OrderBy(x => x.NombreRegion).ToList();
                return listaRegion;
            }
            catch (Exception)
            {
                return new List<Controll_REGION>();
            }
        }

        private List<Controll_REGION> GenerarLista(List<REGION> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_REGION> listaRegion = new List<Controll_REGION>();
            foreach (REGION data in dataList)
            {
                Controll_REGION re = new Controll_REGION();
                CommonDB.Synchronize(data, re);
                re.ObtenerUsuario();
                listaRegion.Add(re);
            }
            return listaRegion;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.REGION
                    .OrderByDescending(x => x.IdRegion)
                    .Select(x => x.IdRegion)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
