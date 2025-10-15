using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
using RCSTecMed_Model;

namespace RCSTecMed_Controll
{
    public class Validaciones_Controll
    {
        public Validaciones_Controll() { } //CONSTRUCTOR DE LA CLASE

        public bool CampoVacio(string campo) //VALIDA SI UN CAMPO ESTA VACIO, SI ESTA VACIO RETORNA TRUE
        {
            return string.IsNullOrWhiteSpace(campo);
        }

        public bool CampoSoloTexto(string campo) //VALIDA SI UN CAMPO ES SOLO TEXTO, SI CUMPLE CONDICION RETORNA TRUE
        {
            if (string.IsNullOrWhiteSpace(campo))
                return false;

            // Expresión regular: solo letras (incluye acentos y ñ), y espacios opcionales
            string patron = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";
            return Regex.IsMatch(campo, patron);
        }

        public bool CampoSoloNumero(string campo) //VALIDA SI UN CAMPO ES SOLO NUMEROS, SI CUMPLE CONDICION RETORNA TRUE
        {
            if (string.IsNullOrWhiteSpace(campo))
                return false;

            return double.TryParse(campo, out _);
        }

        public bool CampoSoloNumeroEntero(string campo) //VALIDA SI UN CAMPO ES SOLO NUMEROS ENTEROS, SI CUMPLE CONDICION RETORNA TRUE
        {
            if (string.IsNullOrWhiteSpace(campo))
                return false;

            return int.TryParse(campo, out _);
        }

        public bool FormatoCorreoValido(string campo) //VALIDA SI EL FORMATO DE EMAIL INGRESADO ES VALIDO, SI CUMPLE CONDICION RETORNA TRUE
        {
            if (string.IsNullOrWhiteSpace(campo))
                return false;

            // Patrón que valida correos estándar con subdominios y puntos
            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(campo, patron);
        }

        public int LargoCampo(string campo) //RETORNA EL LARGO DE UN CAMPO, SI ES NULL RETORNA 0, UTILIZABLE PARA VALIDACION DE LARGOS DE CAMPOS
        {
            return campo?.Length ?? 0;
        }

        public string CalcularDV(int rut) //RETORNA EL DV DE UN RUT, UTILIZABLE PARA VALIDAR RUT 
        {
            int suma = 0;
            int multiplicador = 2;

            while (rut > 0)
            {
                int digito = rut % 10;
                suma += digito * multiplicador;
                rut /= 10;
                multiplicador = (multiplicador == 7) ? 2 : multiplicador + 1;
            }

            int resto = suma % 11;
            int dv = 11 - resto;

            return dv == 11 ? "0" :
                   dv == 10 ? "K" :
                   dv.ToString();
        }

        public bool EsRutNumericoValido(string rut, int largoMaximo = 8)
        {
            rut = rut?.Trim();
            return CampoSoloNumero(rut) && LargoCampo(rut) <= largoMaximo;
        }


        public bool FormatoPassword(string password) //VALIDA SI UNA PASS CUMPLE CON UN FORMATO ESPECIFICO Y UN RANGO ESPECIFICO, SI CUMPLE RETORNA TRUE
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 6 || password.Length > 10)
                return false;

            bool tieneMinuscula = password.Any(char.IsLower);
            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneSimbolo = password.Any(c => !char.IsLetterOrDigit(c));

            return tieneMinuscula && tieneMayuscula && tieneSimbolo;
        }

    }
}
