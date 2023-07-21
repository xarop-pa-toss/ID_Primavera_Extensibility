
namespace FRU_AlterarTerceiros
{
    partial class FormAlterarTerceiros_WF
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
            this.f4_TipoDoc = new PRISDK100.F4();
            this.cbox_Serie = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.datepicker_DataDocInicio = new System.Windows.Forms.DateTimePicker();
            this.datepicker_DataDocFim = new System.Windows.Forms.DateTimePicker();
            this.prigrelha_Docs = new PRISDK100.PriGrelha();
            this.btn_ActualizarPriGrelha = new System.Windows.Forms.Button();
            this.f4_TipoTerceiro = new PRISDK100.F4();
            this.btn_AlterarTerceiro = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.num_NumDocInicio = new System.Windows.Forms.NumericUpDown();
            this.num_NumDocFim = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).BeginInit();
            this.SuspendLayout();
            // 
            // f4_TipoDoc
            // 
            this.f4_TipoDoc.AgrupaOutrosTerceiros = false;
            this.f4_TipoDoc.Audit = "mnuTabDocumentosVenda";
            this.f4_TipoDoc.AutoComplete = false;
            this.f4_TipoDoc.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_TipoDoc.CampoChave = "Documento";
            this.f4_TipoDoc.CampoChaveFisica = "";
            this.f4_TipoDoc.CampoDescricao = "Descricao";
            this.f4_TipoDoc.Caption = "Documento:";
            this.f4_TipoDoc.CarregarValoresEdicao = false;
            this.f4_TipoDoc.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.DocumentosVenda;
            this.f4_TipoDoc.ChaveFisica = "";
            this.f4_TipoDoc.ChaveNumerica = false;
            this.f4_TipoDoc.F4Modal = false;
            this.f4_TipoDoc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_TipoDoc.IDCategoria = "DocumentosVenda";
            this.f4_TipoDoc.Location = new System.Drawing.Point(28, 27);
            this.f4_TipoDoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.f4_TipoDoc.MaxLengthDescricao = 0;
            this.f4_TipoDoc.MaxLengthF4 = 50;
            this.f4_TipoDoc.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4_TipoDoc.Modulo = "VND";
            this.f4_TipoDoc.MostraDescricao = true;
            this.f4_TipoDoc.MostraLink = true;
            this.f4_TipoDoc.Name = "f4_TipoDoc";
            this.f4_TipoDoc.PainesInformacaoRelacionada = false;
            this.f4_TipoDoc.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_TipoDoc.PermiteDrillDown = true;
            this.f4_TipoDoc.PermiteEnabledLink = true;
            this.f4_TipoDoc.PodeEditarDescricao = false;
            this.f4_TipoDoc.ResourceID = 154;
            this.f4_TipoDoc.ResourcePersonalizada = false;
            this.f4_TipoDoc.Restricao = "";
            this.f4_TipoDoc.SelectionFormula = "";
            this.f4_TipoDoc.Size = new System.Drawing.Size(516, 27);
            this.f4_TipoDoc.TabIndex = 31;
            this.f4_TipoDoc.TextoDescricao = "";
            this.f4_TipoDoc.WidthEspacamento = 60;
            this.f4_TipoDoc.WidthF4 = 1100;
            this.f4_TipoDoc.WidthLink = 1300;
            // 
            // cbox_Serie
            // 
            this.cbox_Serie.FormattingEnabled = true;
            this.cbox_Serie.Location = new System.Drawing.Point(550, 27);
            this.cbox_Serie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbox_Serie.Name = "cbox_Serie";
            this.cbox_Serie.Size = new System.Drawing.Size(95, 24);
            this.cbox_Serie.TabIndex = 28;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Final:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Inicial:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datepicker_DataDocInicio);
            this.groupBox1.Controls.Add(this.datepicker_DataDocFim);
            this.groupBox1.Location = new System.Drawing.Point(218, 86);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(201, 106);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data Documento";
            // 
            // datepicker_DataDocInicio
            // 
            this.datepicker_DataDocInicio.CustomFormat = "DD/MM/YYYY";
            this.datepicker_DataDocInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocInicio.Location = new System.Drawing.Point(68, 27);
            this.datepicker_DataDocInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datepicker_DataDocInicio.Name = "datepicker_DataDocInicio";
            this.datepicker_DataDocInicio.Size = new System.Drawing.Size(115, 22);
            this.datepicker_DataDocInicio.TabIndex = 18;
            // 
            // datepicker_DataDocFim
            // 
            this.datepicker_DataDocFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocFim.Location = new System.Drawing.Point(68, 68);
            this.datepicker_DataDocFim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datepicker_DataDocFim.Name = "datepicker_DataDocFim";
            this.datepicker_DataDocFim.Size = new System.Drawing.Size(115, 22);
            this.datepicker_DataDocFim.TabIndex = 19;
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
            this.prigrelha_Docs.Location = new System.Drawing.Point(28, 211);
            this.prigrelha_Docs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.prigrelha_Docs.Size = new System.Drawing.Size(761, 443);
            this.prigrelha_Docs.TabIndex = 35;
            this.prigrelha_Docs.TituloGrelha = "";
            this.prigrelha_Docs.TituloMapa = "";
            this.prigrelha_Docs.TypeNameLinha = "";
            this.prigrelha_Docs.TypeNameLinhas = "";
            // 
            // btn_ActualizarPriGrelha
            // 
            this.btn_ActualizarPriGrelha.Location = new System.Drawing.Point(429, 150);
            this.btn_ActualizarPriGrelha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ActualizarPriGrelha.Name = "btn_ActualizarPriGrelha";
            this.btn_ActualizarPriGrelha.Size = new System.Drawing.Size(121, 42);
            this.btn_ActualizarPriGrelha.TabIndex = 34;
            this.btn_ActualizarPriGrelha.Text = "Actualizar";
            this.btn_ActualizarPriGrelha.UseVisualStyleBackColor = true;
            // 
            // f4_TipoTerceiro
            // 
            this.f4_TipoTerceiro.AgrupaOutrosTerceiros = false;
            this.f4_TipoTerceiro.Audit = "mnuTabTipoTerceiros";
            this.f4_TipoTerceiro.AutoComplete = false;
            this.f4_TipoTerceiro.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_TipoTerceiro.CampoChave = "TipoTerceiro";
            this.f4_TipoTerceiro.CampoChaveFisica = "";
            this.f4_TipoTerceiro.CampoDescricao = "Descricao";
            this.f4_TipoTerceiro.Caption = "Tipo Terceiro:";
            this.f4_TipoTerceiro.CarregarValoresEdicao = false;
            this.f4_TipoTerceiro.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.TiposTerceiro;
            this.f4_TipoTerceiro.ChaveFisica = "";
            this.f4_TipoTerceiro.ChaveNumerica = false;
            this.f4_TipoTerceiro.F4Modal = false;
            this.f4_TipoTerceiro.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_TipoTerceiro.IDCategoria = "TipoTerceiros";
            this.f4_TipoTerceiro.Location = new System.Drawing.Point(428, 93);
            this.f4_TipoTerceiro.Margin = new System.Windows.Forms.Padding(4);
            this.f4_TipoTerceiro.MaxLengthDescricao = 0;
            this.f4_TipoTerceiro.MaxLengthF4 = 50;
            this.f4_TipoTerceiro.MinimumSize = new System.Drawing.Size(49, 26);
            this.f4_TipoTerceiro.Modulo = "BAS";
            this.f4_TipoTerceiro.MostraDescricao = true;
            this.f4_TipoTerceiro.MostraLink = true;
            this.f4_TipoTerceiro.Name = "f4_TipoTerceiro";
            this.f4_TipoTerceiro.PainesInformacaoRelacionada = false;
            this.f4_TipoTerceiro.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_TipoTerceiro.PermiteDrillDown = true;
            this.f4_TipoTerceiro.PermiteEnabledLink = true;
            this.f4_TipoTerceiro.PodeEditarDescricao = false;
            this.f4_TipoTerceiro.ResourceID = 688;
            this.f4_TipoTerceiro.ResourcePersonalizada = false;
            this.f4_TipoTerceiro.Restricao = "";
            this.f4_TipoTerceiro.SelectionFormula = "";
            this.f4_TipoTerceiro.Size = new System.Drawing.Size(356, 26);
            this.f4_TipoTerceiro.TabIndex = 30;
            this.f4_TipoTerceiro.TextoDescricao = "";
            this.f4_TipoTerceiro.WidthEspacamento = 60;
            this.f4_TipoTerceiro.WidthF4 = 1100;
            this.f4_TipoTerceiro.WidthLink = 1350;
            // 
            // btn_AlterarTerceiro
            // 
            this.btn_AlterarTerceiro.Location = new System.Drawing.Point(644, 150);
            this.btn_AlterarTerceiro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_AlterarTerceiro.Name = "btn_AlterarTerceiro";
            this.btn_AlterarTerceiro.Size = new System.Drawing.Size(145, 42);
            this.btn_AlterarTerceiro.TabIndex = 29;
            this.btn_AlterarTerceiro.Text = "Alterar Terceiro";
            this.btn_AlterarTerceiro.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.num_NumDocInicio);
            this.groupBox2.Controls.Add(this.num_NumDocFim);
            this.groupBox2.Location = new System.Drawing.Point(28, 86);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(168, 106);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Num Documentos";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inicial:";
            // 
            // num_NumDocInicio
            // 
            this.num_NumDocInicio.Location = new System.Drawing.Point(73, 28);
            this.num_NumDocInicio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.num_NumDocInicio.Size = new System.Drawing.Size(79, 22);
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
            this.num_NumDocFim.Location = new System.Drawing.Point(73, 68);
            this.num_NumDocFim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.num_NumDocFim.Size = new System.Drawing.Size(79, 22);
            this.num_NumDocFim.TabIndex = 17;
            this.num_NumDocFim.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            // 
            // FormAlterarTerceiros_WF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 673);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.f4_TipoDoc);
            this.Controls.Add(this.cbox_Serie);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prigrelha_Docs);
            this.Controls.Add(this.btn_ActualizarPriGrelha);
            this.Controls.Add(this.f4_TipoTerceiro);
            this.Controls.Add(this.btn_AlterarTerceiro);
            this.Name = "FormAlterarTerceiros_WF";
            this.Text = "Alterar Terceiros";
            this.Load += new System.EventHandler(this.FormAlterarTerceiros_WF_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f4_TipoDoc;
        private System.Windows.Forms.ComboBox cbox_Serie;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocInicio;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocFim;
        private PRISDK100.PriGrelha prigrelha_Docs;
        private System.Windows.Forms.Button btn_ActualizarPriGrelha;
        private PRISDK100.F4 f4_TipoTerceiro;
        private System.Windows.Forms.Button btn_AlterarTerceiro;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown num_NumDocInicio;
        private System.Windows.Forms.NumericUpDown num_NumDocFim;
    }
}