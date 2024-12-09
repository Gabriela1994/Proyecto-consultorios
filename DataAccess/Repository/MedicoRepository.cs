using ApiGestionTurnosMedicos.CustomModels;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MedicoRepository
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public MedicoRepository(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public List<Medico> GetAllDoctors()
        {
            List<Medico> doctors = new List<Medico>();
            using (_context)
            {
                doctors = _context.Medicos.ToList();
            }

            return doctors;
        }

        public GestionTurnosContext Get_context()
        {
            return _context;
        }

        public Medico GetDoctorForId(int id, GestionTurnosContext _context)
        {
            Medico oDoctor = new Medico();

            oDoctor = _context.Medicos.Find(id);
            return oDoctor;
        }


        public void CreateDoctor(Medico oDoctor)
        {
            using (_context)
            {
                _context.Add(oDoctor);
                _context.SaveChanges();
            }
        }

        public void UpdateDoctor(Medico oDoctor)
        {
            using (_context)
            {
                _context.Entry(oDoctor).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteDoctor(Medico oDoctor)
        {
            using (_context)
            {
                _context.Medicos.Remove(oDoctor);
                _context.SaveChanges();
            }
        }

        public Medico GetDoctorForDNI(string dni)
        {
            Medico oDoctor = new Medico();

            oDoctor = _context.Medicos.Find(dni);
            return oDoctor;
        }


        public bool VerifyIfDoctorExist(string nombre, string dni)
        {
            return _context.Medicos.Any(d => d.Nombre == nombre && d.Dni == dni);
        }

        public List<Medico> FindDoctorForSpecialty(int id)
        {
            List<Medico> listDoctorForSpecialty  = new List<Medico>();
            using(_context)
            {
                listDoctorForSpecialty = _context.Medicos.Where(o => o.EspecialidadId == id).ToList();
            }

            return listDoctorForSpecialty;
        }

        public bool VerifyIfDoctorExistReturnBool(int id)
        {
            return _context.Medicos.Any(d=> d.Id == id);
        }

        public Medico ReturnHorariosForDoctor(int id)
        {
            Medico oDoctor = new Medico();
            {

                oDoctor = _context.Medicos.Where(m => m.Id == id)
                                            .Select(m => new Medico
                                            {
                                                HorarioAtencionInicio = m.HorarioAtencionInicio,
                                                HorarioAtencionFin = m.HorarioAtencionFin
                                            }).First();
                return oDoctor;
            }
        }

        public List<MedicoCustom> ReturnAllDoctorsWithOurSpecialty()
        {

            MedicoCustom medico = new MedicoCustom();
            List<MedicoCustom> all_doctors = new List<MedicoCustom>();

            all_doctors = (from m in _context.Medicos
                           join e in _context.Especialidades
                           on m.EspecialidadId equals e.Id
                           select new MedicoCustom
                           {
                               Id = m.Id,
                               Nombre = m.Nombre,
                               Apellido = m.Apellido,
                               Telefono = m.Telefono,
                               Dni = m.Dni,
                               Direccion = m.Direccion,
                               FechaAltaLaboral = m.FechaAltaLaboral,
                               HorarioAtencionInicio = m.HorarioAtencionInicio.ToString(@"hh\:mm"),
                               HorarioAtencionFin = m.HorarioAtencionFin.ToString(@"hh\:mm"),
                               Especialidad = e.Nombre
                           }).ToList();

            return all_doctors;
        }

        public MedicoConEspecialidad ReturnDoctorWithSpecialty(int id)
        {
            MedicoConEspecialidad medico = new MedicoConEspecialidad();

            medico = (from m in _context.Medicos
                      join e in _context.Especialidades on m.EspecialidadId equals e.Id
                      where m.Id == id
                      select new MedicoConEspecialidad
                      {
                          Nombre = m.Nombre,
                          Apellido = m.Apellido,
                          Especialidad = e.Nombre
                      }).First();
            return medico;
        }
    }
}