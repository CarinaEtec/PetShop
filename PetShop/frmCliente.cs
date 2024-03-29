﻿using PetShop.BO;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetShop
{
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            txtNome.Enabled = false;
            mskCpf.Enabled = false;
            mskCep.Enabled = false;
            txtEndereco.Enabled = false;
            txtCidade.Enabled = false;
            txtNumero.Enabled = false;
            mskTelefone.Enabled = false;
            txtEmail.Enabled = false;

            btnSalvar.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscarCod.Visible = false;
            btnBuscarCep.Visible = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            txtNome.Enabled = true;
            mskCpf.Enabled = true;
            mskCep.Enabled = true;
            txtNumero.Enabled = true;
            mskTelefone.Enabled = true;
            txtEmail.Enabled = true;

            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            btnBuscar.Enabled = false;
            btnBuscarCep.Visible = true;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 voltar = new Form1();
            voltar.Closed += (s, args) => this.Close();
            voltar.Show();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtCodigo.Enabled = true;
            btnBuscarCod.Visible = true;
            btnNovo.Enabled = false;
        }

        private void btnBuscarCod_Click(object sender, EventArgs e)
        {
            btnExcluir.Enabled = true;
            btnEditar.Enabled = true;
            btnBuscar.Enabled = false;
        }

        private void btnBuscarCep_Click(object sender, EventArgs e)
        {
            try
            {
                var webService = new WSCorreios.AtendeClienteClient();
                var resposta = webService.consultaCEP(mskCep.Text);

                txtEndereco.Text = resposta.end;
                txtCidade.Text = resposta.cidade;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                mskCep.Clear();
                txtEndereco.Clear();
                txtCidade.Clear();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente();
            ClienteBO clienteBO = new ClienteBO();

            cliente.Nome = txtNome.Text;
            cliente.Cpf = Convert.ToInt16(mskCpf.Text);
            cliente.Cep = mskCep.Text;
            cliente.Endereco = txtEndereco.Text;
            cliente.Cidade = txtCidade.Text;
            cliente.Numero = txtNumero.Text;
            cliente.Telefone = mskTelefone.Text;
            cliente.Email = txtEmail.Text;

            clienteBO.Gravar(cliente);
            MessageBox.Show("Cliente cadastrado com sucesso");
        }
    }
}
