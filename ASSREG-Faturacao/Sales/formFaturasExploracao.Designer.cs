
namespace ASRLB_ImportacaoFatura.Sales
{
    partial class janelaFaturasExploracao
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
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.listBoxErros = new System.Windows.Forms.ListBox();
            this.listBoxFicheiros = new System.Windows.Forms.ListBox();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnApagarLista = new System.Windows.Forms.Button();
            this.btnEscolherFicheiro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(629, 772);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(99, 28);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // listBoxErros
            // 
            this.listBoxErros.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxErros.FormattingEnabled = true;
            this.listBoxErros.ItemHeight = 16;
            this.listBoxErros.Location = new System.Drawing.Point(23, 455);
            this.listBoxErros.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxErros.Name = "listBoxErros";
            this.listBoxErros.Size = new System.Drawing.Size(705, 308);
            this.listBoxErros.TabIndex = 2;
            // 
            // listBoxFicheiros
            // 
            this.listBoxFicheiros.FormattingEnabled = true;
            this.listBoxFicheiros.HorizontalScrollbar = true;
            this.listBoxFicheiros.ItemHeight = 16;
            this.listBoxFicheiros.Location = new System.Drawing.Point(23, 59);
            this.listBoxFicheiros.Name = "listBoxFicheiros";
            this.listBoxFicheiros.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFicheiros.Size = new System.Drawing.Size(705, 340);
            this.listBoxFicheiros.TabIndex = 3;
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(641, 405);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(87, 31);
            this.btnRemover.TabIndex = 4;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnApagarLista
            // 
            this.btnApagarLista.Location = new System.Drawing.Point(514, 405);
            this.btnApagarLista.Name = "btnApagarLista";
            this.btnApagarLista.Size = new System.Drawing.Size(108, 31);
            this.btnApagarLista.TabIndex = 5;
            this.btnApagarLista.Text = "Limpar Lista";
            this.btnApagarLista.UseVisualStyleBackColor = true;
            this.btnApagarLista.Click += new System.EventHandler(this.btnApagarLista_Click);
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(595, 18);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(133, 35);
            this.btnEscolherFicheiro.TabIndex = 6;
            this.btnEscolherFicheiro.Text = "Escolher Ficheiros";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.Click += new System.EventHandler(this.btnEscolherFicheiro_Click);
            // 
            // janelaFaturasExploracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnEscolherFicheiro);
            this.Controls.Add(this.btnApagarLista);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.listBoxFicheiros);
            this.Controls.Add(this.listBoxErros);
            this.Controls.Add(this.btnConfirmar);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "janelaFaturasExploracao";
            this.Size = new System.Drawing.Size(756, 811);
            this.Text = "janelaFaturasExploracao";
            this.Load += new System.EventHandler(this.formFaturasExploracao_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.ListBox listBoxErros;
        private System.Windows.Forms.ListBox listBoxFicheiros;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnApagarLista;
        private System.Windows.Forms.Button btnEscolherFicheiro;
    }
}