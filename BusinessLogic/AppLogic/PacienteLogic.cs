using DataAccess.Data;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.AppLogic
{
    public class PacienteLogic
    {
        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public PacienteLogic(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public List<Paciente> PatientsList()
        {
            PacienteRepository repPatient = new PacienteRepository(_context);
            return repPatient.GetAllPatients();
        }

        public Paciente GetPatientForId(int id)
        {
            if (id == 0) throw new ArgumentException("Id cannot be 0");

            try
            {
                PacienteRepository repPatient = new PacienteRepository(_context);
                Paciente oPatientFound = repPatient.GetPatientForId(id) ?? throw new ArgumentException("No patient was found with that id");
                return oPatientFound;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void CreateAPatient(Paciente oPatient)
        {
            PacienteRepository repoPatient = new PacienteRepository(_context);

            try
            {
                repoPatient.CreatePatient(oPatient);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }        
        
        public void UpdatePatient(int id, Paciente oPatient)
        {
            PacienteRepository repoPatient = new PacienteRepository(_context);

            try
            {
                Paciente patientFound = repoPatient.GetPatientForId(id) ?? throw new ArgumentException("No patient was found with that id");
                patientFound.Nombre = oPatient.Nombre;
                patientFound.Apellido = oPatient.Apellido;
                patientFound.Dni = oPatient.Dni;
                patientFound.Telefono = oPatient.Telefono;
                patientFound.Email = oPatient.Email;
                patientFound.FechaNacimiento = oPatient.FechaNacimiento;
                patientFound.Password = oPatient.Password;

                repoPatient.UpdatePatient(patientFound);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void DeletePatient(int id)
        {
            PacienteRepository repPatient = new PacienteRepository(_context);

            try
            {
                Paciente oPatientFound = repPatient.GetPatientForId(id) ?? throw new ArgumentException("No patient was found with that id");
                repPatient.DeletePatient(oPatientFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public Paciente GetPatientForDNI(string dni)
        {
            PacienteRepository repPatient = new PacienteRepository(_context);

            try
            {
                Paciente oPatientFound = repPatient.GetPatientForDNI(dni) ?? throw new ArgumentException("No se encuentra un paciente con ese DNI");
                return oPatientFound;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

    }
}
