using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Курсач_2
{
    public partial class Form1 : Form
    {
        public string TrainText;

        public void FormatText(RichTextBox RTB)
        {
            int i;
            RTB.Text = RTB.Text.ToLower();
            for (i = 0; i < RTB.Text.Length; i++)
            {
                if (((Convert.ToInt32(RTB.Text[i]) < 1072) || (Convert.ToInt32(RTB.Text[i]) > 1103)) && (Convert.ToInt32(RTB.Text[i]) != 1105) && (RTB.Text[i] != ' '))
                {
                    RTB.Text = RTB.Text.Remove(i, 1);
                    i--;
                }
                if (Convert.ToInt32(RTB.Text[i]) == 1105)
                {
                    RTB.Text = RTB.Text.Remove(i, 1);
                    RTB.Text = RTB.Text.Insert(i, "е");
                }
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
            string fragment;
            FormatText(richTextBox2);
            fragment = ExtractWord(richTextBox2, 3);
            label4.Text = fragment;
        }
    }
}
