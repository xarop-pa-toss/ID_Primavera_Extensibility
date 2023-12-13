﻿using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.CustomForm;
using Primavera.Extensibility.Extensions;
using StdPlatBS100;
using ErpBS100;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using PP_Extens.Sales;
using System.Windows.Forms;

namespace PP_Extens
{
    public partial class InputForm : CustomForm
    {
        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            this.Text = FormServicoDados.Titulo;
            lbl_Descricao.Text = FormServicoDados.Descricao;
            txtBox_Resposta.Text = FormServicoDados.ValorDefeito;

            // Ajustar as altura do form à altura da descrição
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Height += lbl_Descricao.Height;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.PerformLayout();
        }

        private void InputForm_Shown(object sender, EventArgs e)
        {
        }

        #region Botões
        private void btn_OK_Click(object sender, EventArgs e)
        {
            FormServicoDados.Resposta = txtBox_Resposta.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        #endregion

        private void InputForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (DialogResult == null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            
        }

        // Quando textbox está em foco, carregar no Enter activa o botão OK
        private void txtBox_Resposta_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btn_OK.PerformClick();
                e.Handled = true;
            }
        }
    }
}