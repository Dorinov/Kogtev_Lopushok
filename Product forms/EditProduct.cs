using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class EditProduct : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);
        MoreInfo mi;
        Products pr;
        int id;

        List<int> storageIds = new List<int>();
        List<int> newStorageIds = new List<int>();
        List<string> storageNames = new List<string>();
        List<string> storageAddress = new List<string>();
        List<int> counts = new List<int>();
        int storageLastIndex = 0;

        DataTable types = new DataTable();
        string productImage = null;

        public EditProduct(MoreInfo m, Products p, int i = 0)
        {
            mi = m;
            pr = p;
            id = i;
            InitializeComponent();
        }

        private void EditProduct_Load(object sender, EventArgs e)
        {
            fillStorageInfo();
            fillTypes();
            fillForm();
        }

        private void button_PicDel_Click(object sender, EventArgs e)
        {
            productImage = null;
            try { pictureBox1.Image = Image.FromFile("images/non.png"); } catch { pictureBox1.Image = null; }
        }

        private void button_PicChange_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                productImage = openFileDialog1.FileName;
                try { pictureBox1.Image = Image.FromFile(productImage); } catch { pictureBox1.Image = null; }
            }
        }

        private void button_ProductDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить данный продукт?", "Удаление", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
                deleteProd();
        }

        private void button_ProductApply_Click(object sender, EventArgs e)
        {
            saveChanges();
        }

        private void button_ChangeMaterials_Click(object sender, EventArgs e)
        {
            ProductMaterials pm = new ProductMaterials(id);
            pm.ShowDialog();
        }

        private void comboBox_Storage_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_StorageAddress.Text = $"Адрес:\n{storageAddress[comboBox_Storage.SelectedIndex]}";
            numericUpDown1.Value = counts[comboBox_Storage.SelectedIndex];
            storageLastIndex = comboBox_Storage.SelectedIndex;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            try { counts[storageLastIndex] = (int)numericUpDown1.Value; } catch { }
        }





        private void fillStorageInfo()
        {
            con.Open();
            var cmd = new NpgsqlCommand("select * from \"Storage\" order by \"ID\";", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                storageIds.Add(reader.GetInt32(0));
                storageNames.Add(reader.GetString(1));
                comboBox_Storage.Items.Add(reader.GetString(1));
                storageAddress.Add(reader.GetString(2));
            }
            reader.Close();

            if (id != 0)
            {
                cmd = new NpgsqlCommand($"select \"Count\" from \"StorageAvailability\" where \"ProductID\" = {id} order by \"StorageID\";", con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    counts.Add(reader.GetInt32(0));
                reader.Close();

                int index = counts.Count;
                while (counts.Count != storageIds.Count)
                {
                    newStorageIds.Add(storageIds[index]);
                    counts.Add(0);
                    index++;
                }
            }
            else
                for (int i = 0; i < storageIds.Count; i++)
                    counts.Add(0);
            con.Close();

            comboBox_Storage.SelectedIndex = 0;
        }

        private void fillTypes()
        {
            var da = new NpgsqlDataAdapter("select * from \"ProductType\" order by \"ID\" asc;", con);
            da.Fill(types);
            foreach (DataRow dr in types.Rows)
                comboBox_Type.Items.Add(dr[1]);
        }

        private void fillForm()
        {
            try { pictureBox1.Image = Image.FromFile("images/non.png"); } catch { pictureBox1.Image = null; }

            if (id != 0)
            {
                con.Open();
                var cmd = new NpgsqlCommand($"select * from \"Product\" where \"ID\" = {id};", con);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox_ID.Text = reader.GetInt32(0).ToString();
                    textBox_Name.Text = reader.GetString(1);
                    int type = reader.GetInt32(2);
                    textBox_Article.Text = reader.GetString(3);
                    try { richTextBox_Desc.Text = reader.GetString(4); } catch { }
                    textBox_Cost.Text = reader.GetDouble(6).ToString();

                    int i = 0;
                    foreach (DataRow dr in types.Rows)
                    {
                        if ((int)dr[0] == type)
                        {
                            comboBox_Type.SelectedIndex = i;
                            break;
                        }
                        i++;
                    }

                    try { productImage = reader.GetString(5); } catch { }
                    if (productImage != "")
                        try { pictureBox1.Image = Image.FromFile(productImage); } catch { productImage = null; }
                }
                con.Close();
            }
            else
            {
                textBox_Article.Text = getNewArticle();
                button_ChangeMaterials.Enabled = false;
                button_ProductDel.Enabled = false;
                comboBox_Storage.Enabled = false;
                numericUpDown1.Enabled = false;
            }
        }

        private void saveChanges()
        {
            string cmd_text = "";

            try
            {
                changeImagePath();
                var price = textBox_Cost.Text.Replace(",", ".");

                if (id == 0)
                {
                    cmd_text = "INSERT INTO \"Product\"(\"Title\", \"ProductTypeID\", \"ArticleNumber\", \"Description\", \"Image\", \"Cost\") " +
                        $"VALUES ('{textBox_Name.Text}', {types.Rows[comboBox_Type.SelectedIndex][0]}, '{textBox_Article.Text}'," +
                        $"'{richTextBox_Desc.Text}', '{productImage}', {price});";
                }
                else
                {
                    cmd_text = $"UPDATE \"Product\" SET \"Title\" = '{textBox_Name.Text}'," +
                        $"\"ProductTypeID\" = {types.Rows[comboBox_Type.SelectedIndex][0]}," +
                        $"\"Description\" = '{richTextBox_Desc.Text}'," +
                        $"\"Image\" = '{productImage}'," +
                        $"\"Cost\" = {price} " +
                        $"WHERE \"ID\" = {id};";
                }

                con.Open();
                var cmd = new NpgsqlCommand(cmd_text, con);
                cmd.ExecuteNonQuery();
                con.Close();

                saveStorageInfo(textBox_Article.Text);

                MessageBox.Show("Данные успешно сохранены", "Редактор");

                if (mi != null)
                    mi.closeForm();
                pr.applySearch(true);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }

        private void saveStorageInfo(string article)
        {
            string cmd_text = "";
            int c = 0;
            if (id == 0)
            {
                int _id = getProductIdByArticle(article);
                foreach (int sid in storageIds)
                {
                    cmd_text += $"insert into \"StorageAvailability\" values({sid}, {_id}, {counts[c]});\n";
                    c++;
                }
            }
            else
            {
                foreach (int sid in storageIds)
                {
                    if (isNewStorage(sid))
                        cmd_text += $"insert into \"StorageAvailability\" values({sid}, {id}, {counts[c]});\n";
                    else
                        cmd_text += $"update \"StorageAvailability\" set \"Count\" = {counts[c]} " +
                            $"where \"StorageID\" = {sid} and \"ProductID\" = {id};\n";
                    c++;
                }
            }

            con.Open();
            var cmd = new NpgsqlCommand(cmd_text, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private bool isNewStorage(int tid)
        {
            foreach (int i in newStorageIds)
                if (i == tid)
                    return true;
            return false;
        }

        private int getProductIdByArticle(string article)
        {
            int res = 0;
            con.Open();
            var cmd = new NpgsqlCommand($"select \"ID\" from \"Product\" where \"ArticleNumber\" = '{article}';", con);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                res = reader.GetInt32(0);
            con.Close();
            return res;
        }

        private void changeImagePath()
        {
            int i = 0;
            if (productImage != null && productImage != "")
                if (!productImage.Contains("images"))
                    while (true)
                    {
                        if (!File.Exists($"images/image_{i}.jpeg"))
                        {
                            pictureBox1.Image.Save($"images/image_{i}.jpeg");
                            productImage = $"images/image_{i}.jpeg";
                            break;
                        }
                        i++;
                    }
        }

        private void deleteProd()
        {
            con.Open();
            try
            {
                var cmd = new NpgsqlCommand($"delete from \"Product\" where \"ID\" = {id};" +
                    $"delete from \"ProductMaterial\" where \"ProductID\" = {id};" +
                    $"delete from \"StorageAvailability\" where \"ProductID\" = {id};", con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Продукт успешно удален", "Удаление");

                if (mi != null)
                    mi.closeForm();
                pr.applySearch(true);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка удаления");
            }
            con.Close();
        }

        private string getNewArticle()
        {
            string res = "";
            var values = getAllArticles();

            Random r = new Random();
            string alf = "0123456789";

            do
            {
                res = "";
                for (int i = 0; i < 6; i++)
                {
                    res += res == "" ? alf[r.Next(1, alf.Length - 1)] : alf[r.Next(0, alf.Length - 1)];
                }
            }
            while (haveArticle(values, res));

            return res;
        }

        private bool haveArticle(List<string> a, string b)
        {
            foreach (string s in a)
                if (s == b)
                    return true;
            return false;
        }

        private List<string> getAllArticles()
        {
            var res = new List<string>();
            con.Open();
            using (var cmd = new NpgsqlCommand("select \"ArticleNumber\" from \"Product\"", con))
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                    res.Add(reader.GetString(0));
            }
            con.Close();
            return res;
        }
    }
}
