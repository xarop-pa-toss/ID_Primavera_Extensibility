
namespace ASRLB_ImportacaoFatura
{
    partial class janelaImportarFatura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(janelaImportarFatura));
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFicheiroPath = new System.Windows.Forms.TextBox();
            this.btnEscolherFicheiro = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxArtigo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cBoxDoc = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnIniciar.Location = new System.Drawing.Point(357, 351);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancelar.Location = new System.Drawing.Point(438, 351);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(13, 68);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(500, 277);
            this.listBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ficheiro:";
            // 
            // txtFicheiroPath
            // 
            this.txtFicheiroPath.Location = new System.Drawing.Point(63, 13);
            this.txtFicheiroPath.Name = "txtFicheiroPath";
            this.txtFicheiroPath.Size = new System.Drawing.Size(347, 20);
            this.txtFicheiroPath.TabIndex = 4;
            this.txtFicheiroPath.Text = "import.txt no servidor";
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(416, 12);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(97, 50);
            this.btnEscolherFicheiro.TabIndex = 5;
            this.btnEscolherFicheiro.Text = "Escolher Ficheiro";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.Click += new System.EventHandler(this.btnEscolherFicheiro_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(13, 347);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 27);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // datePicker
            // 
            this.datePicker.CustomFormat = "dd/MM/yyyy";
            this.datePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePicker.Location = new System.Drawing.Point(63, 39);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(99, 20);
            this.datePicker.TabIndex = 10;
            this.datePicker.Value = new System.DateTime(2022, 6, 28, 10, 7, 59, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "Data:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Artigo:";
            // 
            // cBoxArtigo
            // 
            this.cBoxArtigo.FormattingEnabled = true;
            this.cBoxArtigo.Items.AddRange(new object[] {
            "Não alterar",
            "TE",
            "TC"});
            this.cBoxArtigo.Location = new System.Drawing.Point(332, 39);
            this.cBoxArtigo.Name = "cBoxArtigo";
            this.cBoxArtigo.Size = new System.Drawing.Size(78, 21);
            this.cBoxArtigo.TabIndex = 13;
            this.cBoxArtigo.Text = "Não alterar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(176, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Doc:";
            // 
            // cBoxDoc
            // 
            this.cBoxDoc.FormattingEnabled = true;
            this.cBoxDoc.Items.AddRange(new object[] {
            "FTEVB",
            "FTE"});
            this.cBoxDoc.Location = new System.Drawing.Point(209, 39);
            this.cBoxDoc.Name = "cBoxDoc";
            this.cBoxDoc.Size = new System.Drawing.Size(74, 21);
            this.cBoxDoc.TabIndex = 15;
            this.cBoxDoc.Text = "FTEVB";
            // 
            // janelaImportarFatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.Name = "janelaImportarFatura";
            this.Size = new System.Drawing.Size(528, 381);
            this.Text = "Faturas em Lote";
            this.Load += new System.EventHandler(this.janelaImportarFatura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFicheiroPath;
        private System.Windows.Forms.Button btnEscolherFicheiro;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxArtigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBoxDoc;
    }
}