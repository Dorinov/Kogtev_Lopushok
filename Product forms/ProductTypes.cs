using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class ProductTypes : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);
        Products pr;

        BindingSource bind = new BindingSource();
        NpgsqlDataAdapter da = new NpgsqlDataAdapter();

        public ProductTypes(Products p)
        {
            pr = p;
            InitializeComponent();
        }

        private void ProductTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bind;
            fillInfo();
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value);

            if (MessageBox.Show($"Вы уверены, что хотите удалить тип с ID {id}?", "Удаление типа", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                con.Open();
                var cmd = new NpgsqlCommand($"delete from \"ProductType\" where \"ID\" = {id};", con);
                cmd.ExecuteNonQuery();
                con.Close();

                fillInfo();

                replaceUnavailableTypes();

                pr.clearFilters();
                pr.fillDataTables();
                pr.fillComboBoxes();
            }
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update((DataTable)bind.DataSource);
                fillInfo();

                replaceUnavailableTypes();

                pr.clearFilters();
                pr.fillDataTables();
                pr.fillComboBoxes();
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
                da = new NpgsqlDataAdapter("select * from \"ProductType\" order by \"ID\" asc", con);
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

        private void replaceUnavailableTypes()
        {
            List<int> availableTypesIds = new List<int>();
            List<List<int>> productIdsAndTypesIds = new List<List<int>>();

            con.Open();
            var cmd = new NpgsqlCommand("select \"ID\" from \"ProductType\";", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                availableTypesIds.Add(reader.GetInt32(0));
            reader.Close();

            cmd = new NpgsqlCommand("select \"ID\", \"ProductTypeID\" from \"Product\";", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productIdsAndTypesIds.Add(new List<int> { reader.GetInt32(0), reader.GetInt32(1) });
            }
            reader.Close();

            string cmd_text = "";
            foreach (List<int> i in productIdsAndTypesIds)
            {
                var found = false;
                foreach (int h in availableTypesIds)
                    if (i[1] == h)
                    {
                        found = true;
                        break;
                    }

                if (!found)
                    cmd_text += $"update \"Product\" set \"ProductTypeID\" = {availableTypesIds[0]} where \"ID\" = {i[0]};";
            }
            
            if (cmd_text != "")
            {
                cmd = new NpgsqlCommand(cmd_text, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("При удалении или изменении типов возникла ситуация, " +
                    $"когда у продукта не оказалось доступного типа. Поэтому для таких продуктов произошла замена на тип с ID {availableTypesIds[0]}.", "Замена типов");
            }

            con.Close();
        }
    }
}
