using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AgendaCrud
{
    public partial class Form1 : Form
    {
        private Data.Factory.ConnectionString _data = new Data.Factory.ConnectionString();
        MySqlCommand cmd;
        int IDENT = 0;

        public Form1()
        {
            InitializeComponent();
            ExibirDados();
        }

        private void ExibirDados()
        {
            try
            {

                var sql = "SELECT * FROM Contatos";
                var dt = _data.Read(sql, null);
                dgvAgenda.DataSource = dt.Tables[0];
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void LimparDados()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            IDENT = 0;
        }

        private void btn_Sair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Sair do programa ?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                txtNome.Focus();
            }

        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEmail.Text = "";
            txtNome.Focus();
        }

        private string StringConnection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["StringConnection"].ToString();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtTelefone.Text != "" && txtEmail.Text != "")
            {
                 try
                 {

                    var query = "INSERT INTO Contatos(nome,endereco,celular,telefone,email) " + "VALUES ('" + txtNome.Text + "', '" + txtEndereco.Text + "', '" + txtCelular.Text + "', '" + txtTelefone.Text + "', '" + txtEmail.Text + "')";
                    var ret = _data.Executar(query, null);
                    MessageBox.Show("Registro Salvo com sucesso...");
                    

                }
                 catch (Exception ex)
                 {   
                     MessageBox.Show("Erro: " + ex.Message);
                 }
                 finally
                 {
                   
                     ExibirDados();
                     LimparDados();
                 }
             }
             else
             {
                 MessageBox.Show("Informe todos os dados requeridos!!");
             }
            }

        private void btnDeletar_Click_1(object sender, EventArgs e)
        {
            if (IDENT != 0)
            {
                if (MessageBox.Show("Deseja Deletar este registro ?", "Agenda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var query = "DELETE from Contatos WHERE id='"+ IDENT +"'";
                        var ret =  _data.Executar(query, null);
                        MessageBox.Show("registro deletado com sucesso...!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro : " + ex.Message);
                    }
                    finally
                    {
                        
                        ExibirDados();
                        LimparDados();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um registro para deletar");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEndereco.Text != "" && txtCelular.Text != "" && txtTelefone.Text != "" && txtEmail.Text != "")
            {
                try
                {
                     var query = "UPDATE Contatos SET nome='" + txtNome.Text + "', endereco='" + txtEndereco.Text + "', celular='" + txtCelular.Text + "', telefone='" + txtTelefone.Text + "', email='" + txtEmail.Text + "' WHERE id='"+ IDENT +"'";
                     var ret = _data.Executar(query, null);
                     MessageBox.Show("Registro atualizado com sucesso...");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
                finally
                {
                    
                    ExibirDados();
                    LimparDados();
                }
            }
            else
            {
                MessageBox.Show("Informe todos os dados requeridos!!");
            }
        }

        private void btn_About_Click_1(object sender, EventArgs e)
        {

            MessageBox.Show("Adaptado por Valter Gomes .net", "Agenda", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            txtNome.Focus();
        }

        private void dgvAgenda_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                IDENT = Convert.ToInt32(dgvAgenda.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNome.Text = dgvAgenda.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEndereco.Text = dgvAgenda.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCelular.Text = dgvAgenda.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTelefone.Text = dgvAgenda.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmail.Text = dgvAgenda.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}


