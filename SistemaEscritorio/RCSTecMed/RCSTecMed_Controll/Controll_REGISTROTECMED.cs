using Microsoft.Win32;
using RCSTecMed_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace RCSTecMed_Controll
{
    public class Controll_REGISTROTECMED
    {
        /*ATRIBUSTOS DE LA CLASE*/
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string IdNacionalidad { get; set; }
        public int IdCentroAcademico { get; set; }
        public int IdCertificaciones { get; set; }
        public DateTime FechaAntecedente { get; set; }
        public int Registro { get; set; }
        public string TipoInstitucion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public int IdCertificacion { get; set; }
        public int IdRegion { get; set; }
        public string Mencion { get; set; }
        public string UniversidadExtranjera { get; set; }
        public string Pais { get; set; }
        public string Especialista { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaNacimientoMostrar { get; set; }
        public string FechaAntecedenteMostrar { get; set; }
        public string FechaInscripcionMostrar { get; set; }
        string NacionalidadAfiliado;
        public string AfiliadoNacionalidad { get { return NacionalidadAfiliado; } }
        string CentroAcademico;
        public string AcedemicoCentro { get { return CentroAcademico; } }
        string NombreCertificacion;
        public string CertificacionNombre { get { return NombreCertificacion; } }
        string CertificacionAfiliado;
        public string AfiliadoCertificacion { get { return CertificacionAfiliado; } }
        string RegionAfiliado;
        public string AfiliadoRegion { get { return RegionAfiliado; } }
        string UsuarioRegistra;
        public string RegistraUsuario { get { return UsuarioRegistra; } }

        private void Init() //INICIALIZACION DE LOS ATRIBUTOS
        {
            /*ATRIBUSTOS DE LA CLASE*/
            Rut = 0;
            Dv = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            Nombres = string.Empty;
            FechaNacimiento = DateTime.Today;
            Sexo = string.Empty;
            IdNacionalidad = string.Empty;
            IdCentroAcademico = 0;
            IdCertificaciones = 0;
            FechaAntecedente = DateTime.Today;
            Registro = 0;
            TipoInstitucion = string.Empty;
            FechaInscripcion = DateTime.Today;
            IdCertificacion = 0;
            IdRegion = 0;
            Mencion = string.Empty;
            UniversidadExtranjera = string.Empty;
            Pais = string.Empty;
            Especialista = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaNacimientoMostrar = string.Empty;
            FechaAntecedenteMostrar = string.Empty;
            FechaInscripcionMostrar = string.Empty;
            NacionalidadAfiliado = string.Empty;
            CentroAcademico = string.Empty;
            NombreCertificacion = string.Empty;
            CertificacionAfiliado = string.Empty;
            RegionAfiliado = string.Empty;
            UsuarioRegistra = string.Empty;
        }

        public Controll_REGISTROTECMED() { Init(); } //CONSTRUCTOR DE LA CLASE

        //METODOS DE LLAMADA COMPLEMENTARIOS
        private void ObtenerNacionalidad()
        {
            var nac = new Controll_NACIONALIDAD { IdNacionalidad = IdNacionalidad };
            NacionalidadAfiliado = nac.ReadId() ? nac.NombreNacionalidad ?? string.Empty : string.Empty;
        }

        private void ObtenerCentroAcademico()
        {
            var ca = new Controll_CENTROACADEMICO { IdCentroAcademico = IdCentroAcademico };
            CentroAcademico = ca.ReadId() ? ca.NombreCentroAcademico ?? string.Empty : string.Empty;
        }

        private void ObtenerNombreCertificacion()
        {
            var cer = new Controll_CERTIFICACIONES { IdCertificaciones = IdCertificaciones };
            NombreCertificacion = cer.ReadId() ? cer.NombreCertificacion ?? string.Empty : string.Empty;
        }

        private void ObtenerCertificacion()
        {
            var cer = new Controll_CERTIFICACION { IdCertificacion = IdCertificacion };
            CertificacionAfiliado = cer.ReadId() ? cer.DescripcionCertificacion ?? string.Empty : string.Empty;
        }

        private void ObtenerRegion()
        {
            var reg = new Controll_REGION { IdRegion = IdRegion };
            RegionAfiliado = reg.ReadId() ? reg.NombreRegion ?? string.Empty : string.Empty;
        }

        private void ObtenerUsuario()
        {
            var us = new Controll_USUARIO { IdUsuario = IdUsuario };
            UsuarioRegistra = us.ReadId() ? us.UserName ?? string.Empty : string.Empty;
        }

        //METODOS DE CRUD
        public bool Create() //CREA REGISTRO QUE SE GRABA EN LA BASE DE DATOS
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                REGISTRO_TECMED rt = new REGISTRO_TECMED();
                try
                {
                    CommonDB.Synchronize(this, rt);
                    db.REGISTRO_TECMED.Add(rt);
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
                    REGISTRO_TECMED rt = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rt == null)
                        return false;

                    CommonDB.Synchronize(rt, this);

                    ObtenerNacionalidad();
                    ObtenerCentroAcademico();
                    ObtenerNombreCertificacion();
                    ObtenerCertificacion();
                    ObtenerRegion();
                    ObtenerUsuario();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReadApPaterno() //BUSCA UN REGISTRO GRABADO EN LA BASE DE DATOS A TRAVEZ DEL ID
        {
            using (RCSTecMed_Entities db = new RCSTecMed_Entities())
            {
                try
                {
                    REGISTRO_TECMED rt = db.REGISTRO_TECMED.FirstOrDefault(x => x.ApellidoPaterno == ApellidoPaterno);
                    if (rt == null)
                        return false;

                    CommonDB.Synchronize(rt, this);

                    ObtenerNacionalidad();
                    ObtenerCentroAcademico();
                    ObtenerNombreCertificacion();
                    ObtenerCertificacion();
                    ObtenerRegion();
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
                    REGISTRO_TECMED rt = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rt == null)
                        return false;

                    CommonDB.Synchronize(this, rt);
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
                    REGISTRO_TECMED rt = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rt == null)
                        return false;

                    db.REGISTRO_TECMED.Remove(rt);
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
        public List<Controll_REGISTROTECMED> ReadAll() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<REGISTRO_TECMED> listaDatos = db.REGISTRO_TECMED.ToList<REGISTRO_TECMED>();
                List<Controll_REGISTROTECMED> listaRegTecMed = GenerarLista(listaDatos);
                return listaRegTecMed;
            }
            catch (Exception)
            {
                return new List<Controll_REGISTROTECMED>();
            }
        }

        public List<Controll_REGISTROTECMED> ListaRegTecMedOrdenado() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA POR DESCRIPCION
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<REGISTRO_TECMED> listaDatos = db.REGISTRO_TECMED.ToList<REGISTRO_TECMED>();
                List<Controll_REGISTROTECMED> listaRegTecMed = GenerarLista(listaDatos);
                listaRegTecMed = listaRegTecMed.OrderBy(x => x.ApellidoPaterno).ToList();
                return listaRegTecMed;
            }
            catch (Exception)
            {
                return new List<Controll_REGISTROTECMED>();
            }
        }

        
        private List<Controll_REGISTROTECMED> GenerarLista(List<REGISTRO_TECMED> dataList) //GENERA LISTA DE REGISTROS DE LA BASE DE DATOS A MOSTRAR
        {
            List<Controll_REGISTROTECMED> listaRegTecMed = new List<Controll_REGISTROTECMED>();
            foreach (REGISTRO_TECMED data in dataList)
            {
                Controll_REGISTROTECMED rtm = new Controll_REGISTROTECMED();
                CommonDB.Synchronize(data, rtm);

                rtm.ObtenerNacionalidad();
                rtm.ObtenerCentroAcademico();
                rtm.ObtenerNombreCertificacion();
                rtm.ObtenerCertificacion();
                rtm.ObtenerRegion();
                rtm.ObtenerUsuario();

                listaRegTecMed.Add(rtm);
            }
            return listaRegTecMed;
        }

    }
}
