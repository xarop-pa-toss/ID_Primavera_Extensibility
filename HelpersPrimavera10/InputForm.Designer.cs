﻿using System.Drawing;

namespace HelpersPrimavera10
{
    partial class InputForm
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
            this.lbl_Descricao = new System.Windows.Forms.Label();
            this.txtBox_Resposta = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Descricao
            // 
            this.lbl_Descricao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Descricao.AutoSize = true;
            this.lbl_Descricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Descricao.Location = new System.Drawing.Point(16, 16);
            this.lbl_Descricao.Name = "lbl_Descricao";
            this.lbl_Descricao.Size = new System.Drawing.Size(46, 18);
            this.lbl_Descricao.TabIndex = 0;
            this.lbl_Descricao.Text = "label1";
            // 
            // txtBox_Resposta
            // 
            this.txtBox_Resposta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBox_Resposta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBox_Resposta.Location = new System.Drawing.Point(19, 45);
            this.txtBox_Resposta.Name = "txtBox_Resposta";
            this.txtBox_Resposta.Size = new System.Drawing.Size(327, 24);
            this.txtBox_Resposta.TabIndex = 1;
            this.txtBox_Resposta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBox_Resposta_KeyPress);
            // 
            // btn_OK
            // 
            this.btn_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OK.Location = new System.Drawing.Point(180, 84);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(80, 30);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancelar.Location = new System.Drawing.Point(266, 84);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(80, 30);
            this.btn_Cancelar.TabIndex = 3;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = true;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // InputForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(365, 123);
            this.Controls.Add(this.btn_Cancelar);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txtBox_Resposta);
            this.Controls.Add(this.lbl_Descricao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputForm_FormClosing);
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Descricao;
        private System.Windows.Forms.TextBox txtBox_Resposta;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancelar;
    }
}