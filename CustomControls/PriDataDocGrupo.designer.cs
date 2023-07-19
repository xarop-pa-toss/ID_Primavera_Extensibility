
namespace FRU_AlterarTerceiros
{
    partial class PriDataDocGrupo
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.date_DataDocInicio = new System.Windows.Forms.DateTimePicker();
            this.date_DataDocFim = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.date_DataDocInicio);
            this.groupBox1.Controls.Add(this.date_DataDocFim);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 86);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Documento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Final:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Inicial:";
            // 
            // date_DataDocInicio
            // 
            this.date_DataDocInicio.CustomFormat = "DD/MM/YYYY";
            this.date_DataDocInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_DataDocInicio.Location = new System.Drawing.Point(51, 22);
            this.date_DataDocInicio.Margin = new System.Windows.Forms.Padding(2);
            this.date_DataDocInicio.Name = "date_DataDocInicio";
            this.date_DataDocInicio.Size = new System.Drawing.Size(87, 20);
            this.date_DataDocInicio.TabIndex = 18;
            // 
            // date_DataDocFim
            // 
            this.date_DataDocFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_DataDocFim.Location = new System.Drawing.Point(51, 55);
            this.date_DataDocFim.Margin = new System.Windows.Forms.Padding(2);
            this.date_DataDocFim.Name = "date_DataDocFim";
            this.date_DataDocFim.Size = new System.Drawing.Size(87, 20);
            this.date_DataDocFim.TabIndex = 19;
            // 
            // PriDataDocGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PriDataDocGrupo";
            this.Size = new System.Drawing.Size(159, 91);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker date_DataDocInicio;
        private System.Windows.Forms.DateTimePicker date_DataDocFim;
    }
}
