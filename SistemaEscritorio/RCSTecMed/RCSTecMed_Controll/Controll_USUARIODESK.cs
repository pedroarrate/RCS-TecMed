using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_USUARIODESK
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdUsuarioDesk { get; set; }
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
            IdUsuarioDesk = 0;
            IdUsuario = 0;
            IdRol = 0;

        /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
            NombreRol = string.Empty;
        }

        public Controll_USUARIODESK() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE LLAMADA COMPLEMENTARIOS*/
        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            NombreUsuario = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        private void ObtenerRol()
        {
            var rol = new Controll_ROLUSUARIO { IdRol = IdRol };
            NombreRol = rol.ReadId() ? $"Perfil: {rol.NombreRol}" ?? string.Empty : string.Empty;
        }

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                USUARIO_DESK usd = new USUARIO_DESK();
                try
                {
                    CommonDB.Synchronize(this, usd);
                    db.USUARIO_DESK.Add(usd);
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
                    USUARIO_DESK usd = db.USUARIO_DESK.FirstOrDefault(x => x.IdUsuarioDesk == IdUsuarioDesk);
                    if (usd == null)
                        return false;

                    CommonDB.Synchronize(usd, this);
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

        public bool ReadIdUsuario() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DEL ID
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    USUARIO_DESK usd = db.USUARIO_DESK.FirstOrDefault(x => x.IdUsuario == IdUsuario;
                    if (usd == null)
                        return false;

                    CommonDB.Synchronize(usd, this);
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
                    USUARIO_DESK usd = db.USUARIO_DESK.FirstOrDefault(x => x.IdUsuarioDesk == IdUsuarioDesk);
                    if (usd == null)
                        return false;

                    CommonDB.Synchronize(this, usd);
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
                    USUARIO_DESK us = db.USUARIO_DESK.FirstOrDefault(x => x.IdUsuarioDesk == IdUsuarioDesk);
                    if (us == null)
                        return false;

                    db.USUARIO_DESK.Remove(us);
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
        public List<Controll_USUARIODESK> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO_DESK> listaDatos = db.USUARIO_DESK.ToList<USUARIO_DESK>();
                List<Controll_USUARIODESK> listaUsuarioDesk = GenerarLista(listaDatos);
                return listaUsuarioDesk;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIODESK>();
            }
        }

        public List<Controll_USUARIODESK> ReadAllIdUsuario(int idUser) //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<USUARIO_DESK> listaDatos = db.USUARIO_DESK.Where(x => x.IdUsuario == idUser).ToList<USUARIO_DESK>();
                List<Controll_USUARIODESK> listaUsuarioDesk = GenerarLista(listaDatos);
                return listaUsuarioDesk;
            }
            catch (Exception)
            {
                return new List<Controll_USUARIODESK>();
            }
        }

        private List<Controll_USUARIODESK> GenerarLista(List<USUARIO_DESK> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_USUARIODESK> listaUsuarioDesk = new List<Controll_USUARIODESK>();
            foreach (USUARIO_DESK data in dataList)
            {
                Controll_USUARIODESK usd = new Controll_USUARIODESK();
                CommonDB.Synchronize(data, usd);
                usd.ObtenerUsuario();
                usd.ObtenerRol();
                listaUsuarioDesk.Add(usd);
            }
            return listaUsuarioDesk;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.USUARIO_DESK
                    .OrderByDescending(x => x.IdUsuarioDesk)
                    .Select(x => x.IdUsuarioDesk)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
