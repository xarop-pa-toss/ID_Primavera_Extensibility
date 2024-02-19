namespace PP_Qualidade
{
    partial class FormQueryStatus_WF
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
            this.lbl_QueryStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_QueryStatus
            // 
            this.lbl_QueryStatus.AutoSize = true;
            this.lbl_QueryStatus.Location = new System.Drawing.Point(26, 35);
            this.lbl_QueryStatus.Name = "lbl_QueryStatus";
            this.lbl_QueryStatus.Size = new System.Drawing.Size(35, 13);
            this.lbl_QueryStatus.TabIndex = 0;
            this.lbl_QueryStatus.Text = "label1";
            // 
            // FormQueryStatus_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 82);
            this.Controls.Add(this.lbl_QueryStatus);
            this.Name = "FormQueryStatus_WF";
            this.Text = "A ler base de dados...";
            this.Load += new System.EventHandler(this.FormQueryStatus_WF_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_QueryStatus;
    }
}