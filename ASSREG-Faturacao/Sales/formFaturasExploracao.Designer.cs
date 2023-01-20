
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
            this.btnConfirmar.Location = new System.Drawing.Point(472, 627);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(74, 23);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // listBoxErros
            // 
            this.listBoxErros.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxErros.FormattingEnabled = true;
            this.listBoxErros.Location = new System.Drawing.Point(17, 370);
            this.listBoxErros.Name = "listBoxErros";
            this.listBoxErros.Size = new System.Drawing.Size(530, 251);
            this.listBoxErros.TabIndex = 2;
            // 
            // listBoxFicheiros
            // 
            this.listBoxFicheiros.FormattingEnabled = true;
            this.listBoxFicheiros.HorizontalScrollbar = true;
            this.listBoxFicheiros.Location = new System.Drawing.Point(17, 48);
            this.listBoxFicheiros.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.listBoxFicheiros.Name = "listBoxFicheiros";
            this.listBoxFicheiros.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxFicheiros.Size = new System.Drawing.Size(530, 277);
            this.listBoxFicheiros.TabIndex = 3;
            // 
            // btnRemover
            // 
            this.btnRemover.Location = new System.Drawing.Point(481, 329);
            this.btnRemover.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(65, 25);
            this.btnRemover.TabIndex = 4;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnApagarLista
            // 
            this.btnApagarLista.Location = new System.Drawing.Point(386, 329);
            this.btnApagarLista.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApagarLista.Name = "btnApagarLista";
            this.btnApagarLista.Size = new System.Drawing.Size(81, 25);
            this.btnApagarLista.TabIndex = 5;
            this.btnApagarLista.Text = "Limpar Lista";
            this.btnApagarLista.UseVisualStyleBackColor = true;
            this.btnApagarLista.Click += new System.EventHandler(this.btnApagarLista_Click);
            // 
            // btnEscolherFicheiro
            // 
            this.btnEscolherFicheiro.Location = new System.Drawing.Point(446, 14);
            this.btnEscolherFicheiro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnEscolherFicheiro.Name = "btnEscolherFicheiro";
            this.btnEscolherFicheiro.Size = new System.Drawing.Size(100, 29);
            this.btnEscolherFicheiro.TabIndex = 6;
            this.btnEscolherFicheiro.Text = "Escolher Ficheiros";
            this.btnEscolherFicheiro.UseVisualStyleBackColor = true;
            this.btnEscolherFicheiro.Click += new System.EventHandler(this.btnEscolherFicheiro_Click);
            // 
            // janelaFaturasExploracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnEscolherFicheiro);
            this.Controls.Add(this.btnApagarLista);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.listBoxFicheiros);
            this.Controls.Add(this.listBoxErros);
            this.Controls.Add(this.btnConfirmar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "janelaFaturasExploracao";
            this.Size = new System.Drawing.Size(567, 659);
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