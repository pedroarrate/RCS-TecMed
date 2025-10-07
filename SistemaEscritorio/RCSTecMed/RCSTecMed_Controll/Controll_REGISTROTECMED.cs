using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Controll_REGISTROTECMED
    {
        /*ATRIBUTOS DE LA CLASE*/
        public int Rut { get; set; }
        public string Dv { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Nacionalidad { get; set; }
        public string Institucion { get; set; }
        public DateTime FechaAntecedente { get; set; }
        public int Registro { get; set; }
        public string TipoInstitucion { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string NombreTitulo { get; set; }
        public string RegionTrabajo { get; set; }
        public string Mencion { get; set; }
        public string UniversidadExtranjera { get; set; }
        public string Pais { get; set; }
        public string Especialista { get; set; }
        public int IdUsuario { get; set; }

        /*VARIABLES COMPLEMENTARIAS*/
        public string FechaNacimientoMostrar { get; set; }
        public string FechaAntecedenteMostrar { get; set; }
        public string FechaInscripcionMostrar { get; set; }
        string NombreUsuario;
        public string _nombreUsuario { get { return NombreUsuario; } }

        private void Init() //INICIALIZACION DE LA CLASE
        {
            /*ATRIBUTOS DE LA CLASE*/
            Rut = 0;
            Dv = string.Empty;
            ApellidoPaterno = string.Empty;
            ApellidoMaterno = string.Empty;
            Nombres = string.Empty;
            FechaNacimiento = DateTime.Today;
            Sexo = string.Empty;
            Nacionalidad = string.Empty;
            Institucion = string.Empty;
            FechaAntecedente = DateTime.Today;
            Registro = 0;
            TipoInstitucion = string.Empty;
            FechaInscripcion = DateTime.Today;
            NombreTitulo = string.Empty;
            RegionTrabajo = string.Empty;
            Mencion = string.Empty;
            UniversidadExtranjera = string.Empty;
            Pais = string.Empty;
            Especialista = string.Empty;
            IdUsuario = 0;

            /*VARIABLES COMPLEMENTARIAS*/
            FechaNacimientoMostrar = string.Empty;
            FechaAntecedenteMostrar = string.Empty;
            FechaInscripcionMostrar = string.Empty;
            NombreUsuario = string.Empty;
        }

        public Controll_REGISTROTECMED() { Init(); } //CONSTRUCTOR DE LA CLASE

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
                REGISTRO_TECMED rtm = new REGISTRO_TECMED();
                try
                {
                    CommonDB.Synchronize(this, rtm);
                    db.REGISTRO_TECMED.Add(rtm);
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
                    REGISTRO_TECMED rtm = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rtm == null)
                        return false;

                    CommonDB.Synchronize(rtm, this);
                    ObtenerUsuario();
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
                    REGISTRO_TECMED rtm = db.REGISTRO_TECMED.First(x => x.ApellidoPaterno == ApellidoPaterno);
                    if (rtm == null)
                        return false;

                    CommonDB.Synchronize(rtm, this);
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
                    REGISTRO_TECMED rtm = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rtm == null)
                        return false;

                    CommonDB.Synchronize(this, rtm);
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
                    REGISTRO_TECMED rtm = db.REGISTRO_TECMED.FirstOrDefault(x => x.Rut == Rut);
                    if (rtm == null)
                        return false;

                    db.REGISTRO_TECMED.Remove(rtm);
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

        public List<Controll_REGISTROTECMED> ReadAllOrdenadoRut() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
        {
            RCSTecMed_Entities db = new RCSTecMed_Entities();
            try
            {
                List<REGISTRO_TECMED> listaDatos = db.REGISTRO_TECMED.ToList<REGISTRO_TECMED>();
                List<Controll_REGISTROTECMED> listaRegTecMed = GenerarLista(listaDatos);
                listaRegTecMed = listaRegTecMed.OrderBy(x => x.Rut).ToList();
                return listaRegTecMed;
            }
            catch (Exception)
            {
                return new List<Controll_REGISTROTECMED>();
            }
        }

        public List<Controll_REGISTROTECMED> ReadAllOrdenadoApellido() //MUESTRA LISTA DE REGISTROS DE LA BASE DE DATOS ORDENADA 
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
                rtm.FechaNacimientoMostrar = data.FechaNacimiento.ToString("dd-MM-yyyy");
                rtm.FechaAntecedenteMostrar = data.FechaAntecedente.ToString("dd-MM-yyyy");
                rtm.FechaInscripcionMostrar = data.FechaInscripcion.ToString("dd-MM-yyyy");
                rtm.ObtenerUsuario();
                listaRegTecMed.Add(rtm);
            }
            return listaRegTecMed;
        }


    }
}
