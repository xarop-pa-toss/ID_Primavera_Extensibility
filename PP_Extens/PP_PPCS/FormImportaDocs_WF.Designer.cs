
namespace PP_PPCS
{
    partial class FormImportaDocs_WF
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Actualizar_WF = new System.Windows.Forms.Button();
            this.datepicker_DataDocNovo_WF = new System.Windows.Forms.DateTimePicker();
            this.datepicker_DataDocImportar_WF = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.prigrelha_Docs_WF = new PRISDK100.PriGrelha();
            this.btn_Sair_WF = new System.Windows.Forms.Button();
            this.btn_Processar_WF = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs_WF)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(203, 577);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(449, 49);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Legenda para coluna \"Importar\"";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(374, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "A - Documento anulado   S - Importar documento   N - Não importar";
            // 
            // btn_Actualizar_WF
            // 
            this.btn_Actualizar_WF.Location = new System.Drawing.Point(319, 518);
            this.btn_Actualizar_WF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Actualizar_WF.Name = "btn_Actualizar_WF";
            this.btn_Actualizar_WF.Size = new System.Drawing.Size(100, 41);
            this.btn_Actualizar_WF.TabIndex = 37;
            this.btn_Actualizar_WF.Text = "Actualizar";
            this.btn_Actualizar_WF.UseVisualStyleBackColor = true;
            this.btn_Actualizar_WF.Click += new System.EventHandler(this.btn_Actualizar_WF_Click);
            // 
            // datepicker_DataDocNovo_WF
            // 
            this.datepicker_DataDocNovo_WF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocNovo_WF.Location = new System.Drawing.Point(641, 527);
            this.datepicker_DataDocNovo_WF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.datepicker_DataDocNovo_WF.Name = "datepicker_DataDocNovo_WF";
            this.datepicker_DataDocNovo_WF.Size = new System.Drawing.Size(125, 20);
            this.datepicker_DataDocNovo_WF.TabIndex = 36;
            // 
            // datepicker_DataDocImportar_WF
            // 
            this.datepicker_DataDocImportar_WF.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocImportar_WF.Location = new System.Drawing.Point(183, 527);
            this.datepicker_DataDocImportar_WF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.datepicker_DataDocImportar_WF.Name = "datepicker_DataDocImportar_WF";
            this.datepicker_DataDocImportar_WF.Size = new System.Drawing.Size(127, 20);
            this.datepicker_DataDocImportar_WF.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(476, 530);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Data para documentos:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 530);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 15);
            this.label1.TabIndex = 33;
            this.label1.Text = "Data docs a importar:";
            // 
            // prigrelha_Docs_WF
            // 
            this.prigrelha_Docs_WF.BackColor = System.Drawing.Color.WhiteSmoke;
            this.prigrelha_Docs_WF.BandaMenuContexto = "";
            this.prigrelha_Docs_WF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prigrelha_Docs_WF.BotaoConfigurarActiveBar = true;
            this.prigrelha_Docs_WF.BotaoProcurarActiveBar = false;
            this.prigrelha_Docs_WF.CaminhoTemplateImpressao = "";
            this.prigrelha_Docs_WF.Cols = null;
            this.prigrelha_Docs_WF.ColsFrozen = -1;
            this.prigrelha_Docs_WF.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prigrelha_Docs_WF.Location = new System.Drawing.Point(15, 14);
            this.prigrelha_Docs_WF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.prigrelha_Docs_WF.Name = "prigrelha_Docs_WF";
            this.prigrelha_Docs_WF.NumeroMaxRegistosSemPag = 150000;
            this.prigrelha_Docs_WF.NumeroRegistos = 0;
            this.prigrelha_Docs_WF.NumLinhasCabecalho = 1;
            this.prigrelha_Docs_WF.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.prigrelha_Docs_WF.ParentFormModal = false;
            this.prigrelha_Docs_WF.PermiteActiveBar = false;
            this.prigrelha_Docs_WF.PermiteActualizar = true;
            this.prigrelha_Docs_WF.PermiteAgrupamentosUser = true;
            this.prigrelha_Docs_WF.PermiteConfigurarDetalhes = false;
            this.prigrelha_Docs_WF.PermiteContextoVazia = false;
            this.prigrelha_Docs_WF.PermiteDataFill = true;
            this.prigrelha_Docs_WF.PermiteDetalhes = true;
            this.prigrelha_Docs_WF.PermiteEdicao = true;
            this.prigrelha_Docs_WF.PermiteFiltros = true;
            this.prigrelha_Docs_WF.PermiteGrafico = true;
            this.prigrelha_Docs_WF.PermiteGrandeTotal = false;
            this.prigrelha_Docs_WF.PermiteOrdenacao = true;
            this.prigrelha_Docs_WF.PermitePaginacao = false;
            this.prigrelha_Docs_WF.PermiteScrollBars = true;
            this.prigrelha_Docs_WF.PermiteStatusBar = true;
            this.prigrelha_Docs_WF.PermiteVistas = true;
            this.prigrelha_Docs_WF.PosicionaColunaSeguinte = true;
            this.prigrelha_Docs_WF.Size = new System.Drawing.Size(961, 488);
            this.prigrelha_Docs_WF.TabIndex = 32;
            this.prigrelha_Docs_WF.TituloGrelha = "";
            this.prigrelha_Docs_WF.TituloMapa = "";
            this.prigrelha_Docs_WF.TypeNameLinha = "";
            this.prigrelha_Docs_WF.TypeNameLinhas = "";
            // 
            // btn_Sair_WF
            // 
            this.btn_Sair_WF.Location = new System.Drawing.Point(856, 574);
            this.btn_Sair_WF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Sair_WF.Name = "btn_Sair_WF";
            this.btn_Sair_WF.Size = new System.Drawing.Size(120, 39);
            this.btn_Sair_WF.TabIndex = 40;
            this.btn_Sair_WF.Text = "Sair";
            this.btn_Sair_WF.UseVisualStyleBackColor = true;
            this.btn_Sair_WF.Click += new System.EventHandler(this.btn_Sair_WF_Click);
            // 
            // btn_Processar_WF
            // 
            this.btn_Processar_WF.Location = new System.Drawing.Point(856, 527);
            this.btn_Processar_WF.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_Processar_WF.Name = "btn_Processar_WF";
            this.btn_Processar_WF.Size = new System.Drawing.Size(120, 39);
            this.btn_Processar_WF.TabIndex = 39;
            this.btn_Processar_WF.Text = "Processar";
            this.btn_Processar_WF.UseVisualStyleBackColor = true;
            this.btn_Processar_WF.Click += new System.EventHandler(this.btn_Processar_WF_Click);
            // 
            // FormImportaDocs_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 638);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Actualizar_WF);
            this.Controls.Add(this.datepicker_DataDocNovo_WF);
            this.Controls.Add(this.datepicker_DataDocImportar_WF);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prigrelha_Docs_WF);
            this.Controls.Add(this.btn_Sair_WF);
            this.Controls.Add(this.btn_Processar_WF);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormImportaDocs_WF";
            this.Text = "FormImportaDocs_WF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormImportaDocs_WF_FormClosing);
            this.Load += new System.EventHandler(this.FormImportaDocs_WF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs_WF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Actualizar_WF;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocNovo_WF;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocImportar_WF;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PRISDK100.PriGrelha prigrelha_Docs_WF;
        private System.Windows.Forms.Button btn_Sair_WF;
        private System.Windows.Forms.Button btn_Processar_WF;
    }
}