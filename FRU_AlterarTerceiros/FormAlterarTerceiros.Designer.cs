
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxTipoDoc = new System.Windows.Forms.ComboBox();
            this.cboxSerie = new System.Windows.Forms.ComboBox();
            this.btnAlterarTerceiro = new System.Windows.Forms.Button();
            this.f4Terceiros = new PRISDK100.F4();
            this.f4TipoDoc = new PRISDK100.F4();
            this.numericNumDoc = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericNumDoc)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nº Doc:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo Doc:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Série:";
            // 
            // cboxTipoDoc
            // 
            this.cboxTipoDoc.FormattingEnabled = true;
            this.cboxTipoDoc.Location = new System.Drawing.Point(120, 38);
            this.cboxTipoDoc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxTipoDoc.Name = "cboxTipoDoc";
            this.cboxTipoDoc.Size = new System.Drawing.Size(100, 24);
            this.cboxTipoDoc.TabIndex = 6;
            // 
            // cboxSerie
            // 
            this.cboxSerie.FormattingEnabled = true;
            this.cboxSerie.Location = new System.Drawing.Point(298, 38);
            this.cboxSerie.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboxSerie.Name = "cboxSerie";
            this.cboxSerie.Size = new System.Drawing.Size(71, 24);
            this.cboxSerie.TabIndex = 7;
            // 
            // btnAlterarTerceiro
            // 
            this.btnAlterarTerceiro.Location = new System.Drawing.Point(605, 391);
            this.btnAlterarTerceiro.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAlterarTerceiro.Name = "btnAlterarTerceiro";
            this.btnAlterarTerceiro.Size = new System.Drawing.Size(131, 34);
            this.btnAlterarTerceiro.TabIndex = 9;
            this.btnAlterarTerceiro.Text = "Alterar Terceiro";
            this.btnAlterarTerceiro.UseVisualStyleBackColor = true;
            this.btnAlterarTerceiro.Click += new System.EventHandler(this.btnAlterarTerceiro_Click);
            // 
            // f4Terceiros
            // 
            this.f4Terceiros.AgrupaOutrosTerceiros = false;
            this.f4Terceiros.Audit = "mnuTabTipoTerceiros";
            this.f4Terceiros.AutoComplete = false;
            this.f4Terceiros.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4Terceiros.CampoChave = "TipoTerceiro";
            this.f4Terceiros.CampoChaveFisica = "";
            this.f4Terceiros.CampoDescricao = "Descricao";
            this.f4Terceiros.Caption = "Tipo Terceiro:";
            this.f4Terceiros.CarregarValoresEdicao = false;
            this.f4Terceiros.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.TiposTerceiro;
            this.f4Terceiros.ChaveFisica = "";
            this.f4Terceiros.ChaveNumerica = false;
            this.f4Terceiros.F4Modal = false;
            this.f4Terceiros.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4Terceiros.IDCategoria = "TipoTerceiros";
            this.f4Terceiros.Location = new System.Drawing.Point(226, 174);
            this.f4Terceiros.Margin = new System.Windows.Forms.Padding(4);
            this.f4Terceiros.MaxLengthDescricao = 0;
            this.f4Terceiros.MaxLengthF4 = 50;
            this.f4Terceiros.MinimumSize = new System.Drawing.Size(49, 26);
            this.f4Terceiros.Modulo = "BAS";
            this.f4Terceiros.MostraDescricao = true;
            this.f4Terceiros.MostraLink = true;
            this.f4Terceiros.Name = "f4Terceiros";
            this.f4Terceiros.PainesInformacaoRelacionada = false;
            this.f4Terceiros.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4Terceiros.PermiteDrillDown = true;
            this.f4Terceiros.PermiteEnabledLink = true;
            this.f4Terceiros.PodeEditarDescricao = false;
            this.f4Terceiros.ResourceID = 688;
            this.f4Terceiros.ResourcePersonalizada = false;
            this.f4Terceiros.Restricao = "";
            this.f4Terceiros.SelectionFormula = "";
            this.f4Terceiros.Size = new System.Drawing.Size(351, 43);
            this.f4Terceiros.TabIndex = 12;
            this.f4Terceiros.TextoDescricao = "";
            this.f4Terceiros.WidthEspacamento = 60;
            this.f4Terceiros.WidthF4 = 1100;
            this.f4Terceiros.WidthLink = 1350;
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
            this.f4TipoDoc.Location = new System.Drawing.Point(226, 129);
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
            this.f4TipoDoc.Size = new System.Drawing.Size(351, 38);
            this.f4TipoDoc.TabIndex = 13;
            this.f4TipoDoc.TextoDescricao = "";
            this.f4TipoDoc.WidthEspacamento = 60;
            this.f4TipoDoc.WidthF4 = 1100;
            this.f4TipoDoc.WidthLink = 1300;
            this.f4TipoDoc.TextChange += new PRISDK100.F4.TextChangeHandler(this.f4TipoDoc_TextChange);
            // 
            // numericNumDoc
            // 
            this.numericNumDoc.Location = new System.Drawing.Point(120, 84);
            this.numericNumDoc.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericNumDoc.Name = "numericNumDoc";
            this.numericNumDoc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numericNumDoc.Size = new System.Drawing.Size(79, 22);
            this.numericNumDoc.TabIndex = 14;
            this.numericNumDoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // FromAlterarTerceiros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericNumDoc);
            this.Controls.Add(this.f4TipoDoc);
            this.Controls.Add(this.f4Terceiros);
            this.Controls.Add(this.btnAlterarTerceiro);
            this.Controls.Add(this.cboxSerie);
            this.Controls.Add(this.cboxTipoDoc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FromAlterarTerceiros";
            this.Size = new System.Drawing.Size(753, 442);
            this.Text = "Alteração Terceiros";
            this.Load += new System.EventHandler(this.FormAlterarTerceiros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericNumDoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxTipoDoc;
        private System.Windows.Forms.ComboBox cboxSerie;
        private System.Windows.Forms.Button btnAlterarTerceiro;
        private PRISDK100.F4 f4Terceiros;
        private PRISDK100.F4 f4TipoDoc;
        private System.Windows.Forms.NumericUpDown numericNumDoc;
    }
}