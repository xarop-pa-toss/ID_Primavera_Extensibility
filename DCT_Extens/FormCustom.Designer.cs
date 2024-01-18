namespace DCT_Extens
{
    partial class FormCustom
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
            this.f4 = new PRISDK100.F4();
            this.SuspendLayout();
            // 
            // f4
            // 
            this.f4.AgrupaOutrosTerceiros = false;
            this.f4.Audit = "";
            this.f4.AutoComplete = false;
            this.f4.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4.CampoChave = "";
            this.f4.CampoChaveFisica = "";
            this.f4.CampoDescricao = "";
            this.f4.Caption = "";
            this.f4.CarregarValoresEdicao = false;
            this.f4.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.NaoDefinida;
            this.f4.ChaveFisica = "";
            this.f4.ChaveNumerica = false;
            this.f4.F4Modal = false;
            this.f4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4.IDCategoria = "";
            this.f4.Location = new System.Drawing.Point(96, 151);
            this.f4.MaxLengthDescricao = 0;
            this.f4.MaxLengthF4 = 50;
            this.f4.MinimumSize = new System.Drawing.Size(37, 21);
            this.f4.Modulo = "";
            this.f4.MostraDescricao = true;
            this.f4.MostraLink = true;
            this.f4.Name = "f4";
            this.f4.PainesInformacaoRelacionada = false;
            this.f4.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4.PermiteDrillDown = true;
            this.f4.PermiteEnabledLink = true;
            this.f4.PodeEditarDescricao = false;
            this.f4.ResourceID = 0;
            this.f4.ResourcePersonalizada = false;
            this.f4.Restricao = "";
            this.f4.SelectionFormula = "";
            this.f4.Size = new System.Drawing.Size(474, 30);
            this.f4.TabIndex = 0;
            this.f4.TextoDescricao = "";
            this.f4.WidthEspacamento = 60;
            this.f4.WidthF4 = 1590;
            this.f4.WidthLink = 1575;
            // 
            // FormCustom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.f4);
            this.Name = "FormCustom";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "FormCustom";
            this.Load += new System.EventHandler(this.FormCustom_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f4;
    }
}