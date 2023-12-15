namespace DCT_Extens
{
    partial class FormCargaDescarga
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
            this.priGrelha_Moradas = new PRISDK100.PriGrelha();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.f4_Entidade = new PRISDK100.F4();
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha_Moradas)).BeginInit();
            this.SuspendLayout();
            // 
            // priGrelha_Moradas
            // 
            this.priGrelha_Moradas.BackColor = System.Drawing.Color.White;
            this.priGrelha_Moradas.BandaMenuContexto = "";
            this.priGrelha_Moradas.BotaoConfigurarActiveBar = true;
            this.priGrelha_Moradas.BotaoProcurarActiveBar = false;
            this.priGrelha_Moradas.CaminhoTemplateImpressao = "";
            this.priGrelha_Moradas.Cols = null;
            this.priGrelha_Moradas.ColsFrozen = -1;
            this.priGrelha_Moradas.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.priGrelha_Moradas.Location = new System.Drawing.Point(20, 196);
            this.priGrelha_Moradas.Margin = new System.Windows.Forms.Padding(4);
            this.priGrelha_Moradas.Name = "priGrelha_Moradas";
            this.priGrelha_Moradas.NumeroMaxRegistosSemPag = 150000;
            this.priGrelha_Moradas.NumeroRegistos = 0;
            this.priGrelha_Moradas.NumLinhasCabecalho = 1;
            this.priGrelha_Moradas.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.priGrelha_Moradas.ParentFormModal = false;
            this.priGrelha_Moradas.PermiteActiveBar = false;
            this.priGrelha_Moradas.PermiteActualizar = true;
            this.priGrelha_Moradas.PermiteAgrupamentosUser = true;
            this.priGrelha_Moradas.PermiteConfigurarDetalhes = false;
            this.priGrelha_Moradas.PermiteContextoVazia = false;
            this.priGrelha_Moradas.PermiteDataFill = false;
            this.priGrelha_Moradas.PermiteDetalhes = true;
            this.priGrelha_Moradas.PermiteEdicao = false;
            this.priGrelha_Moradas.PermiteFiltros = true;
            this.priGrelha_Moradas.PermiteGrafico = true;
            this.priGrelha_Moradas.PermiteGrandeTotal = false;
            this.priGrelha_Moradas.PermiteOrdenacao = true;
            this.priGrelha_Moradas.PermitePaginacao = false;
            this.priGrelha_Moradas.PermiteScrollBars = true;
            this.priGrelha_Moradas.PermiteStatusBar = true;
            this.priGrelha_Moradas.PermiteVistas = true;
            this.priGrelha_Moradas.PosicionaColunaSeguinte = true;
            this.priGrelha_Moradas.Size = new System.Drawing.Size(1168, 441);
            this.priGrelha_Moradas.TabIndex = 0;
            this.priGrelha_Moradas.TituloGrelha = "";
            this.priGrelha_Moradas.TituloMapa = "";
            this.priGrelha_Moradas.TypeNameLinha = "";
            this.priGrelha_Moradas.TypeNameLinhas = "";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // f4_Entidade
            // 
            this.f4_Entidade.AgrupaOutrosTerceiros = false;
            this.f4_Entidade.Audit = "mnuTabClientes";
            this.f4_Entidade.AutoComplete = false;
            this.f4_Entidade.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_Entidade.CampoChave = "Cliente";
            this.f4_Entidade.CampoChaveFisica = "";
            this.f4_Entidade.CampoDescricao = "Nome";
            this.f4_Entidade.Caption = "Cliente:";
            this.f4_Entidade.CarregarValoresEdicao = false;
            this.f4_Entidade.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Clientes;
            this.f4_Entidade.ChaveFisica = "";
            this.f4_Entidade.ChaveNumerica = false;
            this.f4_Entidade.F4Modal = false;
            this.f4_Entidade.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_Entidade.IDCategoria = "Clientes";
            this.f4_Entidade.Location = new System.Drawing.Point(20, 31);
            this.f4_Entidade.Margin = new System.Windows.Forms.Padding(4);
            this.f4_Entidade.MaxLengthDescricao = 0;
            this.f4_Entidade.MaxLengthF4 = 50;
            this.f4_Entidade.MinimumSize = new System.Drawing.Size(49, 26);
            this.f4_Entidade.Modulo = "BAS";
            this.f4_Entidade.MostraDescricao = true;
            this.f4_Entidade.MostraLink = true;
            this.f4_Entidade.Name = "f4_Entidade";
            this.f4_Entidade.PainesInformacaoRelacionada = false;
            this.f4_Entidade.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_Entidade.PermiteDrillDown = true;
            this.f4_Entidade.PermiteEnabledLink = true;
            this.f4_Entidade.PodeEditarDescricao = false;
            this.f4_Entidade.ResourceID = 669;
            this.f4_Entidade.ResourcePersonalizada = false;
            this.f4_Entidade.Restricao = "";
            this.f4_Entidade.SelectionFormula = "";
            this.f4_Entidade.Size = new System.Drawing.Size(579, 26);
            this.f4_Entidade.TabIndex = 1;
            this.f4_Entidade.TextoDescricao = "";
            this.f4_Entidade.WidthEspacamento = 100;
            this.f4_Entidade.WidthF4 = 1600;
            this.f4_Entidade.WidthLink = 1000;
            // 
            // btn_Actualizar
            // 
            this.btn_Actualizar.Location = new System.Drawing.Point(607, 31);
            this.btn_Actualizar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(133, 50);
            this.btn_Actualizar.TabIndex = 2;
            this.btn_Actualizar.Text = "Actualizar";
            this.btn_Actualizar.UseVisualStyleBackColor = true;
            this.btn_Actualizar.Click += new System.EventHandler(this.btn_Actualizar_Click);
            // 
            // btn_Confirmar
            // 
            this.btn_Confirmar.Location = new System.Drawing.Point(748, 31);
            this.btn_Confirmar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Confirmar.Name = "btn_Confirmar";
            this.btn_Confirmar.Size = new System.Drawing.Size(133, 50);
            this.btn_Confirmar.TabIndex = 3;
            this.btn_Confirmar.Text = "Confirmar";
            this.btn_Confirmar.UseVisualStyleBackColor = true;
            this.btn_Confirmar.Click += new System.EventHandler(this.btn_Confirmar_Click);
            // 
            // FormCargaDescarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Confirmar);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.f4_Entidade);
            this.Controls.Add(this.priGrelha_Moradas);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormCargaDescarga";
            this.Size = new System.Drawing.Size(1216, 660);
            this.Text = "FormCargaDescarga";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCargaDescarga_FormClosed);
            this.Load += new System.EventHandler(this.FormCargaDescarga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha_Moradas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.PriGrelha priGrelha_Moradas;
        private System.Windows.Forms.PrintDialog printDialog1;
        private PRISDK100.F4 f4_Entidade;
        private System.Windows.Forms.Button btn_Actualizar;
        private System.Windows.Forms.Button btn_Confirmar;
    }
}