﻿namespace DCT_Extens.Forms.FormEncomendas
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 558);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(994, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(314, 17);
            this.toolStripStatusLabel1.Text = "Preencher os campos para pesquisar encomendas abertas.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(707, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
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
            this.dataGrid_Docs.Location = new System.Drawing.Point(18, 80);
            this.dataGrid_Docs.Name = "dataGrid_Docs";
            this.dataGrid_Docs.Size = new System.Drawing.Size(962, 415);
            this.dataGrid_Docs.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(565, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Data Inicial";
            // 
            // btn_UpdateDB
            // 
            this.btn_UpdateDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateDB.Location = new System.Drawing.Point(18, 508);
            this.btn_UpdateDB.Name = "btn_UpdateDB";
            this.btn_UpdateDB.Size = new System.Drawing.Size(133, 43);
            this.btn_UpdateDB.TabIndex = 26;
            this.btn_UpdateDB.Text = "Fechar Documentos";
            this.btn_UpdateDB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(424, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Documento";
            // 
            // btn_Sair
            // 
            this.btn_Sair.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sair.Location = new System.Drawing.Point(896, 508);
            this.btn_Sair.Name = "btn_Sair";
            this.btn_Sair.Size = new System.Drawing.Size(84, 43);
            this.btn_Sair.TabIndex = 25;
            this.btn_Sair.Text = "Sair";
            this.btn_Sair.UseVisualStyleBackColor = true;
            // 
            // btn_VerDocs
            // 
            this.btn_VerDocs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_VerDocs.Location = new System.Drawing.Point(847, 15);
            this.btn_VerDocs.Name = "btn_VerDocs";
            this.btn_VerDocs.Size = new System.Drawing.Size(133, 43);
            this.btn_VerDocs.TabIndex = 24;
            this.btn_VerDocs.Text = "Ver Documentos";
            this.btn_VerDocs.UseVisualStyleBackColor = true;
            this.btn_VerDocs.Click += new System.EventHandler(this.btn_VerDocs_Click);
            // 
            // dtPicker_DataFinal
            // 
            this.dtPicker_DataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataFinal.Location = new System.Drawing.Point(710, 34);
            this.dtPicker_DataFinal.Name = "dtPicker_DataFinal";
            this.dtPicker_DataFinal.Size = new System.Drawing.Size(100, 20);
            this.dtPicker_DataFinal.TabIndex = 23;
            // 
            // dtPicker_DataInicial
            // 
            this.dtPicker_DataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataInicial.Location = new System.Drawing.Point(568, 34);
            this.dtPicker_DataInicial.Name = "dtPicker_DataInicial";
            this.dtPicker_DataInicial.Size = new System.Drawing.Size(100, 20);
            this.dtPicker_DataInicial.TabIndex = 22;
            // 
            // txtBox_TipoDoc
            // 
            this.txtBox_TipoDoc.Location = new System.Drawing.Point(427, 34);
            this.txtBox_TipoDoc.Name = "txtBox_TipoDoc";
            this.txtBox_TipoDoc.Size = new System.Drawing.Size(100, 20);
            this.txtBox_TipoDoc.TabIndex = 21;
            // 
            // radio_Vendas
            // 
            this.radio_Vendas.AutoSize = true;
            this.radio_Vendas.Location = new System.Drawing.Point(16, 7);
            this.radio_Vendas.Name = "radio_Vendas";
            this.radio_Vendas.Size = new System.Drawing.Size(61, 17);
            this.radio_Vendas.TabIndex = 32;
            this.radio_Vendas.TabStop = true;
            this.radio_Vendas.Text = "Vendas";
            this.radio_Vendas.UseVisualStyleBackColor = true;
            // 
            // radio_Compras
            // 
            this.radio_Compras.AutoSize = true;
            this.radio_Compras.Location = new System.Drawing.Point(16, 33);
            this.radio_Compras.Name = "radio_Compras";
            this.radio_Compras.Size = new System.Drawing.Size(66, 17);
            this.radio_Compras.TabIndex = 33;
            this.radio_Compras.TabStop = true;
            this.radio_Compras.Text = "Compras";
            this.radio_Compras.UseVisualStyleBackColor = true;
            // 
            // panel_TipoDocRadios
            // 
            this.panel_TipoDocRadios.Controls.Add(this.radio_Vendas);
            this.panel_TipoDocRadios.Controls.Add(this.radio_Compras);
            this.panel_TipoDocRadios.Location = new System.Drawing.Point(328, 10);
            this.panel_TipoDocRadios.Name = "panel_TipoDocRadios";
            this.panel_TipoDocRadios.Size = new System.Drawing.Size(89, 56);
            this.panel_TipoDocRadios.TabIndex = 34;
            // 
            // FormEncomendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
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
            this.Name = "FormEncomendas";
            this.Size = new System.Drawing.Size(994, 580);
            this.Text = "FormEncomendas_Compras";
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