using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class MoreInfo : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);
        Products pr;
        int id;

        List<string> storageNames = new List<string>();
        List<string> storageAddress = new List<string>();
        List<int> counts = new List<int>();

        public MoreInfo(Products p, int i)
        {
            pr = p;
            id = i;
            InitializeComponent();

            ActiveControl = panel1;
        }

        private void MoreInfo_Load(object sender, EventArgs e)
        {
            getInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_StorageAddress.Text = $"Адрес:\n{storageAddress[comboBox1.SelectedIndex]}";
            label_Count.Text = $"В наличии: {counts[comboBox1.SelectedIndex]}";
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            EditProduct ep = new EditProduct(this, pr, id);
            ep.ShowDialog();
        }





        private void getInfo()
        {
            con.Open();
            var cmd = new NpgsqlCommand($"select \"Product\".\"ID\", \"Product\".\"Title\"," +
                $"\"ProductType\".\"Title\", \"ArticleNumber\", \"Description\"," +
                $"\"Image\", \"Cost\" from \"Product\" inner join \"ProductType\" on " +
                $"\"ProductType\".\"ID\" = \"Product\".\"ProductTypeID\" where \"Product\".\"ID\" = {id};", con);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                label_Title.Text = $"Наименование: {reader.GetString(1)}";
                label_Type.Text = $"Тип: {reader.GetString(2)}";
                label_Article.Text = $"Артикул: {reader.GetString(3)}";
                var desc = "";
                try { desc = reader.GetString(4); } catch { }
                label_Desc.Text = $"Описание: {desc}";
                label_Cost.Text = $"Цена: {reader.GetDouble(6)} ₽";

                var productImage = "";
                try { productImage = reader.GetString(5); } catch { }
                if (productImage != "")
                    try { pictureBox1.Image = Image.FromFile(productImage); } catch { productImage = ""; }
                if (productImage == "")
                    try { pictureBox1.Image = Image.FromFile("images/non.png"); } catch { pictureBox1.Image = null; }
            }
            reader.Close();

            cmd = new NpgsqlCommand($"select \"Material\".\"Title\"," +
                $"\"ProductMaterial\".\"Count\" from \"ProductMaterial\" inner join \"Material\" " +
                $"on \"Material\".\"ID\" = \"ProductMaterial\".\"MaterialID\" " +
                $"where \"ProductMaterial\".\"ProductID\" = {id};", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
                label_Materials.Text += $"{reader.GetString(0)} — {reader.GetInt32(1)} шт.\n";
            reader.Close();

            cmd = new NpgsqlCommand("select * from \"Storage\" order by \"ID\";", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                storageNames.Add(reader.GetString(1));
                comboBox1.Items.Add(reader.GetString(1));
                storageAddress.Add(reader.GetString(2));
            }
            reader.Close();

            cmd = new NpgsqlCommand($"select \"Count\" from \"StorageAvailability\" where \"ProductID\" = {id} order by \"StorageID\";", con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
                counts.Add(reader.GetInt32(0));
            reader.Close();
            con.Close();

            while (counts.Count != storageNames.Count)
                counts.Add(0);

            comboBox1.SelectedIndex = 0;
        }

        public void closeForm()
        {
            Close();
        }
    }
}
