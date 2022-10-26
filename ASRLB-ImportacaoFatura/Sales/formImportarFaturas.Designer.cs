
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
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFicheiroPath = new System.Windows.Forms.TextBox();
            this.btnEscolherFicheiro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(278, 295);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 0;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(359, 295);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(18, 54);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(416, 225);
            this.listBox.TabIndex = 2;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ficheiro:";
            // 
            // txtFicheiroPath
            // 
            this.txtFicheiroPath.Location = new System.Drawing.Point(71, 32);
            this.txtFicheiroPath.Name = "txtFicheiroPath";
            this.txtFicheiroPath.Size = new System.Drawing.Size(333, 20);
            this.txtFicheiroPath.TabIndex = 4;
            this.txtFicheiroPath.Text = "import.txt no servidor";
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(410, 30);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(24, 23);
            this.btnEscolherFicheiro.TabIndex = 5;
            this.btnEscolherFicheiro.Text = "F";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.Click += new System.EventHandler(this.btnEscolherFicheiro_Click);
            // 
            // janelaImportarFatura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnEscolherFicheiro);
            this.Controls.Add(this.txtFicheiroPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnIniciar);
            this.Name = "janelaImportarFatura";
            this.Size = new System.Drawing.Size(451, 330);
            this.Text = "PriCustomForm1";
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
    }
}