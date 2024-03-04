
namespace PP_PPCS
{
    partial class FormPropDocVenda
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
            this.cBoxSerie = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.f4TipoDoc = new PRISDK100.F4();
            this.priGrelhaDocs = new PRISDK100.PriGrelha();
            this.numUpDownNumDoc = new System.Windows.Forms.NumericUpDown();
            this.btnGravar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelhaDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNumDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // cBoxSerie
            // 
            this.cBoxSerie.FormattingEnabled = true;
            this.cBoxSerie.Location = new System.Drawing.Point(540, 20);
            this.cBoxSerie.Margin = new System.Windows.Forms.Padding(4);
            this.cBoxSerie.Name = "cBoxSerie";
            this.cBoxSerie.Size = new System.Drawing.Size(88, 24);
            this.cBoxSerie.TabIndex = 4;
            this.cBoxSerie.SelectedIndexChanged += new System.EventHandler(this.cBoxSerie_SelectedIndexChanged);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(743, 21);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(132, 46);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(1421, 545);
            this.btnSair.Margin = new System.Windows.Forms.Padding(4);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(132, 41);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
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
            this.f4TipoDoc.Location = new System.Drawing.Point(17, 20);
            this.f4TipoDoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.f4TipoDoc.MaxLengthDescricao = 0;
            this.f4TipoDoc.MaxLengthF4 = 50;
            this.f4TipoDoc.MinimumSize = new System.Drawing.Size(37, 21);
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
            this.f4TipoDoc.Size = new System.Drawing.Size(516, 27);
            this.f4TipoDoc.TabIndex = 14;
            this.f4TipoDoc.TextoDescricao = "";
            this.f4TipoDoc.WidthEspacamento = 60;
            this.f4TipoDoc.WidthF4 = 1250;
            this.f4TipoDoc.WidthLink = 1300;
            this.f4TipoDoc.TextChange += new PRISDK100.F4.TextChangeHandler(this.f4TipoDoc_TextChange);
            // 
            // priGrelhaDocs
            // 
            this.priGrelhaDocs.BackColor = System.Drawing.Color.White;
            this.priGrelhaDocs.BandaMenuContexto = "";
            this.priGrelhaDocs.BotaoConfigurarActiveBar = true;
            this.priGrelhaDocs.BotaoProcurarActiveBar = false;
            this.priGrelhaDocs.CaminhoTemplateImpressao = "";
            this.priGrelhaDocs.Cols = null;
            this.priGrelhaDocs.ColsFrozen = -1;
            this.priGrelhaDocs.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.priGrelhaDocs.Location = new System.Drawing.Point(17, 93);
            this.priGrelhaDocs.Margin = new System.Windows.Forms.Padding(4);
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
            this.priGrelhaDocs.PermiteDataFill = true;
            this.priGrelhaDocs.PermiteDetalhes = true;
            this.priGrelhaDocs.PermiteEdicao = true;
            this.priGrelhaDocs.PermiteFiltros = true;
            this.priGrelhaDocs.PermiteGrafico = true;
            this.priGrelhaDocs.PermiteGrandeTotal = false;
            this.priGrelhaDocs.PermiteOrdenacao = true;
            this.priGrelhaDocs.PermitePaginacao = false;
            this.priGrelhaDocs.PermiteScrollBars = true;
            this.priGrelhaDocs.PermiteStatusBar = true;
            this.priGrelhaDocs.PermiteVistas = true;
            this.priGrelhaDocs.PosicionaColunaSeguinte = true;
            this.priGrelhaDocs.Size = new System.Drawing.Size(1536, 444);
            this.priGrelhaDocs.TabIndex = 8;
            this.priGrelhaDocs.TituloGrelha = "";
            this.priGrelhaDocs.TituloMapa = "";
            this.priGrelhaDocs.TypeNameLinha = "";
            this.priGrelhaDocs.TypeNameLinhas = "";
            this.priGrelhaDocs.LeaveRow += new PRISDK100.PriGrelha.LeaveRowHandler(this.priGrelhaDocs_LeaveRow);
            // 
            // numUpDownNumDoc
            // 
            this.numUpDownNumDoc.Location = new System.Drawing.Point(635, 21);
            this.numUpDownNumDoc.Name = "numUpDownNumDoc";
            this.numUpDownNumDoc.Size = new System.Drawing.Size(84, 20);
            this.numUpDownNumDoc.TabIndex = 15;
            // 
            // btnGravar
            // 
            this.btnGravar.Location = new System.Drawing.Point(882, 22);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(128, 45);
            this.btnGravar.TabIndex = 16;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            // 
            // FormPropDocVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.numUpDownNumDoc);
            this.Controls.Add(this.f4TipoDoc);
            this.Controls.Add(this.priGrelhaDocs);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.cBoxSerie);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormPropDocVenda";
            this.Size = new System.Drawing.Size(1576, 601);
            this.Text = "FormPropDocVenda";
            this.Load += new System.EventHandler(this.FormPropDocVenda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.priGrelhaDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNumDoc)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cBoxSerie;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSair;
        private PRISDK100.F4 f4TipoDoc;
        private PRISDK100.PriGrelha priGrelhaDocs;
        private System.Windows.Forms.NumericUpDown numUpDownNumDoc;
        private System.Windows.Forms.Button btnGravar;
    }
}