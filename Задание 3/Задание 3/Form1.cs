using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Задание_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false; //это запрет на добавление срок в таблицу
            openFileDialog1.Filter = "Text Files | * .txt";
            saveFileDialog1.Filter = "Text Files | * .txt";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string md = textBox2.Text;
            string c = textBox3.Text;
            string w = textBox4.Text;
            string t = textBox5.Text;
            dataGridView1.Rows.Add(name, md, c, w, t);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelLine_Click(object sender, EventArgs e)
        {
            //если строк нет, то выдаст ошибку, потому что не знает что улдалять
            //поэтому не стоит удалять строку, если в таблице строк нет
            int ind = dataGridView1.SelectedCells[0].RowIndex;
            dataGridView1.Rows.Remove(dataGridView1.Rows[ind]);
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Stream mystr = null;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((mystr = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter mywr = new StreamWriter(mystr);
                   

                    try
                    {

                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                mywr.Write(dataGridView1.Rows[i].Cells[j].Value.ToString() + ("@"));
                            }
                            mywr.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mywr.Close();
                    }

                    
                }
            }

            

        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            Stream mystr = null;
            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((mystr = openFileDialog1.OpenFile()) != null)
                {
                    
                    StreamReader myread = new StreamReader(mystr);
                    string[] str;
                    int num = 0;

                    try
                    {
                        string[] str1 = myread.ReadToEnd().Split('\n');
                        num = str1.Count();


                        dataGridView1.RowCount = num;

                        for (int i = 0; i < num; i++)
                        {

                            str = str1[i].Split('@');



                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                try
                                {

                                    dataGridView1.Rows[i].Cells[j].Value = str[j];
                                }

                                catch { }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message); //если не получится щагрузить файл, появится ошибка
                    }
                    finally
                    {
                        myread.Close(); //закрываем объект Reader
                    }

                    
                }

            }

           //после загрузки из файла, появляется пустая строка, ее лучше удалить, 
           //потому что при добавлении новой строки, новая строка перескочет через нее!!!

        }

        private void btnReq_Click(object sender, EventArgs e)
        {
            //нахождение минимального значения из 5 столбца
            //индексы столбцов начинаются с 0

            dataGridView1.AllowUserToAddRows = false;

            int[] columnData = (from DataGridViewRow row in dataGridView1.Rows
                                where row.Cells[2].FormattedValue.ToString() != string.Empty
                                select Convert.ToInt32(row.Cells[4].FormattedValue)).ToArray();

            textBox6.Text = columnData.Min().ToString();

        }
    }
}
