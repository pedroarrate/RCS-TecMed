using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_USUARIO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdUsuario { get; set; }
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int IdEstado { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaCracionMostrar {  get; set; }
        string EstadoUsuario;
        public string _estadoUsuario { get { return EstadoUsuario; } }

        private void Init() //INICILIAZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdUsuario = 0;
            Rut = 0;
            Dv = string.Empty;
            ApellidoPaterno = string.Empty;
            Nombre = string.Empty;
            Email = string.Empty;
            FechaCreacion = DateTime.Today;
            IdEstado = 0;
            UserName = string.Empty;
            Password = string.Empty;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaCracionMostrar = string.Empty;
            EstadoUsuario = string.Empty;
        }

        public Controll_USUARIO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerEstadoUsuario()
        {
            var eu = new Controll_ESTADOUSUARIO { IdEstado = IdEstado };
            EstadoUsuario = eu.ReadId() ? eu.EstadoUsuario ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                USUARIO us = new USUARIO();
                try
                {
                    CommonDB.Synchronize(this, us);
                    db.USUARIO.Add(us);
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
                    USUARIO us = db.USUARIO.FirstOrDefault(x => x.IdUsuario == IdUsuario);
                    if (us == null)
                        return false;

                    CommonDB.Synchronize(us, this);
                    ObtenerEstadoUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadApellido() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    USUARIO us = db.USUARIO.First(x => x.ApellidoPaterno == ApellidoPaterno);
                    if (us == null)
                        return false;

                    CommonDB.Synchronize(us, this);
                    ObtenerEstadoUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadUserName() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    USUARIO us = db.USUARIO.First(x => x.UserName == UserName);
                    if (us == null)
                        return false;

                    CommonDB.Synchronize(us, this);
                    ObtenerEstadoUsuario();
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
                    USUARIO us = db.USUARIO.FirstOrDefault(x => x.IdUsuario == IdUsuario);
                    if (us == null)
                        return false;

                    CommonDB.Synchronize(this, us);
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
                    USUARIO us = db.USUARIO.FirstOrDefault(x => x.IdUsuario == IdUsuario);
                    if (us == null)
                        return false;

                    db.USUARIO.Remove(us);
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
        public List<Controll_USUARIO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO> listaDatos = db.USUARIO.ToList<USUARIO>();
                List<Controll_USUARIO> listaUsuario = GenerarLista(listaDatos);
                return listaUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIO>();
            }
        }

        public List<Controll_USUARIO> ReadAllOrdenadoRut() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO> listaDatos = db.USUARIO.ToList<USUARIO>();
                List<Controll_USUARIO> listaUsuario = GenerarLista(listaDatos);
                listaUsuario = listaUsuario.OrderBy(x => x.Rut).ToList();
                return listaUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIO>();
            }
        }

        public List<Controll_USUARIO> ReadAllOrdenadoApellido() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO> listaDatos = db.USUARIO.ToList<USUARIO>();
                List<Controll_USUARIO> listaUsuario = GenerarLista(listaDatos);
                listaUsuario = listaUsuario.OrderBy(x => x.ApellidoPaterno).ToList();
                return listaUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIO>();
            }
        }

        public List<Controll_USUARIO> ReadAllOrdenadoUserName() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO> listaDatos = db.USUARIO.ToList<USUARIO>();
                List<Controll_USUARIO> listaUsuario = GenerarLista(listaDatos);
                listaUsuario = listaUsuario.OrderBy(x => x.UserName).ToList();
                return listaUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIO>();
            }
        }

        private List<Controll_USUARIO> GenerarLista(List<USUARIO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_USUARIO> listaUsuario = new List<Controll_USUARIO>();
            foreach (USUARIO data in dataList)
            {
                Controll_USUARIO es = new Controll_USUARIO();
                CommonDB.Synchronize(data, es);
                es.ObtenerEstadoUsuario();
                listaUsuario.Add(es);
            }
            return listaUsuario;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.USUARIO
                    .OrderByDescending(x => x.IdUsuario)
                    .Select(x => x.IdUsuario)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }


    }

}

