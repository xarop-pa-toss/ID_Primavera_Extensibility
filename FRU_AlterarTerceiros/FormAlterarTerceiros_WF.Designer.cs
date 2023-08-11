
namespace FRU_AlterarTerceiros
{
    partial class FormAlterarTerceiros_WF
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
            if (disposing && (components != null)) {
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
            this.cbox_Serie = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datepicker_DataDocInicio = new System.Windows.Forms.DateTimePicker();
            this.datepicker_DataDocFim = new System.Windows.Forms.DateTimePicker();
            this.btn_ActualizarPriGrelha = new System.Windows.Forms.Button();
            this.btn_AlterarTerceiro = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.num_NumDocInicio = new System.Windows.Forms.NumericUpDown();
            this.num_NumDocFim = new System.Windows.Forms.NumericUpDown();
            this.datagrid_Docs = new System.Windows.Forms.DataGridView();
            this.Cf = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumDoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoTerceiro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cbox_Docs = new System.Windows.Forms.ComboBox();
            this.cbox_TipoTerceiro = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Docs)).BeginInit();
            this.SuspendLayout();
            // 
            // cbox_Serie
            // 
            this.cbox_Serie.FormattingEnabled = true;
            this.cbox_Serie.Location = new System.Drawing.Point(431, 27);
            this.cbox_Serie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbox_Serie.Name = "cbox_Serie";
            this.cbox_Serie.Size = new System.Drawing.Size(95, 24);
            this.cbox_Serie.TabIndex = 28;
            this.cbox_Serie.SelectedIndexChanged += new System.EventHandler(this.cbox_Serie_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Final:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Inicial:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datepicker_DataDocInicio);
            this.groupBox1.Controls.Add(this.datepicker_DataDocFim);
            this.groupBox1.Location = new System.Drawing.Point(219, 86);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(201, 106);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Documento";
            // 
            // datepicker_DataDocInicio
            // 
            this.datepicker_DataDocInicio.CustomFormat = "DD/MM/YYYY";
            this.datepicker_DataDocInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocInicio.Location = new System.Drawing.Point(68, 26);
            this.datepicker_DataDocInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datepicker_DataDocInicio.Name = "datepicker_DataDocInicio";
            this.datepicker_DataDocInicio.Size = new System.Drawing.Size(115, 22);
            this.datepicker_DataDocInicio.TabIndex = 18;
            // 
            // datepicker_DataDocFim
            // 
            this.datepicker_DataDocFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocFim.Location = new System.Drawing.Point(68, 68);
            this.datepicker_DataDocFim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datepicker_DataDocFim.Name = "datepicker_DataDocFim";
            this.datepicker_DataDocFim.Size = new System.Drawing.Size(115, 22);
            this.datepicker_DataDocFim.TabIndex = 19;
            // 
            // btn_ActualizarPriGrelha
            // 
            this.btn_ActualizarPriGrelha.Location = new System.Drawing.Point(429, 150);
            this.btn_ActualizarPriGrelha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ActualizarPriGrelha.Name = "btn_ActualizarPriGrelha";
            this.btn_ActualizarPriGrelha.Size = new System.Drawing.Size(121, 42);
            this.btn_ActualizarPriGrelha.TabIndex = 34;
            this.btn_ActualizarPriGrelha.Text = "Actualizar";
            this.btn_ActualizarPriGrelha.UseVisualStyleBackColor = true;
            this.btn_ActualizarPriGrelha.Click += new System.EventHandler(this.btn_ActualizarPriGrelha_Click);
            // 
            // btn_AlterarTerceiro
            // 
            this.btn_AlterarTerceiro.Location = new System.Drawing.Point(649, 150);
            this.btn_AlterarTerceiro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AlterarTerceiro.Name = "btn_AlterarTerceiro";
            this.btn_AlterarTerceiro.Size = new System.Drawing.Size(147, 42);
            this.btn_AlterarTerceiro.TabIndex = 29;
            this.btn_AlterarTerceiro.Text = "Alterar Terceiro";
            this.btn_AlterarTerceiro.UseVisualStyleBackColor = true;
            this.btn_AlterarTerceiro.Click += new System.EventHandler(this.btn_AlterarTerceiro_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.num_NumDocInicio);
            this.groupBox2.Controls.Add(this.num_NumDocFim);
            this.groupBox2.Location = new System.Drawing.Point(28, 86);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(168, 106);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Num Documentos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicial:";
            // 
            // num_NumDocInicio
            // 
            this.num_NumDocInicio.Location = new System.Drawing.Point(72, 28);
            this.num_NumDocInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.num_NumDocInicio.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_NumDocInicio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumDocInicio.Name = "num_NumDocInicio";
            this.num_NumDocInicio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.num_NumDocInicio.Size = new System.Drawing.Size(89, 22);
            this.num_NumDocInicio.TabIndex = 14;
            this.num_NumDocInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumDocInicio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_NumDocFim
            // 
            this.num_NumDocFim.Location = new System.Drawing.Point(72, 68);
            this.num_NumDocFim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.num_NumDocFim.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_NumDocFim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumDocFim.Name = "num_NumDocFim";
            this.num_NumDocFim.Size = new System.Drawing.Size(89, 22);
            this.num_NumDocFim.TabIndex = 17;
            this.num_NumDocFim.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            // 
            // datagrid_Docs
            // 
            this.datagrid_Docs.AllowUserToAddRows = false;
            this.datagrid_Docs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_Docs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cf,
            this.Data,
            this.TipoDoc,
            this.Serie,
            this.NumDoc,
            this.TipoTerceiro,
            this.TotalDocumento});
            this.datagrid_Docs.EnableHeadersVisualStyles = false;
            this.datagrid_Docs.Location = new System.Drawing.Point(28, 217);
            this.datagrid_Docs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datagrid_Docs.Name = "datagrid_Docs";
            this.datagrid_Docs.RowHeadersVisible = false;
            this.datagrid_Docs.RowHeadersWidth = 51;
            this.datagrid_Docs.RowTemplate.Height = 24;
            this.datagrid_Docs.Size = new System.Drawing.Size(768, 644);
            this.datagrid_Docs.TabIndex = 38;
            // 
            // Cf
            // 
            this.Cf.HeaderText = "Cf";
            this.Cf.MinimumWidth = 6;
            this.Cf.Name = "Cf";
            this.Cf.Width = 40;
            // 
            // Data
            // 
            this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Data.HeaderText = "Data";
            this.Data.MinimumWidth = 6;
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 67;
            // 
            // TipoDoc
            // 
            this.TipoDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TipoDoc.HeaderText = "Documento";
            this.TipoDoc.MinimumWidth = 6;
            this.TipoDoc.Name = "TipoDoc";
            this.TipoDoc.ReadOnly = true;
            this.TipoDoc.Width = 109;
            // 
            // Serie
            // 
            this.Serie.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Serie.HeaderText = "Serie";
            this.Serie.MinimumWidth = 6;
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            this.Serie.Width = 70;
            // 
            // NumDoc
            // 
            this.NumDoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.NumDoc.HeaderText = "Número";
            this.NumDoc.MinimumWidth = 6;
            this.NumDoc.Name = "NumDoc";
            this.NumDoc.ReadOnly = true;
            this.NumDoc.Width = 87;
            // 
            // TipoTerceiro
            // 
            this.TipoTerceiro.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TipoTerceiro.HeaderText = "TipoTerceiro";
            this.TipoTerceiro.MinimumWidth = 6;
            this.TipoTerceiro.Name = "TipoTerceiro";
            this.TipoTerceiro.ReadOnly = true;
            this.TipoTerceiro.Width = 118;
            // 
            // TotalDocumento
            // 
            this.TotalDocumento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TotalDocumento.HeaderText = "Total";
            this.TotalDocumento.MinimumWidth = 6;
            this.TotalDocumento.Name = "TotalDocumento";
            this.TotalDocumento.ReadOnly = true;
            this.TotalDocumento.Width = 69;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 39;
            this.label3.Text = "Documento:";
            // 
            // cbox_Docs
            // 
            this.cbox_Docs.FormattingEnabled = true;
            this.cbox_Docs.Location = new System.Drawing.Point(120, 27);
            this.cbox_Docs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbox_Docs.Name = "cbox_Docs";
            this.cbox_Docs.Size = new System.Drawing.Size(300, 24);
            this.cbox_Docs.TabIndex = 40;
            this.cbox_Docs.SelectedIndexChanged += new System.EventHandler(this.cBox_Docs_SelectedIndexChanged);
            // 
            // cbox_TipoTerceiro
            // 
            this.cbox_TipoTerceiro.FormattingEnabled = true;
            this.cbox_TipoTerceiro.Location = new System.Drawing.Point(649, 109);
            this.cbox_TipoTerceiro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbox_TipoTerceiro.Name = "cbox_TipoTerceiro";
            this.cbox_TipoTerceiro.Size = new System.Drawing.Size(147, 24);
            this.cbox_TipoTerceiro.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(546, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 42;
            this.label4.Text = "Tipo Terceiro:";
            // 
            // FormAlterarTerceiros_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(823, 879);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbox_TipoTerceiro);
            this.Controls.Add(this.cbox_Docs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datagrid_Docs);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cbox_Serie);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_ActualizarPriGrelha);
            this.Controls.Add(this.btn_AlterarTerceiro);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(841, 926);
            this.MinimumSize = new System.Drawing.Size(841, 926);
            this.Name = "FormAlterarTerceiros_WF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alterar Terceiros";
            this.Load += new System.EventHandler(this.FormAlterarTerceiros_WF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Docs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbox_Serie;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocInicio;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocFim;
        private System.Windows.Forms.Button btn_ActualizarPriGrelha;
        private System.Windows.Forms.Button btn_AlterarTerceiro;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_NumDocInicio;
        private System.Windows.Forms.NumericUpDown num_NumDocFim;
        private System.Windows.Forms.DataGridView datagrid_Docs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbox_Docs;
        private System.Windows.Forms.ComboBox cbox_TipoTerceiro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Cf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumDoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoTerceiro;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDocumento;
    }
}