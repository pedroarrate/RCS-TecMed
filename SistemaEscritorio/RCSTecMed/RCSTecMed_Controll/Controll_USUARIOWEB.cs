using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_USUARIOWEB
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdUsuarioWeb { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }
        string NombreRol;
        public string _nombreRol { get { return NombreRol; } }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdUsuarioWeb = 0;
            IdUsuario = 0;
            IdRol = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
            NombreRol = string.Empty;
        }

        public Controll_USUARIOWEB() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        private void ObtenerRol()
        {
            var rol = new Controll_ROLUSUARIO { IdRol = IdRol };
            NombreRol = rol.ReadId() ? rol.NombreRol ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                USUARIO_WEB usw = new USUARIO_WEB();
                try
                {
                    CommonDB.Synchronize(this, usw);
                    db.USUARIO_WEB.Add(usw);
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
                    USUARIO_WEB usw = db.USUARIO_WEB.FirstOrDefault(x => x.IdUsuarioWeb == IdUsuarioWeb);
                    if (usw == null)
                        return false;

                    CommonDB.Synchronize(usw, this);
                    ObtenerUsuario();
                    ObtenerRol();
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
                    USUARIO_WEB usw = db.USUARIO_WEB.FirstOrDefault(x => x.IdUsuarioWeb == IdUsuarioWeb);
                    if (usw == null)
                        return false;

                    CommonDB.Synchronize(this, usw);
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
                    USUARIO_WEB us = db.USUARIO_WEB.FirstOrDefault(x => x.IdUsuarioWeb == IdUsuarioWeb);
                    if (us == null)
                        return false;

                    db.USUARIO_WEB.Remove(us);
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
        public List<Controll_USUARIOWEB> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO_WEB> listaDatos = db.USUARIO_WEB.ToList<USUARIO_WEB>();
                List<Controll_USUARIOWEB> listaUsuarioDesk = GenerarLista(listaDatos);
                return listaUsuarioDesk;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIOWEB>();
            }
        }

        private List<Controll_USUARIOWEB> GenerarLista(List<USUARIO_WEB> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_USUARIOWEB> listaUsuarioDesk = new List<Controll_USUARIOWEB>();
            foreach (USUARIO_WEB data in dataList)
            {
                Controll_USUARIOWEB usw = new Controll_USUARIOWEB();
                CommonDB.Synchronize(data, usw);
                usw.ObtenerUsuario();
                usw.ObtenerRol();
                listaUsuarioDesk.Add(usw);
            }
            return listaUsuarioDesk;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.USUARIO_WEB
                    .OrderByDescending(x => x.IdUsuarioWeb)
                    .Select(x => x.IdUsuarioWeb)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }
    }
}
