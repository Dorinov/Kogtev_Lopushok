using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class Materials : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);

        BindingSource bind = new BindingSource();
        NpgsqlDataAdapter da = new NpgsqlDataAdapter();

        public Materials()
        {
            InitializeComponent();
        }

        private void Materials_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bind;
            fillInfo();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value);

            if (MessageBox.Show($"Вы уверены, что хотите удалить материал с ID {id}?", "Удаление материала", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                con.Open();
                var cmd = new NpgsqlCommand($"delete from \"Material\" where \"ID\" = {id};" +
                    $"delete from \"ProductMaterial\" where \"MaterialID\" = {id};", con);
                cmd.ExecuteNonQuery();
                con.Close();

                fillInfo();
            }
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update((DataTable)bind.DataSource);
                fillInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка БД");
            }
        }



        private void fillInfo()
        {
            try
            {
                da = new NpgsqlDataAdapter("select * from \"Material\" order by \"ID\" asc", con);
                NpgsqlCommandBuilder cb = new NpgsqlCommandBuilder(da);
                DataTable dt = new DataTable();
                da.Fill(dt);
                bind.DataSource = dt;
                dataGridView1.Columns["ID"].ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка БД");
            }
        }
    }
}
