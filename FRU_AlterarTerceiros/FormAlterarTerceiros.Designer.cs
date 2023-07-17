
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbox_Serie = new System.Windows.Forms.ComboBox();
            this.btnAlterarTerceiro = new System.Windows.Forms.Button();
            this.f4TipoTerceiro = new PRISDK100.F4();
            this.f4TipoDoc = new PRISDK100.F4();
            this.num_NumDocInicio = new System.Windows.Forms.NumericUpDown();
            this.num_NumDocFim = new System.Windows.Forms.NumericUpDown();
            this.date_DataDocInicio = new System.Windows.Forms.DateTimePicker();
            this.date_DataDocFim = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActualizarPriGrelha = new System.Windows.Forms.Button();
            this.priGrelhaDocs = new PRISDK100.PriGrelha();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelhaDocs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Doc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Série:";
            // 
            // cbox_Serie
            // 
            this.cbox_Serie.FormattingEnabled = true;
            this.cbox_Serie.Location = new System.Drawing.Point(337, 29);
            this.cbox_Serie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbox_Serie.Name = "cbox_Serie";
            this.cbox_Serie.Size = new System.Drawing.Size(54, 21);
            this.cbox_Serie.TabIndex = 7;
            // 
            // btnAlterarTerceiro
            // 
            this.btnAlterarTerceiro.Location = new System.Drawing.Point(254, 364);
            this.btnAlterarTerceiro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAlterarTerceiro.Name = "btnAlterarTerceiro";
            this.btnAlterarTerceiro.Size = new System.Drawing.Size(98, 28);
            this.btnAlterarTerceiro.TabIndex = 9;
            this.btnAlterarTerceiro.Text = "Alterar Terceiro";
            this.btnAlterarTerceiro.UseVisualStyleBackColor = true;
            this.btnAlterarTerceiro.Click += new System.EventHandler(this.btnAlterarTerceiro_Click);
            // 
            // f4TipoTerceiro
            // 
            this.f4TipoTerceiro.AgrupaOutrosTerceiros = false;
            this.f4TipoTerceiro.Audit = "mnuTabTipoTerceiros";
            this.f4TipoTerceiro.AutoComplete = false;
            this.f4TipoTerceiro.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4TipoTerceiro.CampoChave = "TipoTerceiro";
            this.f4TipoTerceiro.CampoChaveFisica = "";
            this.f4TipoTerceiro.CampoDescricao = "Descricao";
            this.f4TipoTerceiro.Caption = "Tipo Terceiro:";
            this.f4TipoTerceiro.CarregarValoresEdicao = false;
            this.f4TipoTerceiro.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.TiposTerceiro;
            this.f4TipoTerceiro.ChaveFisica = "";
            this.f4TipoTerceiro.ChaveNumerica = false;
            this.f4TipoTerceiro.F4Modal = false;
            this.f4TipoTerceiro.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4TipoTerceiro.IDCategoria = "TipoTerceiros";
            this.f4TipoTerceiro.Location = new System.Drawing.Point(296, 109);
            this.f4TipoTerceiro.MaxLengthDescricao = 0;
            this.f4TipoTerceiro.MaxLengthF4 = 50;
            this.f4TipoTerceiro.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4TipoTerceiro.Modulo = "BAS";
            this.f4TipoTerceiro.MostraDescricao = true;
            this.f4TipoTerceiro.MostraLink = true;
            this.f4TipoTerceiro.Name = "f4TipoTerceiro";
            this.f4TipoTerceiro.PainesInformacaoRelacionada = false;
            this.f4TipoTerceiro.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4TipoTerceiro.PermiteDrillDown = true;
            this.f4TipoTerceiro.PermiteEnabledLink = true;
            this.f4TipoTerceiro.PodeEditarDescricao = false;
            this.f4TipoTerceiro.ResourceID = 688;
            this.f4TipoTerceiro.ResourcePersonalizada = false;
            this.f4TipoTerceiro.Restricao = "";
            this.f4TipoTerceiro.SelectionFormula = "";
            this.f4TipoTerceiro.Size = new System.Drawing.Size(267, 21);
            this.f4TipoTerceiro.TabIndex = 12;
            this.f4TipoTerceiro.TextoDescricao = "";
            this.f4TipoTerceiro.WidthEspacamento = 60;
            this.f4TipoTerceiro.WidthF4 = 1100;
            this.f4TipoTerceiro.WidthLink = 1350;
            // 
            // f4TipoDoc
            // 
            this.f4TipoDoc.AgrupaOutrosTerceiros = false;
            this.f4TipoDoc.Audit = "mnuTabDocumentosVenda";
            this.f4TipoDoc.AutoComplete = false;
            this.f4TipoDoc.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4TipoDoc.CampoChave = "Documento";
            this.f4TipoDoc.CampoChaveFisica = "";
            this.f4TipoDoc.CampoDescricao = "Descricao";
            this.f4TipoDoc.Caption = "Documento:";
            this.f4TipoDoc.CarregarValoresEdicao = false;
            this.f4TipoDoc.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.DocumentosVenda;
            this.f4TipoDoc.ChaveFisica = "";
            this.f4TipoDoc.ChaveNumerica = false;
            this.f4TipoDoc.F4Modal = false;
            this.f4TipoDoc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4TipoDoc.IDCategoria = "DocumentosVenda";
            this.f4TipoDoc.Location = new System.Drawing.Point(15, 29);
            this.f4TipoDoc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.f4TipoDoc.MaxLengthDescricao = 0;
            this.f4TipoDoc.MaxLengthF4 = 50;
            this.f4TipoDoc.MinimumSize = new System.Drawing.Size(28, 17);
            this.f4TipoDoc.Modulo = "VND";
            this.f4TipoDoc.MostraDescricao = true;
            this.f4TipoDoc.MostraLink = true;
            this.f4TipoDoc.Name = "f4TipoDoc";
            this.f4TipoDoc.PainesInformacaoRelacionada = false;
            this.f4TipoDoc.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4TipoDoc.PermiteDrillDown = true;
            this.f4TipoDoc.PermiteEnabledLink = true;
            this.f4TipoDoc.PodeEditarDescricao = false;
            this.f4TipoDoc.ResourceID = 154;
            this.f4TipoDoc.ResourcePersonalizada = false;
            this.f4TipoDoc.Restricao = "";
            this.f4TipoDoc.SelectionFormula = "";
            this.f4TipoDoc.Size = new System.Drawing.Size(254, 22);
            this.f4TipoDoc.TabIndex = 13;
            this.f4TipoDoc.TextoDescricao = "";
            this.f4TipoDoc.WidthEspacamento = 60;
            this.f4TipoDoc.WidthF4 = 1100;
            this.f4TipoDoc.WidthLink = 1300;
            this.f4TipoDoc.TextChange += new PRISDK100.F4.TextChangeHandler(this.f4TipoDoc_TextChange);
            // 
            // num_NumDocInicio
            // 
            this.num_NumDocInicio.Location = new System.Drawing.Point(73, 110);
            this.num_NumDocInicio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.num_NumDocFim.Location = new System.Drawing.Point(151, 110);
            this.num_NumDocFim.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // date_DataDocInicio
            // 
            this.date_DataDocInicio.CustomFormat = "DD/MM/YYYY";
            this.date_DataDocInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_DataDocInicio.Location = new System.Drawing.Point(73, 74);
            this.date_DataDocInicio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.date_DataDocInicio.Name = "date_DataDocInicio";
            this.date_DataDocInicio.Size = new System.Drawing.Size(87, 20);
            this.date_DataDocInicio.TabIndex = 18;
            // 
            // date_DataDocFim
            // 
            this.date_DataDocFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.date_DataDocFim.Location = new System.Drawing.Point(180, 74);
            this.date_DataDocFim.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.date_DataDocFim.Name = "date_DataDocFim";
            this.date_DataDocFim.Size = new System.Drawing.Size(89, 20);
            this.date_DataDocFim.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Data Doc:";
            // 
            // btnActualizarPriGrelha
            // 
            this.btnActualizarPriGrelha.Location = new System.Drawing.Point(396, 364);
            this.btnActualizarPriGrelha.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnActualizarPriGrelha.Name = "btnActualizarPriGrelha";
            this.btnActualizarPriGrelha.Size = new System.Drawing.Size(98, 28);
            this.btnActualizarPriGrelha.TabIndex = 21;
            this.btnActualizarPriGrelha.Text = "Actualizar";
            this.btnActualizarPriGrelha.UseVisualStyleBackColor = true;
            this.btnActualizarPriGrelha.Click += new System.EventHandler(this.btnActualizarPriGrelha_Click);
            // 
            // priGrelhaDocs
            // 
            this.priGrelhaDocs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.priGrelhaDocs.BandaMenuContexto = "";
            this.priGrelhaDocs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.priGrelhaDocs.BotaoConfigurarActiveBar = true;
            this.priGrelhaDocs.BotaoProcurarActiveBar = false;
            this.priGrelhaDocs.CaminhoTemplateImpressao = "";
            this.priGrelhaDocs.Cols = null;
            this.priGrelhaDocs.ColsFrozen = -1;
            this.priGrelhaDocs.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.priGrelhaDocs.Location = new System.Drawing.Point(14, 180);
            this.priGrelhaDocs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.priGrelhaDocs.Name = "priGrelhaDocs";
            this.priGrelhaDocs.NumeroMaxRegistosSemPag = 150000;
            this.priGrelhaDocs.NumeroRegistos = 0;
            this.priGrelhaDocs.NumLinhasCabecalho = 1;
            this.priGrelhaDocs.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.priGrelhaDocs.ParentFormModal = false;
            this.priGrelhaDocs.PermiteActiveBar = false;
            this.priGrelhaDocs.PermiteActualizar = true;
            this.priGrelhaDocs.PermiteAgrupamentosUser = true;
            this.priGrelhaDocs.PermiteConfigurarDetalhes = false;
            this.priGrelhaDocs.PermiteContextoVazia = false;
            this.priGrelhaDocs.PermiteDataFill = false;
            this.priGrelhaDocs.PermiteDetalhes = true;
            this.priGrelhaDocs.PermiteEdicao = false;
            this.priGrelhaDocs.PermiteFiltros = true;
            this.priGrelhaDocs.PermiteGrafico = true;
            this.priGrelhaDocs.PermiteGrandeTotal = false;
            this.priGrelhaDocs.PermiteOrdenacao = true;
            this.priGrelhaDocs.PermitePaginacao = false;
            this.priGrelhaDocs.PermiteScrollBars = true;
            this.priGrelhaDocs.PermiteStatusBar = true;
            this.priGrelhaDocs.PermiteVistas = true;
            this.priGrelhaDocs.PosicionaColunaSeguinte = true;
            this.priGrelhaDocs.Size = new System.Drawing.Size(719, 179);
            this.priGrelhaDocs.TabIndex = 22;
            this.priGrelhaDocs.TituloGrelha = "";
            this.priGrelhaDocs.TituloMapa = "";
            this.priGrelhaDocs.TypeNameLinha = "";
            this.priGrelhaDocs.TypeNameLinhas = "";
            this.priGrelhaDocs.Load += new System.EventHandler(this.priGrelhaDocs_Load);
            // 
            // FormAlterarTerceiros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.priGrelhaDocs);
            this.Controls.Add(this.btnActualizarPriGrelha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.date_DataDocFim);
            this.Controls.Add(this.date_DataDocInicio);
            this.Controls.Add(this.num_NumDocFim);
            this.Controls.Add(this.num_NumDocInicio);
            this.Controls.Add(this.f4TipoDoc);
            this.Controls.Add(this.f4TipoTerceiro);
            this.Controls.Add(this.btnAlterarTerceiro);
            this.Controls.Add(this.cbox_Serie);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormAlterarTerceiros";
            this.Size = new System.Drawing.Size(751, 408);
            this.Text = "Alteração Terceiros";
            this.Load += new System.EventHandler(this.FormAlterarTerceiros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocInicio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumDocFim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelhaDocs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbox_Serie;
        private System.Windows.Forms.Button btnAlterarTerceiro;
        private PRISDK100.F4 f4TipoTerceiro;
        private PRISDK100.F4 f4TipoDoc;
        private System.Windows.Forms.NumericUpDown num_NumDocInicio;
        private System.Windows.Forms.NumericUpDown num_NumDocFim;
        private System.Windows.Forms.DateTimePicker date_DataDocInicio;
        private System.Windows.Forms.DateTimePicker date_DataDocFim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizarPriGrelha;
        private PRISDK100.PriGrelha priGrelhaDocs;
    }
}