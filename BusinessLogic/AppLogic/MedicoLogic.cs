using ApiGestionTurnosMedicos.CustomModels;
using DataAccess.Data;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.AppLogic
{
    public class MedicoLogic
    {
        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public MedicoLogic(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public List<Medico> DoctorList()
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);
            return repDoctor.GetAllDoctors();
        }

        public Medico GetDoctorForId(int id)
        {
            try
            {
                MedicoRepository repDoctor = new MedicoRepository(_context);
                Medico oDoctorFound = repDoctor.GetDoctorForId(id, repDoctor.Get_context()) ?? throw new ArgumentException("No doctor was found with that id");
                return oDoctorFound;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Processing failed: {e.Message}");
                throw;
            }
        }

        public void CreateDoctor(Medico oDoctor)
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);

            try
            {
                repDoctor.CreateDoctor(oDoctor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }
        

        public void UpdateDoctor(int id, MedicoCustom oDoctor)
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);
            MedicoCustom doctorCustom = new MedicoCustom();

            try
            {
                Medico oDoctorFound = repDoctor.GetDoctorForId(id, repDoctor.Get_context()) ?? throw new ArgumentException("No doctor was found with that id");

                oDoctorFound.Nombre = oDoctor.Nombre;
                oDoctorFound.Apellido = oDoctor.Apellido;
                oDoctorFound.Dni = oDoctor.Dni;
                oDoctorFound.Telefono = oDoctor.Telefono;
                oDoctorFound.Direccion = oDoctor.Direccion;
                oDoctorFound.EspecialidadId = oDoctor.EspecialidadId;
                oDoctorFound.FechaAltaLaboral = oDoctor.FechaAltaLaboral;
                oDoctorFound.HorarioAtencionInicio = doctorCustom.ModifyStartTime(oDoctor.HorarioAtencionInicio);
                oDoctorFound.HorarioAtencionFin = doctorCustom.ModifyEndTime(oDoctor.HorarioAtencionFin);

                repDoctor.UpdateDoctor(oDoctorFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void DeleteDoctor(int id)
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);

            try
            {
                Medico oDoctorFound = repDoctor.GetDoctorForId(id, repDoctor.Get_context()) ?? throw new ArgumentException("No doctor was found with that id");
                repDoctor.DeleteDoctor(oDoctorFound);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public List<Medico> FindDoctorForSpecialty(int id)
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);

            if(id != 0)
            {
                try
                {
                    List<Medico> doctors =  repDoctor.FindDoctorForSpecialty(id);
                    
                    if(doctors.Count == 0)
                    {
                        throw new ArgumentException("Doctor not found");                        
                    }
                    return doctors;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    throw;
                }
            }
            else
            {
                throw new ArgumentException("The id can't be 0");
            }
        }

        public List<MedicoCustom> ReturnAllDoctorsWithOurSpecialty()
        {
            MedicoRepository repDoctor = new MedicoRepository(_context);

            try
            {
                return repDoctor.ReturnAllDoctorsWithOurSpecialty();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

        }
    }
}
