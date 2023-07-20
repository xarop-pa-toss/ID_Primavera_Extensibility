
namespace PP_PPCS
{
    partial class FormImportaDocs
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
            this.prigrelha_Docs = new PRISDK100.PriGrelha();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.datepicker_DataDocImport = new System.Windows.Forms.DateTimePicker();
            this.datepicker_DataDocNew = new System.Windows.Forms.DateTimePicker();
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Processar = new System.Windows.Forms.Button();
            this.btn_Sair = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // prigrelha_Docs
            // 
            this.prigrelha_Docs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.prigrelha_Docs.BandaMenuContexto = "";
            this.prigrelha_Docs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prigrelha_Docs.BotaoConfigurarActiveBar = true;
            this.prigrelha_Docs.BotaoProcurarActiveBar = false;
            this.prigrelha_Docs.CaminhoTemplateImpressao = "";
            this.prigrelha_Docs.Cols = null;
            this.prigrelha_Docs.ColsFrozen = -1;
            this.prigrelha_Docs.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.prigrelha_Docs.Location = new System.Drawing.Point(18, 20);
            this.prigrelha_Docs.Margin = new System.Windows.Forms.Padding(2);
            this.prigrelha_Docs.Name = "prigrelha_Docs";
            this.prigrelha_Docs.NumeroMaxRegistosSemPag = 150000;
            this.prigrelha_Docs.NumeroRegistos = 0;
            this.prigrelha_Docs.NumLinhasCabecalho = 1;
            this.prigrelha_Docs.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.prigrelha_Docs.ParentFormModal = false;
            this.prigrelha_Docs.PermiteActiveBar = false;
            this.prigrelha_Docs.PermiteActualizar = true;
            this.prigrelha_Docs.PermiteAgrupamentosUser = true;
            this.prigrelha_Docs.PermiteConfigurarDetalhes = false;
            this.prigrelha_Docs.PermiteContextoVazia = false;
            this.prigrelha_Docs.PermiteDataFill = false;
            this.prigrelha_Docs.PermiteDetalhes = true;
            this.prigrelha_Docs.PermiteEdicao = false;
            this.prigrelha_Docs.PermiteFiltros = true;
            this.prigrelha_Docs.PermiteGrafico = true;
            this.prigrelha_Docs.PermiteGrandeTotal = false;
            this.prigrelha_Docs.PermiteOrdenacao = true;
            this.prigrelha_Docs.PermitePaginacao = false;
            this.prigrelha_Docs.PermiteScrollBars = true;
            this.prigrelha_Docs.PermiteStatusBar = true;
            this.prigrelha_Docs.PermiteVistas = true;
            this.prigrelha_Docs.PosicionaColunaSeguinte = true;
            this.prigrelha_Docs.Size = new System.Drawing.Size(721, 388);
            this.prigrelha_Docs.TabIndex = 23;
            this.prigrelha_Docs.TituloGrelha = "";
            this.prigrelha_Docs.TituloMapa = "";
            this.prigrelha_Docs.TypeNameLinha = "";
            this.prigrelha_Docs.TypeNameLinhas = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 431);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Data docs a importar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 431);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Data para documentos:";
            // 
            // datepicker_DataDocImport
            // 
            this.datepicker_DataDocImport.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocImport.Location = new System.Drawing.Point(144, 428);
            this.datepicker_DataDocImport.Name = "datepicker_DataDocImport";
            this.datepicker_DataDocImport.Size = new System.Drawing.Size(96, 20);
            this.datepicker_DataDocImport.TabIndex = 26;
            // 
            // datepicker_DataDocNew
            // 
            this.datepicker_DataDocNew.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocNew.Location = new System.Drawing.Point(488, 428);
            this.datepicker_DataDocNew.Name = "datepicker_DataDocNew";
            this.datepicker_DataDocNew.Size = new System.Drawing.Size(95, 20);
            this.datepicker_DataDocNew.TabIndex = 27;
            // 
            // btn_Actualizar
            // 
            this.btn_Actualizar.Location = new System.Drawing.Point(246, 421);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(75, 33);
            this.btn_Actualizar.TabIndex = 28;
            this.btn_Actualizar.Text = "Actualizar";
            this.btn_Actualizar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(103, 460);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 40);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legenda para coluna \"Importar\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(324, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "A - Documento anulado   S - Importar documento   N - Não importar";
            // 
            // btn_Processar
            // 
            this.btn_Processar.Location = new System.Drawing.Point(655, 430);
            this.btn_Processar.Name = "btn_Processar";
            this.btn_Processar.Size = new System.Drawing.Size(90, 32);
            this.btn_Processar.TabIndex = 30;
            this.btn_Processar.Text = "Processar";
            this.btn_Processar.UseVisualStyleBackColor = true;
            // 
            // btn_Sair
            // 
            this.btn_Sair.Location = new System.Drawing.Point(655, 467);
            this.btn_Sair.Name = "btn_Sair";
            this.btn_Sair.Size = new System.Drawing.Size(90, 32);
            this.btn_Sair.TabIndex = 31;
            this.btn_Sair.Text = "Sair";
            this.btn_Sair.UseVisualStyleBackColor = true;
            this.btn_Sair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // FormImportaDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Sair);
            this.Controls.Add(this.btn_Processar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.datepicker_DataDocNew);
            this.Controls.Add(this.datepicker_DataDocImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prigrelha_Docs);
            this.Name = "FormImportaDocs";
            this.Size = new System.Drawing.Size(758, 522);
            this.Text = "FormImportaDocs";
            this.Load += new System.EventHandler(this.FormImportaDocs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PRISDK100.PriGrelha prigrelha_Docs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocImport;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocNew;
        private System.Windows.Forms.Button btn_Actualizar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Processar;
        private System.Windows.Forms.Button btn_Sair;
    }
}