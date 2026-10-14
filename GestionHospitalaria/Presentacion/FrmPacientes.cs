using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionHospitalaria.Datos;
using GestionHospitalaria.Negocio;

namespace GestionHospitalaria.Presentacion
{
    public partial class FrmPacientes : Form
    {
        private readonly PacienteService _pacienteService;
        private int _pacienteSeleccionadoID = 0;

        public FrmPacientes()
        {
            InitializeComponent();
            _pacienteService = new PacienteService();
        }

        private void FrmPacientes_Load(object? sender, EventArgs e)
        {
            CargarEspecialistas();
            RefrescarGrillaSegunFiltro();
            LimpiarFormulario();
        }

        private void CargarEspecialistas()
        {
            try
            {
                cboEspecialistas.SelectedIndexChanged -= cboEspecialistas_SelectedIndexChanged;

                // 1. Combo de filtro superior
                List<Especialista> listaFiltro = _pacienteService.ObtenerEspecialistas(true);
                cboEspecialistas.DataSource = listaFiltro;
                cboEspecialistas.DisplayMember = "especialidad"; 
                cboEspecialistas.ValueMember = "especialistaID";

                // 2. Combo del formulario derecho
                List<Especialista> listaForm = _pacienteService.ObtenerEspecialistas(false);
                cboEspecialistasForm.DataSource = listaForm;
                cboEspecialistasForm.DisplayMember = "especialidad";
                cboEspecialistasForm.ValueMember = "especialistaID";

                cboEspecialistas.SelectedIndexChanged += cboEspecialistas_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los especialistas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene el ID del especialista seleccionado de manera segura.
        /// </summary>
        private void RefrescarGrillaSegunFiltro()
        {
            int especialistaId = 0;

            if (cboEspecialistas.SelectedItem is Especialista esp)
            {
                especialistaId = esp.especialistaID;
            }
            else if (cboEspecialistas.SelectedValue != null && int.TryParse(cboEspecialistas.SelectedValue.ToString(), out int id))
            {
                especialistaId = id;
            }

            CargarGrillaPacientes(especialistaId);
        }

        private void CargarGrillaPacientes(int especialistaId)
        {
            try
            {
                List<PacienteDTO> pacientes = _pacienteService.ObtenerPacientesPorEspecialista(especialistaId);

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
            if (dgvPacientes.Columns.Count == 0) return;

            OcultarColumna("PacienteID");
            OcultarColumna("Nombre");                  
            OcultarColumna("Apellido");                
            OcultarColumna("FechaNacimiento");         
            OcultarColumna("Email");                   
            OcultarColumna("EspecialistaAsignadoID");  
            OcultarColumna("Estado");                  

            RenombrarEncabezado("Rut", "RUT");
            RenombrarEncabezado("NombreCompleto", "Nombre Completo");
            RenombrarEncabezado("Edad", "Edad");
            RenombrarEncabezado("Telefono", "Teléfono");
            RenombrarEncabezado("NombreEspecialista", "Especialista");

            dgvPacientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPacientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPacientes.MultiSelect = false;
            dgvPacientes.ReadOnly = true;
            dgvPacientes.AllowUserToAddRows = false;
            dgvPacientes.AllowUserToDeleteRows = false;
            dgvPacientes.RowHeadersVisible = false;
        }

        // ==========================================
        // BOTONES DEL CRUD
        // ==========================================

        private void btnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                int espId = ObtenerIdEspecialistaFormulario();

                bool guardado = _pacienteService.GuardarPaciente(
                    txtRut.Text,
                    txtNombre.Text,
                    txtApellido.Text,
                    dtpFechaNacimiento.Value,
                    txtTelefono.Text,
                    txtEmail.Text,
                    espId
                );

                if (guardado)
                {
                    MessageBox.Show("Paciente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrillaSegunFiltro();
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_pacienteSeleccionadoID <= 0)
                {
                    MessageBox.Show("Seleccione un paciente de la grilla para actualizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int espId = ObtenerIdEspecialistaFormulario();

                bool actualizado = _pacienteService.ActualizarPaciente(
                    _pacienteSeleccionadoID,
                    txtRut.Text,
                    txtNombre.Text,
                    txtApellido.Text,
                    dtpFechaNacimiento.Value,
                    txtTelefono.Text,
                    txtEmail.Text,
                    espId
                );

                if (actualizado)
                {
                    MessageBox.Show("Datos del paciente actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrillaSegunFiltro();
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("No se realizaron cambios en la base de datos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_pacienteSeleccionadoID <= 0)
                {
                    MessageBox.Show("Seleccione un paciente de la grilla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resp = MessageBox.Show("¿Está seguro de desactivar/eliminar este paciente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    bool eliminado = _pacienteService.EliminarPaciente(_pacienteSeleccionadoID);
                    if (eliminado)
                    {
                        MessageBox.Show("Paciente eliminado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefrescarGrillaSegunFiltro();
                        LimpiarFormulario();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvPacientes_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvPacientes.CurrentRow != null)
            {
                if (dgvPacientes.CurrentRow.DataBoundItem is PacienteDTO paciente)
                {
                    _pacienteSeleccionadoID = paciente.PacienteID;
                    txtRut.Text = paciente.Rut;
                    txtNombre.Text = paciente.Nombre;
                    txtApellido.Text = paciente.Apellido;
                    dtpFechaNacimiento.Value = paciente.FechaNacimiento;
                    txtTelefono.Text = paciente.Telefono;
                    txtEmail.Text = paciente.Email;
                    
                    if (cboEspecialistasForm.DataSource is List<Especialista> lista)
                    {
                        cboEspecialistasForm.SelectedValue = paciente.EspecialistaAsignadoID;
                    }
                }
            }
        }

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================

        private int ObtenerIdEspecialistaFormulario()
        {
            if (cboEspecialistasForm.SelectedItem is Especialista esp)
            {
                return esp.especialistaID;
            }
            if (cboEspecialistasForm.SelectedValue != null && int.TryParse(cboEspecialistasForm.SelectedValue.ToString(), out int id))
            {
                return id;
            }
            return 0;
        }

        private void LimpiarFormulario()
        {
            _pacienteSeleccionadoID = 0;
            txtRut.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            dtpFechaNacimiento.Value = DateTime.Today;
        }

        private void OcultarColumna(string nombreColumna)
        {
            DataGridViewColumn? col = dgvPacientes.Columns[nombreColumna];
            if (col != null) col.Visible = false;
        }

        private void RenombrarEncabezado(string nombreColumna, string textoEncabezado)
        {
            DataGridViewColumn? col = dgvPacientes.Columns[nombreColumna];
            if (col != null) col.HeaderText = textoEncabezado;
        }

        private void cboEspecialistas_SelectedIndexChanged(object? sender, EventArgs e)
        {
            RefrescarGrillaSegunFiltro();
        }
    }
}