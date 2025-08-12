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
using System.Diagnostics;
using ClosedXML.Excel;

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

        // Mueve la línea de formato de columna al método LoadAllTasks, después de asignar el DataSource.
        // Así se asegura que dgvToDo y sus columnas existen y están inicializadas.

        private void LoadAllTasks()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tareas$]", conn);
                    conn.Open();

                    DataTable allTasksDt = new DataTable();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(allTasksDt);

                    DataView toDoView = new DataView(allTasksDt) { RowFilter = "Status = 'Por Hacer'" };
                    DataView inProgressView = new DataView(allTasksDt) { RowFilter = "Status = 'En Proceso'" };
                    DataView doneView = new DataView(allTasksDt) { RowFilter = "Status = 'Hecho'" };

                    dgvToDo.DataSource = toDoView;
                    dgvInProgress.DataSource = inProgressView;
                    dgvDone.DataSource = doneView;

                    // --- CORRECCIÓN: Formato de fecha en la columna DueDate ---
                    if (dgvToDo.Columns["DueDate"] != null)
                    {
                        dgvToDo.Columns["DueDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
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
            DataGridView activeDgv = GetActiveDataGridView();

            if (activeDgv == null || activeDgv.CurrentRow == null)
            {
                MessageBox.Show("Por favor, selecciona una tarea para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmation = MessageBox.Show("¿Estás seguro de que deseas eliminar esta tarea?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                string taskId = activeDgv.CurrentRow.Cells["ID"].Value.ToString();
                DeleteTaskFromExcel(taskId);
                LoadAllTasks();
            }
        }

        private void btnOpenFileLocation_Click(object sender, EventArgs e)
        {
            // Verificamos que el archivo realmente exista antes de intentar abrir su ubicación.
            if (File.Exists(excelFilePath))
            {
                // Usamos "explorer.exe" con el argumento "/select," para abrir la carpeta
                // y dejar el archivo seleccionado, listo para que el usuario lo vea.
                Process.Start("explorer.exe", "/select,\"" + excelFilePath + "\"");
            }
            else
            {
                // Si por alguna razón el archivo no se encuentra, informamos al usuario.
                MessageBox.Show("No se pudo encontrar el archivo de base de datos.", "Archivo no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView GetActiveDataGridView()
        {
            foreach (Control ctrl in tabControlMain.SelectedTab.Controls)
            {
                if (ctrl is DataGridView dgv)
                    return dgv;
            }
            return null;
        }


        private void DeleteTaskFromExcel(string taskId)
        {
            try
            {
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet("Tareas");
                    var rows = worksheet.RowsUsed().Where(r => r.Cell("A").GetValue<string>() == taskId).ToList();

                    foreach (var row in rows)
                        row.Delete();

                    workbook.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la tarea en Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
