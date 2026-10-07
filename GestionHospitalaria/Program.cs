using System;
using System.Windows.Forms;
using GestionHospitalaria.Presentacion; // Permite acceder a FrmPacientes dentro de la carpeta Presentacion

namespace GestionHospitalaria
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Inicializa las configuraciones de la aplicación (escalado DPI, estilos visuales, etc.)
            ApplicationConfiguration.Initialize();

            // Inicia la aplicación cargando FrmPacientes como el formulario principal
            Application.Run(new FrmPacientes());
        }
    }
}