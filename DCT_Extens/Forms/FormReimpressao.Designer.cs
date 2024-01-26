using System.Drawing;
using System.Windows.Forms;

namespace DCT_Extens
{
    partial class FormReimpressao
    {
        private Button btn_Actualizar;
        private Button btn_Imprimir;
        private Label labelInicial;
        private Label labelFinal;
        private DateTimePicker dtPicker_DataDocInicial;
        private DateTimePicker dtPicker_DataDocFinal;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReimpressao));
            this.btn_Actualizar = new System.Windows.Forms.Button();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.labelInicial = new System.Windows.Forms.Label();
            this.labelFinal = new System.Windows.Forms.Label();
            this.dtPicker_DataDocInicial = new System.Windows.Forms.DateTimePicker();
            this.dtPicker_DataDocFinal = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBox_Mapas = new System.Windows.Forms.ComboBox();
            this.numUpDown_NumVias = new System.Windows.Forms.NumericUpDown();
            this.f4_Cliente = new PRISDK100.F4();
            this.priGrelha_Docs = new PRISDK100.PriGrelha();
            this.btn_SeleccionarTodos = new System.Windows.Forms.Button();
            this.btn_LimparSeleccao = new System.Windows.Forms.Button();
            this.listBox_TipoDoc = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_NumVias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha_Docs)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Actualizar
            // 
            this.btn_Actualizar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Actualizar.Image")));
            this.btn_Actualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Actualizar.Location = new System.Drawing.Point(13, 12);
            this.btn_Actualizar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Actualizar.Name = "btn_Actualizar";
            this.btn_Actualizar.Size = new System.Drawing.Size(107, 33);
            this.btn_Actualizar.TabIndex = 0;
            this.btn_Actualizar.Text = "Actualizar";
            this.btn_Actualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Actualizar.Click += new System.EventHandler(this.btn_Actualizar_Click);
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Imprimir.Image")));
            this.btn_Imprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Imprimir.Location = new System.Drawing.Point(128, 12);
            this.btn_Imprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(107, 33);
            this.btn_Imprimir.TabIndex = 1;
            this.btn_Imprimir.Text = "Imprimir  ";
            this.btn_Imprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
            // 
            // labelInicial
            // 
            this.labelInicial.Location = new System.Drawing.Point(7, 32);
            this.labelInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInicial.Name = "labelInicial";
            this.labelInicial.Size = new System.Drawing.Size(52, 20);
            this.labelInicial.TabIndex = 0;
            this.labelInicial.Text = "Inicial:";
            // 
            // labelFinal
            // 
            this.labelFinal.Location = new System.Drawing.Point(8, 68);
            this.labelFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFinal.Name = "labelFinal";
            this.labelFinal.Size = new System.Drawing.Size(47, 20);
            this.labelFinal.TabIndex = 1;
            this.labelFinal.Text = "Final:";
            // 
            // dtPicker_DataDocInicial
            // 
            this.dtPicker_DataDocInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataDocInicial.Location = new System.Drawing.Point(60, 32);
            this.dtPicker_DataDocInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtPicker_DataDocInicial.Name = "dtPicker_DataDocInicial";
            this.dtPicker_DataDocInicial.Size = new System.Drawing.Size(128, 22);
            this.dtPicker_DataDocInicial.TabIndex = 2;
            // 
            // dtPicker_DataDocFinal
            // 
            this.dtPicker_DataDocFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker_DataDocFinal.Location = new System.Drawing.Point(60, 68);
            this.dtPicker_DataDocFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtPicker_DataDocFinal.Name = "dtPicker_DataDocFinal";
            this.dtPicker_DataDocFinal.Size = new System.Drawing.Size(128, 22);
            this.dtPicker_DataDocFinal.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelInicial);
            this.groupBox1.Controls.Add(this.labelFinal);
            this.groupBox1.Controls.Add(this.dtPicker_DataDocFinal);
            this.groupBox1.Controls.Add(this.dtPicker_DataDocInicial);
            this.groupBox1.Location = new System.Drawing.Point(13, 68);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(203, 113);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data dos Documentos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mapa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 126);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nº Vias:";
            // 
            // cmbBox_Mapas
            // 
            this.cmbBox_Mapas.FormattingEnabled = true;
            this.cmbBox_Mapas.Location = new System.Drawing.Point(297, 81);
            this.cmbBox_Mapas.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBox_Mapas.Name = "cmbBox_Mapas";
            this.cmbBox_Mapas.Size = new System.Drawing.Size(559, 24);
            this.cmbBox_Mapas.TabIndex = 6;
            // 
            // numUpDown_NumVias
            // 
            this.numUpDown_NumVias.Location = new System.Drawing.Point(297, 126);
            this.numUpDown_NumVias.Margin = new System.Windows.Forms.Padding(4);
            this.numUpDown_NumVias.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown_NumVias.Name = "numUpDown_NumVias";
            this.numUpDown_NumVias.Size = new System.Drawing.Size(49, 22);
            this.numUpDown_NumVias.TabIndex = 7;
            this.numUpDown_NumVias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown_NumVias.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // f4_Cliente
            // 
            this.f4_Cliente.AgrupaOutrosTerceiros = false;
            this.f4_Cliente.Audit = "mnuTabClientes";
            this.f4_Cliente.AutoComplete = false;
            this.f4_Cliente.BackColorLocked = System.Drawing.SystemColors.ButtonFace;
            this.f4_Cliente.CampoChave = "Cliente";
            this.f4_Cliente.CampoChaveFisica = "";
            this.f4_Cliente.CampoDescricao = "Nome";
            this.f4_Cliente.Caption = "Cliente:";
            this.f4_Cliente.CarregarValoresEdicao = true;
            this.f4_Cliente.Categoria = PRISDK100.clsSDKTypes.EnumCategoria.Clientes;
            this.f4_Cliente.CausesValidation = false;
            this.f4_Cliente.ChaveFisica = "";
            this.f4_Cliente.ChaveNumerica = false;
            this.f4_Cliente.F4Modal = true;
            this.f4_Cliente.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.f4_Cliente.IDCategoria = "Clientes";
            this.f4_Cliente.Location = new System.Drawing.Point(13, 201);
            this.f4_Cliente.Margin = new System.Windows.Forms.Padding(4);
            this.f4_Cliente.MaxLengthDescricao = 0;
            this.f4_Cliente.MaxLengthF4 = 50;
            this.f4_Cliente.MinimumSize = new System.Drawing.Size(49, 26);
            this.f4_Cliente.Modulo = "BAS";
            this.f4_Cliente.MostraDescricao = true;
            this.f4_Cliente.MostraLink = true;
            this.f4_Cliente.Name = "f4_Cliente";
            this.f4_Cliente.PainesInformacaoRelacionada = false;
            this.f4_Cliente.PainesInformacaoRelacionadaMultiplasChaves = false;
            this.f4_Cliente.PermiteDrillDown = true;
            this.f4_Cliente.PermiteEnabledLink = true;
            this.f4_Cliente.PodeEditarDescricao = false;
            this.f4_Cliente.ResourceID = 669;
            this.f4_Cliente.ResourcePersonalizada = false;
            this.f4_Cliente.Restricao = "";
            this.f4_Cliente.SelectionFormula = "";
            this.f4_Cliente.Size = new System.Drawing.Size(599, 26);
            this.f4_Cliente.TabIndex = 8;
            this.f4_Cliente.TextoDescricao = "";
            this.f4_Cliente.WidthEspacamento = 60;
            this.f4_Cliente.WidthF4 = 1300;
            this.f4_Cliente.WidthLink = 800;
            // 
            // priGrelha_Docs
            // 
            this.priGrelha_Docs.BackColor = System.Drawing.Color.White;
            this.priGrelha_Docs.BandaMenuContexto = "";
            this.priGrelha_Docs.BotaoConfigurarActiveBar = true;
            this.priGrelha_Docs.BotaoProcurarActiveBar = false;
            this.priGrelha_Docs.CaminhoTemplateImpressao = "";
            this.priGrelha_Docs.Cols = null;
            this.priGrelha_Docs.ColsFrozen = -1;
            this.priGrelha_Docs.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.priGrelha_Docs.Location = new System.Drawing.Point(13, 306);
            this.priGrelha_Docs.Margin = new System.Windows.Forms.Padding(4);
            this.priGrelha_Docs.Name = "priGrelha_Docs";
            this.priGrelha_Docs.NumeroMaxRegistosSemPag = 150000;
            this.priGrelha_Docs.NumeroRegistos = 0;
            this.priGrelha_Docs.NumLinhasCabecalho = 1;
            this.priGrelha_Docs.OrientacaoMapa = PRISDK100.clsSDKTypes.OrientacaoImpressao.oiDefault;
            this.priGrelha_Docs.ParentFormModal = false;
            this.priGrelha_Docs.PermiteActiveBar = false;
            this.priGrelha_Docs.PermiteActualizar = true;
            this.priGrelha_Docs.PermiteAgrupamentosUser = true;
            this.priGrelha_Docs.PermiteConfigurarDetalhes = false;
            this.priGrelha_Docs.PermiteContextoVazia = false;
            this.priGrelha_Docs.PermiteDataFill = false;
            this.priGrelha_Docs.PermiteDetalhes = true;
            this.priGrelha_Docs.PermiteEdicao = false;
            this.priGrelha_Docs.PermiteFiltros = true;
            this.priGrelha_Docs.PermiteGrafico = true;
            this.priGrelha_Docs.PermiteGrandeTotal = false;
            this.priGrelha_Docs.PermiteOrdenacao = true;
            this.priGrelha_Docs.PermitePaginacao = false;
            this.priGrelha_Docs.PermiteScrollBars = true;
            this.priGrelha_Docs.PermiteStatusBar = true;
            this.priGrelha_Docs.PermiteVistas = true;
            this.priGrelha_Docs.PosicionaColunaSeguinte = true;
            this.priGrelha_Docs.Size = new System.Drawing.Size(844, 407);
            this.priGrelha_Docs.TabIndex = 10;
            this.priGrelha_Docs.TituloGrelha = "";
            this.priGrelha_Docs.TituloMapa = "";
            this.priGrelha_Docs.TypeNameLinha = "";
            this.priGrelha_Docs.TypeNameLinhas = "";
            // 
            // btn_SeleccionarTodos
            // 
            this.btn_SeleccionarTodos.Image = ((System.Drawing.Image)(resources.GetObject("btn_SeleccionarTodos.Image")));
            this.btn_SeleccionarTodos.Location = new System.Drawing.Point(13, 271);
            this.btn_SeleccionarTodos.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SeleccionarTodos.Name = "btn_SeleccionarTodos";
            this.btn_SeleccionarTodos.Size = new System.Drawing.Size(33, 28);
            this.btn_SeleccionarTodos.TabIndex = 11;
            this.btn_SeleccionarTodos.UseVisualStyleBackColor = true;
            this.btn_SeleccionarTodos.Click += new System.EventHandler(this.btn_SeleccionarTodos_Click);
            // 
            // btn_LimparSeleccao
            // 
            this.btn_LimparSeleccao.Image = ((System.Drawing.Image)(resources.GetObject("btn_LimparSeleccao.Image")));
            this.btn_LimparSeleccao.Location = new System.Drawing.Point(55, 271);
            this.btn_LimparSeleccao.Margin = new System.Windows.Forms.Padding(4);
            this.btn_LimparSeleccao.Name = "btn_LimparSeleccao";
            this.btn_LimparSeleccao.Size = new System.Drawing.Size(33, 28);
            this.btn_LimparSeleccao.TabIndex = 12;
            this.btn_LimparSeleccao.UseVisualStyleBackColor = true;
            this.btn_LimparSeleccao.Click += new System.EventHandler(this.btn_LimparSeleccao_Click);
            // 
            // listBox_TipoDoc
            // 
            this.listBox_TipoDoc.FormattingEnabled = true;
            this.listBox_TipoDoc.ItemHeight = 16;
            this.listBox_TipoDoc.Location = new System.Drawing.Point(620, 127);
            this.listBox_TipoDoc.Margin = new System.Windows.Forms.Padding(4);
            this.listBox_TipoDoc.Name = "listBox_TipoDoc";
            this.listBox_TipoDoc.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_TipoDoc.Size = new System.Drawing.Size(236, 164);
            this.listBox_TipoDoc.TabIndex = 13;
            // 
            // FormReimpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.f4_Cliente);
            this.Controls.Add(this.listBox_TipoDoc);
            this.Controls.Add(this.btn_LimparSeleccao);
            this.Controls.Add(this.btn_SeleccionarTodos);
            this.Controls.Add(this.priGrelha_Docs);
            this.Controls.Add(this.numUpDown_NumVias);
            this.Controls.Add(this.cmbBox_Mapas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Actualizar);
            this.Controls.Add(this.btn_Imprimir);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormReimpressao";
            this.Size = new System.Drawing.Size(872, 730);
            this.Text = "FormReimpressão";
            this.Shown += new System.EventHandler(this.FormReimpressao_Shown);
            this.Load += new System.EventHandler(this.FormReimpressao_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_NumVias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priGrelha_Docs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private ComboBox cmbBox_Mapas;
        private NumericUpDown numUpDown_NumVias;
        private PRISDK100.PriGrelha priGrelha_Docs;
        private Button btn_SeleccionarTodos;
        private Button btn_LimparSeleccao;
        private ListBox listBox_TipoDoc;
        public PRISDK100.F4 f4_Cliente;
    }
}