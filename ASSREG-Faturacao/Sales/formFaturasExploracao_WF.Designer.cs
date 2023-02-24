
namespace ASRLB_ImportacaoFatura.Sales
{
    partial class formFaturasExploracao_WF
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
            this.btnEscolherFicheiro_WF = new System.Windows.Forms.Button();
            this.btnConfirmar_WF = new System.Windows.Forms.Button();
            this.listBoxErros_WF = new System.Windows.Forms.ListBox();
            this.listBoxFicheiros_WF = new System.Windows.Forms.ListBox();
            this.btnRemover_WF = new System.Windows.Forms.Button();
            this.btnLimparLista_WF = new System.Windows.Forms.Button();
            this.cBoxTipoFatura = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cBoxEmpresa = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnEscolherFicheiro_WF
            // 
            this.btnEscolherFicheiro_WF.Location = new System.Drawing.Point(599, 10);
            this.btnEscolherFicheiro_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEscolherFicheiro_WF.Name = "btnEscolherFicheiro_WF";
            this.btnEscolherFicheiro_WF.Size = new System.Drawing.Size(133, 36);
            this.btnEscolherFicheiro_WF.TabIndex = 6;
            this.btnEscolherFicheiro_WF.Text = "Escolher Ficheiros";
            this.btnEscolherFicheiro_WF.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro_WF.Click += new System.EventHandler(this.btnEscolherFicheiro_WF_Click);
            // 
            // btnConfirmar_WF
            // 
            this.btnConfirmar_WF.Location = new System.Drawing.Point(633, 559);
            this.btnConfirmar_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmar_WF.Name = "btnConfirmar_WF";
            this.btnConfirmar_WF.Size = new System.Drawing.Size(99, 28);
            this.btnConfirmar_WF.TabIndex = 1;
            this.btnConfirmar_WF.Text = "Confirmar";
            this.btnConfirmar_WF.UseVisualStyleBackColor = true;
            this.btnConfirmar_WF.Click += new System.EventHandler(this.btnConfirmar_WF_Click);
            // 
            // listBoxErros_WF
            // 
            this.listBoxErros_WF.FormattingEnabled = true;
            this.listBoxErros_WF.ItemHeight = 16;
            this.listBoxErros_WF.Location = new System.Drawing.Point(27, 243);
            this.listBoxErros_WF.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxErros_WF.Name = "listBoxErros_WF";
            this.listBoxErros_WF.Size = new System.Drawing.Size(705, 292);
            this.listBoxErros_WF.TabIndex = 2;
            // 
            // listBoxFicheiros_WF
            // 
            this.listBoxFicheiros_WF.FormattingEnabled = true;
            this.listBoxFicheiros_WF.HorizontalScrollbar = true;
            this.listBoxFicheiros_WF.ItemHeight = 16;
            this.listBoxFicheiros_WF.Location = new System.Drawing.Point(27, 53);
            this.listBoxFicheiros_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxFicheiros_WF.Name = "listBoxFicheiros_WF";
            this.listBoxFicheiros_WF.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFicheiros_WF.Size = new System.Drawing.Size(705, 116);
            this.listBoxFicheiros_WF.TabIndex = 3;
            // 
            // btnRemover_WF
            // 
            this.btnRemover_WF.Location = new System.Drawing.Point(645, 193);
            this.btnRemover_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemover_WF.Name = "btnRemover_WF";
            this.btnRemover_WF.Size = new System.Drawing.Size(87, 31);
            this.btnRemover_WF.TabIndex = 4;
            this.btnRemover_WF.Text = "Remover";
            this.btnRemover_WF.UseVisualStyleBackColor = true;
            this.btnRemover_WF.Click += new System.EventHandler(this.btnRemover_WF_Click);
            // 
            // btnLimparLista_WF
            // 
            this.btnLimparLista_WF.Location = new System.Drawing.Point(519, 193);
            this.btnLimparLista_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimparLista_WF.Name = "btnLimparLista_WF";
            this.btnLimparLista_WF.Size = new System.Drawing.Size(108, 31);
            this.btnLimparLista_WF.TabIndex = 5;
            this.btnLimparLista_WF.Text = "Limpar Lista";
            this.btnLimparLista_WF.UseVisualStyleBackColor = true;
            this.btnLimparLista_WF.Click += new System.EventHandler(this.btnLimparLista_WF_Click);
            // 
            // cBoxTipoFatura
            // 
            this.cBoxTipoFatura.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cBoxTipoFatura.FormattingEnabled = true;
            this.cBoxTipoFatura.Items.AddRange(new object[] {
            "AHSLP",
            "Benaciate"});
            this.cBoxTipoFatura.Location = new System.Drawing.Point(472, 17);
            this.cBoxTipoFatura.Name = "cBoxTipoFatura";
            this.cBoxTipoFatura.Size = new System.Drawing.Size(101, 24);
            this.cBoxTipoFatura.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Empresa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(386, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tipo Fatura:";
            // 
            // cBoxEmpresa
            // 
            this.cBoxEmpresa.FormattingEnabled = true;
            this.cBoxEmpresa.Items.AddRange(new object[] {
            "0012004",
            "IDCLONE"});
            this.cBoxEmpresa.Location = new System.Drawing.Point(277, 17);
            this.cBoxEmpresa.Name = "cBoxEmpresa";
            this.cBoxEmpresa.Size = new System.Drawing.Size(101, 24);
            this.cBoxEmpresa.TabIndex = 10;
            // 
            // formFaturasExploracao_WF
            // 
            this.ClientSize = new System.Drawing.Size(757, 598);
            this.Controls.Add(this.cBoxEmpresa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cBoxTipoFatura);
            this.Controls.Add(this.btnEscolherFicheiro_WF);
            this.Controls.Add(this.btnLimparLista_WF);
            this.Controls.Add(this.btnConfirmar_WF);
            this.Controls.Add(this.btnRemover_WF);
            this.Controls.Add(this.listBoxErros_WF);
            this.Controls.Add(this.listBoxFicheiros_WF);
            this.MaximumSize = new System.Drawing.Size(785, 850);
            this.Name = "formFaturasExploracao_WF";
            this.Text = "Facturação de Exploração";
            this.Load += new System.EventHandler(this.formFaturasExploracao_WF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEscolherFicheiro;
        private System.Windows.Forms.Button btnApagarLista;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.ListBox listBoxFicheiros;
        private System.Windows.Forms.ListBox listBoxErros;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnEscolherFicheiro_WF;
        private System.Windows.Forms.Button btnConfirmar_WF;
        private System.Windows.Forms.ListBox listBoxErros_WF;
        private System.Windows.Forms.ListBox listBoxFicheiros_WF;
        private System.Windows.Forms.Button btnRemover_WF;
        private System.Windows.Forms.Button btnLimparLista_WF;
        private System.Windows.Forms.ComboBox cBoxTipoFatura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxEmpresa;
    }
}