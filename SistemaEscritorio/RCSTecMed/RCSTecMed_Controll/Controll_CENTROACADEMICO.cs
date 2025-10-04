using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_CENTROACADEMICO
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int IdCentroAcademico { get; set; }
        public string NombreCentroAcademico { get; set; }
        public string EmailCentroAcademico { get; set; }
        public int TelefonoCentroAcademico { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZADOR DE LA CLASE
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdCentroAcademico = 0;
            NombreCentroAcademico = string.Empty;
            EmailCentroAcademico = string.Empty;
            TelefonoCentroAcademico = 0;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            NombreUsuario = string.Empty;
        }

        public Controll_CENTROACADEMICO() { Init(); } //CONSTRUCTOR DE LA CLASE

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
                    CENTRO_ACADEMICO ca = db.CENTRO_ACADEMICO.FirstOrDefault(x => x.IdCentroAcademico == IdCentroAcademico);
                    if (ca == null)
                        return false;

                    CommonDB.Synchronize(ca, this);
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadCentroAcademico() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    CENTRO_ACADEMICO ca = db.CENTRO_ACADEMICO.First(x => x.NombreCentroAcademico == NombreCentroAcademico);
                    if (ca == null)
                        return false;

                    CommonDB.Synchronize(ca, this);
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
                    CENTRO_ACADEMICO ca = db.CENTRO_ACADEMICO.FirstOrDefault(x => x.IdCentroAcademico == IdCentroAcademico);
                    if (ca == null)
                        return false;

                    CommonDB.Synchronize(this, ca);
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
                    CENTRO_ACADEMICO ca = db.CENTRO_ACADEMICO.FirstOrDefault(x => x.IdCentroAcademico == IdCentroAcademico);
                    if (ca == null)
                        return false;

                    db.CENTRO_ACADEMICO.Remove(ca);
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
        public List<Controll_CENTROACADEMICO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CENTRO_ACADEMICO> listaDatos = db.CENTRO_ACADEMICO.ToList<CENTRO_ACADEMICO>();
                List<Controll_CENTROACADEMICO> listaCentroAcademico = GenerarLista(listaDatos);
                return listaCentroAcademico;
            }
            catch (Exception)
            {
                return new List<Controll_CENTROACADEMICO>();
            }
        }

        public List<Controll_CENTROACADEMICO> ReadAllOrdenadoCentroAcademico() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<CENTRO_ACADEMICO> listaDatos = db.CENTRO_ACADEMICO.ToList<CENTRO_ACADEMICO>();
                List<Controll_CENTROACADEMICO> listaCentroAcademico = GenerarLista(listaDatos);
                listaCentroAcademico = listaCentroAcademico.OrderBy(x => x.NombreCentroAcademico).ToList();
                return listaCentroAcademico;
            }
            catch (Exception)
            {
                return new List<Controll_CENTROACADEMICO>();
            }
        }

        private List<Controll_CENTROACADEMICO> GenerarLista(List<CENTRO_ACADEMICO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_CENTROACADEMICO> listaCentroAcademico = new List<Controll_CENTROACADEMICO>();
            foreach (CENTRO_ACADEMICO data in dataList)
            {
                Controll_CENTROACADEMICO ca = new Controll_CENTROACADEMICO();
                CommonDB.Synchronize(data, ca);
                ca.ObtenerUsuario();
                listaCentroAcademico.Add(ca);
            }
            return listaCentroAcademico;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.CENTRO_ACADEMICO
                    .OrderByDescending(x => x.IdCentroAcademico)
                    .Select(x => x.IdCentroAcademico)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }


    }
}
