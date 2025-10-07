using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ROLUSUARIO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            IdRol = 0;
            NombreRol = string.Empty;
        }

        public Controll_ROLUSUARIO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS DE CRUD*/
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                ROL_USUARIO rol = new ROL_USUARIO();
                try
                {
                    CommonDB.Synchronize(this, rol);
                    db.ROL_USUARIO.Add(rol);
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
                    ROL_USUARIO rol = db.ROL_USUARIO.FirstOrDefault(x => x.IdRol == IdRol);
                    if (rol == null)
                        return false;

                    CommonDB.Synchronize(rol, this);
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
                    ROL_USUARIO rol = db.ROL_USUARIO.First(x => x.NombreRol == NombreRol);
                    if (rol == null)
                        return false;

                    CommonDB.Synchronize(rol, this);
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
                    ROL_USUARIO rol = db.ROL_USUARIO.FirstOrDefault(x => x.IdRol == IdRol);
                    if (rol == null)
                        return false;

                    CommonDB.Synchronize(this, rol);
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
                    ROL_USUARIO rol = db.ROL_USUARIO.FirstOrDefault(x => x.IdRol == IdRol);
                    if (rol == null)
                        return false;

                    db.ROL_USUARIO.Remove(rol);
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
        public List<Controll_ROLUSUARIO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ROL_USUARIO> listaDatos = db.ROL_USUARIO.ToList<ROL_USUARIO>();
                List<Controll_ROLUSUARIO> listaRol = GenerarLista(listaDatos);
                return listaRol;
            }
            catch (Exception)
            {
                return new List<Controll_ROLUSUARIO>();
            }
        }

        public List<Controll_ROLUSUARIO> ReadAllOrdenadoRol() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ROL_USUARIO> listaDatos = db.ROL_USUARIO.ToList<ROL_USUARIO>();
                List<Controll_ROLUSUARIO> listaRol = GenerarLista(listaDatos);
                listaRol = listaRol.OrderBy(x => x.NombreRol).ToList();
                return listaRol;
            }
            catch (Exception)
            {
                return new List<Controll_ROLUSUARIO>();
            }
        }

        private List<Controll_ROLUSUARIO> GenerarLista(List<ROL_USUARIO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ROLUSUARIO> listaRol = new List<Controll_ROLUSUARIO>();
            foreach (ROL_USUARIO data in dataList)
            {
                Controll_ROLUSUARIO rol = new Controll_ROLUSUARIO();
                CommonDB.Synchronize(data, rol);
                listaRol.Add(rol);
            }
            return listaRol;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ROL_USUARIO
                    .OrderByDescending(x => x.IdRol)
                    .Select(x => x.IdRol)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }


    }
}
