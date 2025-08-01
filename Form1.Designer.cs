namespace AppOrganizaciónPendientes
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddTask = new System.Windows.Forms.Button();
            this.btnModifyTask = new System.Windows.Forms.Button();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageToDo = new System.Windows.Forms.TabPage();
            this.dgvToDo = new System.Windows.Forms.DataGridView();
            this.tabPageInProgress = new System.Windows.Forms.TabPage();
            this.dgvInProgress = new System.Windows.Forms.DataGridView();
            this.tabPageDone = new System.Windows.Forms.TabPage();
            this.dgvDone = new System.Windows.Forms.DataGridView();
            this.btnOpenFileLocation = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageToDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDo)).BeginInit();
            this.tabPageInProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInProgress)).BeginInit();
            this.tabPageDone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDone)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnAddTask, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnModifyTask, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteTask, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tabControlMain, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnOpenFileLocation, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(765, 387);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btnAddTask
            // 
            this.btnAddTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddTask.Location = new System.Drawing.Point(3, 351);
            this.btnAddTask.Name = "btnAddTask";
            this.btnAddTask.Size = new System.Drawing.Size(185, 33);
            this.btnAddTask.TabIndex = 3;
            this.btnAddTask.Text = "Agregar Tarea";
            this.btnAddTask.UseVisualStyleBackColor = true;
            this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click);
            // 
            // btnModifyTask
            // 
            this.btnModifyTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModifyTask.Location = new System.Drawing.Point(193, 350);
            this.btnModifyTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnModifyTask.Name = "btnModifyTask";
            this.btnModifyTask.Size = new System.Drawing.Size(187, 35);
            this.btnModifyTask.TabIndex = 4;
            this.btnModifyTask.Text = "Modificar Tarea";
            this.btnModifyTask.UseVisualStyleBackColor = true;
            this.btnModifyTask.Click += new System.EventHandler(this.btnModifyTask_Click);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteTask.Location = new System.Drawing.Point(384, 350);
            this.btnDeleteTask.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(187, 35);
            this.btnDeleteTask.TabIndex = 5;
            this.btnDeleteTask.Text = "Eliminar Tarea";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // tabControlMain
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.tabControlMain, 4);
            this.tabControlMain.Controls.Add(this.tabPageToDo);
            this.tabControlMain.Controls.Add(this.tabPageInProgress);
            this.tabControlMain.Controls.Add(this.tabPageDone);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(3, 3);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(759, 342);
            this.tabControlMain.TabIndex = 1;
            // 
            // tabPageToDo
            // 
            this.tabPageToDo.Controls.Add(this.dgvToDo);
            this.tabPageToDo.Location = new System.Drawing.Point(4, 22);
            this.tabPageToDo.Name = "tabPageToDo";
            this.tabPageToDo.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageToDo.Size = new System.Drawing.Size(751, 316);
            this.tabPageToDo.TabIndex = 0;
            this.tabPageToDo.Text = "Tareas por Hacer";
            this.tabPageToDo.UseVisualStyleBackColor = true;
            // 
            // dgvToDo
            // 
            this.dgvToDo.AllowUserToAddRows = false;
            this.dgvToDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvToDo.Location = new System.Drawing.Point(3, 3);
            this.dgvToDo.Name = "dgvToDo";
            this.dgvToDo.ReadOnly = true;
            this.dgvToDo.RowTemplate.Height = 24;
            this.dgvToDo.Size = new System.Drawing.Size(745, 310);
            this.dgvToDo.TabIndex = 0;
            // 
            // tabPageInProgress
            // 
            this.tabPageInProgress.Controls.Add(this.dgvInProgress);
            this.tabPageInProgress.Location = new System.Drawing.Point(4, 22);
            this.tabPageInProgress.Name = "tabPageInProgress";
            this.tabPageInProgress.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageInProgress.Size = new System.Drawing.Size(751, 315);
            this.tabPageInProgress.TabIndex = 1;
            this.tabPageInProgress.Text = "En Proceso";
            this.tabPageInProgress.UseVisualStyleBackColor = true;
            // 
            // dgvInProgress
            // 
            this.dgvInProgress.AllowUserToAddRows = false;
            this.dgvInProgress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInProgress.Location = new System.Drawing.Point(3, 3);
            this.dgvInProgress.Name = "dgvInProgress";
            this.dgvInProgress.ReadOnly = true;
            this.dgvInProgress.RowTemplate.Height = 24;
            this.dgvInProgress.Size = new System.Drawing.Size(745, 309);
            this.dgvInProgress.TabIndex = 0;
            // 
            // tabPageDone
            // 
            this.tabPageDone.Controls.Add(this.dgvDone);
            this.tabPageDone.Location = new System.Drawing.Point(4, 22);
            this.tabPageDone.Name = "tabPageDone";
            this.tabPageDone.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPageDone.Size = new System.Drawing.Size(751, 315);
            this.tabPageDone.TabIndex = 2;
            this.tabPageDone.Text = "Completado";
            this.tabPageDone.UseVisualStyleBackColor = true;
            // 
            // dgvDone
            // 
            this.dgvDone.AllowUserToAddRows = false;
            this.dgvDone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDone.Location = new System.Drawing.Point(3, 3);
            this.dgvDone.Name = "dgvDone";
            this.dgvDone.ReadOnly = true;
            this.dgvDone.RowTemplate.Height = 24;
            this.dgvDone.Size = new System.Drawing.Size(745, 309);
            this.dgvDone.TabIndex = 0;
            // 
            // btnOpenFileLocation
            // 
            this.btnOpenFileLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOpenFileLocation.Location = new System.Drawing.Point(576, 351);
            this.btnOpenFileLocation.Name = "btnOpenFileLocation";
            this.btnOpenFileLocation.Size = new System.Drawing.Size(186, 33);
            this.btnOpenFileLocation.TabIndex = 6;
            this.btnOpenFileLocation.Text = "Abrir Ubicación del Archivo";
            this.btnOpenFileLocation.UseVisualStyleBackColor = true;
            this.btnOpenFileLocation.Click += new System.EventHandler(this.btnOpenFileLocation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 387);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Organizador";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageToDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDo)).EndInit();
            this.tabPageInProgress.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInProgress)).EndInit();
            this.tabPageDone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDone)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageToDo;
        private System.Windows.Forms.TabPage tabPageInProgress;
        private System.Windows.Forms.DataGridView dgvInProgress;
        private System.Windows.Forms.TabPage tabPageDone;
        private System.Windows.Forms.DataGridView dgvDone;
        private System.Windows.Forms.DataGridView dgvToDo;
        private System.Windows.Forms.Button btnAddTask;
        private System.Windows.Forms.Button btnModifyTask;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnOpenFileLocation;
    }
}

