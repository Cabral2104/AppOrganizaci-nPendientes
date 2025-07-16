using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace AppOrganizaciónPendientes
{
    public partial class MainForm: Form
    {
        private string connectionString;
        private string excelFilePath;

        public MainForm()
        {
            InitializeComponent();
            // --- CAMBIO IMPORTANTE ---
            // Ahora, el archivo Excel se guardará en la misma carpeta que el archivo .exe de la aplicación.
            // Esto lo hace fácilmente accesible para el usuario.
            string startupPath = Application.StartupPath;
            excelFilePath = Path.Combine(startupPath, "TasksDB.xlsx");

            // La cadena de conexión ahora apunta a esta nueva ruta.
            connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={excelFilePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;\"";
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeDatabase();
            LoadAllTasks();
        }

        // ---INICIALIZACIÓN DE LA BASE DE DATOS ---
        private void InitializeDatabase()
        {
            // Verificamos si el archivo Excel ya existe.
            if (!File.Exists(excelFilePath))
            {
                // Si no existe, lo creamos con la estructura necesaria.
                CreateExcelFile();
            }
        }

        private void CreateExcelFile()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    // Usamos un comando SQL "CREATE TABLE" para definir las columnas en la primera hoja.
                    // La hoja se llamará 'Tareas'.
                    string createTableQuery = "CREATE TABLE [Tareas] (ID VARCHAR(50), TaskName VARCHAR(255), DueDate DATETIME, Status VARCHAR(50), Details MEMO)";
                    OleDbCommand cmd = new OleDbCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fatal al crear la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Si falla la creación, cerramos la app porque no puede funcionar.
                Application.Exit();
            }
        }

        // --- LÓGICA PARA CARGAR TAREAS ---
        private void LoadAllTasks()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    // Obtenemos TODAS las tareas en un solo DataTable.
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tareas$]", conn);
                    conn.Open();

                    DataTable allTasksDt = new DataTable();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(allTasksDt);

                    // Creamos "vistas" filtradas de la tabla principal para cada estado.
                    // Esto es mucho más eficiente que hacer tres consultas separadas a la base de datos.
                    DataView toDoView = new DataView(allTasksDt) { RowFilter = "Status = 'Por Hacer'" };
                    DataView inProgressView = new DataView(allTasksDt) { RowFilter = "Status = 'En Proceso'" };
                    DataView doneView = new DataView(allTasksDt) { RowFilter = "Status = 'Hecho'" };

                    // Asignamos cada vista a su DataGridView correspondiente.
                    dgvToDo.DataSource = toDoView;
                    dgvInProgress.DataSource = inProgressView;
                    dgvDone.DataSource = doneView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las tareas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new TaskForm(connectionString);
            taskForm.ShowDialog();
            LoadAllTasks(); // Recargamos todo para ver los cambios.
        }

        private void btnModifyTask_Click(object sender, EventArgs e)
        {
            // Obtenemos la tabla activa para saber de dónde sacar la tarea.
            DataGridView activeDgv = tabControlMain.SelectedTab.Controls[0] as DataGridView;

            if (activeDgv.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona una tarea para modificar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string taskId = activeDgv.CurrentRow.Cells["ID"].Value.ToString();

            TaskForm taskForm = new TaskForm(connectionString, taskId);
            taskForm.ShowDialog();
            LoadAllTasks();
        }

        private void btnDeleteTask_Click(object sender, EventArgs e)
        {
            DataGridView activeDgv = tabControlMain.SelectedTab.Controls[0] as DataGridView;

            if (activeDgv.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona una tarea para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmation = MessageBox.Show("¿Estás seguro de que deseas eliminar esta tarea?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                try
                {
                    string taskId = activeDgv.CurrentRow.Cells["ID"].Value.ToString();
                    using (OleDbConnection conn = new OleDbConnection(connectionString))
                    {
                        string query = "DELETE FROM [Tareas$] WHERE ID = @ID";
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", taskId);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    LoadAllTasks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la tarea: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
