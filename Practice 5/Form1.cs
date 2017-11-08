using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Practice_5
{
    public partial class Form1 : Form
    {
        public class Film
        {

            private string name;
            private string director;
            private int year;
            private int box_office;
            private string picture;

            public Film(string Name, string Director, int Year, int BoxOffice, string pic)
            {
                name = Name;
                director = Director;
                year = Year;
                box_office = BoxOffice;
                picture = pic;
            }
            public Film()
            {
                name = "Hello";
                director = "World";
                year = 1;
                box_office = 1;
                picture = "";
            }

            [DisplayName("Название"), Category("Сводка")]
            public string NameFilm
            {
                get { return name; }
                set { name = value; }
            }
            [DisplayName("Режиссер"), Category("Сводка")]
            public string DirectorFilm
            {
                get { return director; }
                set { director = value; }
            }
            [DisplayName("Год"), Category("Сводка")]
            public int YearFilm
            {
                get { return year; }
                set { year = value; }
            }
            [DisplayName("Кассовые сборы $"), Category("Сводка")]
            public int Box_officeFilm
            {
                get { return box_office; }
                set { box_office = value; }
            }
            public string pic1
            {
                get { return picture; }
                set { picture = value; }
            }
        }
        BindingSource bs = new BindingSource();
        List<Film> bdFilms = new List<Film> {
         new Film("Star Wars: New Hope", "George Lucas", 1977, 775398007,
             //@"C:\Users\tosha\Documents\Visual Studio 2015\Projects\Practice 5\Practice 5\ImagesFilms\starwars4.jpg"),
             "starwars4.jpg"),
        new Film("Star Wars: The Empire Strikes Back", "George Lucas", 1980, 538375067,
            //@"C:\Users\tosha\Documents\Visual Studio 2015\Projects\Practice 5\Practice 5\ImagesFilms\starwars5jpg"),
             "starwars5.jpg"),
        new Film("Star Wars: Return of the Jedi", "George Lucas", 1983, 475106177,
           // @"C:\Users\tosha\Documents\Visual Studio 2015\Projects\Practice 5\Practice 5\ImagesFilms\starwars6.jpg")};
            "starwars6.jpg") };
        public Form1()
        {
            InitializeComponent();
            //  dataGridView1.DataSource = bdFilms;

            bs.DataSource = bdFilms;
            dataGridView1.DataSource = bs;
            bindingNavigator1.BindingSource = bs;
            chart1.DataSource = bs;
            propertyGrid1.DataBindings.Add("SelectedObject", bs, "");
            //pictureBox1.DataBindings.Add("Image", bs, "pic1", true);
            pictureBox1.DataBindings.Add("ImageLocation", bs, "pic1", true);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            chart1.Series[0].XValueMember = "NameFilm";
            chart1.Series[0].YValueMembers = "Box_officeFilm";
            chart1.Legends.Clear();
            chart1.Titles.Add("Кассовые сборы $");
            bs.CurrentChanged += (o, e) => chart1.DataBind();
            //splitContainer2.Orientation = Orientation.Horizontal;

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {

                toolStripComboBox1.Items.Add(dataGridView1.Columns[i].Name);
            }
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void загрузитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bdFilms.Clear();
            Stream sr = new FileStream("dbFilmsLoad.xml", FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Film>));
            bdFilms = (List<Film>)xmlSer.Deserialize(sr);
            sr.Close();
            bs.DataSource = bdFilms;
        }

        private void сохранитьДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream sr = new FileStream("dbFilmsSave.xml", FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<Film>));
            xmlSer.Serialize(sr, bdFilms);
            sr.Close();
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (toolStripTextBox1.Text == dataGridView1[toolStripComboBox1.SelectedItem.ToString(), i].Value.ToString())
                    {

                        //bs.Position = bs.Find(toolStripTextBox1.Text, dataGridView1[toolStripComboBox1.SelectedItem.ToString(), i].ToString());
                        // var nameCol = toolStripComboBox1.SelectedItem.ToString();
                        //var text = toolStripTextBox1.Text;
                        // bs.Position = bs.Find("FileName", "Star Wars: The Empire Strikes Back");
                        //bs.Position = bs.Find(toolStripComboBox1.SelectedItem.ToString(), toolStripTextBox1.Text);

                        bs.Position = i; //почему не работает bs.Find()
                        break;
                        //}
                        //dataGridView1.Columns[toolStripComboBox1.SelectedItem.ToString()].
                        //}
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Совпадений не найдено");
            }
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}    

    

