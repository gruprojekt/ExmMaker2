using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExmMaker2
{
    public partial class Form1 : Form
    {
        List<Pytanie> listaPytan = new List<Pytanie>();
        List<TextBox> listaText = new List<TextBox>();
        List<RadioButton> listaRadio = new List<RadioButton>();
        List<CheckBox> listaCheck = new List<CheckBox>();


        int index = 1;
        public Form1()
        {
            InitializeComponent();
            listaPytan.Add(new Pytanie());
            Wyswietlanie();
        }
        private void Wyswietlanie()
        {
            if (index - 1 < 0)
            {

            }
            else
            {
                if (index > listaPytan.Count)
                {
                    listaPytan.Add(new Pytanie());
                }

                label1.Text = index.ToString();
                checkBox1.Checked = listaPytan[index - 1].czyJednaOdpowiedz;
                checkBox2.Checked = listaPytan[index - 1].czyJestObraz;
                checkBox3.Checked = listaPytan[index - 1].czyPytaniaLosowo;
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
                stworzOdpowiedzi();
            }
       }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > listaPytan[index - 1].trescOdpowiedzi.Count)
            {
                listaPytan[index - 1].trescOdpowiedzi.Add("");
                listaPytan[index - 1].ktorePoprawne.Add(false);
            }
            if (numericUpDown1.Value < listaPytan[index-1].trescOdpowiedzi.Count)
            {
                listaPytan[index - 1].trescOdpowiedzi.RemoveAt((int)numericUpDown1.Value);
                listaPytan[index - 1].ktorePoprawne.RemoveAt((int)numericUpDown1.Value);
            }

            Wyswietlanie();
        }
        private void stworzOdpowiedzi()
        {
            int x = 20;
            int y = 30;
            splitContainer1.Panel1.Controls.Clear();
            listaText.Clear();
            listaCheck.Clear();
            listaRadio.Clear();
            for (int i = 0; i < listaPytan[index-1].trescOdpowiedzi.Count; i++)
            {
                Point polozenie_textboxa = new Point(x, y);
                listaText.Add(new TextBox());
                listaText[i].Location = polozenie_textboxa;
                listaText[i].Multiline = true;
                listaText[i].Size = new System.Drawing.Size(250, 35);
                listaText[i].ScrollBars = ScrollBars.Vertical;
                splitContainer1.Panel1.Controls.Add(listaText[i]);
                listaText[i].Text = listaPytan[index-1].trescOdpowiedzi[i];
                if (listaPytan[index-1].czyJednaOdpowiedz)
                {
                    Point polozenie_radio = new Point(x + 270, y);
                    listaRadio.Add(new RadioButton());
                    listaRadio[i].Location = polozenie_radio;
                    splitContainer1.Panel1.Controls.Add(listaRadio[i]);
                    listaRadio[i].Checked = listaPytan[index - 1].ktorePoprawne[i];
                }
                else
                {
                    Point polozenie_check = new Point(x + 270, y);
                    listaCheck.Add(new CheckBox());
                    listaCheck[i].Location = polozenie_check;
                    splitContainer1.Panel1.Controls.Add(listaCheck[i]);
                    listaCheck[i].Checked = listaPytan[index - 1].ktorePoprawne[i];
                }
                y = y + 40;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Dopisz();
            listaPytan[index - 1].czyJednaOdpowiedz = checkBox1.Checked;
            Wyswietlanie();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Dopisz();
            listaPytan[index - 1].czyJestObraz = checkBox2.Checked;
            Wyswietlanie();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Dopisz();
            listaPytan[index - 1].czyPytaniaLosowo = checkBox3.Checked;
            Wyswietlanie();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index > 1)
            {
                Dopisz();
                index--;
                Wyswietlanie();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dopisz();
            index++;
            Wyswietlanie();
        }
        private void Dopisz()
        {
            if (listaPytan[index -1].trescOdpowiedzi.Count != 0)
            {
                for (int i = 0; i < listaText.Count; i++)
                {
                    listaPytan[index - 1].trescOdpowiedzi[i] = listaText[i].Text;
                    if (listaPytan[index - 1].czyJednaOdpowiedz)
                    {

                        listaPytan[index - 1].ktorePoprawne[i] = listaRadio[i].Checked;
                    }
                    else
                    {
                        listaPytan[index - 1].ktorePoprawne[i] = listaCheck[i].Checked;
                    }
                }
            }
        }

    }
}
