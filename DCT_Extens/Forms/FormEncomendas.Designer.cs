namespace DCT_Extens
{
    partial class FormEncomendas
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGrid_Docs = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_UpdateDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Sair = new System.Windows.Forms.Button();
            this.btn_VerDocs = new System.Windows.Forms.Button();
            this.dtPicker_DataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_DataInicial = new System.Windows.Forms.DateTimePicker();
            this.txtBox_TipoDoc = new System.Windows.Forms.TextBox();
            this.radio_Vendas = new System.Windows.Forms.RadioButton();
            this.radio_Compras = new System.Windows.Forms.RadioButton();
            this.panel_TipoDocRadios = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Docs)).BeginInit();
            this.panel_TipoDocRadios.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 688);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1325, 26);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(394, 20);
            this.toolStripStatusLabel1.Text = "Preencher os campos para pesquisar encomendas abertas.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(943, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Data Final";
            // 
            // dataGrid_Docs
            // 
            this.dataGrid_Docs.AllowUserToAddRows = false;
            this.dataGrid_Docs.AllowUserToDeleteRows = false;
            this.dataGrid_Docs.AllowUserToResizeColumns = false;
            this.dataGrid_Docs.AllowUserToResizeRows = false;
            this.dataGrid_Docs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid_Docs.Location = new System.Drawing.Point(24, 98);
            this.dataGrid_Docs.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid_Docs.Name = "dataGrid_Docs";
            this.dataGrid_Docs.RowHeadersWidth = 51;
            this.dataGrid_Docs.Size = new System.Drawing.Size(1283, 511);
            this.dataGrid_Docs.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(753, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 29;
            this.label2.Text = "Data Inicial";
            // 
            // btn_UpdateDB
            // 
            this.btn_UpdateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateDB.Location = new System.Drawing.Point(24, 625);
            this.btn_UpdateDB.Margin = new System.Windows.Forms.Padding(4);
            this.btn_UpdateDB.Name = "btn_UpdateDB";
            this.btn_UpdateDB.Size = new System.Drawing.Size(177, 53);
            this.btn_UpdateDB.TabIndex = 26;
            this.btn_UpdateDB.Text = "Fechar Documentos";
            this.btn_UpdateDB.UseVisualStyleBackColor = true;
            this.btn_UpdateDB.Click += new System.EventHandler(this.btn_UpdateDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(565, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 28;
            this.label1.Text = "Documento";
            // 
            // btn_Sair
            // 
            this.btn_Sair.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sair.Location = new System.Drawing.Point(1195, 625);
            this.btn_Sair.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Sair.Name = "btn_Sair";
            this.btn_Sair.Size = new System.Drawing.Size(112, 53);
            this.btn_Sair.TabIndex = 25;
            this.btn_Sair.Text = "Sair";
            this.btn_Sair.UseVisualStyleBackColor = true;
            // 
            // btn_VerDocs
            // 
            this.btn_VerDocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_VerDocs.Location = new System.Drawing.Point(1129, 18);
            this.btn_VerDocs.Margin = new System.Windows.Forms.Padding(4);
            this.btn_VerDocs.Name = "btn_VerDocs";
            this.btn_VerDocs.Size = new System.Drawing.Size(177, 53);
            this.btn_VerDocs.TabIndex = 24;
            this.btn_VerDocs.Text = "Ver Documentos";
            this.btn_VerDocs.UseVisualStyleBackColor = true;
            this.btn_VerDocs.Click += new System.EventHandler(this.btn_VerDocs_Click);
            // 
            // dtPicker_DataFinal
            // 
            this.dtPicker_DataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataFinal.Location = new System.Drawing.Point(947, 42);
            this.dtPicker_DataFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtPicker_DataFinal.Name = "dtPicker_DataFinal";
            this.dtPicker_DataFinal.Size = new System.Drawing.Size(132, 22);
            this.dtPicker_DataFinal.TabIndex = 23;
            // 
            // dtPicker_DataInicial
            // 
            this.dtPicker_DataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataInicial.Location = new System.Drawing.Point(757, 42);
            this.dtPicker_DataInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtPicker_DataInicial.Name = "dtPicker_DataInicial";
            this.dtPicker_DataInicial.Size = new System.Drawing.Size(132, 22);
            this.dtPicker_DataInicial.TabIndex = 22;
            // 
            // txtBox_TipoDoc
            // 
            this.txtBox_TipoDoc.Location = new System.Drawing.Point(569, 42);
            this.txtBox_TipoDoc.Margin = new System.Windows.Forms.Padding(4);
            this.txtBox_TipoDoc.Name = "txtBox_TipoDoc";
            this.txtBox_TipoDoc.Size = new System.Drawing.Size(132, 22);
            this.txtBox_TipoDoc.TabIndex = 21;
            // 
            // radio_Vendas
            // 
            this.radio_Vendas.AutoSize = true;
            this.radio_Vendas.Location = new System.Drawing.Point(21, 9);
            this.radio_Vendas.Margin = new System.Windows.Forms.Padding(4);
            this.radio_Vendas.Name = "radio_Vendas";
            this.radio_Vendas.Size = new System.Drawing.Size(75, 20);
            this.radio_Vendas.TabIndex = 32;
            this.radio_Vendas.TabStop = true;
            this.radio_Vendas.Text = "Vendas";
            this.radio_Vendas.UseVisualStyleBackColor = true;
            // 
            // radio_Compras
            // 
            this.radio_Compras.AutoSize = true;
            this.radio_Compras.Location = new System.Drawing.Point(21, 41);
            this.radio_Compras.Margin = new System.Windows.Forms.Padding(4);
            this.radio_Compras.Name = "radio_Compras";
            this.radio_Compras.Size = new System.Drawing.Size(83, 20);
            this.radio_Compras.TabIndex = 33;
            this.radio_Compras.TabStop = true;
            this.radio_Compras.Text = "Compras";
            this.radio_Compras.UseVisualStyleBackColor = true;
            // 
            // panel_TipoDocRadios
            // 
            this.panel_TipoDocRadios.Controls.Add(this.radio_Vendas);
            this.panel_TipoDocRadios.Controls.Add(this.radio_Compras);
            this.panel_TipoDocRadios.Location = new System.Drawing.Point(437, 12);
            this.panel_TipoDocRadios.Margin = new System.Windows.Forms.Padding(4);
            this.panel_TipoDocRadios.Name = "panel_TipoDocRadios";
            this.panel_TipoDocRadios.Size = new System.Drawing.Size(119, 69);
            this.panel_TipoDocRadios.TabIndex = 34;
            // 
            // FormEncomendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_TipoDocRadios);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGrid_Docs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_UpdateDB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Sair);
            this.Controls.Add(this.btn_VerDocs);
            this.Controls.Add(this.dtPicker_DataFinal);
            this.Controls.Add(this.dtPicker_DataInicial);
            this.Controls.Add(this.txtBox_TipoDoc);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormEncomendas";
            this.Size = new System.Drawing.Size(1325, 714);
            this.Text = "Fechar Encomendas";
            this.Load += new System.EventHandler(this.FormEncomendas_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_Docs)).EndInit();
            this.panel_TipoDocRadios.ResumeLayout(false);
            this.panel_TipoDocRadios.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGrid_Docs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_UpdateDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Sair;
        private System.Windows.Forms.Button btn_VerDocs;
        private System.Windows.Forms.DateTimePicker dtPicker_DataFinal;
        private System.Windows.Forms.DateTimePicker dtPicker_DataInicial;
        private System.Windows.Forms.TextBox txtBox_TipoDoc;
        private System.Windows.Forms.RadioButton radio_Vendas;
        private System.Windows.Forms.RadioButton radio_Compras;
        private System.Windows.Forms.Panel panel_TipoDocRadios;
    }
}