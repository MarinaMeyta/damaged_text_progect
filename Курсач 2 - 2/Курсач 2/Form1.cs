using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Курсач_2
{
    public partial class Form1 : Form
    {
        public string TrainText;
        public int[,] FMatrix = new int[32, 32];

        public void FormatText(RichTextBox RTB)
        {
            int i;
            RTB.Text = RTB.Text.ToLower();
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if (((Convert.ToInt32(RTB.Text[i]) < 1072) || (Convert.ToInt32(RTB.Text[i]) > 1103)) && (Convert.ToInt32(RTB.Text[i]) != 1105) && (RTB.Text[i] != ' '))
                {
                    RTB.Text = RTB.Text.Remove(i, 1);
                    RTB.Text = RTB.Text.Insert(i, " ");
                }
                if (Convert.ToInt32(RTB.Text[i]) == 1105)
                {
                    RTB.Text = RTB.Text.Remove(i, 1);
                    RTB.Text = RTB.Text.Insert(i, "е");
                }
            }
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if (i + 1 < RTB.Text.Length)
                {
                    if ((RTB.Text[i] == ' ') && (RTB.Text[i + 1] == ' '))
                    {
                        RTB.Text = RTB.Text.Remove(i, 1);
                        i--;
                    }
                }
            }
            if (RTB.Text[0] == ' ')
                RTB.Text = RTB.Text.Remove(0, 1);
            if (RTB.Text.Length - 1 >= 0)
            {
                if (RTB.Text[RTB.Text.Length - 1] == ' ')
                    RTB.Text = RTB.Text.Remove(RTB.Text.Length - 1, 1);
            }
        }

        public void FormatText(TextBox TB)
        {
            int i;
            TB.Text = TB.Text.ToLower();
            for (i = 0; i < TB.Text.Length; i++)
            {
                if (((Convert.ToInt32(TB.Text[i]) < 1072) || (Convert.ToInt32(TB.Text[i]) > 1103)) && (Convert.ToInt32(TB.Text[i]) != 1105) && (TB.Text[i] != ' '))
                {
                    TB.Text = TB.Text.Remove(i, 1);
                    TB.Text = TB.Text.Insert(i, " ");
                }
                if (Convert.ToInt32(TB.Text[i]) == 1105)
                {
                    TB.Text = TB.Text.Remove(i, 1);
                    TB.Text = TB.Text.Insert(i, "е");
                }
            }
            for (i = 0; i < TB.Text.Length; i++)
            {
                if (i + 1 < TB.Text.Length)
                {
                    if ((TB.Text[i] == ' ') && (TB.Text[i + 1] == ' '))
                    {
                        TB.Text = TB.Text.Remove(i, 1);
                        i--;
                    }
                }
            }
            if (TB.Text[0] == ' ')
                TB.Text = TB.Text.Remove(0, 1);
            if (TB.Text[TB.Text.Length - 1] == ' ')
                TB.Text = TB.Text.Remove(TB.Text.Length - 1, 1);
        }

        public string ExtractWord(RichTextBox RTB, int wordnum)
        {
            int i = 0, lp = 0, count = 1;
            string dest = "";
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if ((RTB.Text[i] == ' ') && (count == wordnum))
                {
                    dest = RTB.Text.Substring(lp, i - lp) ;
                    break;
                }
                else if ((i == RTB.Text.Length - 1) && (count == wordnum))
                {
                    dest = RTB.Text.Substring(lp, i - lp + 1);
                    break;
                }
                else if ((RTB.Text[i] == ' ') && (count < wordnum))
                {
                    count++;
                    if (i + 1 < RTB.Text.Length)
                        lp = i + 1;
                }
            }
            return dest;
        }

        public string ExtractWord(TextBox TB, int wordnum)
        {
            int i = 0, lp = 0, count = 1;
            string dest = "";
            for (i = 0; i < TB.Text.Length; i++)
            {
                if ((TB.Text[i] == ' ') && (count == wordnum))
                {
                    dest = TB.Text.Substring(lp, i - lp);
                    break;
                }
                else if ((i == TB.Text.Length - 1) && (count == wordnum))
                {
                    dest = TB.Text.Substring(lp, i - lp + 1);
                    break;
                }
                else if ((TB.Text[i] == ' ') && (count < wordnum))
                {
                    count++;
                    if (i + 1 < TB.Text.Length)
                        lp = i + 1;
                }
            }
            return dest;
        }

        public void AddToFMat(string text)
        {
            int i, j;
            StreamReader sr; StreamWriter sw;
            if (File.Exists(@"D:\\FMAT.dat") == false)
                File.Create(@"D:\\FMAT.dat");
            sr = new StreamReader(@"D:\\FMAT.dat");
            for (i = 0; i < 32; i++)
            {
                for (j = 0; j < 32; j++)
                {
                    FMatrix[i, j] = Convert.ToInt32(sr.ReadLine());
                }
            }
            sr.Close();
            for (i = 0; i < text.Length - 1; i++)
            {
                FMatrix[Convert.ToInt32(text[i]) - 1072, Convert.ToInt32(text[i + 1]) - 1072]++;
            }
            sw = new StreamWriter(@"D:\\FMAT.dat");
            for (i = 0; i < 32; i++)
            {
                for (j = 0; j < 32; j++)
                {
                    sw.WriteLine(Convert.ToString(FMatrix[i, j]));
                }
            }
            sw.Close();
        }

        public void AddToDict(string text)
        {
            StreamWriter sw;
            FileStream fs;
            string path;
            try
            {
                path = @"D:\\DICT\" + Convert.ToString(text[0]) + @"\" + Convert.ToString(text.Length) + ".dat";
                if (File.Exists(path) == false)
                {
                    fs = File.Create(path);
                    fs.Close();
                }
                sw = File.AppendText(path);
                sw.WriteLine(text);
                sw.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка: " + err.Message);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox1.Width = this.Width / 2;
            //richTextBox2.Width = this.Width / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int k = 1;
            string source = " ";
            if (richTextBox2.Text == "")
                MessageBox.Show("Недопустимое значение в текстовом поле!");
            else
            {
                FormatText(richTextBox2);
                source = ExtractWord(richTextBox2, k);
                while (source != "")
                {
                    k++;
                    AddToFMat(source);
                    AddToDict(source);
                    source = ExtractWord(richTextBox2, k);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string source;
            FormatText(textBox1);
            source = ExtractWord(textBox1, 1);
            AddToFMat(source);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i;
            try
            {
                StreamWriter sw = new StreamWriter(@"D:\\FMAT.dat");
                for (i = 0; i < 1024; i++)
                {
                    sw.WriteLine("0");
                }
                sw.Dispose();
                sw.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка: " + err.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string source;
            FormatText(textBox1);
            source = ExtractWord(textBox1, 1);
            AddToDict(source);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int i;
            try
            {
                for (i = 0; i < 32; i++)
                {
                    Directory.Delete(@"D:\\DICT\" + Convert.ToString(Convert.ToChar(i + 1072)), true);
                    Directory.CreateDirectory(@"D:\\DICT\" + Convert.ToString(Convert.ToChar(i + 1072)));
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Ошибка: " + err.Message);
            }
        }
    }
}