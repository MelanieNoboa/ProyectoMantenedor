using System;
using System.Windows.Forms;
using GestionHospitalaria.Negocio; // Espacio de nombres de la capa de negocio

namespace GestionHospitalaria.Presentacion
{
    public partial class FrmPacientes : Form
    {
        // Instancia de la capa de negocio
        private readonly PacienteService _pacienteService;

        public FrmPacientes()
        {
            InitializeComponent();
            _pacienteService = new PacienteService();
        }

        private void FrmPacientes_Load(object sender, EventArgs e)
        {
            // 1. Cargar el ComboBox de especialistas al abrir el formulario
            CargarEspecialistas();

            // 2. Cargar todos los pacientes en la grilla por defecto (especialistaId = 0)
            CargarGrillaPacientes();
        }

        private void CargarEspecialistas()
        {
            try
            {
                // Desenganchar temporalmente el evento para evitar disparos prematuros
                cboEspecialistas.SelectedIndexChanged -= cboEspecialistas_SelectedIndexChanged;

                // Obtener la lista de especialistas
                var listaEspecialistas = _pacienteService.ObtenerEspecialistas(true);

                cboEspecialistas.DataSource = listaEspecialistas;
                cboEspecialistas.DisplayMember = "especialidad"; 
                cboEspecialistas.ValueMember = "especialistaID";

                // Reenganchar el evento después de enlazar correctamente los datos
                cboEspecialistas.SelectedIndexChanged += cboEspecialistas_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los especialistas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGrillaPacientes(int especialistaId = 0)
        {
            try
            {
                // Consulta a la capa de negocio con el parámetro obligatorio
                var pacientes = _pacienteService.ObtenerPacientesPorEspecialista(especialistaId);

                dgvPacientes.DataSource = null;
                dgvPacientes.DataSource = pacientes;

                ConfigurarColumnasGrilla();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pacientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasGrilla()
        {
            if (dgvPacientes.Columns["PacienteID"] is var colId && colId != null)
                colId.Visible = false; // Ocultar ID interno

            if (dgvPacientes.Columns["NombreCompleto"] is var colNom && colNom != null)
                colNom.HeaderText = "Nombre Completo";

            if (dgvPacientes.Columns["Rut"] is var colRut && colRut != null)
                colRut.HeaderText = "RUT";

            if (dgvPacientes.Columns["FechaNacimiento"] is var colFec && colFec != null)
            {
                colFec.HeaderText = "F. Nacimiento";
                colFec.DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvPacientes.Columns["Edad"] is var colEdad && colEdad != null)
                colEdad.HeaderText = "Edad";

            if (dgvPacientes.Columns["Telefono"] is var colTel && colTel != null)
                colTel.HeaderText = "Teléfono";

            if (dgvPacientes.Columns["Email"] is var colEmail && colEmail != null)
                colEmail.HeaderText = "Correo Electrónico";

            if (dgvPacientes.Columns["NombreEspecialista"] is var colEsp && colEsp != null)
                colEsp.HeaderText = "Especialista Asignado";
                
            if (dgvPacientes.Columns["Estado"] is var colEst && colEst != null)
                colEst.HeaderText = "Activo";
        }

        // Evento al cambiar la selección del ComboBox
        private void cboEspecialistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEspecialistas.SelectedValue != null && int.TryParse(cboEspecialistas.SelectedValue.ToString(), out int especialistaId))
            {
                CargarGrillaPacientes(especialistaId);
            }
        }
    }
}