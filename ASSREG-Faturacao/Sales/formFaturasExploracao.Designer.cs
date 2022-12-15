
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
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DtGridExploracao)).BeginInit();
            this.SuspendLayout();
            // 
            // DtGridExploracao
            // 
            this.DtGridExploracao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtGridExploracao.Location = new System.Drawing.Point(23, 20);
            this.DtGridExploracao.Margin = new System.Windows.Forms.Padding(4);
            this.DtGridExploracao.Name = "DtGridExploracao";
            this.DtGridExploracao.RowHeadersWidth = 51;
            this.DtGridExploracao.Size = new System.Drawing.Size(1658, 732);
            this.DtGridExploracao.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1606, 759);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // janelaFaturasExploracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DtGridExploracao);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "janelaFaturasExploracao";
            this.Size = new System.Drawing.Size(1703, 799);
            this.Text = "janelaFaturasExploracao";
            this.Load += new System.EventHandler(this.formFaturasExploracao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtGridExploracao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DtGridExploracao;
        private System.Windows.Forms.Button button1;
    }
}