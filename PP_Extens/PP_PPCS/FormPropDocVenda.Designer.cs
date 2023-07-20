﻿
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
            this.label3 = new System.Windows.Forms.Label();
            this.cBoxSerie = new System.Windows.Forms.ComboBox();
            this.cBoxNumero = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.priGrelha1 = new PRISDK100.PriGrelha();
            this.f4TipoDoc = new PRISDK100.F4();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Número";
            // 
            // cBoxSerie
            // 
            this.cBoxSerie.FormattingEnabled = true;
            this.cBoxSerie.Location = new System.Drawing.Point(408, 16);
            this.cBoxSerie.Name = "cBoxSerie";
            this.cBoxSerie.Size = new System.Drawing.Size(77, 21);
            this.cBoxSerie.TabIndex = 4;
            // 
            // cBoxNumero
            // 
            this.cBoxNumero.FormattingEnabled = true;
            this.cBoxNumero.Location = new System.Drawing.Point(63, 46);
            this.cBoxNumero.Name = "cBoxNumero";
            this.cBoxNumero.Size = new System.Drawing.Size(121, 21);
            this.cBoxNumero.TabIndex = 5;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(661, 37);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(99, 37);
            this.btnActualizar.TabIndex = 6;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(661, 443);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(99, 33);
            this.btnSair.TabIndex = 7;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
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
            this.priGrelha1.Location = new System.Drawing.Point(16, 94);
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
            this.priGrelha1.Size = new System.Drawing.Size(761, 391);
            this.priGrelha1.TabIndex = 8;
            this.priGrelha1.TituloGrelha = "";
            this.priGrelha1.TituloMapa = "";
            this.priGrelha1.TypeNameLinha = "";
            this.priGrelha1.TypeNameLinhas = "";
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
            this.f4TipoDoc.Location = new System.Drawing.Point(16, 16);
            this.f4TipoDoc.Margin = new System.Windows.Forms.Padding(2);
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
            this.f4TipoDoc.Size = new System.Drawing.Size(387, 22);
            this.f4TipoDoc.TabIndex = 14;
            this.f4TipoDoc.TextoDescricao = "";
            this.f4TipoDoc.WidthEspacamento = 60;
            this.f4TipoDoc.WidthF4 = 1100;
            this.f4TipoDoc.WidthLink = 1300;
            this.f4TipoDoc.TextChange += new PRISDK100.F4.TextChangeHandler(this.f4TipoDoc_TextChange);
            // 
            // FormPropDocVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.f4TipoDoc);
            this.Controls.Add(this.priGrelha1);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.cBoxNumero);
            this.Controls.Add(this.cBoxSerie);
            this.Controls.Add(this.label3);
            this.Name = "FormPropDocVenda";
            this.Size = new System.Drawing.Size(794, 498);
            this.Text = "FormPropDocVenda";
            this.Load += new System.EventHandler(this.FormPropDocVenda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cBoxSerie;
        private System.Windows.Forms.ComboBox cBoxNumero;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSair;
        private PRISDK100.PriGrelha priGrelha1;
        private PRISDK100.F4 f4TipoDoc;
    }
}