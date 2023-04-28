using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class ProductMaterials : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);
        int id;

        DataTable materials = new DataTable();
        DataTable actualMaterials = new DataTable();

        int mid;
        string lastQuery = "";

        Timer timer_mat = new Timer();

        public ProductMaterials(int i)
        {
            id = i;
            InitializeComponent();

            timer_mat.Interval = 2000;
            timer_mat.Tick += new EventHandler(timer_tick_mat);
            timer_mat.Start();
        }

        private void ProductMaterials_Load(object sender, EventArgs e)
        {
            getAllMaterials();
            getCurrentMaterials();
            fillComboBox();
        }

        private void button_MaterialDel_Click(object sender, EventArgs e)
        {
            try
            {
                int _id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                int count = (int)dataGridView1.SelectedRows[0].Cells[2].Value;
                if (MessageBox.Show($"Вы действительно хотите удалить выбранный материал (ID: {_id})?", "Удаление",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand($"delete from \"ProductMaterial\" where " +
                        $"\"MaterialID\" = {_id} and \"ProductID\" = {id} and \"Count\" = {count}", con))
                        cmd.ExecuteNonQuery();
                    con.Close();
                    getCurrentMaterials();
                }
            }
            catch (Exception ex)
                { MessageBox.Show(ex.ToString(), "Ошибка"); }
        }

        private void button_AddMaterial_Click(object sender, EventArgs e)
        {
            if (mid != -1)
                try
                {
                    con.Open();
                    using (var cmd = new NpgsqlCommand($"INSERT INTO \"ProductMaterial\" VALUES ({id}, {mid}, {numericUpDown1.Value})", con))
                        cmd.ExecuteNonQuery();
                    con.Close();
                    getCurrentMaterials();
                    comboBox1.Text = "";
                    numericUpDown1.Value = 1;
                }
                catch (Exception ex)
                    { MessageBox.Show(ex.ToString(), "Ошибка"); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mid = Convert.ToInt32(materials.Rows[comboBox1.SelectedIndex][0]);
        }





        private void getAllMaterials(string query = "")
        {
            materials.Clear();
            using (var da = new NpgsqlDataAdapter("select * from \"Material\"" + query + " order by \"ID\" asc", con))
                da.Fill(materials);
        }

        private void getCurrentMaterials()
        {
            string cmd = $"select \"Material\".\"ID\", \"Material\".\"Title\"," +
                $"\"ProductMaterial\".\"Count\" from \"ProductMaterial\" inner join \"Material\" " +
                $"on \"Material\".\"ID\" = \"ProductMaterial\".\"MaterialID\" " +
                $"where \"ProductMaterial\".\"ProductID\" = {id}";
            using (var da = new NpgsqlDataAdapter(cmd, con))
            {
                actualMaterials.Clear();
                da.Fill(actualMaterials);
                dataGridView1.DataSource = actualMaterials;
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 285;
                dataGridView1.Columns[2].Width = 55;
            }
        }

        private void fillComboBox()
        {
            comboBox1.Items.Clear();
            for (int i = 0; i < materials.Rows.Count; i++)
                comboBox1.Items.Add($"{materials.Rows[i][1]}");
        }

        private bool notCBItem()
        {
            bool res = true;
            foreach (var item in comboBox1.Items)
                if (comboBox1.Text == item.ToString())
                {
                    res = false;
                    break;
                }
            return res;
        }

        private void timer_tick_mat(Object o, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                if (lastQuery != "")
                {
                    lastQuery = "";
                    mid = -1;
                    getAllMaterials();
                    fillComboBox();
                }
            }
            else
            {
                if (comboBox1.Text != lastQuery && notCBItem())
                {
                    lastQuery = comboBox1.Text;
                    getAllMaterials($" where lower(\"Title\") like lower(N'%{lastQuery}%')");
                    fillComboBox();
                    comboBox1.SelectionStart = comboBox1.Text.Length;
                }
            }
        }
    }
}
