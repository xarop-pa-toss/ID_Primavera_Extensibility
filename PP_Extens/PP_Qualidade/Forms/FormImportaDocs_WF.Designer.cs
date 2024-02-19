
namespace PP_Qualidade
{
    partial class FormImportaDocs_WF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Actualizar_WF = new System.Windows.Forms.Button();
            this.datepicker_DataDocImportar_WF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Sair_WF = new System.Windows.Forms.Button();
            this.btn_Processar_WF = new System.Windows.Forms.Button();
            this.DataGrid1 = new System.Windows.Forms.DataGridView();
            this.datepicker_DataDocNovo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(188, 469);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 40);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legenda para coluna \"Importar\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "A - Documento anulado   S - Importar documento   N - Não importar";
            // 
            // btn_Actualizar_WF
            // 
            this.btn_Actualizar_WF.Location = new System.Drawing.Point(262, 418);
            this.btn_Actualizar_WF.Name = "btn_Actualizar_WF";
            this.btn_Actualizar_WF.Size = new System.Drawing.Size(75, 33);
            this.btn_Actualizar_WF.TabIndex = 2;
            this.btn_Actualizar_WF.Text = "Actualizar";
            this.btn_Actualizar_WF.UseVisualStyleBackColor = true;
            this.btn_Actualizar_WF.Click += new System.EventHandler(this.btn_Actualizar_WF_Click);
            // 
            // datepicker_DataDocImportar_WF
            // 
            this.datepicker_DataDocImportar_WF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocImportar_WF.Location = new System.Drawing.Point(135, 424);
            this.datepicker_DataDocImportar_WF.Name = "datepicker_DataDocImportar_WF";
            this.datepicker_DataDocImportar_WF.Size = new System.Drawing.Size(105, 20);
            this.datepicker_DataDocImportar_WF.TabIndex = 1;
            this.datepicker_DataDocImportar_WF.ValueChanged += new System.EventHandler(this.datepicker_DataDocImportar_WF_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(399, 428);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Data para documentos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 428);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Data docs a importar:";
            // 
            // btn_Sair_WF
            // 
            this.btn_Sair_WF.Location = new System.Drawing.Point(807, 466);
            this.btn_Sair_WF.Name = "btn_Sair_WF";
            this.btn_Sair_WF.Size = new System.Drawing.Size(90, 36);
            this.btn_Sair_WF.TabIndex = 40;
            this.btn_Sair_WF.Text = "Sair";
            this.btn_Sair_WF.UseVisualStyleBackColor = true;
            this.btn_Sair_WF.Click += new System.EventHandler(this.btn_Sair_WF_Click);
            // 
            // btn_Processar_WF
            // 
            this.btn_Processar_WF.Location = new System.Drawing.Point(807, 420);
            this.btn_Processar_WF.Name = "btn_Processar_WF";
            this.btn_Processar_WF.Size = new System.Drawing.Size(90, 36);
            this.btn_Processar_WF.TabIndex = 39;
            this.btn_Processar_WF.Text = "Processar";
            this.btn_Processar_WF.UseVisualStyleBackColor = true;
            this.btn_Processar_WF.Click += new System.EventHandler(this.btn_Processar_WF_Click);
            // 
            // DataGrid1
            // 
            this.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid1.Location = new System.Drawing.Point(12, 12);
            this.DataGrid1.Name = "DataGrid1";
            this.DataGrid1.RowHeadersWidth = 51;
            this.DataGrid1.Size = new System.Drawing.Size(885, 372);
            this.DataGrid1.TabIndex = 41;
            // 
            // datepicker_DataDocNovo
            // 
            this.datepicker_DataDocNovo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocNovo.Location = new System.Drawing.Point(533, 424);
            this.datepicker_DataDocNovo.Name = "datepicker_DataDocNovo";
            this.datepicker_DataDocNovo.Size = new System.Drawing.Size(100, 20);
            this.datepicker_DataDocNovo.TabIndex = 42;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(533, 450);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 43;
            // 
            // FormImportaDocs_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 518);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.datepicker_DataDocNovo);
            this.Controls.Add(this.DataGrid1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Actualizar_WF);
            this.Controls.Add(this.datepicker_DataDocImportar_WF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Sair_WF);
            this.Controls.Add(this.btn_Processar_WF);
            this.Name = "FormImportaDocs_WF";
            this.Text = "FormImportaDocs_WF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImportaDocs_WF_FormClosing);
            this.Load += new System.EventHandler(this.FormImportaDocs_WF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Actualizar_WF;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocImportar_WF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Sair_WF;
        private System.Windows.Forms.Button btn_Processar_WF;
        private System.Windows.Forms.DataGridView DataGrid1;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocNovo;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}