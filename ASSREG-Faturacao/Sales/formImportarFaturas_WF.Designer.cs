
namespace ASRLB_ImportacaoFatura.Sales
{
    partial class formImportarFaturas_WF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formImportarFaturas_WF));
            this.cBoxDoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cBoxArtigo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnEscolherFicheiro = new System.Windows.Forms.Button();
            this.txtFicheiroPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cBoxDoc
            // 
            this.cBoxDoc.FormattingEnabled = true;
            this.cBoxDoc.Items.AddRange(new object[] {
            "FTEVB",
            "FTE"});
            this.cBoxDoc.Location = new System.Drawing.Point(277, 42);
            this.cBoxDoc.Margin = new System.Windows.Forms.Padding(4);
            this.cBoxDoc.Name = "cBoxDoc";
            this.cBoxDoc.Size = new System.Drawing.Size(97, 24);
            this.cBoxDoc.TabIndex = 28;
            this.cBoxDoc.Text = "FTEVB";
            this.cBoxDoc.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "Doc:";
            this.label4.UseWaitCursor = true;
            // 
            // cBoxArtigo
            // 
            this.cBoxArtigo.FormattingEnabled = true;
            this.cBoxArtigo.Items.AddRange(new object[] {
            "Não alterar",
            "TE",
            "TC"});
            this.cBoxArtigo.Location = new System.Drawing.Point(441, 42);
            this.cBoxArtigo.Margin = new System.Windows.Forms.Padding(4);
            this.cBoxArtigo.Name = "cBoxArtigo";
            this.cBoxArtigo.Size = new System.Drawing.Size(103, 24);
            this.cBoxArtigo.TabIndex = 26;
            this.cBoxArtigo.Text = "Não alterar";
            this.cBoxArtigo.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "Artigo:";
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Data:";
            this.label2.UseWaitCursor = true;
            // 
            // datePicker
            // 
            this.datePicker.CustomFormat = "dd/MM/yyyy";
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(82, 42);
            this.datePicker.Margin = new System.Windows.Forms.Padding(4);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(131, 22);
            this.datePicker.TabIndex = 23;
            this.datePicker.UseWaitCursor = true;
            this.datePicker.Value = new System.DateTime(2022, 6, 28, 10, 7, 59, 0);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(15, 421);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(53, 33);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.UseWaitCursor = true;
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(553, 9);
            this.btnEscolherFicheiro.Margin = new System.Windows.Forms.Padding(4);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(129, 62);
            this.btnEscolherFicheiro.TabIndex = 21;
            this.btnEscolherFicheiro.Text = "Escolher Ficheiro";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.UseWaitCursor = true;
            // 
            // txtFicheiroPath
            // 
            this.txtFicheiroPath.Location = new System.Drawing.Point(82, 10);
            this.txtFicheiroPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtFicheiroPath.Name = "txtFicheiroPath";
            this.txtFicheiroPath.Size = new System.Drawing.Size(461, 22);
            this.txtFicheiroPath.TabIndex = 20;
            this.txtFicheiroPath.Text = "import.txt no servidor";
            this.txtFicheiroPath.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Ficheiro:";
            this.label1.UseWaitCursor = true;
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(15, 79);
            this.listBox.Margin = new System.Windows.Forms.Padding(4);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(667, 340);
            this.listBox.TabIndex = 18;
            this.listBox.UseWaitCursor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Location = new System.Drawing.Point(582, 427);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 28);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.UseWaitCursor = true;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIniciar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnIniciar.Location = new System.Drawing.Point(472, 427);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(4);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(100, 28);
            this.btnIniciar.TabIndex = 16;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.UseWaitCursor = true;
            // 
            // formImportarFaturas_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 463);
            this.Controls.Add(this.cBoxDoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cBoxArtigo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.datePicker);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnEscolherFicheiro);
            this.Controls.Add(this.txtFicheiroPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnIniciar);
            this.Name = "formImportarFaturas_WF";
            this.Text = "formImportarFaturas_WF";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnEscolherFicheiro;
        private System.Windows.Forms.TextBox txtFicheiroPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnIniciar;
    }
}