
namespace PP_PPCS
{
    partial class FormPropDocVenda
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxDocumento = new System.Windows.Forms.ComboBox();
            this.cBoxSerie = new System.Windows.Forms.ComboBox();
            this.cBoxNumero = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Documento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Série";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Número";
            // 
            // cBoxDocumento
            // 
            this.cBoxDocumento.FormattingEnabled = true;
            this.cBoxDocumento.Location = new System.Drawing.Point(106, 37);
            this.cBoxDocumento.Name = "cBoxDocumento";
            this.cBoxDocumento.Size = new System.Drawing.Size(121, 21);
            this.cBoxDocumento.TabIndex = 3;
            this.cBoxDocumento.SelectedIndexChanged += new System.EventHandler(this.cBoxDocumento_SelectedIndexChanged);
            // 
            // cBoxSerie
            // 
            this.cBoxSerie.FormattingEnabled = true;
            this.cBoxSerie.Location = new System.Drawing.Point(289, 37);
            this.cBoxSerie.Name = "cBoxSerie";
            this.cBoxSerie.Size = new System.Drawing.Size(121, 21);
            this.cBoxSerie.TabIndex = 4;
            // 
            // cBoxNumero
            // 
            this.cBoxNumero.FormattingEnabled = true;
            this.cBoxNumero.Location = new System.Drawing.Point(491, 37);
            this.cBoxNumero.Name = "cBoxNumero";
            this.cBoxNumero.Size = new System.Drawing.Size(121, 21);
            this.cBoxNumero.TabIndex = 5;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(661, 37);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(99, 37);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(661, 443);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(99, 33);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            // 
            // FormPropDocVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.cBoxNumero);
            this.Controls.Add(this.cBoxSerie);
            this.Controls.Add(this.cBoxDocumento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormPropDocVenda";
            this.Size = new System.Drawing.Size(794, 498);
            this.Text = "FormPropDocVenda";
            this.Load += new System.EventHandler(this.FormPropDocVenda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxDocumento;
        private System.Windows.Forms.ComboBox cBoxSerie;
        private System.Windows.Forms.ComboBox cBoxNumero;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSair;
    }
}