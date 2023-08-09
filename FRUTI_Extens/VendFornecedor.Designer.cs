
namespace FRUTI_Extens
{
    partial class VendFornecedor
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
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtPicker_dataFinal = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_dataInicial = new System.Windows.Forms.DateTimePicker();
            this.f4_Fornecedor = new PRISDK100.F4();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Location = new System.Drawing.Point(318, 230);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(112, 34);
            this.btn_Cancelar.TabIndex = 9;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Imprimir.Location = new System.Drawing.Point(188, 230);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(112, 34);
            this.btn_Imprimir.TabIndex = 8;
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.UseVisualStyleBackColor = true;
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtPicker_dataFinal);
            this.groupBox1.Controls.Add(this.dtPicker_dataInicial);
            this.groupBox1.Location = new System.Drawing.Point(109, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 127);
            this.groupBox1.TabIndex = 7;
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
            // f4_Fornecedor
            // 
            this.f4_Fornecedor.AgrupaOutrosTerceiros = false;
            this.f4_Fornecedor.Audit = "mnuTabFornecedores";
            this.f4_Fornecedor.AutoComplete = false;
            this.f4_Fornecedor.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_Fornecedor.CampoChave = "Fornecedor";
            this.f4_Fornecedor.CampoChaveFisica = "";
            this.f4_Fornecedor.CampoDescricao = "Nome";
            this.f4_Fornecedor.Caption = "Fornecedor:";
            this.f4_Fornecedor.CarregarValoresEdicao = false;
            this.f4_Fornecedor.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Fornecedores;
            this.f4_Fornecedor.ChaveFisica = "";
            this.f4_Fornecedor.ChaveNumerica = false;
            this.f4_Fornecedor.F4Modal = false;
            this.f4_Fornecedor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_Fornecedor.IDCategoria = "Fornecedores";
            this.f4_Fornecedor.Location = new System.Drawing.Point(35, 170);
            this.f4_Fornecedor.MaxLengthDescricao = 0;
            this.f4_Fornecedor.MaxLengthF4 = 50;
            this.f4_Fornecedor.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4_Fornecedor.Modulo = "BAS";
            this.f4_Fornecedor.MostraDescricao = true;
            this.f4_Fornecedor.MostraLink = true;
            this.f4_Fornecedor.Name = "f4_Fornecedor";
            this.f4_Fornecedor.PainesInformacaoRelacionada = false;
            this.f4_Fornecedor.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_Fornecedor.PermiteDrillDown = true;
            this.f4_Fornecedor.PermiteEnabledLink = true;
            this.f4_Fornecedor.PodeEditarDescricao = false;
            this.f4_Fornecedor.ResourceID = 673;
            this.f4_Fornecedor.ResourcePersonalizada = false;
            this.f4_Fornecedor.Restricao = "";
            this.f4_Fornecedor.SelectionFormula = "";
            this.f4_Fornecedor.Size = new System.Drawing.Size(395, 22);
            this.f4_Fornecedor.TabIndex = 10;
            this.f4_Fornecedor.TextoDescricao = "";
            this.f4_Fornecedor.WidthEspacamento = 60;
            this.f4_Fornecedor.WidthF4 = 1590;
            this.f4_Fornecedor.WidthLink = 1575;
            this.f4_Fornecedor.Load += new System.EventHandler(this.f4_Subfamilias_Load);
            // 
            // VendFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 276);
            this.Controls.Add(this.f4_Fornecedor);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_Imprimir);
            this.Controls.Add(this.groupBox1);
            this.Name = "VendFornecedor";
            this.Text = "VendFornecedor";
            this.Load += new System.EventHandler(this.VendFornecedor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.Button btn_Imprimir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtPicker_dataFinal;
        private System.Windows.Forms.DateTimePicker dtPicker_dataInicial;
        private PRISDK100.F4 f4_Fornecedor;
    }
}