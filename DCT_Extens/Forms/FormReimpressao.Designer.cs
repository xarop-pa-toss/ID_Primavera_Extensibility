using System.Drawing;
using System.Windows.Forms;

namespace DCT_Extens.Forms
{
    partial class FormReimpressao
    {
        private Button btn_Actualizar;
        private Button btn_Imprimir;
        private Label labelInicial;
        private Label labelFinal;
        private DateTimePicker dtPicker_DataDocInicial;
        private DateTimePicker dtPicker_DataDocFinal;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReimpressao));
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.labelInicial = new System.Windows.Forms.Label();
            this.labelFinal = new System.Windows.Forms.Label();
            this.dtPicker_DataDocInicial = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_DataDocFinal = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Actualizar
            // 
            this.btn_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Actualizar.Image")));
            this.btn_Actualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Actualizar.Location = new System.Drawing.Point(10, 10);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(84, 27);
            this.btn_Actualizar.TabIndex = 0;
            this.btn_Actualizar.Text = "Button1";
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Imprimir.Image")));
            this.btn_Imprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Imprimir.Location = new System.Drawing.Point(100, 10);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(84, 27);
            this.btn_Imprimir.TabIndex = 1;
            this.btn_Imprimir.Text = "Imprimir";
            // 
            // labelInicial
            // 
            this.labelInicial.Location = new System.Drawing.Point(5, 26);
            this.labelInicial.Name = "labelInicial";
            this.labelInicial.Size = new System.Drawing.Size(39, 16);
            this.labelInicial.TabIndex = 0;
            this.labelInicial.Text = "Inicial:";
            // 
            // labelFinal
            // 
            this.labelFinal.Location = new System.Drawing.Point(6, 55);
            this.labelFinal.Name = "labelFinal";
            this.labelFinal.Size = new System.Drawing.Size(35, 16);
            this.labelFinal.TabIndex = 1;
            this.labelFinal.Text = "Final:";
            // 
            // dtPicker_DataDocInicial
            // 
            this.dtPicker_DataDocInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataDocInicial.Location = new System.Drawing.Point(45, 26);
            this.dtPicker_DataDocInicial.Name = "dtPicker_DataDocInicial";
            this.dtPicker_DataDocInicial.Size = new System.Drawing.Size(97, 20);
            this.dtPicker_DataDocInicial.TabIndex = 2;
            // 
            // dtPicker_DataDocFinal
            // 
            this.dtPicker_DataDocFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataDocFinal.Location = new System.Drawing.Point(45, 55);
            this.dtPicker_DataDocFinal.Name = "dtPicker_DataDocFinal";
            this.dtPicker_DataDocFinal.Size = new System.Drawing.Size(97, 20);
            this.dtPicker_DataDocFinal.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInicial);
            this.groupBox1.Controls.Add(this.labelFinal);
            this.groupBox1.Controls.Add(this.dtPicker_DataDocFinal);
            this.groupBox1.Controls.Add(this.dtPicker_DataDocInicial);
            this.groupBox1.Location = new System.Drawing.Point(10, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 92);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data dos Documentos";
            // 
            // FormReimpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.btn_Imprimir);
            this.Name = "FormReimpressao";
            this.Size = new System.Drawing.Size(500, 465);
            this.Text = "FormReimpressão";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
    }
}