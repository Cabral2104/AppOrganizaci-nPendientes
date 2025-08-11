using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace AppOrganizaciónPendientes
{
    public partial class TaskForm: Form
    {
        private string connectionString;
        private string currentTaskId;

        public TaskForm(string connString, string taskId = null)
        {
            InitializeComponent();
            connectionString = connString;
            currentTaskId = taskId;
        }

        private void TaskForm_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.Add("Por Hacer");
            cmbStatus.Items.Add("En Proceso");
            cmbStatus.Items.Add("Hecho");

            if (!string.IsNullOrEmpty(currentTaskId))
            {
                this.Text = "Modificar Tarea";
                LoadTaskData();
            }
            else
            {
                this.Text = "Agregar Nueva Tarea";
                cmbStatus.SelectedItem = "Por Hacer";
            }
        }

        // --- NUEVO MÉTODO PARA OBTENER Y ACTUALIZAR EL ID ---
        private int GetNextId()
        {
            // El archivo del contador se guardará junto al archivo .exe de la aplicación.
            string counterFilePath = Path.Combine(Application.StartupPath, "id_counter.txt");
            int nextId = 1; // Por defecto, el primer ID será 1 si el archivo no existe.

            if (File.Exists(counterFilePath))
            {
                // Si el archivo ya existe, leemos el último número guardado y le sumamos 1.
                int.TryParse(File.ReadAllText(counterFilePath), out int lastId);
                nextId = lastId + 1;
            }

            // Escribimos el nuevo último ID en el archivo para la próxima vez.
            File.WriteAllText(counterFilePath, nextId.ToString());

            return nextId;
        }

        private void LoadTaskData()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Tareas$] WHERE ID = @ID";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", currentTaskId);

                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtTaskName.Text = reader["TaskName"].ToString();
                        dtpDueDate.Value = Convert.ToDateTime(reader["DueDate"]);
                        cmbStatus.SelectedItem = reader["Status"].ToString();
                        txtDetails.Text = reader["Details"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos de la tarea: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("El nombre de la tarea no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(currentTaskId))
            {
                AddNewTask();
            }
            else
            {
                UpdateTask();
            }
        }

        private void AddNewTask()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "INSERT INTO [Tareas$] (ID, TaskName, DueDate, Status, Details) VALUES (@ID, @TaskName, @DueDate, @Status, @Details)";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    // --- CAMBIO CLAVE: Obtenemos el nuevo ID autoincremental ---
                    int newId = GetNextId();
                    cmd.Parameters.AddWithValue("@ID", newId.ToString()); // Lo guardamos como texto

                    // El resto de los parámetros se mantiene igual
                    cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
                    cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value); // Guardar el objeto DateTime completo es mejor
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Details", txtDetails.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tarea agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la tarea: " + ex.Message);
            }
        }

        private void UpdateTask()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "UPDATE [Tareas$] SET TaskName = @TaskName, DueDate = @DueDate, Status = @Status, Details = @Details WHERE ID = @ID";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
                    cmd.Parameters.AddWithValue("@DueDate", dtpDueDate.Value); // Guardar el objeto DateTime completo
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Details", txtDetails.Text);
                    cmd.Parameters.AddWithValue("@ID", currentTaskId); // Usamos el ID existente para la cláusula WHERE

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tarea actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la tarea: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
