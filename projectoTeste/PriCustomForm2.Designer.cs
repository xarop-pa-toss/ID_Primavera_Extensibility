namespace projectoTeste
{
    partial class PriCustomForm2
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
            this.f41 = new PRISDK100.F4();
            this.f42 = new PRISDK100.F4();
            this.SuspendLayout();
            // 
            // f41
            // 
            this.f41.AgrupaOutrosTerceiros = false;
            this.f41.Audit = "";
            this.f41.AutoComplete = false;
            this.f41.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f41.CampoChave = "";
            this.f41.CampoChaveFisica = "";
            this.f41.CampoDescricao = "";
            this.f41.Caption = "";
            this.f41.CarregarValoresEdicao = false;
            this.f41.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.NaoDefinida;
            this.f41.ChaveFisica = "";
            this.f41.ChaveNumerica = false;
            this.f41.F4Modal = false;
            this.f41.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f41.IDCategoria = "";
            this.f41.Location = new System.Drawing.Point(477, 173);
            this.f41.MaxLengthDescricao = 0;
            this.f41.MaxLengthF4 = 50;
            this.f41.MinimumSize = new System.Drawing.Size(37, 21);
            this.f41.Modulo = "";
            this.f41.MostraDescricao = true;
            this.f41.MostraLink = true;
            this.f41.Name = "f41";
            this.f41.PainesInformacaoRelacionada = false;
            this.f41.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f41.PermiteDrillDown = true;
            this.f41.PermiteEnabledLink = true;
            this.f41.PodeEditarDescricao = false;
            this.f41.ResourceID = 0;
            this.f41.ResourcePersonalizada = false;
            this.f41.Restricao = "";
            this.f41.SelectionFormula = "";
            this.f41.Size = new System.Drawing.Size(37, 21);
            this.f41.TabIndex = 0;
            this.f41.TextoDescricao = "";
            this.f41.WidthEspacamento = 60;
            this.f41.WidthF4 = 1590;
            this.f41.WidthLink = 1575;
            // 
            // f42
            // 
            this.f42.AgrupaOutrosTerceiros = false;
            this.f42.Audit = "mnuTabClientes";
            this.f42.AutoComplete = false;
            this.f42.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f42.CampoChave = "Cliente";
            this.f42.CampoChaveFisica = "";
            this.f42.CampoDescricao = "Nome";
            this.f42.Caption = "Cliente:";
            this.f42.CarregarValoresEdicao = false;
            this.f42.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Clientes;
            this.f42.ChaveFisica = "";
            this.f42.ChaveNumerica = false;
            this.f42.F4Modal = false;
            this.f42.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f42.IDCategoria = "Clientes";
            this.f42.Location = new System.Drawing.Point(59, 58);
            this.f42.MaxLengthDescricao = 0;
            this.f42.MaxLengthF4 = 50;
            this.f42.MinimumSize = new System.Drawing.Size(37, 21);
            this.f42.Modulo = "BAS";
            this.f42.MostraDescricao = true;
            this.f42.MostraLink = true;
            this.f42.Name = "f42";
            this.f42.PainesInformacaoRelacionada = false;
            this.f42.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f42.PermiteDrillDown = true;
            this.f42.PermiteEnabledLink = true;
            this.f42.PodeEditarDescricao = false;
            this.f42.ResourceID = 669;
            this.f42.ResourcePersonalizada = false;
            this.f42.Restricao = "";
            this.f42.SelectionFormula = "";
            this.f42.Size = new System.Drawing.Size(304, 22);
            this.f42.TabIndex = 1;
            this.f42.TextoDescricao = "";
            this.f42.WidthEspacamento = 60;
            this.f42.WidthF4 = 1590;
            this.f42.WidthLink = 1575;
            // 
            // PriCustomForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.f42);
            this.Controls.Add(this.f41);
            this.Name = "PriCustomForm2";
            this.Size = new System.Drawing.Size(800, 450);
            this.Text = "PriCustomForm2";
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f41;
        private PRISDK100.F4 f42;
    }
}