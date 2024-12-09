# Gestión de Turnos Médicos

Este proyecto es una API para gestionar turnos en un contexto de consultorios médicos.  
Fue desarrollada con **.NET Core**, siguiendo el **patrón de diseño de repositorios** para una arquitectura limpia y escalable.

## Módulos Incluidos

### 1. **Módulo de Especialidades**
Este módulo gestiona las especialidades médicas disponibles.

- **Crear Especialidad**: Permite registrar una nueva especialidad.
- **Editar Especialidad**: Modifica los datos de una especialidad existente.
- **Listar Todas las Especialidades**: Devuelve un listado de todas las especialidades.
- **Buscar Especialidad por ID**: Obtiene los detalles de una especialidad mediante su identificador único.
- **Eliminar Especialidad**: Elimina una especialidad del sistema.

---

### 2. **Módulo de Estados**
Administra los estados relacionados con los turnos.

- **Crear Estado**: Registra un nuevo estado.
- **Editar Estado**: Actualiza los datos de un estado existente.
- **Listar Todos los Estados**: Devuelve un listado de todos los estados disponibles.
- **Buscar Estado por ID**: Busca un estado específico mediante su identificador único.
- **Eliminar Estado**: Elimina un estado del sistema.

---

### 3. **Módulo de Médicos**
Gestión de los médicos registrados en el sistema.

- **Crear Médico**: Permite registrar un nuevo médico.
- **Editar Médico**: Modifica los datos de un médico existente.
- **Listar Todos los Médicos**: Devuelve un listado de todos los médicos registrados.
- **Listar Médicos por Especialidad**: Filtra a los médicos según su especialidad.
- **Buscar Médico por ID**: Obtiene los detalles de un médico mediante su identificador único.
- **Eliminar Médico**: Elimina un médico del sistema.

---

### 4. **Módulo de Pacientes**
Gestión de los pacientes que solicitan turnos.

- **Crear Paciente**: Registra un nuevo paciente.
- **Editar Paciente**: Actualiza los datos de un paciente existente.
- **Listar Todos los Pacientes**: Devuelve un listado de todos los pacientes registrados.
- **Buscar Paciente por ID**: Obtiene los detalles de un paciente mediante su identificador único.
- **Buscar Paciente por DNI**: Filtra a los pacientes por su documento de identidad.
- **Eliminar Paciente**: Elimina un paciente del sistema.

---

### 5. **Módulo de Turnos**
Gestión completa de los turnos médicos.

- **Crear Turno**: Registra un nuevo turno en el sistema.
- **Editar Turno**: Modifica los datos de un turno existente.
- **Editar Estado de un Turno**: Cambia el estado de un turno (por ejemplo, "Pendiente", "Confirmado").
- **Listar Todos los Turnos**: Devuelve un listado de todos los turnos registrados.
- **Listar Turnos por Médico**: Filtra los turnos asignados a un médico específico.
- **Listar Turnos de un Paciente**: Muestra todos los turnos asociados a un paciente.
- **Listar Turnos de un Médico por su ID**: Devuelve los turnos de un médico específico.
- **Listar Turnos Disponibles de un Médico**: Muestra únicamente los turnos libres de un médico.
- **Listar Turnos por Fecha**: Filtra los turnos según una fecha específica.
- **Buscar Turno por ID**: Obtiene los detalles de un turno mediante su identificador único.
- **Eliminar Turno**: Elimina un turno del sistema.

---

## Tecnologías Utilizadas
- **Framework**: .NET Core
- **Arquitectura**: Patrón de Repositorios, Patrón Singleton para el modulo de enviar correos electrónicos.
- **Base de Datos**: Sql Server



