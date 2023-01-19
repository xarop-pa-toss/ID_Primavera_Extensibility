
namespace ASRLB_ImportacaoFatura.Sales
{
    partial class janelaFaturasExploracao_test
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
            this.DtGridExploracao.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.DtGridExploracao.Location = new System.Drawing.Point(23, 20);
            this.DtGridExploracao.Margin = new System.Windows.Forms.Padding(4);
            this.DtGridExploracao.MaximumSize = new System.Drawing.Size(1726, 599);
            this.DtGridExploracao.MinimumSize = new System.Drawing.Size(1726, 599);
            this.DtGridExploracao.Name = "DtGridExploracao";
            this.DtGridExploracao.ReadOnly = true;
            this.DtGridExploracao.RowHeadersWidth = 51;
            this.DtGridExploracao.Size = new System.Drawing.Size(1726, 599);
            this.DtGridExploracao.TabIndex = 0;
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(1660, 779);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(89, 28);
            this.btnConfirmar.TabIndex = 1;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(23, 627);
            this.listBox.Margin = new System.Windows.Forms.Padding(4);
            this.listBox.MaximumSize = new System.Drawing.Size(1726, 148);
            this.listBox.MinimumSize = new System.Drawing.Size(1726, 148);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(1726, 148);
            this.listBox.TabIndex = 2;
            // 
            // janelaFaturasExploracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.DtGridExploracao);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "janelaFaturasExploracao";
            this.Size = new System.Drawing.Size(1769, 817);
            this.Text = "janelaFaturasExploracao";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.janelaFaturasExploracao_FormClosing);
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