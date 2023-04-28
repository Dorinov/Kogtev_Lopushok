using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Npgsql;

namespace Kogtev_Lopushok
{
    public partial class Products : Form
    {
        NpgsqlConnection con = new NpgsqlConnection(Authorization.con_string);
        Authorization auth;
        //int rules;

        bool loaded = false;

        Timer timer = new Timer();
        DataTable products = new DataTable();
        DataTable product_types = new DataTable();
        string[] sort_table = new string[] { "Стандарт", "А-я", "Я-а", "Цена ↑", "Цена ↓" };
        string search_text = "";
        string search_type = "";

        Label[] types;
        Label[] titles;
        Label[] articles;
        Label[] costs;
        Label[] descs;
        Panel[] panels;
        PictureBox[] pictures;
        List<int> currentIds = new List<int>();


        public Products(Authorization a, int r)
        {
            auth = a;
            rules = r;
            InitializeComponent();

            types = new Label[] { label_Type1, label_Type2, label_Type3, label_Type4, label_Type5, label_Type6 };
            titles = new Label[] { label_Title1, label_Title2, label_Title3, label_Title4, label_Title5, label_Title6 };
            articles = new Label[] { label_Article1, label_Article2, label_Article3, label_Article4, label_Article5, label_Article6 };
            costs = new Label[] { label_Cost1, label_Cost2, label_Cost3, label_Cost4, label_Cost5, label_Cost6 };
            descs = new Label[] { label_Desc1, label_Desc2, label_Desc3, label_Desc4, label_Desc5, label_Desc6 };
            panels = new Panel[] { panel1, panel2, panel3, panel4, panel5, panel6 };
            pictures = new PictureBox[] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6 };

            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            fillDataTables();
            fillComboBoxes();
            getMaxPage();
            fillPage(1);

            loaded = true;
        }

        private void Products_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void Products_FormClosed(object sender, FormClosedEventArgs e)
        {
            auth.Show();
        }

        private void button_PreviousPage_Click(object sender, EventArgs e)
        {
            numericUpDown1.DownButton();
        }

        private void button_NextPage_Click(object sender, EventArgs e)
        {
            numericUpDown1.UpButton();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            fillPage((int)numericUpDown1.Value);
        }

        private void comboBox_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
                applySearch();
        }

        private void comboBox_ProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (comboBox_ProductType.SelectedIndex == 0)
                    search_type = "";
                else
                    search_type = product_types.Rows[comboBox_ProductType.SelectedIndex - 1][0].ToString();
                applySearch();
            }
        }






        private void fillDataTables()
        {
            var da1 = new NpgsqlDataAdapter("select \"Product\".\"ID\", \"Product\".\"Title\"," +
                "\"ProductType\".\"Title\", \"ArticleNumber\", \"Description\", \"Image\"," +
                "\"Cost\" from \"Product\" inner join \"ProductType\" " +
                "on \"ProductType\".\"ID\" = \"Product\".\"ProductTypeID\" order by \"Product\".\"ID\";", con);
            da1.Fill(products);
            var da2 = new NpgsqlDataAdapter("select * from \"ProductType\" order by \"ID\";", con);
            da2.Fill(product_types);

            label_RowsCount.Text = $"Результаты: {products.Rows.Count}";
        }

        private void fillComboBoxes()
        {
            comboBox_ProductType.Items.Add("Все");

            foreach (string s in sort_table)
                comboBox_Sort.Items.Add(s);
            foreach (DataRow dr in product_types.Rows)
                comboBox_ProductType.Items.Add(dr[1]);

            comboBox_Sort.SelectedIndex = 0;
            comboBox_ProductType.SelectedIndex = 0;
        }

        private void getMaxPage()
        {
            int rows = products.Rows.Count;

            if (rows != 0)
            {
                while (rows % 6 != 0)
                    rows++;
                numericUpDown1.Maximum = rows / 6;
            }
            else
                numericUpDown1.Maximum = 1;
        }

        private void fillPage(int page)
        {
            currentIds.Clear();
            int startRow = (page - 1) * 6;
            for (int i = 0; i < 6; i++)
            {
                if ((startRow + i) <= (products.Rows.Count - 1))
                {
                    panels[i].Visible = true;
                    int r = startRow + i;

                    currentIds.Add((int)products.Rows[r][0]);

                    titles[i].Text = $"Наименование: {products.Rows[r][1]}";
                    types[i].Text = $"Тип: {products.Rows[r][2]}";
                    articles[i].Text = $"Артикул: {products.Rows[r][3]}";
                    descs[i].Text = $"Описание: {products.Rows[r][4]}";
                    costs[i].Text = $"Цена: {products.Rows[r][6]} ₽";

                    string productImage = products.Rows[r][5].ToString();
                    if (productImage != "")
                        try { pictures[i].Image = Image.FromFile(productImage); } catch { productImage = ""; }
                    if (productImage == "")
                        try { pictures[i].Image = Image.FromFile("images/non.png"); } catch { pictures[i].Image = null; }
                }
                else
                    panels[i].Visible = false;
            }
        }

        public void applySearch(bool afterEdit = false)
        {
            string[] sort_toQuery = new string[]
            {
            "order by \"Product\".\"ID\" asc",
            "order by \"Product\".\"Title\" asc",
            "order by \"Product\".\"Title\" desc",
            "order by \"Product\".\"Cost\" asc",
            "order by \"Product\".\"Cost\" desc"
            };

            bool have_where = false;
            string cmd = "select \"Product\".\"ID\", \"Product\".\"Title\"," +
                "\"ProductType\".\"Title\", \"ArticleNumber\", \"Description\", \"Image\"," +
                "\"Cost\" from \"Product\" inner join \"ProductType\" " +
                "on \"ProductType\".\"ID\" = \"Product\".\"ProductTypeID\" ";

            if (search_text != "")
            {
                have_where = true;
                cmd += $"where lower(\"Product\".\"Title\") like lower(N'%{search_text}%') ";
            }
            if (search_type != "" && search_type != "0")
                cmd += (have_where ? "and " : "where ") + $"\"Product\".\"ProductTypeID\" = {search_type} ";

            cmd += sort_toQuery[comboBox_Sort.SelectedIndex];

            products.Clear();
            var da = new NpgsqlDataAdapter(cmd, con);
            da.Fill(products);

            if (!afterEdit)
            {
                numericUpDown1.Value = 1;
                getMaxPage();
            }
            else
            {
                getMaxPage();
                fillPage((int)numericUpDown1.Value);
            }

            label_RowsCount.Text = $"Результаты: {products.Rows.Count}";
        }

        private void timer_tick(Object o, EventArgs e)
        {
            if (textBox_Search.Text != search_text && loaded)
            {
                search_text = textBox_Search.Text;
                applySearch();
            }
        }





        private void button_Edit1_Click(object sender, EventArgs e)
        {
            editProduct(1);
        }

        private void button_Edit2_Click(object sender, EventArgs e)
        {
            editProduct(2);
        }

        private void button_Edit3_Click(object sender, EventArgs e)
        {
            editProduct(3);
        }

        private void button_Edit4_Click(object sender, EventArgs e)
        {
            editProduct(4);
        }

        private void button_Edit5_Click(object sender, EventArgs e)
        {
            editProduct(5);
        }

        private void button_Edit6_Click(object sender, EventArgs e)
        {
            editProduct(6);
        }

        private void editProduct(int num)
        {
            EditProduct ep = new EditProduct(this, currentIds[num - 1]);
            ep.ShowDialog();
        }

        private void добавитьПродуктToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditProduct ep = new EditProduct(this);
            ep.ShowDialog();
        }
    }
}
