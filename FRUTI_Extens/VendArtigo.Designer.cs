
namespace FRUTI_Extens
{
    partial class VendArtigo
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
            this.f4_subfamilia = new PRISDK100.F4();
            this.SuspendLayout();
            // 
            // f4_subfamilia
            // 
            this.f4_subfamilia.AgrupaOutrosTerceiros = false;
            this.f4_subfamilia.Audit = "mnuTabFamilias";
            this.f4_subfamilia.AutoComplete = false;
            this.f4_subfamilia.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_subfamilia.CampoChave = "SubFamilia";
            this.f4_subfamilia.CampoChaveFisica = "";
            this.f4_subfamilia.CampoDescricao = "Descricao";
            this.f4_subfamilia.Caption = "Subfamília:";
            this.f4_subfamilia.CarregarValoresEdicao = false;
            this.f4_subfamilia.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.SubFamilia;
            this.f4_subfamilia.ChaveFisica = "";
            this.f4_subfamilia.ChaveNumerica = false;
            this.f4_subfamilia.F4Modal = false;
            this.f4_subfamilia.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_subfamilia.IDCategoria = "SubFamilias";
            this.f4_subfamilia.Location = new System.Drawing.Point(36, 21);
            this.f4_subfamilia.MaxLengthDescricao = 0;
            this.f4_subfamilia.MaxLengthF4 = 50;
            this.f4_subfamilia.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4_subfamilia.Modulo = "BAS";
            this.f4_subfamilia.MostraDescricao = true;
            this.f4_subfamilia.MostraLink = true;
            this.f4_subfamilia.Name = "f4_subfamilia";
            this.f4_subfamilia.PainesInformacaoRelacionada = false;
            this.f4_subfamilia.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_subfamilia.PermiteDrillDown = true;
            this.f4_subfamilia.PermiteEnabledLink = true;
            this.f4_subfamilia.PodeEditarDescricao = false;
            this.f4_subfamilia.ResourceID = 684;
            this.f4_subfamilia.ResourcePersonalizada = false;
            this.f4_subfamilia.Restricao = "";
            this.f4_subfamilia.SelectionFormula = "";
            this.f4_subfamilia.Size = new System.Drawing.Size(484, 22);
            this.f4_subfamilia.TabIndex = 0;
            this.f4_subfamilia.TextoDescricao = "";
            this.f4_subfamilia.WidthEspacamento = 60;
            this.f4_subfamilia.WidthF4 = 1590;
            this.f4_subfamilia.WidthLink = 1575;
            // 
            // VendArtigo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 322);
            this.Controls.Add(this.f4_subfamilia);
            this.Name = "VendArtigo";
            this.Text = "VendArtigo";
            this.Load += new System.EventHandler(this.VendArtigo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f4_subfamilia;
    }
}