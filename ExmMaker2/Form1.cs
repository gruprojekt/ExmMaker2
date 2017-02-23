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

namespace ExmMaker2
{
    public partial class Form1 : Form
    {
        // tab 1
        List<Pytanie> listaPytan = new List<Pytanie>();
        List<TextBox> listaText = new List<TextBox>();
        List<RadioButton> listaRadio = new List<RadioButton>();
        List<CheckBox> listaCheck = new List<CheckBox>();
        int index = 1;

        // tab2
        List<Pytanie> listaPytan2 = new List<Pytanie>();
        List<CustomTextBox> listaText2 = new List<CustomTextBox>();
        List<RadioButton> listaRadio2 = new List<RadioButton>();
        List<CheckBox> listaCheck2 = new List<CheckBox>();
        List<List<bool>> listaOdpowiedziUzytkownika = new List<List<bool>>();
        int liczbaPoprawnychOdpowiedziUzytkownika = 0;
        int liczbaMozliwychPoprawnychOdpowiedzi = 0;
        int liczbaNiepoprawnychOdpowiedziUzytkownika = 0;
        int index2 = 1;
        List<int> listaUzytych = new List<int>();
        List<int> listaPotrzebna = new List<int>();
        List<Pytanie> listaPytan3 = new List<Pytanie>();
        TimeSpan t = new TimeSpan(0 , 0, 0);
        public Form1()
        {
            InitializeComponent();
            listaPytan.Add(new Pytanie());
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            Wyswietlanie();
  
        }
        private void Wyswietlanie()
        {
            if (listaPytan.Count == 0)
            {
                listaPytan.Add(new Pytanie());

            }
            label1.Text = index + "/" + listaPytan.Count;
            numericUpDown2.Value = listaPytan[0].minuty;
            if (listaPytan[index - 1].czyJestObraz)
            {
                pictureBox1.ImageLocation = (listaPytan[index - 1].pathObrazka);
            }
            else
            {
                pictureBox1.ImageLocation = "";
            }
            //numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
            textBox1.Text = listaPytan[index - 1].trescPytania;
            checkBox1.Checked = listaPytan[index - 1].czyJednaOdpowiedz;
            checkBox2.Checked = listaPytan[index - 1].czyJestObraz;
            checkBox3.Checked = listaPytan[0].czyPytaniaLosowo;
            for (int i = 0; i < listaPytan[index -1].trescOdpowiedzi.Count; i++)
            {
                listaText[i].Text = listaPytan[index - 1].trescOdpowiedzi[i];
                if (listaPytan[index - 1].czyJednaOdpowiedz)
                {
                    listaRadio[i].Checked = listaPytan[index - 1].ktorePoprawne[i];
                }
                else
                {
                    listaCheck[i].Checked = listaPytan[index - 1].ktorePoprawne[i];
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > listaPytan[index - 1].trescOdpowiedzi.Count)
            {
                listaPytan[index - 1].trescOdpowiedzi.Add("");
                listaPytan[index - 1].ktorePoprawne.Add(false);
            }
            else
            {
                if (listaPytan[index - 1].trescOdpowiedzi.Count != 0 && numericUpDown1.Value < listaPytan[index - 1].trescOdpowiedzi.Count)
                {
                    listaPytan[index - 1].trescOdpowiedzi.RemoveAt(listaPytan[index - 1].trescOdpowiedzi.Count - 1);
                    listaPytan[index - 1].ktorePoprawne.RemoveAt(listaPytan[index - 1].ktorePoprawne.Count - 1);
                }
            }
            stworzOdpowiedzi();
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
            for (int i = 0; i < listaPytan[index - 1].trescOdpowiedzi.Count; i++)
            {
                Point polozenie_textboxa = new Point(x, y);
                listaText.Add(new TextBox());
                listaText[i].Location = polozenie_textboxa;
                listaText[i].Multiline = true;
                listaText[i].Size = new System.Drawing.Size(250, 35);
                listaText[i].ScrollBars = ScrollBars.Vertical;
                splitContainer1.Panel1.Controls.Add(listaText[i]);
                listaText[i].TabIndex = i;
                listaText[i].TextChanged += new EventHandler(textChanged);

                if (listaPytan[index-1].czyJednaOdpowiedz)
                {
                    Point polozenie_radio = new Point(x + 270, y);
                    listaRadio.Add(new RadioButton());
                    listaRadio[i].Location = polozenie_radio;
                    listaRadio[i].TabIndex = i;
                    listaRadio[i].CheckedChanged += new EventHandler(radioCheckedChanged);
                    splitContainer1.Panel1.Controls.Add(listaRadio[i]);
                }
                else
                {
                    Point polozenie_check = new Point(x + 270, y);
                    listaCheck.Add(new CheckBox());
                    listaCheck[i].Location = polozenie_check;
                    listaCheck[i].TabIndex = i;
                    listaCheck[i].CheckedChanged += new EventHandler(checkCheckedChanged);
                    splitContainer1.Panel1.Controls.Add(listaCheck[i]);
                }
                y = y + 40;
            }
        }
        private void stworzOdpowiedzi2()
        {

            int x = 20;
            int y = 30;
            label6.Text = index2 + "/" + listaPytan2.Count;
            splitContainer2.Panel1.Controls.Clear();
            listaText2.Clear();
            listaCheck2.Clear();
            listaRadio2.Clear();
            Point polozenie_label = new Point(x, y);
            CustomTextBox textbox = new CustomTextBox();
            textbox.Size = new System.Drawing.Size(400, 50);
            textbox.Multiline = true;
            textbox.ReadOnly = true;
            textbox.ScrollBars = ScrollBars.Vertical;
            textbox.Text = listaPytan2[index2 - 1].trescPytania;
            textbox.Location = polozenie_label;
            textbox.BorderStyle = BorderStyle.None;
            splitContainer2.Panel1.Controls.Add(textbox);
            y = y + textbox.Height + 10;

            pictureBox2.ImageLocation = (listaPytan2[index2 - 1].pathObrazka);

            for (int i = 0; i < listaPytan2[index2 - 1].trescOdpowiedzi.Count; i++)
            {
                if (listaPytan2[index2 - 1].czyJednaOdpowiedz)
                {
                    Point polozenie_radio = new Point(x, y - 5);
                    listaRadio2.Add(new RadioButton());
                    listaRadio2[i].Location = polozenie_radio;
                    listaRadio2[i].AutoSize = false;
                    listaRadio2[i].Width = 30;
                    listaRadio2[i].Height = 30;
                    splitContainer2.Panel1.Controls.Add(listaRadio2[i]);
                    listaRadio2[i].TabIndex = i;
                    listaRadio2[i].CheckedChanged += new EventHandler(radioCheckedChanged2);
                    listaRadio2[i].Checked = listaOdpowiedziUzytkownika[index2 - 1][i];

                }
                else
                {
                    Point polozenie_check = new Point(x, y - 5);
                    listaCheck2.Add(new CheckBox());
                    listaCheck2[i].Location = polozenie_check;
                    listaCheck2[i].AutoSize = false;
                    listaCheck2[i].Width = 30;
                    listaCheck2[i].Height = 30;
                    splitContainer2.Panel1.Controls.Add(listaCheck2[i]);
                    listaCheck2[i].TabIndex = i;
                    listaCheck2[i].CheckedChanged += new EventHandler(checkCheckedChanged2);
                    listaCheck2[i].Checked = listaOdpowiedziUzytkownika[index2 - 1][i];
                }

                Point polozenie_textboxa = new Point(x + 40, y);
                listaText2.Add(new CustomTextBox());
                listaText2[i].Location = polozenie_textboxa;
                listaText2[i].ReadOnly = true;
                listaText2[i].Multiline = true;
                listaText2[i].Size = new System.Drawing.Size(250, 35);
                listaText2[i].ScrollBars = ScrollBars.Vertical;
                splitContainer2.Panel1.Controls.Add(listaText2[i]);
                listaText2[i].Text = listaPytan2[index2 - 1].trescOdpowiedzi[i];
                listaText2[i].BorderStyle = BorderStyle.None;
                y = y + 40;

            }
        }
        protected void radioCheckedChanged(object sender,EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            listaPytan[index - 1].ktorePoprawne[radio.TabIndex] = radio.Checked;
        }
        protected void checkCheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            listaPytan[index - 1].ktorePoprawne[check.TabIndex] = check.Checked;
        }
        protected void radioCheckedChanged2(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            listaOdpowiedziUzytkownika[index2 - 1][radio.TabIndex] = radio.Checked;
        }
        protected void checkCheckedChanged2(object sender, EventArgs e)
        {
            CheckBox check = sender as CheckBox;
            listaOdpowiedziUzytkownika[index2 - 1][check.TabIndex] = check.Checked;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            listaPytan[index - 1].czyJednaOdpowiedz = checkBox1.Checked;
            stworzOdpowiedzi();
            Wyswietlanie();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            listaPytan[index - 1].czyJestObraz = checkBox2.Checked;
            stworzOdpowiedzi();
            if (checkBox2.Checked && listaPytan[index -1].pathObrazka == "")
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Title = "Open Image";
                dlg.Filter = "bmp files (*.bmp)|*.bmp";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    listaPytan[index - 1].pathObrazka = dlg.FileName;
                }
                else
                {
                    listaPytan[index - 1].czyJestObraz = false;
                    checkBox2.Checked = false;
                }

                dlg.Dispose();
            }
            if (!checkBox2.Checked)
            {
                listaPytan[index - 1].pathObrazka = "";
            }
            Wyswietlanie();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPytan.Count; i++)
            {
                listaPytan[i].czyPytaniaLosowo = checkBox3.Checked;
            }
            stworzOdpowiedzi();
            Wyswietlanie();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index <= 1)
            {

            }
            else
            {
                index--;
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
                stworzOdpowiedzi();
                Wyswietlanie();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listaPytan[index - 1].trescPytania != "" && listaPytan[index -1].trescOdpowiedzi.Count != 0)
            {


                if (index == listaPytan.Count)
                {
                    listaPytan.Add(new Pytanie());
                }
                index++;
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
                stworzOdpowiedzi();
                Wyswietlanie();
            }
            else
            {
                MessageBox.Show("Nie możesz stworzyć nowego pytania jeżeli nie uzupełniłeś poprzedniego");
            }
        }

        protected void textChanged(object sender, EventArgs e)
        {
            TextBox textbox = sender as TextBox;
            listaPytan[index - 1].trescOdpowiedzi[textbox.TabIndex] = textbox.Text;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listaPytan[index - 1].trescPytania = textBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (index == 1)
            {
                listaPytan.RemoveAt(index - 1);
            }
            if (listaPytan.Count > 1 && index > 1)
            {
                listaPytan.RemoveAt(index - 1);
                index--;
            }
            if (listaPytan.Count > 0)
            {
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
            }
            else
            {
                Wyswietlanie();
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPytan.Count; i++)
            {
                listaPytan[i].minuty = (int)numericUpDown2.Value;
            }
            Wyswietlanie();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {


                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "(*.xml)|*.xml";
                dialog.ShowDialog();

                if (dialog.FileName != "")
                {
                    StreamWriter wr = new StreamWriter(dialog.FileName); //To służy do zapisu danych
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pytanie>)); //To będzie je formatowało :)
                    serializer.Serialize(wr, listaPytan); //Serializujemy
                    wr.Flush();
                    wr.Close();
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się zapisać pliku");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Jestes pewny ? Stracisz dotychczasowy postęp.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (d == DialogResult.Yes)

            {
                listaPytan.Clear();
                Wyswietlanie();
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
            }

            else if (d == DialogResult.No)

            {

            }
       }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "(*.xml) | *.xml";
                openFileDialog1.Title = "Wybierz test";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamReader r = new StreamReader(openFileDialog1.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pytanie>));
                    listaPytan = (List<Pytanie>)serializer.Deserialize(r);
                    r.Close();
                }
                index = 1;
                numericUpDown1.Value = listaPytan[index - 1].trescOdpowiedzi.Count;
            }
            catch
            {
                MessageBox.Show("Nie udało się otworzyć pliku");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "(*.xml) | *.xml";
                openFileDialog1.Title = "Wybierz test";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    StreamReader r = new StreamReader(openFileDialog1.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pytanie>));
                    listaPytan2 = (List<Pytanie>)serializer.Deserialize(r);
                    r.Close();
                }
                if (listaPytan2[0].czyPytaniaLosowo)
                {
                    WypelnianieListy(listaPytan2.Count);
                }
                for (int i = 0; i < listaPytan2.Count; i++)
                {
                    listaOdpowiedziUzytkownika.Add(new List<bool>());
                    for (int j = 0; j < listaPytan2[i].ktorePoprawne.Count; j++)
                    {
                        listaOdpowiedziUzytkownika[i].Add(false);
                    }
                }
                button7.Enabled = false;
                if (listaPytan2[0].minuty != 0)
                {
                    DialogResult d = MessageBox.Show("Test ma ustawiony limit czasowy " + listaPytan2[0].minuty + " minut, Nacisnij 'tak' gdy bedziesz gotowy", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)

                    {
                        t = new TimeSpan(0, listaPytan2[0].minuty, 0);
                        timer1.Start();
                        stworzOdpowiedzi2();
                        button10.Enabled = true;
                    }
                    else if (d == DialogResult.No)
                    {
                        button10_Click(sender, e);
                    }

                }
                else
                {
                    stworzOdpowiedzi2();
                    button10.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Nie udało się otworzyć pliku");
            }
        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (index2 < listaPytan2.Count)
            {
                index2++;
                stworzOdpowiedzi2();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (index2 > 1)
            {
                index2--;
                stworzOdpowiedzi2();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listaPytan2.Count; i++)
            {
                for (int j = 0; j < listaPytan2[i].ktorePoprawne.Count; j++)
                {
                    if (listaOdpowiedziUzytkownika[i][j] == true && listaPytan2[i].ktorePoprawne[j] == true)
                    {
                        liczbaPoprawnychOdpowiedziUzytkownika++;
                    }
                    if (listaPytan2[i].ktorePoprawne[j] == true)
                    {
                        liczbaMozliwychPoprawnychOdpowiedzi++;
                    }
                    if (listaOdpowiedziUzytkownika[i][j] != listaPytan2[i].ktorePoprawne[j] && listaOdpowiedziUzytkownika[i][j] == true)
                    {
                        liczbaNiepoprawnychOdpowiedziUzytkownika++;
                    }
                }
            }
            label7.Text = "";
            timer1.Stop();
            splitContainer2.Panel1.Controls.Clear();
            listaOdpowiedziUzytkownika.Clear();
            label6.Text = "";
            pictureBox2.ImageLocation = "";
            MessageBox.Show("Zakończyłeś test, zdobyłeś " + liczbaPoprawnychOdpowiedziUzytkownika + " punktów z " + liczbaMozliwychPoprawnychOdpowiedzi + " możliwych, pomylileś się "+ liczbaNiepoprawnychOdpowiedziUzytkownika+ " razy", "WYNIKI");
            button10.Enabled = false;
            listaPytan3.Clear();
            listaPytan2.Clear();
            liczbaMozliwychPoprawnychOdpowiedzi = 0;
            liczbaPoprawnychOdpowiedziUzytkownika = 0;
            listaUzytych.Clear();
            listaPotrzebna.Clear();
            index2 = 1;
            button7.Enabled = true;

        }
        public int RandomIndex(int a)//funkcja ktora wyrzuca liczbe randomową nie potwarzająca sie indeksach (a to ilosc pytan)
        {
            Random rnd = new Random();
            int liczba = 0;
            bool OK = true;
            while (OK)
            {
                liczba = rnd.Next(0, (a));
                if (!listaUzytych.Contains(liczba))
                {
                    listaUzytych.Add(liczba);
                    OK = false;
                }
            }
            return liczba;
        }
        public void WypelnianieListy(int a)// tutaj wypelniamy liste indeksow aby moc w innej kolejnosci wyrzucac pytania
        {
            for (int i = 0; i < a; i++)
            {
                this.listaPotrzebna.Add(RandomIndex(a));
            }
            foreach (int value in listaPotrzebna)
            {
                listaPytan3.Add(listaPytan2[value]);
            }
            listaPytan2 = listaPytan3;
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            t -= new TimeSpan(0, 0, 1);
            label7.Text = t.ToString();
            if (t == new TimeSpan(0, 0, 0))
            {
                button10_Click(sender, e);
                label7.Text = "";
                timer1.Stop();
            }
        }
        public static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }
        public partial class CustomTextBox : TextBox
        {
            public CustomTextBox()
            {
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.Transparent;
            }
        }
        protected override void OnFormClosing( FormClosingEventArgs e)
        {

            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Jeżeli nie zapisałeś postępów, mogą zostać utracone. Napewno chcesz zakończyć?", "Zamykanie aplikacji !", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
