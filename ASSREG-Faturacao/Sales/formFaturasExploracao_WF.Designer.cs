
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
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxTipoFatura = new System.Windows.Forms.ComboBox();
            this.checkBox_Benaciate = new System.Windows.Forms.CheckBox();
            this.checkBox_Penalizacao = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnEscolherFicheiro_WF
            // 
            this.btnEscolherFicheiro_WF.Location = new System.Drawing.Point(621, 27);
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
            this.btnConfirmar_WF.Location = new System.Drawing.Point(656, 563);
            this.btnConfirmar_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmar_WF.Name = "btnConfirmar_WF";
            this.btnConfirmar_WF.Size = new System.Drawing.Size(99, 31);
            this.btnConfirmar_WF.TabIndex = 1;
            this.btnConfirmar_WF.Text = "Iniciar";
            this.btnConfirmar_WF.UseVisualStyleBackColor = true;
            this.btnConfirmar_WF.Click += new System.EventHandler(this.btnConfirmar_WF_Click);
            // 
            // listBoxErros_WF
            // 
            this.listBoxErros_WF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxErros_WF.FormattingEnabled = true;
            this.listBoxErros_WF.ItemHeight = 17;
            this.listBoxErros_WF.Location = new System.Drawing.Point(13, 213);
            this.listBoxErros_WF.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxErros_WF.Name = "listBoxErros_WF";
            this.listBoxErros_WF.Size = new System.Drawing.Size(741, 344);
            this.listBoxErros_WF.TabIndex = 2;
            // 
            // listBoxFicheiros_WF
            // 
            this.listBoxFicheiros_WF.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxFicheiros_WF.FormattingEnabled = true;
            this.listBoxFicheiros_WF.HorizontalScrollbar = true;
            this.listBoxFicheiros_WF.ItemHeight = 17;
            this.listBoxFicheiros_WF.Location = new System.Drawing.Point(13, 77);
            this.listBoxFicheiros_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxFicheiros_WF.Name = "listBoxFicheiros_WF";
            this.listBoxFicheiros_WF.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFicheiros_WF.Size = new System.Drawing.Size(741, 72);
            this.listBoxFicheiros_WF.TabIndex = 3;
            // 
            // btnRemover_WF
            // 
            this.btnRemover_WF.Location = new System.Drawing.Point(667, 160);
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
            this.btnLimparLista_WF.Location = new System.Drawing.Point(553, 160);
            this.btnLimparLista_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLimparLista_WF.Name = "btnLimparLista_WF";
            this.btnLimparLista_WF.Size = new System.Drawing.Size(108, 31);
            this.btnLimparLista_WF.TabIndex = 5;
            this.btnLimparLista_WF.Text = "Limpar Lista";
            this.btnLimparLista_WF.UseVisualStyleBackColor = true;
            this.btnLimparLista_WF.Click += new System.EventHandler(this.btnLimparLista_WF_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(334, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Tipo Fatura:";
            // 
            // cBoxTipoFatura
            // 
            this.cBoxTipoFatura.FormattingEnabled = true;
            this.cBoxTipoFatura.Items.AddRange(new object[] {
            "FTE",
            "FTEVB"});
            this.cBoxTipoFatura.Location = new System.Drawing.Point(418, 20);
            this.cBoxTipoFatura.Name = "cBoxTipoFatura";
            this.cBoxTipoFatura.Size = new System.Drawing.Size(87, 21);
            this.cBoxTipoFatura.TabIndex = 12;
            // 
            // checkBox_Benaciate
            // 
            this.checkBox_Benaciate.AutoSize = true;
            this.checkBox_Benaciate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_Benaciate.Location = new System.Drawing.Point(534, 46);
            this.checkBox_Benaciate.Name = "checkBox_Benaciate";
            this.checkBox_Benaciate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox_Benaciate.Size = new System.Drawing.Size(74, 17);
            this.checkBox_Benaciate.TabIndex = 15;
            this.checkBox_Benaciate.Text = "Benaciate";
            this.checkBox_Benaciate.UseVisualStyleBackColor = true;
            // 
            // checkBox_Penalizacao
            // 
            this.checkBox_Penalizacao.AutoSize = true;
            this.checkBox_Penalizacao.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_Penalizacao.Location = new System.Drawing.Point(524, 19);
            this.checkBox_Penalizacao.Name = "checkBox_Penalizacao";
            this.checkBox_Penalizacao.Size = new System.Drawing.Size(84, 17);
            this.checkBox_Penalizacao.TabIndex = 16;
            this.checkBox_Penalizacao.Text = "Penalização";
            this.checkBox_Penalizacao.UseVisualStyleBackColor = true;
            // 
            // formFaturasExploracao_WF
            // 
            this.ClientSize = new System.Drawing.Size(767, 601);
            this.Controls.Add(this.checkBox_Penalizacao);
            this.Controls.Add(this.checkBox_Benaciate);
            this.Controls.Add(this.cBoxTipoFatura);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnEscolherFicheiro_WF);
            this.Controls.Add(this.btnLimparLista_WF);
            this.Controls.Add(this.btnConfirmar_WF);
            this.Controls.Add(this.btnRemover_WF);
            this.Controls.Add(this.listBoxErros_WF);
            this.Controls.Add(this.listBoxFicheiros_WF);
            this.MaximumSize = new System.Drawing.Size(785, 850);
            this.Name = "formFaturasExploracao_WF";
            this.Text = "Facturação de Exploração";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formFaturasExploracao_WF_FormClosed);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxTipoFatura;
        private System.Windows.Forms.CheckBox checkBox_Benaciate;
        private System.Windows.Forms.CheckBox checkBox_Penalizacao;
    }
}