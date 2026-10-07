using Microsoft.Data.SqlClient;
 
namespace GestionHospitalaria.Datos

{

    public static class Conexion

    {

        // Se agrega '@' antes de la cadena para que acepte la barra invertida '\' sin dar error de escape

        //private static readonly string _connectionString = @"SJAPLA3040051\sqlexpress;Database='BD_GestionHospitalaria;User Id=SA;Password=User.sede;TrustServerCertificate=True;";
        private static readonly string _connectionString = @"Server=SJAPLA3040011\SQLEXPRESS;Database=BD_GestionHospitalaria;User Id=SA;Password=User.sede;TrustServerCertificate=True;";

        //version notebbok CORFO

        //private static readonly string _connectionString = 

                   // @"Server=localhost\SQLEXPRESS01;Database=TiendaDb;Integrated Security=True;TrustServerCertificate=True;";
 
        public static SqlConnection ObtenerConexion()

        {

            return new SqlConnection(_connectionString);

        }

    }

}
 