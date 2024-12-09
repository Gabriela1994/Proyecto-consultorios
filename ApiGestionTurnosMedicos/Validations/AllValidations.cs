using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AllValidations
    {
        public bool EsStringNoVacio(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public bool EsNumeroPositivo(int numero)
        {
            return numero > 0;
        }

        public bool EsFechaPasada(DateTime fecha)
        {
            return fecha <= DateTime.Now;
        }

        public bool EsFormatoEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, patronEmail);
        }

        public bool EsFormatoHoraValido(string hora)
        {
            if (string.IsNullOrWhiteSpace(hora))
            {
                return false;
            }

            string patronHora = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            return Regex.IsMatch(hora, patronHora);
        }

        public bool ExisteEnLaLista<T>(T elemento, T[] lista)
        {
            return Array.Exists(lista, item => item.Equals(elemento));
        }

        public bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Now.AddYears(-edad))
            {
                edad--;
            }
            return edad >= 18;
        }

        public bool EsLongitudValida(string input, int min, int max)
        {
            return input.Length >= min && input.Length <= max;
        }

        public bool EsListaNoVacia<T>(IEnumerable<T> lista)
        {
            return lista != null && lista.Any();
        }

        public bool EsSoloLetras(string input)
        {
            //return Regex.IsMatch(input, @"^[a-zA-Z]+$");
            // Acepta solo letras (incluyendo acentuadas, con diéresis y la "ñ") y espacios.
            return Regex.IsMatch(input, @"^[a-zA-ZÀ-ÿüÜñÑ\s]+$");
        }

        public bool EsSoloNumeros(string input)
        {
            return Regex.IsMatch(input, @"^\d+$");
        }

        public bool EsTelefonoValido(string telefono)
        {
            // Ejemplo para números de teléfono con formato (123) 456-7890
            // Esto es un problema si el teléfono tiene 4 dígitos para la
            // característica (por ejemplo los de Carlos Paz)
            return Regex.IsMatch(telefono, @"^\(\d{3}\) \d{3}-\d{4}$");
        }

        public bool EsFechaEnRango(DateTime fecha, DateTime fechaInicio, DateTime fechaFin)
        {
            return fecha >= fechaInicio && fecha <= fechaFin;
        }

        public bool ContieneLetras(string input)
        {
            return int.TryParse(input, out int ip);
        }

        public bool EsContrasenaSegura(string contrasena)
        {
            return Regex.IsMatch(contrasena, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[~!@#$%^&*()_+=\[{\]};:<>|./?,-]).{8,}$");
        }

        public bool EsFechaNacimientoValida(DateTime fechaNacimiento)
        {
            return fechaNacimiento <= DateTime.Now;
        }
    }


}
