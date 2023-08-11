
namespace FRU_AlterarTerceiros
{
    partial class PriNumDocGrupo
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.num_NumDocInicio = new System.Windows.Forms.NumericUpDown();
            this.num_NumDocFim = new System.Windows.Forms.NumericUpDown();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.num_NumDocInicio);
            this.groupBox2.Controls.Add(this.num_NumDocFim);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 86);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Num Documentos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicial:";
            // 
            // num_NumDocInicio
            // 
            this.num_NumDocInicio.Location = new System.Drawing.Point(55, 23);
            this.num_NumDocInicio.Margin = new System.Windows.Forms.Padding(2);
            this.num_NumDocInicio.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_NumDocInicio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumDocInicio.Name = "num_NumDocInicio";
            this.num_NumDocInicio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.num_NumDocInicio.Size = new System.Drawing.Size(59, 20);
            this.num_NumDocInicio.TabIndex = 14;
            this.num_NumDocInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumDocInicio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // num_NumDocFim
            // 
            this.num_NumDocFim.Location = new System.Drawing.Point(55, 55);
            this.num_NumDocFim.Margin = new System.Windows.Forms.Padding(2);
            this.num_NumDocFim.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_NumDocFim.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumDocFim.Name = "num_NumDocFim";
            this.num_NumDocFim.Size = new System.Drawing.Size(59, 20);
            this.num_NumDocFim.TabIndex = 17;
            this.num_NumDocFim.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            // 
            // PriNumDocGrupo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "PriNumDocGrupo";
            this.Size = new System.Drawing.Size(134, 93);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_NumDocInicio;
        private System.Windows.Forms.NumericUpDown num_NumDocFim;
    }
}
