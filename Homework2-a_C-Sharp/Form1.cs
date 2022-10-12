using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;


namespace Homework2_a_C_Sharp
{
    public partial class Form1 : Form
    {
        private string fileName = "";
        List<List<string>> tables = new List<List<string>>();
        List<string> headers = new List<string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (!(this.fileName == ""))
            {
                using (TextFieldParser parser = new TextFieldParser(@fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int i = 0;
                    //int j = 0;
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (headers.Count == 0)
                        {
                            foreach (string field in fields)
                            {
                                this.tables.Add(new List<string>());
                                this.tables[i].Add(field);
                                this.headers.Add(field);
                                i++;
                            }
                        }
                        else
                        {
                            foreach (string field in fields)
                            {
                                this.tables.Add(new List<string>());
                                this.tables[i].Add(field);
                                i++;
                            }
                        }
                        i = 0;
                        this.richTextBox1.AppendText("riuscito\n");
                    }
                    this.listBox1.DataSource = headers;
                }
            }
        }

        private List<string> allLower (List<string> temp)
        {
            List<string> list = new List<string>();
            foreach(string field in temp)
            {
                list.Add(field.ToLower());
            }
            return list;
        }

        private void compute_row(List<string> tempList)
        {
            List <string> currentRow = allLower(tempList);
            currentRow.RemoveAt(0);
            List<List<string>> rows = new List<List<string>>();
            foreach (string row in currentRow)
            {
                if (!(rows.Exists(e => e.Contains(row))))
                {
                    rows.Add(currentRow.FindAll(s => s.Equals(row)));
                }
            }


            foreach (List<string> elem in rows)
            {
                if (elem.First() != "")
                {
                    this.richTextBox2.AppendText("\"" + elem.First() + "\" occurs: " + elem.Count() + " time\n");
                }
                else if (elem.Count() > 1)
                {
                    this.richTextBox2.AppendText("There are " + elem.Count() + " blank values.\n");
                }
                else
                {
                    this.richTextBox2.AppendText("There is " + elem.Count() + " blank value.\n");
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = this.listBox1.SelectedIndex;
            List<string> elem_list = this.tables.ElementAt(i);
            this.richTextBox1.Text = "";
            this.richTextBox2.Text = "";
            foreach (string field in elem_list)
            {
                this.richTextBox1.AppendText(field + " \n");
            }
            compute_row(elem_list);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();
            this.fileName = this.openFileDialog1.FileName;
        }
    }
}