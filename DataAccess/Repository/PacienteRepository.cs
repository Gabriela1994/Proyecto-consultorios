using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PacienteRepository
    {


        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public PacienteRepository(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public List<Paciente> GetAllPatients()
        {
            List<Paciente> patients = new List<Paciente>();
            using (_context)
            {
                patients = _context.Pacientes.ToList();
            }

            return patients;
        }


        public Paciente GetPatientForId(int id)
        {
            Paciente oPatient = new Paciente();

            oPatient = _context.Pacientes.Find(id);
            return oPatient;
        }


        public void CreatePatient(Paciente oPatient)
        {
            using (_context)
            {
                _context.Add(oPatient);
                _context.SaveChanges();
            }
        }

        public void UpdatePatient(Paciente oPatient)
        {
            using (_context)
            {
                _context.Entry(oPatient).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeletePatient(Paciente oPatient)
        {
            using (_context)
            {
                _context.Pacientes.Remove(oPatient);
                _context.SaveChanges();
            }
        }

        public Paciente? GetPatientForDNI(string dni)
        {
            Paciente oPatient = new ();

            oPatient = _context.Pacientes.Where(p => p.Dni == dni).FirstOrDefault();
            return oPatient;
        }


        public bool VerifyIfPatientExist(string nombre, string dni)
        {
            return _context.Pacientes.Any(d => d.Nombre == nombre && d.Dni == dni);
        }

        public List<Paciente> FindPatientForLastName(string lastName)
        {
            List<Paciente> listPatientForLastName = new List<Paciente>();
            using (_context)
            {
                listPatientForLastName= _context.Pacientes.Where(o => o.Apellido == lastName).ToList();
            }

            return listPatientForLastName;
        }

        public bool VerifyIfPatientExistReturnBool(int id)
        {
            return _context.Pacientes.Any(d => d.Id == id);
        }

    }
}
