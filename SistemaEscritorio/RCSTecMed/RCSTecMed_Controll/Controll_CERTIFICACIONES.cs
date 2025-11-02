using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_CERTIFICACIONES
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdCertificaciones { get; set; }
        public string NombreCertificacion { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string NomUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdCertificaciones = 0;
            NombreCertificacion = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_CERTIFICACIONES() { Init(); }//CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIA*/
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
                CERTIFICACIONES cer = new CERTIFICACIONES();
                try
                {
                    CommonDB.Synchronize(this, cer);
                    db.CERTIFICACIONES.Add(cer);
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
                    CERTIFICACIONES cer = db.CERTIFICACIONES.FirstOrDefault(x => x.IdCertificaciones == IdCertificaciones);
                    if (cer == null)
                        return false;

                    CommonDB.Synchronize(cer, this);
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
                    CERTIFICACIONES cer = db.CERTIFICACIONES.First(x => x.NombreCertificacion == NombreCertificacion);
                    if (cer == null)
                        return false;

                    CommonDB.Synchronize(cer, this);
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
                    CERTIFICACIONES cer = db.CERTIFICACIONES.FirstOrDefault(x => x.IdCertificaciones == IdCertificaciones);
                    if (cer == null)
                        return false;

                    CommonDB.Synchronize(this, cer);
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
                    CERTIFICACIONES cer = db.CERTIFICACIONES.FirstOrDefault(x => x.IdCertificaciones == IdCertificaciones);
                    if (cer == null)
                        return false;

                    db.CERTIFICACIONES.Remove(cer);
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
        public List<Controll_CERTIFICACIONES> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACIONES> listaDatos = db.CERTIFICACIONES.ToList<CERTIFICACIONES>();
                List<Controll_CERTIFICACIONES> listaCertificaciones = GenerarLista(listaDatos);
                return listaCertificaciones;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACIONES>();
            }
        }

        public List<Controll_CERTIFICACIONES> ListaNombreCertificacionesOrdenadaDescrpcion() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACIONES> listaDatos = db.CERTIFICACIONES.ToList<CERTIFICACIONES>();
                List<Controll_CERTIFICACIONES> listaCertificaciones = GenerarLista(listaDatos);
                listaCertificaciones = listaCertificaciones.OrderBy(x => x.NombreCertificacion).ToList();
                return listaCertificaciones;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACIONES>();
            }
        }

        public List<Controll_CERTIFICACIONES> ListaNombreCertificacionporId(int id) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACIONES> listaDatos = db.CERTIFICACIONES.Where(x => x.IdCertificaciones == id).ToList<CERTIFICACIONES>();
                List<Controll_CERTIFICACIONES> listaCertificaciones = GenerarLista(listaDatos);
                return listaCertificaciones;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACIONES>();
            }
        }

        public List<Controll_CERTIFICACIONES> ListaNombreCertificacionporDescripcion(string desc) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACIONES> listaDatos = db.CERTIFICACIONES.Where(x => x.NombreCertificacion == desc).ToList<CERTIFICACIONES>();
                List<Controll_CERTIFICACIONES> listaCertificaciones = GenerarLista(listaDatos);
                return listaCertificaciones;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACIONES>();
            }
        }

        private List<Controll_CERTIFICACIONES> GenerarLista(List<CERTIFICACIONES> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_CERTIFICACIONES> listaCertificaciones = new List<Controll_CERTIFICACIONES>();
            foreach (CERTIFICACIONES data in dataList)
            {
                Controll_CERTIFICACIONES cer = new Controll_CERTIFICACIONES();
                CommonDB.Synchronize(data, cer);
                cer.ObtenerUsuario();
                listaCertificaciones.Add(cer);
            }
            return listaCertificaciones;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.CERTIFICACIONES
                    .OrderByDescending(x => x.IdCertificaciones)
                    .Select(x => x.IdCertificaciones)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
