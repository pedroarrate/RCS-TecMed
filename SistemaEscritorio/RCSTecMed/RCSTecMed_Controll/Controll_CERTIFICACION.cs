using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_CERTIFICACION
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdCertificacion { get; set; }
        public string DescripcionCertificacion { get; set; }
        public int IdUsuario { get; set; }
        
        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string UsuarioNombre { get { return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdCertificacion = 0;
            DescripcionCertificacion = string.Empty;
            IdUsuario = 0;            

            /*VARIBLAES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_CERTIFICACION() { Init(); } //CONSTRUCTOR DE LA CLASE

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
                CERTIFICACION cer = new CERTIFICACION();
                try
                {
                    CommonDB.Synchronize(this, cer);
                    db.CERTIFICACION.Add(cer);
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
                    CERTIFICACION cer = db.CERTIFICACION.FirstOrDefault(x => x.IdCertificacion == IdCertificacion);
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
                    CERTIFICACION cer = db.CERTIFICACION.First(x => x.DescripcionCertificacion == DescripcionCertificacion);
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
                    CERTIFICACION cer = db.CERTIFICACION.FirstOrDefault(x => x.IdCertificacion == IdCertificacion);
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
                    CERTIFICACION cer = db.CERTIFICACION.FirstOrDefault(x => x.IdCertificacion == IdCertificacion);
                    if (cer == null)
                        return false;

                    db.CERTIFICACION.Remove(cer);
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
        public List<Controll_CERTIFICACION> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACION> listaDatos = db.CERTIFICACION.ToList<CERTIFICACION>();
                List<Controll_CERTIFICACION> listaCertificacion = GenerarLista(listaDatos);
                return listaCertificacion;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACION>();
            }
        }

        public List<Controll_CERTIFICACION> ListaTipoCertificacionOrdenadaDescrpcion() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACION> listaDatos = db.CERTIFICACION.ToList<CERTIFICACION>();
                List<Controll_CERTIFICACION> listaCertificacion = GenerarLista(listaDatos);
                listaCertificacion = listaCertificacion.OrderBy(x => x.DescripcionCertificacion).ToList();
                return listaCertificacion;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACION>();
            }
        }

        public List<Controll_CERTIFICACION> ListaTipoCertificacionporId(int id) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACION> listaDatos = db.CERTIFICACION.Where(x => x.IdCertificacion == id).ToList<CERTIFICACION>();
                List<Controll_CERTIFICACION> listaCertificacion = GenerarLista(listaDatos);
                return listaCertificacion;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACION>();
            }
        }

        public List<Controll_CERTIFICACION> ListaTipoCertificacionporDescripcion(string desc) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CERTIFICACION> listaDatos = db.CERTIFICACION.Where(x => x.DescripcionCertificacion == desc).ToList<CERTIFICACION>();
                List<Controll_CERTIFICACION> listaCertificacion = GenerarLista(listaDatos);
                return listaCertificacion;
            }
            catch (Exception)
            {
                return new List<Controll_CERTIFICACION>();
            }
        }

        private List<Controll_CERTIFICACION> GenerarLista(List<CERTIFICACION> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_CERTIFICACION> listaCertificacion = new List<Controll_CERTIFICACION>();
            foreach (CERTIFICACION data in dataList)
            {
                Controll_CERTIFICACION cer = new Controll_CERTIFICACION();
                CommonDB.Synchronize(data, cer);
                cer.ObtenerUsuario();
                listaCertificacion.Add(cer);
            }
            return listaCertificacion;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.CERTIFICACION
                    .OrderByDescending(x => x.IdCertificacion)
                    .Select(x => x.IdCertificacion)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
