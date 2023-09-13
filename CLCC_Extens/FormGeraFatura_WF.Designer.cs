
namespace CLCC_Extens
{
    partial class FormGeraFatura_WF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGeraFatura_WF));
            this.label1 = new System.Windows.Forms.Label();
            this.txtbox_Ficheiro = new System.Windows.Forms.TextBox();
            this.btn_EscolherFicheiro = new System.Windows.Forms.Button();
            this.listBox_Output = new System.Windows.Forms.ListBox();
            this.btn_Iniciar = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ficheiro:";
            // 
            // txtbox_Ficheiro
            // 
            this.txtbox_Ficheiro.Location = new System.Drawing.Point(65, 21);
            this.txtbox_Ficheiro.Name = "txtbox_Ficheiro";
            this.txtbox_Ficheiro.Size = new System.Drawing.Size(295, 20);
            this.txtbox_Ficheiro.TabIndex = 1;
            // 
            // btn_EscolherFicheiro
            // 
            this.btn_EscolherFicheiro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_EscolherFicheiro.BackgroundImage")));
            this.btn_EscolherFicheiro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_EscolherFicheiro.Location = new System.Drawing.Point(366, 19);
            this.btn_EscolherFicheiro.Name = "btn_EscolherFicheiro";
            this.btn_EscolherFicheiro.Size = new System.Drawing.Size(28, 25);
            this.btn_EscolherFicheiro.TabIndex = 2;
            this.btn_EscolherFicheiro.UseVisualStyleBackColor = true;
            this.btn_EscolherFicheiro.Click += new System.EventHandler(this.btn_EscolherFicheiro_Click);
            // 
            // listBox_Output
            // 
            this.listBox_Output.FormattingEnabled = true;
            this.listBox_Output.Location = new System.Drawing.Point(14, 55);
            this.listBox_Output.Name = "listBox_Output";
            this.listBox_Output.Size = new System.Drawing.Size(773, 355);
            this.listBox_Output.TabIndex = 3;
            // 
            // btn_Iniciar
            // 
            this.btn_Iniciar.Location = new System.Drawing.Point(620, 419);
            this.btn_Iniciar.Name = "btn_Iniciar";
            this.btn_Iniciar.Size = new System.Drawing.Size(75, 28);
            this.btn_Iniciar.TabIndex = 4;
            this.btn_Iniciar.Text = "Iniciar";
            this.btn_Iniciar.UseVisualStyleBackColor = true;
            this.btn_Iniciar.Click += new System.EventHandler(this.btn_Iniciar_Click);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(713, 419);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(75, 28);
            this.btn_Cancelar.TabIndex = 5;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // FormGeraFatura_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 456);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Iniciar);
            this.Controls.Add(this.listBox_Output);
            this.Controls.Add(this.btn_EscolherFicheiro);
            this.Controls.Add(this.txtbox_Ficheiro);
            this.Controls.Add(this.label1);
            this.Name = "FormGeraFatura_WF";
            this.Text = "Gera Faturas";
            this.Load += new System.EventHandler(this.FormGeraFatura_WF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbox_Ficheiro;
        private System.Windows.Forms.Button btn_EscolherFicheiro;
        private System.Windows.Forms.ListBox listBox_Output;
        private System.Windows.Forms.Button btn_Iniciar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}