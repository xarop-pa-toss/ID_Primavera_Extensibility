namespace DCT_Extens
{
    partial class FormStockQuebras
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBox_Operador = new System.Windows.Forms.ComboBox();
            this.txtBox_MotivoQuebra = new System.Windows.Forms.TextBox();
            this.chkBox_RepetirMotivo = new System.Windows.Forms.CheckBox();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Operador:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Motivo:";
            // 
            // cmbBox_Operador
            // 
            this.cmbBox_Operador.FormattingEnabled = true;
            this.cmbBox_Operador.Location = new System.Drawing.Point(105, 25);
            this.cmbBox_Operador.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBox_Operador.Name = "cmbBox_Operador";
            this.cmbBox_Operador.Size = new System.Drawing.Size(252, 24);
            this.cmbBox_Operador.TabIndex = 2;
            // 
            // txtBox_MotivoQuebra
            // 
            this.txtBox_MotivoQuebra.Location = new System.Drawing.Point(105, 64);
            this.txtBox_MotivoQuebra.Margin = new System.Windows.Forms.Padding(4);
            this.txtBox_MotivoQuebra.Multiline = true;
            this.txtBox_MotivoQuebra.Name = "txtBox_MotivoQuebra";
            this.txtBox_MotivoQuebra.Size = new System.Drawing.Size(252, 101);
            this.txtBox_MotivoQuebra.TabIndex = 3;
            this.txtBox_MotivoQuebra.TextChanged += new System.EventHandler(this.txtBox_MotivoQuebra_TextChanged);
            // 
            // chkBox_RepetirMotivo
            // 
            this.chkBox_RepetirMotivo.AutoSize = true;
            this.chkBox_RepetirMotivo.Location = new System.Drawing.Point(164, 174);
            this.chkBox_RepetirMotivo.Margin = new System.Windows.Forms.Padding(4);
            this.chkBox_RepetirMotivo.Name = "chkBox_RepetirMotivo";
            this.chkBox_RepetirMotivo.Size = new System.Drawing.Size(182, 20);
            this.chkBox_RepetirMotivo.TabIndex = 4;
            this.chkBox_RepetirMotivo.Text = "Igual para todas as linhas";
            this.chkBox_RepetirMotivo.UseVisualStyleBackColor = true;
            // 
            // btn_Confirmar
            // 
            this.btn_Confirmar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Confirmar.Location = new System.Drawing.Point(147, 214);
            this.btn_Confirmar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(100, 28);
            this.btn_Confirmar.TabIndex = 5;
            this.btn_Confirmar.Text = "Confirmar";
            this.btn_Confirmar.UseVisualStyleBackColor = true;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btn_Cancelar.Location = new System.Drawing.Point(255, 214);
            this.btn_Cancelar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(104, 28);
            this.btn_Cancelar.TabIndex = 6;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            // 
            // FormStockQuebras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Confirmar);
            this.Controls.Add(this.chkBox_RepetirMotivo);
            this.Controls.Add(this.txtBox_MotivoQuebra);
            this.Controls.Add(this.cmbBox_Operador);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormStockQuebras";
            this.Size = new System.Drawing.Size(388, 263);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormStockQuebras";
            this.Load += new System.EventHandler(this.FormStockQuebras_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBox_Operador;
        private System.Windows.Forms.TextBox txtBox_MotivoQuebra;
        private System.Windows.Forms.CheckBox chkBox_RepetirMotivo;
        private System.Windows.Forms.Button btn_Confirmar;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}