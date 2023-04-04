namespace ASRLB_ImportacaoFatura.Sales
{
    partial class formImportarTxt_WF
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
            this.cBoxDoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cBoxArtigo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.btnEscolherFicheiro = new System.Windows.Forms.Button();
            this.txtFicheiroPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cBoxDoc
            // 
            this.cBoxDoc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cBoxDoc.FormattingEnabled = true;
            this.cBoxDoc.Items.AddRange(new object[] {
            "FA",
            "FVB"});
            this.cBoxDoc.Location = new System.Drawing.Point(215, 38);
            this.cBoxDoc.Name = "cBoxDoc";
            this.cBoxDoc.Size = new System.Drawing.Size(64, 21);
            this.cBoxDoc.TabIndex = 41;
            this.cBoxDoc.Text = "FA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label4.Location = new System.Drawing.Point(182, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "Doc:";
            // 
            // cBoxArtigo
            // 
            this.cBoxArtigo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cBoxArtigo.FormattingEnabled = true;
            this.cBoxArtigo.Items.AddRange(new object[] {
            "Não alterar",
            "TE",
            "TC"});
            this.cBoxArtigo.Location = new System.Drawing.Point(338, 38);
            this.cBoxArtigo.Name = "cBoxArtigo";
            this.cBoxArtigo.Size = new System.Drawing.Size(78, 21);
            this.cBoxArtigo.TabIndex = 39;
            this.cBoxArtigo.Text = "Não alterar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.Location = new System.Drawing.Point(296, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 38;
            this.label3.Text = "Artigo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Location = new System.Drawing.Point(32, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "Data:";
            // 
            // datePicker
            // 
            this.datePicker.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.datePicker.CustomFormat = "dd/MM/yyyy";
            this.datePicker.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(69, 38);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(96, 20);
            this.datePicker.TabIndex = 36;
            this.datePicker.Value = new System.DateTime(2022, 6, 28, 10, 7, 59, 0);
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(422, 11);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(97, 46);
            this.btnEscolherFicheiro.TabIndex = 34;
            this.btnEscolherFicheiro.Text = "Escolher Ficheiro";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.Click += new System.EventHandler(this.btnEscolherFicheiro_Click_1);
            // 
            // txtFicheiroPath
            // 
            this.txtFicheiroPath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtFicheiroPath.Location = new System.Drawing.Point(69, 12);
            this.txtFicheiroPath.Name = "txtFicheiroPath";
            this.txtFicheiroPath.Size = new System.Drawing.Size(347, 20);
            this.txtFicheiroPath.TabIndex = 33;
            this.txtFicheiroPath.Text = "import.txt no servidor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Location = new System.Drawing.Point(17, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "Ficheiro:";
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(19, 68);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(501, 277);
            this.listBox.TabIndex = 31;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnCancelar.Location = new System.Drawing.Point(444, 353);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 30;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIniciar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnIniciar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnIniciar.Location = new System.Drawing.Point(362, 353);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 29;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click_1);
            // 
            // formImportarTxt_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 384);
            this.Controls.Add(this.cBoxDoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cBoxArtigo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.btnEscolherFicheiro);
            this.Controls.Add(this.txtFicheiroPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnIniciar);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "formImportarTxt_WF";
            this.Text = "Faturas RegSilv";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cBoxDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBoxArtigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Button btnEscolherFicheiro;
        private System.Windows.Forms.TextBox txtFicheiroPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnIniciar;
    }
}