using Prjoect123.classes;
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

namespace Prjoect123
{
    public partial class MainForm : Form
    {
        DataTable dt = new DataTable();
        public MainForm()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private List<TypeOfAnimals> LoadTypes()
        {
            List<TypeOfAnimals> types = new List<TypeOfAnimals>();

            string[] lines = File.ReadAllLines("..\\..\\files\\Типы животных.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 2)
                {
                    types.Add(new TypeOfAnimals { TypeNumber = int.Parse(parts[0]), TypeName = parts[1] });
                }
            }


            return types;
        }

        private string GetTypeName(int typeNumber)
        {
            List<TypeOfAnimals> types = LoadTypes();
            switch (typeNumber)
            {
                case 0:
                    return types[0].TypeName;

                case 1:
                    return types[1].TypeName;

                case 2:
                    return types[2].TypeName;
                    //break;
            }
            return types[0].TypeName;
        }

        private string GetPicName(int picNumber)
        {
            switch (picNumber)
            {
                case 0:
                    return "..\\..\\Images\\0.jpg";

                case 1:
                    return "..\\..\\Images\\1.jpg";

                case 2:
                    return "..\\..\\Images\\2.jpg";

                case 3:
                    return "..\\..\\Images\\3.jpg";
            }
            return "..\\..\\Images\\0.jpg";
        }

        private void LoadAnimals()
        {
            animalsGridView.Rows.Clear();

            string[] lines = File.ReadAllLines("..\\..\\files\\Типы животных.txt");
            string[] values;

            int nextId = 1;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                //int c;
                values[0] = nextId.ToString();
                //c = int.Parse(values[1]
                values[1] = values[3];
                values[2] = GetTypeName(int.Parse(values[1]));
                values[3] = GetPicName(int.Parse(values[values.Length - 4]));
                

                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }

                dt.Rows.Add(row);

                nextId++;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dt.Columns.Add("id", typeof(decimal));
            dt.Columns.Add("Имя", typeof(string));
            dt.Columns.Add("Тип", typeof(string));
            dt.Columns.Add("Изображение", typeof(string));

            dt.Columns["id"].AutoIncrement = true;
            dt.Columns["id"].AutoIncrementSeed = 1; // начальное значение
            dt.Columns["id"].AutoIncrementStep = 1;

            LoadAnimals();

            animalsGridView.DataSource = dt;
        }
    }
}
