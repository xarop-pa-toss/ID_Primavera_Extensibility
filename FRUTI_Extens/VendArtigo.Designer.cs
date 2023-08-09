
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
            this.f4_subfamilia = new PRISDK100.F4();
            this.dtPicker_dataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_dataInicial = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // f4_subfamilia
            // 
            this.f4_subfamilia.AgrupaOutrosTerceiros = false;
            this.f4_subfamilia.Audit = "mnuTabFamilias";
            this.f4_subfamilia.AutoComplete = false;
            this.f4_subfamilia.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_subfamilia.CampoChave = "SubFamilia";
            this.f4_subfamilia.CampoChaveFisica = "";
            this.f4_subfamilia.CampoDescricao = "Descricao";
            this.f4_subfamilia.Caption = "Subfamília:";
            this.f4_subfamilia.CarregarValoresEdicao = false;
            this.f4_subfamilia.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.SubFamilia;
            this.f4_subfamilia.ChaveFisica = "";
            this.f4_subfamilia.ChaveNumerica = false;
            this.f4_subfamilia.F4Modal = false;
            this.f4_subfamilia.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_subfamilia.IDCategoria = "SubFamilias";
            this.f4_subfamilia.Location = new System.Drawing.Point(12, 159);
            this.f4_subfamilia.MaxLengthDescricao = 0;
            this.f4_subfamilia.MaxLengthF4 = 50;
            this.f4_subfamilia.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4_subfamilia.Modulo = "BAS";
            this.f4_subfamilia.MostraDescricao = true;
            this.f4_subfamilia.MostraLink = true;
            this.f4_subfamilia.Name = "f4_subfamilia";
            this.f4_subfamilia.PainesInformacaoRelacionada = false;
            this.f4_subfamilia.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_subfamilia.PermiteDrillDown = true;
            this.f4_subfamilia.PermiteEnabledLink = true;
            this.f4_subfamilia.PodeEditarDescricao = false;
            this.f4_subfamilia.ResourceID = 684;
            this.f4_subfamilia.ResourcePersonalizada = false;
            this.f4_subfamilia.Restricao = "";
            this.f4_subfamilia.SelectionFormula = "";
            this.f4_subfamilia.Size = new System.Drawing.Size(407, 22);
            this.f4_subfamilia.TabIndex = 0;
            this.f4_subfamilia.TextoDescricao = "";
            this.f4_subfamilia.WidthEspacamento = 30;
            this.f4_subfamilia.WidthF4 = 1500;
            this.f4_subfamilia.WidthLink = 1150;
            // 
            // dtPicker_dataFinal
            // 
            this.dtPicker_dataFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_dataFinal.Location = new System.Drawing.Point(106, 39);
            this.dtPicker_dataFinal.Name = "dtPicker_dataFinal";
            this.dtPicker_dataFinal.Size = new System.Drawing.Size(113, 22);
            this.dtPicker_dataFinal.TabIndex = 1;
            // 
            // dtPicker_dataInicial
            // 
            this.dtPicker_dataInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_dataInicial.Location = new System.Drawing.Point(106, 79);
            this.dtPicker_dataInicial.Name = "dtPicker_dataInicial";
            this.dtPicker_dataInicial.Size = new System.Drawing.Size(113, 22);
            this.dtPicker_dataInicial.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtPicker_dataFinal);
            this.groupBox1.Controls.Add(this.dtPicker_dataInicial);
            this.groupBox1.Location = new System.Drawing.Point(98, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 127);
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
            this.btn_Imprimir.Location = new System.Drawing.Point(177, 201);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(112, 34);
            this.btn_Imprimir.TabIndex = 4;
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.UseVisualStyleBackColor = true;
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(307, 201);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(112, 34);
            this.btn_Cancelar.TabIndex = 5;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // VendArtigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 247);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Imprimir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.f4_subfamilia);
            this.Name = "VendArtigo";
            this.Text = "VendArtigo";
            this.Load += new System.EventHandler(this.VendArtigo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f4_subfamilia;
        private System.Windows.Forms.DateTimePicker dtPicker_dataFinal;
        private System.Windows.Forms.DateTimePicker dtPicker_dataInicial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Imprimir;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}