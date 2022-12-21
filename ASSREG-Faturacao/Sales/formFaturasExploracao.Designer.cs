
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
            this.DtGridExploracao = new System.Windows.Forms.DataGridView();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.DtGridExploracao)).BeginInit();
            this.SuspendLayout();
            // 
            // DtGridExploracao
            // 
            this.DtGridExploracao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtGridExploracao.Location = new System.Drawing.Point(17, 16);
            this.DtGridExploracao.Name = "DtGridExploracao";
            this.DtGridExploracao.RowHeadersWidth = 51;
            this.DtGridExploracao.Size = new System.Drawing.Size(834, 602);
            this.DtGridExploracao.TabIndex = 0;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(1190, 632);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(67, 23);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(860, 16);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(397, 602);
            this.listBox.TabIndex = 2;
            // 
            // janelaFaturasExploracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.DtGridExploracao);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "janelaFaturasExploracao";
            this.Size = new System.Drawing.Size(1280, 684);
            this.Text = "janelaFaturasExploracao";
            this.Load += new System.EventHandler(this.formFaturasExploracao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtGridExploracao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DtGridExploracao;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.ListBox listBox;
    }
}