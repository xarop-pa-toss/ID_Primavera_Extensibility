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
            this.priGrelha1 = new PRISDK100.PriGrelha();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.f4_Entidade = new PRISDK100.F4();
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.btn_Confirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha1)).BeginInit();
            this.SuspendLayout();
            // 
            // priGrelha1
            // 
            this.priGrelha1.BackColor = System.Drawing.Color.White;
            this.priGrelha1.BandaMenuContexto = "";
            this.priGrelha1.BotaoConfigurarActiveBar = true;
            this.priGrelha1.BotaoProcurarActiveBar = false;
            this.priGrelha1.CaminhoTemplateImpressao = "";
            this.priGrelha1.Cols = null;
            this.priGrelha1.ColsFrozen = -1;
            this.priGrelha1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.priGrelha1.Location = new System.Drawing.Point(20, 196);
            this.priGrelha1.Margin = new System.Windows.Forms.Padding(4);
            this.priGrelha1.Name = "priGrelha1";
            this.priGrelha1.NumeroMaxRegistosSemPag = 150000;
            this.priGrelha1.NumeroRegistos = 0;
            this.priGrelha1.NumLinhasCabecalho = 1;
            this.priGrelha1.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.priGrelha1.ParentFormModal = false;
            this.priGrelha1.PermiteActiveBar = false;
            this.priGrelha1.PermiteActualizar = true;
            this.priGrelha1.PermiteAgrupamentosUser = true;
            this.priGrelha1.PermiteConfigurarDetalhes = false;
            this.priGrelha1.PermiteContextoVazia = false;
            this.priGrelha1.PermiteDataFill = false;
            this.priGrelha1.PermiteDetalhes = true;
            this.priGrelha1.PermiteEdicao = false;
            this.priGrelha1.PermiteFiltros = true;
            this.priGrelha1.PermiteGrafico = true;
            this.priGrelha1.PermiteGrandeTotal = false;
            this.priGrelha1.PermiteOrdenacao = true;
            this.priGrelha1.PermitePaginacao = false;
            this.priGrelha1.PermiteScrollBars = true;
            this.priGrelha1.PermiteStatusBar = true;
            this.priGrelha1.PermiteVistas = true;
            this.priGrelha1.PosicionaColunaSeguinte = true;
            this.priGrelha1.Size = new System.Drawing.Size(1168, 441);
            this.priGrelha1.TabIndex = 0;
            this.priGrelha1.TituloGrelha = "";
            this.priGrelha1.TituloMapa = "";
            this.priGrelha1.TypeNameLinha = "";
            this.priGrelha1.TypeNameLinhas = "";
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
            // 
            // FormCargaDescarga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Confirmar);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.f4_Entidade);
            this.Controls.Add(this.priGrelha1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormCargaDescarga";
            this.Size = new System.Drawing.Size(1216, 660);
            this.Text = "FormCargaDescarga";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCargaDescarga_FormClosed);
            this.Load += new System.EventHandler(this.FormCargaDescarga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.PriGrelha priGrelha1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private PRISDK100.F4 f4_Entidade;
        private System.Windows.Forms.Button btn_Actualizar;
        private System.Windows.Forms.Button btn_Confirmar;
    }
}