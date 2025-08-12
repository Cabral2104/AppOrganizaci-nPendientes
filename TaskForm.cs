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
using ClosedXML.Excel; // Agrega esto al inicio del archivo

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
                LoadTaskDataWithClosedXML();
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

       

        private void LoadTaskDataWithClosedXML()
        {
            try
            {
                string excelFilePath = Path.Combine(Application.StartupPath, "TasksDB.xlsx");
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet("Tareas");
                    // Buscar la fila donde la columna A (ID) coincide con currentTaskId
                    var row = worksheet.RowsUsed()
                        .FirstOrDefault(r => r.Cell(1).GetValue<string>() == currentTaskId);

                    if (row != null)
                    {
                        txtTaskName.Text = row.Cell(2).GetValue<string>();
                        // Si la celda está vacía, usa la fecha actual
                        DateTime fecha;
                        if (!DateTime.TryParse(row.Cell(3).GetValue<string>(), out fecha))
                            fecha = DateTime.Now;
                        dtpDueDate.Value = fecha;
                        cmbStatus.SelectedItem = row.Cell(4).GetValue<string>();
                        txtDetails.Text = row.Cell(5).GetValue<string>();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la tarea para cargar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                AddNewTaskWithClosedXML();
            }
            else
            {
                UpdateTaskWithClosedXML();
            }
        }

      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddNewTaskWithClosedXML()
        {
            try
            {
                string excelFilePath = Path.Combine(Application.StartupPath, "TasksDB.xlsx");
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet("Tareas");
                    var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;

                    int newId = GetNextId();

                    // Si es la primera fila, crea los encabezados
                    if (lastRow == 1 && worksheet.Cell(1, 1).GetValue<string>() != "ID")
                    {
                        worksheet.Cell(1, 1).Value = "ID";
                        worksheet.Cell(1, 2).Value = "TaskName";
                        worksheet.Cell(1, 3).Value = "DueDate";
                        worksheet.Cell(1, 4).Value = "Status";
                        worksheet.Cell(1, 5).Value = "Details";
                        lastRow = 1;
                    }

                    int newRow = lastRow + 1;
                    worksheet.Cell(newRow, 1).Value = newId.ToString();
                    worksheet.Cell(newRow, 2).Value = txtTaskName.Text;
                    worksheet.Cell(newRow, 3).Value = dtpDueDate.Value.Date; // Solo la fecha, hora en 00:00:00
                    worksheet.Cell(newRow, 4).Value = cmbStatus.SelectedItem.ToString();
                    worksheet.Cell(newRow, 5).Value = txtDetails.Text;

                    workbook.Save();
                }

                MessageBox.Show("Tarea agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la tarea: " + ex.Message);
            }
        }

        private void UpdateTaskWithClosedXML()
        {
            try
            {
                string excelFilePath = Path.Combine(Application.StartupPath, "TasksDB.xlsx");
                using (var workbook = new XLWorkbook(excelFilePath))
                {
                    var worksheet = workbook.Worksheet("Tareas");
                    // Buscar la fila donde la columna A (ID) coincide con currentTaskId
                    var rowToUpdate = worksheet.RowsUsed()
                        .FirstOrDefault(r => r.Cell(1).GetValue<string>() == currentTaskId);

                    if (rowToUpdate != null)
                    {
                        rowToUpdate.Cell(2).Value = txtTaskName.Text;
                        rowToUpdate.Cell(3).Value = dtpDueDate.Value.Date; // Solo la fecha, hora en 00:00:00
                        rowToUpdate.Cell(4).Value = cmbStatus.SelectedItem.ToString();
                        rowToUpdate.Cell(5).Value = txtDetails.Text;

                        workbook.Save();
                        MessageBox.Show("Tarea actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la tarea para actualizar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar la tarea: " + ex.Message);
            }
        }
    }
}
