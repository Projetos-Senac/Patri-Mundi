﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peojeto_Prog_Sistem
{
    public partial class CadastroManutencao : Form
    {
        DataRowCollection dataRowCollection;
        DataRow dataRow;
        Manutencao manutencao = new Manutencao();
        /*
        public int id_manutencao;
        public int id_patrimonio;
        public string descPatri;
        public string previsao;
        public string motivo;
        public string item_patrimonio;
        */

        public CadastroManutencao()
        {
            InitializeComponent();
        }

        private void CadastroManutencao_Load(object sender, EventArgs e)
        {
            string nome;
            string sql = "SELECT id, descricaoPatri FROM patrimonios";
            DataTable listDesc = Banco.consulta(sql);
            dataRowCollection = listDesc.Rows;
            foreach (DataRow item in listDesc.Rows)
            {
                nome = item[0].ToString() + " - " + item[1].ToString();
                cbxPatrimonio.Items.Add(nome);
            }
        }

        private void cbxPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataRow = dataRowCollection[cbxPatrimonio.SelectedIndex];
            }
            catch
            {

            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                manutencao.id_patrimonio = Convert.ToInt32(dataRow[0].ToString());
                manutencao.descPatri = dataRow[1].ToString();
                manutencao.previsao = dtpPrevisao.Text;
                manutencao.motivo = tbxMotivo.Text;

                if (manutencao.previsao != "" && manutencao.motivo != "")
                {
                    Banco.cadastroManutencao(manutencao);
                    Banco.editarPatrimonioManutencao(manutencao.id_patrimonio);
                    tbxMotivo.Text = "";
                    cbxPatrimonio.SelectedIndex = -1;

                }
                else
                {
                    MessageBox.Show("Preencha todos os campos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("Erro ao cadastrar manutenção!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cbxPatrimonio.SelectedIndex = -1;





        }

        /*private void btnSalvar_Click(object sender, EventArgs e)
        {
            Manutencao manutencao = new Manutencao();
            manutencao.cadastro = Convert.ToInt32(cbxPatrimonio.Text);
            manutencao.previsao = dtbManutPrevisao.Text;
            manutencao.motivo = rtbManutMotivo.Text;

            Banco.cadastroManutencao(manutencao);

        }*/
    }
}
