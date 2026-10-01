using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GestionHospitalaria.Datos;
namespace GestionHospitalaria.Negocio
{
   /// <summary>
   /// Objeto de Transferencia de Datos (DTO) diseñado para presentar la información
   /// de los pacientes de forma clara y legible en la capa de presentación (ej. DataGridView).
   /// </summary>
   public class PacienteDTO
   {
       public int PacienteID { get; set; }
       public string Rut { get; set; } = string.Empty;
       public string Nombre { get; set; } = string.Empty;
       public string Apellido { get; set; } = string.Empty;
       public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
       public DateTime FechaNacimiento { get; set; }
       public int Edad => CalcularEdad(FechaNacimiento);
       public string Telefono { get; set; } = string.Empty;
       public string Email { get; set; } = string.Empty;
       public int EspecialistaAsignadoID { get; set; }
       public string NombreEspecialista { get; set; } = string.Empty;
       public bool Estado { get; set; }
       private static int CalcularEdad(DateTime fechaNacimiento)
       {
           var hoy = DateTime.Today;
           var edad = hoy.Year - fechaNacimiento.Year;
           if (fechaNacimiento.Date > hoy.AddYears(-edad)) edad--;
           return edad < 0 ? 0 : edad;
       }
   }
   /// <summary>
   /// Capa de Negocio para la gestión de Pacientes, Especialistas y Auditoría.
   /// Encapsula las reglas de negocio, validaciones y orquestación con la capa de Datos.
   /// </summary>
   public class PacienteService
   {
       private readonly Bd_Paciente _datosRepository;
       public PacienteService()
       {
           _datosRepository = new Bd_Paciente();
       }
       // ==========================================
       // 1. MÉTODOS DE LECTURA (CONSULTAS)
       // ==========================================
       /// <summary>
       /// Obtiene la lista de especialistas activos.
       /// Permite agregar una opción por defecto para desplegables de selección/filtro.
       /// </summary>
       public List<Especialista> ObtenerEspecialistas(bool incluirOpcionSeleccionar = true)
       {
           var especialistas = _datosRepository.ObtenerEspecialistas();
           if (incluirOpcionSeleccionar)
           {
               var listaConOpcion = new List<Especialista>
               {
                   new Especialista
                   {
                       especialistaID = 0,
                       nombre = "-- Seleccione /",
                       apellido = "Todos --",
                       especialidad = "General"
                   }
               };
               listaConOpcion.AddRange(especialistas);
               return listaConOpcion;
           }
           return especialistas;
       }
       /// <summary>
       /// Obtiene todos los pacientes mapeados a PacienteDTO con el nombre del especialista asignado.
       /// </summary>
       public List<PacienteDTO> ObtenerTodosLosPacientes()
       {
           List<Paciente> pacientes = _datosRepository.ObtenerTodosLosPacientes();
           List<Especialista> especialistas = _datosRepository.ObtenerEspecialistas();
           return MapearPacientesADTO(pacientes, especialistas);
       }
       /// <summary>
       /// Obtiene pacientes filtrados por el ID de especialista.
       /// Si el especialistaID es <= 0, retorna todos los pacientes.
       /// </summary>
       public List<PacienteDTO> ObtenerPacientesPorEspecialista(int especialistaID)
       {
           if (especialistaID <= 0)
           {
               return ObtenerTodosLosPacientes();
           }
           List<Paciente> pacientes = _datosRepository.ObtenerPacientesPorEspecialidad(especialistaID);
           List<Especialista> especialistas = _datosRepository.ObtenerEspecialistas();
           return MapearPacientesADTO(pacientes, especialistas);
       }
       /// <summary>
       /// Obtiene el registro histórico de auditoría de la base de datos.
       /// </summary>
       public List<LogAuditoriaDTO> ObtenerHistoricoAuditoria()
       {
           return _datosRepository.ObtenerHistoricoAuditoria();
       }
       // ==========================================
       // 2. MÉTODOS DE ESCRITURA Y C.R.U.D.
       // ==========================================
       /// <summary>
       /// Valida las reglas de negocio y registra un nuevo paciente.
       /// </summary>
       public bool GuardarPaciente(string rut, string nombre, string apellido, DateTime fechaNacimiento, string telefono, string email, int especialistaAsignadoID)
       {
           ValidarDatosPaciente(rut, nombre, apellido, fechaNacimiento, email, especialistaAsignadoID);
           Paciente paciente = new Paciente
           {
               rut = rut.Trim().ToUpper(),
               nombre = nombre.Trim(),
               apellido = apellido.Trim(),
               fechaNacimiento = fechaNacimiento,
               telefono = telefono?.Trim() ?? string.Empty,
               email = email?.Trim().ToLower() ?? string.Empty,
               especialistaAsignadoID = especialistaAsignadoID,
               estado = true
           };
           return _datosRepository.GuardarPaciente(paciente);
       }
       /// <summary>
       /// Valida las reglas de negocio y actualiza los datos de un paciente existente.
       /// </summary>
       public bool ActualizarPaciente(int pacienteID, string rut, string nombre, string apellido, DateTime fechaNacimiento, string telefono, string email, int especialistaAsignadoID)
       {
           if (pacienteID <= 0)
           {
               throw new ArgumentException("Debe especificar un ID de paciente válido para actualizar.");
           }
           ValidarDatosPaciente(rut, nombre, apellido, fechaNacimiento, email, especialistaAsignadoID);
           Paciente paciente = new Paciente
           {
               pacienteID = pacienteID,
               rut = rut.Trim().ToUpper(),
               nombre = nombre.Trim(),
               apellido = apellido.Trim(),
               fechaNacimiento = fechaNacimiento,
               telefono = telefono?.Trim() ?? string.Empty,
               email = email?.Trim().ToLower() ?? string.Empty,
               especialistaAsignadoID = especialistaAsignadoID
           };
           return _datosRepository.ActualizarPaciente(paciente);
       }
       /// <summary>
       /// Procesa la eliminación lógica de un paciente tras verificar el identificador.
       /// </summary>
       public bool EliminarPaciente(int pacienteID)
       {
           if (pacienteID <= 0)
           {
               throw new ArgumentException("Debe seleccionar un paciente válido para eliminar.");
           }
           return _datosRepository.EliminarPaciente(pacienteID);
       }
       // ==========================================
       // 3. MÉTODOS AUXILIARES Y VALIDACIONES
       // ==========================================
       private List<PacienteDTO> MapearPacientesADTO(List<Paciente> pacientes, List<Especialista> especialistas)
       {
           var dictEspecialistas = especialistas.ToDictionary(e => e.especialistaID, e => $"{e.nombre} {e.apellido}".Trim());
           return pacientes.Select(p => new PacienteDTO
           {
               PacienteID = p.pacienteID,
               Rut = p.rut,
               Nombre = p.nombre,
               Apellido = p.apellido,
               FechaNacimiento = p.fechaNacimiento,
               Telefono = p.telefono,
               Email = p.email,
               EspecialistaAsignadoID = p.especialistaAsignadoID,
               NombreEspecialista = dictEspecialistas.TryGetValue(p.especialistaAsignadoID, out string? nombreEspecialista)
                   ? nombreEspecialista
                   : "No Asignado",
               Estado = p.estado
           }).ToList();
       }
       private void ValidarDatosPaciente(string rut, string nombre, string apellido, DateTime fechaNacimiento, string email, int especialistaAsignadoID)
       {
           if (string.IsNullOrWhiteSpace(rut))
           {
               throw new ArgumentException("El RUT del paciente es obligatorio.");
           }
           if (string.IsNullOrWhiteSpace(nombre))
           {
               throw new ArgumentException("El nombre del paciente es obligatorio.");
           }
           if (string.IsNullOrWhiteSpace(apellido))
           {
               throw new ArgumentException("El apellido del paciente es obligatorio.");
           }
           if (fechaNacimiento > DateTime.Today)
           {
               throw new ArgumentException("La fecha de nacimiento no puede ser una fecha futura.");
           }
           if (fechaNacimiento < DateTime.Today.AddYears(-130))
           {
               throw new ArgumentException("Ingrese una fecha de nacimiento válida.");
           }
           if (especialistaAsignadoID <= 0)
           {
               throw new ArgumentException("Debe seleccionar un especialista asignado válido.");
           }
           if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
           {
               throw new ArgumentException("El formato del correo electrónico ingresado no es válido.");
           }
       }
   }
}