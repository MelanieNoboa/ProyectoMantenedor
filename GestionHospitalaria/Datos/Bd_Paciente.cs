using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace GestionHospitalaria.Datos
{
    // ==========================================
    // CLASES DE MODELO (Para usar dentro de Datos)
    // ==========================================
    public class Paciente
    {
        public int pacienteID { get; set; }
        public string rut { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public int especialistaAsignadoID { get; set; }
        public bool estado { get; set; }
    }

    public class Especialista
    {
        public int especialistaID { get; set; }
        public string rut { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string apellido { get; set; } = string.Empty;
        public string especialidad { get; set; } = string.Empty;
        public string telefono { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool estado { get; set; }
    }

    public class LogAuditoriaDTO
    {
        public int LogID { get; set; }
        public string TablaAfectada { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; } = string.Empty;
    }

    // ==========================================
    // REPOSITORIO / ACCESO A DATOS
    // ==========================================
    public class Bd_Paciente
    {
        // 1. Obtener todos los pacientes activos
        public List<Paciente> ObtenerTodosLosPacientes()
        {
            List<Paciente> lista = new List<Paciente>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_LeerPacientesTodos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Paciente
                            {
                                pacienteID = Convert.ToInt32(reader["pacienteID"]),
                                rut = reader["rut"].ToString() ?? string.Empty,
                                nombre = reader["nombre"].ToString() ?? string.Empty,
                                apellido = reader["apellido"].ToString() ?? string.Empty,
                                fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                                telefono = reader["telefono"]?.ToString() ?? string.Empty,
                                email = reader["email"]?.ToString() ?? string.Empty,
                                especialistaAsignadoID = Convert.ToInt32(reader["especialistaAsignadoID"]),
                                estado = Convert.ToBoolean(reader["estado"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // 2. Obtener lista de especialistas
        public List<Especialista> ObtenerEspecialistas()
        {
            List<Especialista> lista = new List<Especialista>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT especialistaID, rut, nombre, apellido, especialidad, telefono, email, estado FROM dbo.Especialista WHERE estado = 1";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Especialista
                            {
                                especialistaID = Convert.ToInt32(reader["especialistaID"]),
                                rut = reader["rut"].ToString() ?? string.Empty,
                                nombre = reader["nombre"].ToString() ?? string.Empty,
                                apellido = reader["apellido"].ToString() ?? string.Empty,
                                especialidad = reader["especialidad"].ToString() ?? string.Empty,
                                telefono = reader["telefono"]?.ToString() ?? string.Empty,
                                email = reader["email"]?.ToString() ?? string.Empty,
                                estado = Convert.ToBoolean(reader["estado"])
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // 3. Obtener pacientes por especialidad
        public List<Paciente> ObtenerPacientesPorEspecialidad(int especialistaID )
        {
            List<Paciente> lista = new List<Paciente>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_LeerPacientesPorEspecialidad", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@@especialistaID", especialistaID);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Paciente
                            {
                                pacienteID = Convert.ToInt32(reader["pacienteID"]),
                                rut = reader["rut"].ToString() ?? string.Empty,
                                nombre = reader["nombre"].ToString() ?? string.Empty,
                                apellido = reader["apellido"].ToString() ?? string.Empty,
                                fechaNacimiento = Convert.ToDateTime(reader["fechaNacimiento"]),
                                telefono = reader["telefono"]?.ToString() ?? string.Empty,
                                email = reader["email"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }

            return lista;
        }

        // 4. Guardar un nuevo paciente
        public bool GuardarPaciente(Paciente paciente)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_CrearPaciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rut", paciente.rut);
                    cmd.Parameters.AddWithValue("@Nombre", paciente.nombre);
                    cmd.Parameters.AddWithValue("@Apellido", paciente.apellido);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", paciente.fechaNacimiento);
                    cmd.Parameters.AddWithValue("@Telefono", (object)paciente.telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)paciente.email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EspecialistaAsignadoID", paciente.especialistaAsignadoID);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. Actualizar un paciente
        public bool ActualizarPaciente(Paciente paciente)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_ActualizarPaciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PacienteID", paciente.pacienteID);
                    cmd.Parameters.AddWithValue("@Telefono", (object)paciente.telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)paciente.email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EspecialistaAsignadoID", paciente.especialistaAsignadoID);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. Eliminar lógicamente un paciente
        public bool EliminarPaciente(int pacienteID)
        {
            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_EliminarPaciente", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PacienteID", pacienteID);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 7. Obtener los registros de auditoría
        public List<LogAuditoriaDTO> ObtenerHistoricoAuditoria()
        {
            List<LogAuditoriaDTO> lista = new List<LogAuditoriaDTO>();

            using (SqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT logID, tablaAfectada, operacion, usuario, fecha, detalle FROM dbo.LogAuditoria ORDER BY fecha DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new LogAuditoriaDTO
                            {
                                LogID = Convert.ToInt32(reader["logID"]),
                                TablaAfectada = reader["tablaAfectada"].ToString() ?? string.Empty,
                                Operacion = reader["operacion"].ToString() ?? string.Empty,
                                Usuario = reader["usuario"].ToString() ?? string.Empty,
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                Detalle = reader["detalle"]?.ToString() ?? string.Empty
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}