namespace projectoTeste
{
    partial class PriCustomForm1
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
            this.SuspendLayout();
            // 
            // f41
            // 
            this.f41.AgrupaOutrosTerceiros = false;
            this.f41.Audit = "mnuTabClientes";
            this.f41.AutoComplete = false;
            this.f41.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f41.CampoChave = "Cliente";
            this.f41.CampoChaveFisica = "";
            this.f41.CampoDescricao = "Nome";
            this.f41.Caption = "Cliente:";
            this.f41.CarregarValoresEdicao = false;
            this.f41.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Clientes;
            this.f41.ChaveFisica = "";
            this.f41.ChaveNumerica = false;
            this.f41.F4Modal = false;
            this.f41.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f41.IDCategoria = "Clientes";
            this.f41.Location = new System.Drawing.Point(40, 82);
            this.f41.MaxLengthDescricao = 0;
            this.f41.MaxLengthF4 = 50;
            this.f41.MinimumSize = new System.Drawing.Size(37, 21);
            this.f41.Modulo = "BAS";
            this.f41.MostraDescricao = true;
            this.f41.MostraLink = true;
            this.f41.Name = "f41";
            this.f41.PainesInformacaoRelacionada = false;
            this.f41.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f41.PermiteDrillDown = true;
            this.f41.PermiteEnabledLink = true;
            this.f41.PodeEditarDescricao = false;
            this.f41.ResourceID = 669;
            this.f41.ResourcePersonalizada = false;
            this.f41.Restricao = "";
            this.f41.SelectionFormula = "";
            this.f41.Size = new System.Drawing.Size(304, 22);
            this.f41.TabIndex = 0;
            this.f41.TextoDescricao = "";
            this.f41.WidthEspacamento = 60;
            this.f41.WidthF4 = 1590;
            this.f41.WidthLink = 1575;
            // 
            // PriCustomForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.f41);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "PriCustomForm1";
            this.Size = new System.Drawing.Size(526, 203);
            this.Text = "PriCustomForm1";
            this.Load += new System.EventHandler(this.PriCustomForm1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private PRISDK100.F4 f41;
    }
}