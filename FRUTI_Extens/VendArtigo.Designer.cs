
namespace FRUTI_Extens
{
    partial class VendArtigo
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
            if (disposing && (components != null)) {
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
            this.dtPicker_dataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_dataInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.cBox_subfamilia = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtPicker_dataFinal
            // 
            this.dtPicker_dataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_dataFinal.Location = new System.Drawing.Point(137, 39);
            this.dtPicker_dataFinal.Name = "dtPicker_dataFinal";
            this.dtPicker_dataFinal.Size = new System.Drawing.Size(140, 22);
            this.dtPicker_dataFinal.TabIndex = 1;
            // 
            // dtPicker_dataInicial
            // 
            this.dtPicker_dataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_dataInicial.Location = new System.Drawing.Point(137, 79);
            this.dtPicker_dataInicial.Name = "dtPicker_dataInicial";
            this.dtPicker_dataInicial.Size = new System.Drawing.Size(140, 22);
            this.dtPicker_dataInicial.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtPicker_dataFinal);
            this.groupBox1.Controls.Add(this.dtPicker_dataInicial);
            this.groupBox1.Location = new System.Drawing.Point(88, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 127);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametros";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Data Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data Inicial:";
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Imprimir.Location = new System.Drawing.Point(150, 229);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(112, 34);
            this.btn_Imprimir.TabIndex = 4;
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.UseVisualStyleBackColor = true;
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(278, 229);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(112, 34);
            this.btn_Cancelar.TabIndex = 5;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // cBox_subfamilia
            // 
            this.cBox_subfamilia.FormattingEnabled = true;
            this.cBox_subfamilia.Location = new System.Drawing.Point(141, 168);
            this.cBox_subfamilia.Name = "cBox_subfamilia";
            this.cBox_subfamilia.Size = new System.Drawing.Size(249, 24);
            this.cBox_subfamilia.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Subfamilia:";
            // 
            // VendArtigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 275);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cBox_subfamilia);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Imprimir);
            this.Controls.Add(this.groupBox1);
            this.Name = "VendArtigo";
            this.Text = "Vendas por Artigo";
            this.Load += new System.EventHandler(this.VendArtigo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtPicker_dataFinal;
        private System.Windows.Forms.DateTimePicker dtPicker_dataInicial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Imprimir;
        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.ComboBox cBox_subfamilia;
        private System.Windows.Forms.Label label3;
    }
}