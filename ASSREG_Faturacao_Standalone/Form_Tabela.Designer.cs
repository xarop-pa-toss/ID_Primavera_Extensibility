
namespace ASSREG_Faturacao_Standalone
{
    partial class Form_Tabela
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
            this.DtGrid_unfiltered = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DtGrid_unfiltered)).BeginInit();
            this.SuspendLayout();
            // 
            // DtGrid_unfiltered
            // 
            this.DtGrid_unfiltered.AllowUserToAddRows = false;
            this.DtGrid_unfiltered.AllowUserToDeleteRows = false;
            this.DtGrid_unfiltered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DtGrid_unfiltered.Location = new System.Drawing.Point(12, 12);
            this.DtGrid_unfiltered.Name = "DtGrid_unfiltered";
            this.DtGrid_unfiltered.RowHeadersWidth = 51;
            this.DtGrid_unfiltered.Size = new System.Drawing.Size(1348, 719);
            this.DtGrid_unfiltered.TabIndex = 0;
            // 
            // Form_Tabela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 743);
            this.Controls.Add(this.DtGrid_unfiltered);
            this.Name = "Form_Tabela";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Tabela_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DtGrid_unfiltered)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DtGrid_unfiltered;
    }
}

