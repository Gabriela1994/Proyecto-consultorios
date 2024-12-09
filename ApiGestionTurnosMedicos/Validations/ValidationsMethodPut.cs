using ApiGestionTurnosMedicos.CustomModels;
using BusinessLogic;
using DataAccess.Data;
using DataAccess.Repository;
using Models.CustomModels;

namespace ApiGestionTurnosMedicos.Validations
{
    public class ValidationsMethodPut
    {

        #region ContextDataBase
        private readonly GestionTurnosContext _context;

        public ValidationsMethodPut(GestionTurnosContext context)
        {
            _context = context;
        }
        #endregion

        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationsMethodPut()
        {

        }

        public ValidationsMethodPut ValidationsMethodPutDoctor(MedicoCustom oDoctor)
        {
            MedicoRepository repoDoctor = new MedicoRepository(_context);
            EspecialidadRepository repSpecialty = new EspecialidadRepository(_context);
            AllValidations validations = new AllValidations();
            Medico doctor = new Medico();

            doctor.HorarioAtencionInicio = oDoctor.ModifyStartTime(oDoctor.HorarioAtencionInicio);
            doctor.HorarioAtencionFin = oDoctor.ModifyEndTime(oDoctor.HorarioAtencionFin);
            try
            {
                #region Validaciones de existencia

                if (!repSpecialty.VerifyIfSpecialtyExist(oDoctor.EspecialidadId)) throw new ArgumentException("Especialidad no encontrada.");

                #endregion

                #region Validaciones de campo

                if (!validations.EsStringNoVacio(oDoctor.Nombre)) throw new ArgumentException("El Nombre no puede quedar vacío.");
                if (!validations.EsStringNoVacio(oDoctor.Dni)) throw new ArgumentException("EL DNI no puede quedar vacío.");

                #endregion

                #region Validaciones lógicas
                if (!validations.EsSoloLetras(oDoctor.Nombre)) throw new ArgumentException("El Nombre solo puede contener letras y espacios.");
                if (!validations.EsSoloLetras(oDoctor.Apellido)) throw new ArgumentException("El Apellido solo puede contener letras y espacios.");
                if (!validations.EsSoloNumeros(oDoctor.Dni)) throw new ArgumentException("El DNI solo puede contener números.");
                if (!validations.EsSoloNumeros(oDoctor.Telefono)) throw new ArgumentException("El Teléfono solo puede contener números.");

                if (doctor.HorarioAtencionInicio < new TimeSpan(7, 0, 0) || doctor.HorarioAtencionInicio > new TimeSpan(18, 0, 0))
                {
                    throw new ArgumentException("El Inicio de Atención debe estar entre las 07:00 y las 18:00");
                }
                if (oDoctor.FechaAltaLaboral > DateTime.Now) throw new ArgumentException("La Fecha de Alta Laboral no puede ser en el futuro.");

                if (oDoctor.EspecialidadId == 0) throw new ArgumentException("El ID de la Especialidad no puede ser cero.");

                #endregion


                return new ValidationsMethodPut { IsValid = true };
            }
            catch (Exception e)
            {
                return new ValidationsMethodPut { IsValid = false, ErrorMessage = e.Message };
            }
        }


        public ValidationsMethodPut ValidationsMethodPutPatient(Paciente oPatient)
        {
            AllValidations validations = new AllValidations();

            try
            {
                PacienteRepository repoPatient = new PacienteRepository(_context);

                #region Validaciones de campo

                if (!validations.EsStringNoVacio(oPatient.Nombre)) throw new ArgumentException("El Nombre no puede quedar vacío.");
                if (!validations.EsStringNoVacio(oPatient.Apellido)) throw new ArgumentException("El Apellido no puede quedar vacío");
                if (!validations.EsStringNoVacio((oPatient.FechaNacimiento).ToString())) throw new ArgumentException("La Fecha de Nacimiento  no puede quedar vacía.");
                if (!validations.EsStringNoVacio(oPatient.Email)) throw new ArgumentException("El Email no puede quedar vacío.");
                if (!validations.EsStringNoVacio(oPatient.Dni)) throw new ArgumentException("EL DNI no puede quedar vacío.");
                if (!validations.EsStringNoVacio(oPatient.Telefono)) throw new ArgumentException("El Teléfono no puede quedar vacío.");

                #endregion

                #region Validaciones lógicas
                if (!validations.EsFormatoEmailValido(oPatient.Email)) throw new ArgumentException("El Email no tiene un formato válido.");
                if (!validations.EsSoloLetras(oPatient.Nombre)) throw new ArgumentException("El Nombre solo puede contener letras y espacios.");
                if (!validations.EsSoloLetras(oPatient.Apellido)) throw new ArgumentException("El Apellido solo puede contener letras y espacios.");
                if (!validations.EsSoloNumeros(oPatient.Dni)) throw new ArgumentException("El DNI solo puede contener números.");
                if (!validations.EsSoloNumeros(oPatient.Telefono)) throw new ArgumentException("El Teléfono solo puede contener números.");
                if (!validations.EsFechaNacimientoValida(oPatient.FechaNacimiento)) throw new ArgumentException("La Fecha de Nacimiento no puede ser en el futuro.");

                #endregion



                return new ValidationsMethodPut { IsValid = true };
            }
            catch (Exception e)
            {
                return new ValidationsMethodPut { IsValid = false, ErrorMessage = e.Message };
            }
        }

        public ValidationsMethodPut ValidationMethodPutStatus(Estado oStatus)
        {
            try { 
                EstadoRepository repoEstado = new (_context);

                if (repoEstado.GetEstadoById(oStatus.Id) == null) throw new ArgumentException("El Estado no existe.");

                return new ValidationsMethodPut { IsValid = true };

            }
            catch(Exception e)
            {
                return new ValidationsMethodPut { IsValid = false, ErrorMessage = e.Message };

            }
        }

        public ValidationsMethodPut ValidationsMethodPutShift(TurnoCustom turno)
        {
            try
            {
                AllValidations validations = new AllValidations();
                MedicoRepository repoDoctor = new MedicoRepository(_context);
                PacienteRepository repoPatient = new PacienteRepository(_context);
                TurnoRepository repoShift = new TurnoRepository(_context);
                EstadoRepository repoState = new EstadoRepository(_context);

                #region Validaciones de existencia

                if (!repoDoctor.VerifyIfDoctorExistReturnBool(turno.MedicoId))
                    throw new ArgumentException("El médico seleccionado no existe");
                if (!repoPatient.VerifyIfPatientExistReturnBool(turno.PacienteId))
                    throw new ArgumentException("El paciente seleccionado no existe");

                #endregion

                #region Validaciones de campo

                if (!validations.EsStringNoVacio((turno.Fecha).ToString()))
                    throw new ArgumentException("Debe elegir una fecha para su turno");
                if (!validations.EsStringNoVacio((turno.Hora).ToString()))
                    throw new ArgumentException("Debe elegir una hora para su turno");
                if (!validations.EsStringNoVacio((turno.MedicoId).ToString()))
                    throw new ArgumentException("Debe elegir un médico");
                if (!validations.EsStringNoVacio((turno.PacienteId).ToString()))
                    throw new ArgumentException("Debe elegir un paciente para el turno");

                #endregion

                #region Validaciones lógicas

                DateTime fechaTurno = DateTime.Parse(turno.Fecha); // Fecha del turno
                TimeSpan horaTurno = TimeSpan.Parse(turno.Hora);   // Hora del turno
                DateTime fechaYHoraTurno = fechaTurno.Add(horaTurno); // Combinas fecha y hora

                // Comparación de fecha y hora del turno con el momento actual
                if (fechaYHoraTurno < DateTime.Now)
                {
                    throw new ArgumentException("La fecha y hora del turno no pueden ser en el pasado");
                }

                // Valida que la hora esté dentro del rango horario del médico                
                Medico doctorHorario = repoDoctor.ReturnHorariosForDoctor(turno.MedicoId);
                if (horaTurno < doctorHorario.HorarioAtencionInicio || horaTurno > doctorHorario.HorarioAtencionFin)
                {
                    throw new ArgumentException("La fecha del turno debe estar dentro del rango horario del médico");
                }

                #endregion

                return new ValidationsMethodPut { IsValid = true };
            }
            catch (Exception e)
            {
                return new ValidationsMethodPut { IsValid = false, ErrorMessage = e.Message };
            }
        }

    }
}
