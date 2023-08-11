
namespace FRU_AlterarTerceiros
{
    partial class FormAlterarTerceiros
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
            this.cbox_Serie = new System.Windows.Forms.ComboBox();
            this.btnAlterarTerceiro = new System.Windows.Forms.Button();
            this.f4_TipoTerceiro = new PRISDK100.F4();
            this.f4_TipoDoc = new PRISDK100.F4();
            this.num_NumDocInicio = new System.Windows.Forms.NumericUpDown();
            this.num_NumDocFim = new System.Windows.Forms.NumericUpDown();
            this.datepicker_DataDocInicio = new System.Windows.Forms.DateTimePicker();
            this.datepicker_DataDocFim = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActualizarPriGrelha = new System.Windows.Forms.Button();
            this.prigrelha_Docs = new PRISDK100.PriGrelha();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // cbox_Serie
            // 
            this.cbox_Serie.FormattingEnabled = true;
            this.cbox_Serie.Location = new System.Drawing.Point(405, 19);
            this.cbox_Serie.Margin = new System.Windows.Forms.Padding(2);
            this.cbox_Serie.Name = "cbox_Serie";
            this.cbox_Serie.Size = new System.Drawing.Size(72, 21);
            this.cbox_Serie.TabIndex = 7;
            // 
            // btnAlterarTerceiro
            // 
            this.btnAlterarTerceiro.Location = new System.Drawing.Point(476, 116);
            this.btnAlterarTerceiro.Margin = new System.Windows.Forms.Padding(2);
            this.btnAlterarTerceiro.Name = "btnAlterarTerceiro";
            this.btnAlterarTerceiro.Size = new System.Drawing.Size(109, 34);
            this.btnAlterarTerceiro.TabIndex = 9;
            this.btnAlterarTerceiro.Text = "Alterar Terceiro";
            this.btnAlterarTerceiro.UseVisualStyleBackColor = true;
            this.btnAlterarTerceiro.Click += new System.EventHandler(this.btnAlterarTerceiro_Click);
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
            this.f4_TipoTerceiro.Location = new System.Drawing.Point(314, 70);
            this.f4_TipoTerceiro.MaxLengthDescricao = 0;
            this.f4_TipoTerceiro.MaxLengthF4 = 50;
            this.f4_TipoTerceiro.MinimumSize = new System.Drawing.Size(37, 21);
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
            this.f4_TipoTerceiro.Size = new System.Drawing.Size(267, 21);
            this.f4_TipoTerceiro.TabIndex = 12;
            this.f4_TipoTerceiro.TextoDescricao = "";
            this.f4_TipoTerceiro.WidthEspacamento = 60;
            this.f4_TipoTerceiro.WidthF4 = 1100;
            this.f4_TipoTerceiro.WidthLink = 1350;
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
            this.f4_TipoDoc.Location = new System.Drawing.Point(14, 19);
            this.f4_TipoDoc.Margin = new System.Windows.Forms.Padding(2);
            this.f4_TipoDoc.MaxLengthDescricao = 0;
            this.f4_TipoDoc.MaxLengthF4 = 50;
            this.f4_TipoDoc.MinimumSize = new System.Drawing.Size(28, 17);
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
            this.f4_TipoDoc.Size = new System.Drawing.Size(387, 22);
            this.f4_TipoDoc.TabIndex = 13;
            this.f4_TipoDoc.TextoDescricao = "";
            this.f4_TipoDoc.WidthEspacamento = 60;
            this.f4_TipoDoc.WidthF4 = 1100;
            this.f4_TipoDoc.WidthLink = 1300;
            this.f4_TipoDoc.TextChange += new PRISDK100.F4.TextChangeHandler(this.f4TipoDoc_TextChange);
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
            // datepicker_DataDocInicio
            // 
            this.datepicker_DataDocInicio.CustomFormat = "DD/MM/YYYY";
            this.datepicker_DataDocInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocInicio.Location = new System.Drawing.Point(51, 22);
            this.datepicker_DataDocInicio.Margin = new System.Windows.Forms.Padding(2);
            this.datepicker_DataDocInicio.Name = "datepicker_DataDocInicio";
            this.datepicker_DataDocInicio.Size = new System.Drawing.Size(87, 20);
            this.datepicker_DataDocInicio.TabIndex = 18;
            // 
            // datepicker_DataDocFim
            // 
            this.datepicker_DataDocFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datepicker_DataDocFim.Location = new System.Drawing.Point(51, 55);
            this.datepicker_DataDocFim.Margin = new System.Windows.Forms.Padding(2);
            this.datepicker_DataDocFim.Name = "datepicker_DataDocFim";
            this.datepicker_DataDocFim.Size = new System.Drawing.Size(87, 20);
            this.datepicker_DataDocFim.TabIndex = 19;
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
            // btnActualizarPriGrelha
            // 
            this.btnActualizarPriGrelha.Location = new System.Drawing.Point(315, 116);
            this.btnActualizarPriGrelha.Margin = new System.Windows.Forms.Padding(2);
            this.btnActualizarPriGrelha.Name = "btnActualizarPriGrelha";
            this.btnActualizarPriGrelha.Size = new System.Drawing.Size(91, 34);
            this.btnActualizarPriGrelha.TabIndex = 21;
            this.btnActualizarPriGrelha.Text = "Actualizar";
            this.btnActualizarPriGrelha.UseVisualStyleBackColor = true;
            this.btnActualizarPriGrelha.Click += new System.EventHandler(this.btnActualizarPriGrelha_Click);
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
            this.prigrelha_Docs.Location = new System.Drawing.Point(14, 166);
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
            this.prigrelha_Docs.Size = new System.Drawing.Size(599, 360);
            this.prigrelha_Docs.TabIndex = 22;
            this.prigrelha_Docs.TituloGrelha = "";
            this.prigrelha_Docs.TituloMapa = "";
            this.prigrelha_Docs.TypeNameLinha = "";
            this.prigrelha_Docs.TypeNameLinhas = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.datepicker_DataDocInicio);
            this.groupBox1.Controls.Add(this.datepicker_DataDocFim);
            this.groupBox1.Location = new System.Drawing.Point(157, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 86);
            this.groupBox1.TabIndex = 24;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.num_NumDocInicio);
            this.groupBox2.Controls.Add(this.num_NumDocFim);
            this.groupBox2.Location = new System.Drawing.Point(14, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(126, 86);
            this.groupBox2.TabIndex = 25;
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
            // FormAlterarTerceiros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.prigrelha_Docs);
            this.Controls.Add(this.btnActualizarPriGrelha);
            this.Controls.Add(this.f4_TipoDoc);
            this.Controls.Add(this.f4_TipoTerceiro);
            this.Controls.Add(this.btnAlterarTerceiro);
            this.Controls.Add(this.cbox_Serie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormAlterarTerceiros";
            this.Size = new System.Drawing.Size(627, 544);
            this.Text = "Alteração Terceiros";
            this.Load += new System.EventHandler(this.FormAlterarTerceiros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prigrelha_Docs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_Serie;
        private System.Windows.Forms.Button btnAlterarTerceiro;
        private PRISDK100.F4 f4_TipoTerceiro;
        private PRISDK100.F4 f4_TipoDoc;
        private System.Windows.Forms.NumericUpDown num_NumDocInicio;
        private System.Windows.Forms.NumericUpDown num_NumDocFim;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocInicio;
        private System.Windows.Forms.DateTimePicker datepicker_DataDocFim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizarPriGrelha;
        private PRISDK100.PriGrelha prigrelha_Docs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
    }
}