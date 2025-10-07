using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_NACIONALIDAD
    {
        /*ATRIBUTOS DE LA CLASE*/
        public string IdNacionalidad { get; set; }
        public string NombreNacionalidad { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string _nombreUsuario { get {  return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            IdNacionalidad = string.Empty;
            NombreNacionalidad = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_NACIONALIDAD() { Init(); } //CONSTRUCTOR DE LA CLASE

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
                NACIONALIDAD nac = new NACIONALIDAD();
                try
                {
                    CommonDB.Synchronize(this, nac);
                    db.NACIONALIDAD.Add(nac);
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
                    NACIONALIDAD nac = db.NACIONALIDAD.FirstOrDefault(x => x.IdNacionalidad == IdNacionalidad);
                    if (nac == null)
                        return false;

                    CommonDB.Synchronize(nac, this);
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
                    NACIONALIDAD nac = db.NACIONALIDAD.First(x => x.NombreNacionalidad == NombreNacionalidad);
                    if (nac == null)
                        return false;

                    CommonDB.Synchronize(nac, this);
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
                    NACIONALIDAD nac = db.NACIONALIDAD.FirstOrDefault(x => x.IdNacionalidad == IdNacionalidad);
                    if (nac == null)
                        return false;

                    CommonDB.Synchronize(this, nac);
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
                    NACIONALIDAD nac = db.NACIONALIDAD.FirstOrDefault(x => x.IdNacionalidad == IdNacionalidad);
                    if (nac == null)
                        return false;

                    db.NACIONALIDAD.Remove(nac);
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
        public List<Controll_NACIONALIDAD> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<NACIONALIDAD> listaDatos = db.NACIONALIDAD.ToList<NACIONALIDAD>();
                List<Controll_NACIONALIDAD> listaNacionalidad = GenerarLista(listaDatos);
                return listaNacionalidad;
            }
            catch (Exception)
            {
                return new List<Controll_NACIONALIDAD>();
            }
        }

        public List<Controll_NACIONALIDAD> ReadAllOrdenadoNacionalidad() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<NACIONALIDAD> listaDatos = db.NACIONALIDAD.ToList<NACIONALIDAD>();
                List<Controll_NACIONALIDAD> listaNacionalidad = GenerarLista(listaDatos);
                listaNacionalidad = listaNacionalidad.OrderBy(x => x.NombreNacionalidad).ToList();
                return listaNacionalidad;
            }
            catch (Exception)
            {
                return new List<Controll_NACIONALIDAD>();
            }
        }

        private List<Controll_NACIONALIDAD> GenerarLista(List<NACIONALIDAD> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_NACIONALIDAD> listaNacionalidad = new List<Controll_NACIONALIDAD>();
            foreach (NACIONALIDAD data in dataList)
            {
                Controll_NACIONALIDAD nac = new Controll_NACIONALIDAD();
                CommonDB.Synchronize(data, nac);
                nac.ObtenerUsuario();
                listaNacionalidad.Add(nac);
            }
            return listaNacionalidad;
        }

    }
}
