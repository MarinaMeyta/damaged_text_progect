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
        public struct Variants
        {
            public int id;
            public string variant;
        }

        public string TrainText;
        public int[,] FMatrix = new int[32, 32];
        public Variants[] vars = new Variants[1];
        public string DefaultPath = @"D:\\DICT\";

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

        public void FormatRepText(RichTextBox RTB)
        {
            int i;
            RTB.Text = RTB.Text.ToLower();
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if (((Convert.ToInt32(RTB.Text[i]) < 1072) || (Convert.ToInt32(RTB.Text[i]) > 1103)) && (Convert.ToInt32(RTB.Text[i]) != 1105) && (RTB.Text[i] != ' ') && (RTB.Text[i] != '*'))
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

        public void InsertWord(RichTextBox RTB, int wordnum, string word)
        {
            int i = 0, lp = 0, count = 1;
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if ((RTB.Text[i] == ' ') && (count == wordnum))
                {
                    RTB.Text = RTB.Text.Remove(lp, i - lp);
                    RTB.Text = RTB.Text.Insert(lp, word);
                    break;
                }
                else if ((i == RTB.Text.Length - 1) && (count == wordnum))
                {
                    RTB.Text = RTB.Text.Remove(lp, i - lp + 1);
                    RTB.Text = RTB.Text.Insert(lp, word);
                    break;
                }
                else if ((RTB.Text[i] == ' ') && (count < wordnum))
                {
                    count++;
                    if (i + 1 < RTB.Text.Length)
                        lp = i + 1;
                }
            }
        }    //Вставка слова в richtextbox

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

        public void ReadFMat()
        {
            int i, j;
            StreamReader sr;
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
        }

        public void AddToFMat(string text)
        {
            int i;
            //ReadFMat();
            for (i = 0; i < text.Length - 1; i++)
            {
                FMatrix[Convert.ToInt32(text[i]) - 1072, Convert.ToInt32(text[i + 1]) - 1072]++;
            }
        }

        public void SaveFMat()
        {
            int i, j;
            StreamWriter sw;
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

        public bool IsInDict(string text, string path)
        {
            StreamReader sr;
            bool found = false;
            sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                if (sr.ReadLine() == text)
                {
                    found = true;
                    break;
                }
            }
            sr.Close();
            if (found == false)
                return false;
            else
                return true;
        }

        public void AddToDict(string text)
        {
            StreamWriter sw;
            FileStream fs;
            string[] catalogs = { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "э", "ю", "я" };
            string path = @"D:\\DICT\";

            for (int i = 0; i < catalogs.Length; ++i)
            {
                DirectoryInfo dir = new DirectoryInfo(path + catalogs[i]);
                dir.Create();
            }

            try
            {
                path = path + Convert.ToString(text[0]) + @"\" + Convert.ToString(text.Length) + ".dat";
                if (File.Exists(path) == false)
                {
                    fs = File.Create(path);
                    fs.Close();
                    fs.Dispose();
                }
                if (IsInDict(text, path) == false)
                {
                    sw = File.AppendText(path);
                    sw.WriteLine(text);
                    sw.Close();
                    sw.Dispose();
                }
                //else
                //MessageBox.Show("Такое слово уже есть!");
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
            ReadFMat();
        }

        private void Learn_Click(object sender, EventArgs e)
        {
            int k = 1;
            string source = " ";
            if (richTextBox2.Text == "")
                MessageBox.Show("Недопустимое значение в текстовом поле!");
            else if (richTextBox2.Text.Length > 2500)
                MessageBox.Show("Слишком длинный текст для обработки!");
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

        private void ToFmat_Click(object sender, EventArgs e)
        {
            string source;
            FormatText(textBox1);
            source = ExtractWord(textBox1, 1);
            AddToFMat(source);
        }

        private void Repare_Click(object sender, EventArgs e)
        {
            bool edited = false;
            int i, j = 1, k, l;
            int count = 0;
            int a0, a1;
            string sub = " ";
            //ReadFMat();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            FormatRepText(richTextBox1);
            while (sub != "")
            {
                sub = ExtractWord(richTextBox1, j);
                for (i = 0; i < sub.Length; i++)
                {
                    int[] m0 = new int[3];
                    int[] m1 = new int[3];
                    int max;
                    if ((i == 0) && (sub[i] == '*'))                            //Если * в самом начале строки
                    {
                        try
                        {
                            edited = true;
                            a1 = Convert.ToInt32(sub[i + 1]) - 1072;
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if (FMatrix[k, a1] > max)
                                {
                                    max = FMatrix[k, a1];
                                    m1[0] = k;
                                }
                            }
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[k, a1] > max) && (k != m1[0]))
                                {
                                    max = FMatrix[k, a1];
                                    m1[1] = k;
                                }
                            }
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[k, a1] > max) && (k != m1[0]) && (k != m1[1]))
                                {
                                    max = FMatrix[k, a1];
                                    m1[2] = k;
                                }
                            }
                            Array.Resize<Variants>(ref vars, 3);
                            for (k = 0; k < 3; k++)
                            {
                                vars[k].variant = sub;
                                vars[k].variant = vars[k].variant.Remove(i, 1);
                                vars[k].variant = vars[k].variant.Insert(i, Convert.ToString(Convert.ToChar(m1[k] + 1072)));
                                vars[k].id = j;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    if ((i == sub.Length - 1) && (sub[i] == '*'))               //Если * в конце строки
                    {
                        try
                        {
                            edited = true;
                            a0 = Convert.ToInt32(sub[i - 1]) - 1072;
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if (FMatrix[a0, k] > max)
                                {
                                    max = FMatrix[a0, k];
                                    m0[0] = k;
                                }
                            }
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[a0, k] > max) && (k != m0[0]))
                                {
                                    max = FMatrix[a0, k];
                                    m0[1] = k;
                                }
                            }
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[a0, k] > max) && (k != m0[0]) && (k != m0[1]))
                                {
                                    max = FMatrix[a0, k];
                                    m0[2] = k;
                                }
                            }
                            Array.Resize<Variants>(ref vars, 3);
                            for (k = 0; k < 3; k++)
                            {
                                vars[k].variant = sub;
                                vars[k].variant = vars[k].variant.Remove(i, 1);
                                vars[k].variant = vars[k].variant.Insert(i, Convert.ToString(Convert.ToChar(m0[k] + 1072)));
                                vars[k].id = j;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    if ((i > 0) && (i < sub.Length - 1) && (sub[i] == '*'))     //Если * где-то вдали от краев
                    {
                        try
                        {
                            edited = true;
                            a0 = Convert.ToInt32(sub[i - 1]) - 1072;
                            a1 = Convert.ToInt32(sub[i + 1]) - 1072;
                            //Поиск самых частых элементов в строке
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if (FMatrix[a0, k] > max)
                                {
                                    max = FMatrix[a0, k];
                                    m0[0] = k;
                                }
                            }
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[a0, k] > max) && (k != m0[0]))
                                {
                                    max = FMatrix[a0, k];
                                    m0[1] = k;
                                }
                            }
                            max = FMatrix[a0, 0];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[a0, k] > max) && (k != m0[0]) && (k != m0[1]))
                                {
                                    max = FMatrix[a0, k];
                                    m0[2] = k;
                                }
                            }
                            //Конец поиска
                            //Поиск по столбцу
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if (FMatrix[k, a1] > max)
                                {
                                    max = FMatrix[k, a1];
                                    m1[0] = k;
                                }
                            }
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[k, a1] > max) && (k != m1[0]))
                                {
                                    max = FMatrix[k, a1];
                                    m1[1] = k;
                                }
                            }
                            max = FMatrix[0, a1];
                            for (k = 1; k < 32; k++)
                            {
                                if ((FMatrix[k, a1] > max) && (k != m1[0]) && (k != m1[1]))
                                {
                                    max = FMatrix[k, a1];
                                    m1[2] = k;
                                }
                            }
                            //Конец поисков
                            for (k = 0; k < 3; k++)
                            {
                                for (l = 0; l < 3; l++)
                                {
                                    if (m0[k] == m1[l])
                                    {
                                        if (vars.Length < count + 1)
                                            Array.Resize(ref vars, vars.Length + 1);
                                        vars[count].variant = sub;
                                        vars[count].variant = vars[count].variant.Remove(i, 1);
                                        vars[count].variant = vars[count].variant.Insert(i, Convert.ToString(Convert.ToChar(m0[k] + 1072)));
                                        vars[count].id = j;
                                        count++;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }

                //Пытался сделать замену найденным из словаря словом, но что-то не прокнуло

                //bool found = false;
                //for (i = 0; i < vars.Length; i++)
                //{
                    //if (vars[i].id == j)
                    //{
                        //if (File.Exists(DefaultPath + vars[i].variant[0] + @"\" + Convert.ToString(vars[i].variant.Length)))
                        //{
                            //if (IsInDict(vars[i].variant, DefaultPath + vars[i].variant[0] + @"\" + Convert.ToString(vars[i].variant.Length)))
                            //{
                                //found = true;
                                //InsertWord(richTextBox1, j, vars[i].variant);
                                //break;
                            //}
                        //}
                    //}
                //}

                if ((sub != "")&&(edited == true))
                    comboBox1.Items.Add(Convert.ToString(j));
                j++;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFMat();
        }

        private void SaveMatrix_Click(object sender, EventArgs e)
        {
            SaveFMat();
        }

        private void ToDict_Click(object sender, EventArgs e)
        {
            string source;
            FormatText(textBox1);
            source = ExtractWord(textBox1, 1);
            AddToDict(source);
        }

        private void DropFmat_Click(object sender, EventArgs e)
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

        private void DropDict_Click(object sender, EventArgs e)
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

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int id, i;
            id = Convert.ToInt32(comboBox1.SelectedItem);
            for (i = 0; i < vars.Length; i++)
            {
                if (vars[i].id == id)
                    comboBox2.Items.Add(vars[i].variant);
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
        }
    }
}