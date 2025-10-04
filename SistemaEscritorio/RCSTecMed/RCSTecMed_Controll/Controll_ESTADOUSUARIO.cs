using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ESTADOUSUARIO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdEstado { get; set; }
        public string EstadoUsuario { get; set; }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            IdEstado = 0;
            EstadoUsuario = string.Empty;
        }

        public Controll_ESTADOUSUARIO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                ESTADO_USUARIO eu = new ESTADO_USUARIO();
                try
                {
                    CommonDB.Synchronize(this, eu);
                    db.ESTADO_USUARIO.Add(eu);
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
                    ESTADO_USUARIO eu = db.ESTADO_USUARIO.FirstOrDefault(x => x.IdEstado == IdEstado);
                    if (eu == null)
                        return false;

                    CommonDB.Synchronize(eu, this);
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
                    ESTADO_USUARIO eu = db.ESTADO_USUARIO.First(x => x.EstadoUsuario == EstadoUsuario);
                    if (eu == null)
                        return false;

                    CommonDB.Synchronize(eu, this);
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
                    ESTADO_USUARIO eu = db.ESTADO_USUARIO.FirstOrDefault(x => x.IdEstado == IdEstado);
                    if (eu == null)
                        return false;

                    CommonDB.Synchronize(this, eu);
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
                    ESTADO_USUARIO eu = db.ESTADO_USUARIO.FirstOrDefault(x => x.IdEstado == IdEstado);
                    if (eu == null)
                        return false;

                    db.ESTADO_USUARIO.Remove(eu);
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
        public List<Controll_ESTADOUSUARIO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTADO_USUARIO> listaDatos = db.ESTADO_USUARIO.ToList<ESTADO_USUARIO>();
                List<Controll_ESTADOUSUARIO> listaEstadoUsuario = GenerarLista(listaDatos);
                return listaEstadoUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_ESTADOUSUARIO>();
            }
        }

        public List<Controll_ESTADOUSUARIO> ReadAllOrdenado() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA POR DESCRIPCION
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ESTADO_USUARIO> listaDatos = db.ESTADO_USUARIO.ToList<ESTADO_USUARIO>();
                List<Controll_ESTADOUSUARIO> listaEstadoUsuario = GenerarLista(listaDatos);
                listaEstadoUsuario = listaEstadoUsuario.OrderBy(x => x.EstadoUsuario).ToList();
                return listaEstadoUsuario;
            }
            catch (Exception)
            {
                return new List<Controll_ESTADOUSUARIO>();
            }
        }

        private List<Controll_ESTADOUSUARIO> GenerarLista(List<ESTADO_USUARIO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ESTADOUSUARIO> listaEstadoUsuario = new List<Controll_ESTADOUSUARIO>();
            foreach (ESTADO_USUARIO data in dataList)
            {
                Controll_ESTADOUSUARIO eu = new Controll_ESTADOUSUARIO();
                CommonDB.Synchronize(data, eu);
                listaEstadoUsuario.Add(eu);
            }
            return listaEstadoUsuario;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ESTADO_USUARIO
                    .OrderByDescending(x => x.IdEstado)
                    .Select(x => x.IdEstado)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }


    }
}
