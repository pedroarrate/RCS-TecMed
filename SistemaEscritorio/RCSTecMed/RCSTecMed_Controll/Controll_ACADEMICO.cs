using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_ACADEMICO
    {
        /*ATRIBUTOS DE LA TABLA*/
        public int IdAcademcico { get; set; }
        public int Rut { get; set; }
        public int IdCertificacion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCentroAcademico { get; set; }
        public string DescripcionAcademico { get; set; }
        public string FolioRegistroAcademico { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIA*/
        public string FechaRegistro { get; set; }
        string NombreSocio;
        public string _nombresocio { get { return NombreSocio; } }
        string DescripcionCertificacion;
        public string _descCertificacion { get { return DescripcionCertificacion; } }
        string DescripcionCentroAcademico;
        public string _descCentroAcademico { get { return DescripcionCentroAcademico; } }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            /*ATRIBUTOS DE LA TABLA*/
            IdAcademcico = 0;
            Rut = 0;
            IdCertificacion = 0;
            Fecha = DateTime.Today;
            IdCentroAcademico = 0;
            DescripcionAcademico = string.Empty;
            FolioRegistroAcademico = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaRegistro = string.Empty;
            NombreSocio = string.Empty;
            DescripcionCertificacion = string.Empty;
            DescripcionCentroAcademico = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_ACADEMICO() { Init(); } //CONSTRUCTOR DE LA CLASE

        /*METODOS COMPLEMENTARIOS DE LLAMADA DE DATOS*/
        private void ObtenerNombreSocio()
        {
            var sc = new Controll_SOCIO { Rut = Rut };
            NombreSocio = sc.ReadId() ? $"{sc.ApellidoPaterno} {sc.ApellidoMaterno} {sc.Nombres}" ?? string.Empty : string.Empty;
        }

        private void ObtenerCertificacion()
        {
            var cer = new Controll_CERTIFICACION { IdCertificacion = IdCertificacion };
            DescripcionCertificacion = cer.ReadId() ? cer.DescripcionCertificacion ?? string.Empty : string.Empty;
        }

        private void ObtenerCentroAcademico()
        {
            var ca = new Controll_CENTROACADEMICO { IdCentroAcademico = IdCentroAcademico };
            DescripcionCentroAcademico = ca.ReadId() ? ca.NombreCentroAcademico ?? string.Empty : string.Empty;
        }

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
                ACADEMICO ac = new ACADEMICO();
                try
                {
                    CommonDB.Synchronize(this, ac);
                    db.ACADEMICO.Add(ac);
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
                    ACADEMICO ac = db.ACADEMICO.FirstOrDefault(x => x.IdAcademcico == IdAcademcico);
                    if (ac == null)
                        return false;

                    CommonDB.Synchronize(ac, this);
                    ObtenerNombreSocio();
                    ObtenerCertificacion();
                    ObtenerCentroAcademico();
                    ObtenerUsuario();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadFolio() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DE LA DESCRIPCION O NOMBRE DE ESTE
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    ACADEMICO ac = db.ACADEMICO.First(x => x.FolioRegistroAcademico == FolioRegistroAcademico);
                    if (ac == null)
                        return false;

                    CommonDB.Synchronize(ac, this);
                    ObtenerNombreSocio();
                    ObtenerCertificacion();
                    ObtenerCentroAcademico();
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
                    ACADEMICO ac = db.ACADEMICO.FirstOrDefault(x => x.IdAcademcico == IdAcademcico);
                    if (ac == null)
                        return false;

                    CommonDB.Synchronize(this, ac);
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
                    ACADEMICO ac = db.ACADEMICO.FirstOrDefault(x => x.IdAcademcico == IdAcademcico);
                    if (ac == null)
                        return false;

                    db.ACADEMICO.Remove(ac);
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
        public List<Controll_ACADEMICO> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ACADEMICO> listaDatos = db.ACADEMICO.ToList<ACADEMICO>();
                List<Controll_ACADEMICO> listaAcademico = GenerarLista(listaDatos);
                return listaAcademico;
            }
            catch (Exception)
            {
                return new List<Controll_ACADEMICO>();
            }
        }

        public List<Controll_ACADEMICO> ReadAllOrdenadoRut() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<ACADEMICO> listaDatos = db.ACADEMICO.ToList<ACADEMICO>();
                List<Controll_ACADEMICO> listaAcademico = GenerarLista(listaDatos);
                listaAcademico = listaAcademico.OrderBy(x => x.Rut).ToList();
                return listaAcademico;
            }
            catch (Exception)
            {
                return new List<Controll_ACADEMICO>();
            }
        }

        private List<Controll_ACADEMICO> GenerarLista(List<ACADEMICO> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_ACADEMICO> listaAcademico = new List<Controll_ACADEMICO>();
            foreach (ACADEMICO data in dataList)
            {
                Controll_ACADEMICO ac = new Controll_ACADEMICO();
                CommonDB.Synchronize(data, ac);
                ac.FechaRegistro = data.Fecha.ToString("dd-MM-yyyy");
                ac.ObtenerNombreSocio();                
                ac.ObtenerCertificacion();
                ac.ObtenerCentroAcademico();
                ac.ObtenerUsuario();
                listaAcademico.Add(ac);
            }
            return listaAcademico;
        }

        // OTROS METODOS
        public int AsignarId() //GENERA AUTOMATICAMENTE ID O CODIGO DE REGISTRO
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                int ultimoId = db.ACADEMICO
                    .OrderByDescending(x => x.IdAcademcico)
                    .Select(x => x.IdAcademcico)
                    .FirstOrDefault();

                return ultimoId + 1;
            }
        }

    }
}
