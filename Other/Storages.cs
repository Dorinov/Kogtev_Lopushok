using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class Storages : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);

        BindingSource bind = new BindingSource();
        NpgsqlDataAdapter da = new NpgsqlDataAdapter();

        public Storages()
        {
            InitializeComponent();
        }

        private void Storages_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bind;
            fillInfo();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value);

            if (MessageBox.Show($"Вы уверены, что хотите удалить склад с ID {id}?", "Удаление склада", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                con.Open();
                var cmd = new NpgsqlCommand($"delete from \"Storage\" where \"ID\" = {id};" +
                    $"delete from \"StorageAvailability\" where \"StorageID\" = {id};", con);
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
                da = new NpgsqlDataAdapter("select * from \"Storage\" order by \"ID\" asc", con);
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
