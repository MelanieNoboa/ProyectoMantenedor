namespace GestionHospitalaria.Presentacion
{
    partial class FrmPacientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblFiltroEspecialista = new System.Windows.Forms.Label();
            this.cboEspecialistas = new System.Windows.Forms.ComboBox();
            this.dgvPacientes = new System.Windows.Forms.DataGridView();
            this.lblRut = new System.Windows.Forms.Label();
            this.txtRut = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEspecialistaForm = new System.Windows.Forms.Label();
            this.cboEspecialistasForm = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).BeginInit();
            this.SuspendLayout();

            // lblFiltroEspecialista
            this.lblFiltroEspecialista.AutoSize = true;
            this.lblFiltroEspecialista.Location = new System.Drawing.Point(20, 20);
            this.lblFiltroEspecialista.Name = "lblFiltroEspecialista";
            this.lblFiltroEspecialista.Size = new System.Drawing.Size(125, 15);
            this.lblFiltroEspecialista.Text = "Filtrar por Especialidad:";

            // cboEspecialistas
            this.cboEspecialistas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEspecialistas.FormattingEnabled = true;
            this.cboEspecialistas.Location = new System.Drawing.Point(155, 17);
            this.cboEspecialistas.Name = "cboEspecialistas";
            this.cboEspecialistas.Size = new System.Drawing.Size(200, 23);
            this.cboEspecialistas.SelectedIndexChanged += new System.EventHandler(this.cboEspecialistas_SelectedIndexChanged);

            // dgvPacientes
            this.dgvPacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPacientes.Location = new System.Drawing.Point(20, 55);
            this.dgvPacientes.Name = "dgvPacientes";
            this.dgvPacientes.Size = new System.Drawing.Size(520, 350);
            this.dgvPacientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPacientes_CellClick);

            // Posicionamiento Panel Derecho
            int startXLabel = 560;
            int startXControl = 680;
            int startY = 55;
            int spacing = 35;

            // RUT
            this.lblRut.AutoSize = true;
            this.lblRut.Location = new System.Drawing.Point(startXLabel, startY);
            this.lblRut.Text = "RUT:";
            this.txtRut.Location = new System.Drawing.Point(startXControl, startY);
            this.txtRut.Size = new System.Drawing.Size(180, 23);

            // Nombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(startXLabel, startY + spacing);
            this.lblNombre.Text = "Nombre:";
            this.txtNombre.Location = new System.Drawing.Point(startXControl, startY + spacing);
            this.txtNombre.Size = new System.Drawing.Size(180, 23);

            // Apellido
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(startXLabel, startY + (spacing * 2));
            this.lblApellido.Text = "Apellido:";
            this.txtApellido.Location = new System.Drawing.Point(startXControl, startY + (spacing * 2));
            this.txtApellido.Size = new System.Drawing.Size(180, 23);

            // Fecha Nacimiento
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(startXLabel, startY + (spacing * 3));
            this.lblFechaNacimiento.Text = "F. Nacimiento:";
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(startXControl, startY + (spacing * 3));
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(180, 23);

            // Telefono
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(startXLabel, startY + (spacing * 4));
            this.lblTelefono.Text = "Teléfono:";
            this.txtTelefono.Location = new System.Drawing.Point(startXControl, startY + (spacing * 4));
            this.txtTelefono.Size = new System.Drawing.Size(180, 23);

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(startXLabel, startY + (spacing * 5));
            this.lblEmail.Text = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(startXControl, startY + (spacing * 5));
            this.txtEmail.Size = new System.Drawing.Size(180, 23);

            // Especialista Asignado
            this.lblEspecialistaForm.AutoSize = true;
            this.lblEspecialistaForm.Location = new System.Drawing.Point(startXLabel, startY + (spacing * 6));
            this.lblEspecialistaForm.Text = "Especialista:";
            this.cboEspecialistasForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEspecialistasForm.Location = new System.Drawing.Point(startXControl, startY + (spacing * 6));
            this.cboEspecialistasForm.Size = new System.Drawing.Size(180, 23);

            // Botones
            int btnY = startY + (spacing * 7) + 10;
            this.btnGuardar.Location = new System.Drawing.Point(560, btnY);
            this.btnGuardar.Size = new System.Drawing.Size(70, 30);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);

            this.btnActualizar.Location = new System.Drawing.Point(635, btnY);
            this.btnActualizar.Size = new System.Drawing.Size(75, 30);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);

            this.btnEliminar.Location = new System.Drawing.Point(715, btnY);
            this.btnEliminar.Size = new System.Drawing.Size(70, 30);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);

            this.btnLimpiar.Location = new System.Drawing.Point(790, btnY);
            this.btnLimpiar.Size = new System.Drawing.Size(70, 30);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(880, 425);
            this.Controls.Add(this.lblFiltroEspecialista);
            this.Controls.Add(this.cboEspecialistas);
            this.Controls.Add(this.dgvPacientes);
            this.Controls.Add(this.lblRut);
            this.Controls.Add(this.txtRut);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblFechaNacimiento);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEspecialistaForm);
            this.Controls.Add(this.cboEspecialistasForm);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);

            this.Name = "FrmPacientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Pacientes";
            this.Load += new System.EventHandler(this.FrmPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPacientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblFiltroEspecialista;
        private System.Windows.Forms.ComboBox cboEspecialistas;
        private System.Windows.Forms.DataGridView dgvPacientes;
        private System.Windows.Forms.Label lblRut;
        private System.Windows.Forms.TextBox txtRut;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEspecialistaForm;
        private System.Windows.Forms.ComboBox cboEspecialistasForm;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
    }
}