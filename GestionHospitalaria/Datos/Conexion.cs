using Microsoft.Data.SqlClient;

namespace GestionHospitalaria.Datos
{
    public static class Conexion
    {
        // Instancia local de SQL Server Express de este equipo
        private static readonly string _connectionString = @"Server=.\SQLEXPRESS01;Database=BD_GestionHospitalaria;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_connectionString);
        }
    }
}