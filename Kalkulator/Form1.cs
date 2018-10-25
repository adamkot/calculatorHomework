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

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        Form2 form; //Okno pamięci
        double? nr1 = null;
        double? nr2 = null;
        char? action = null;
        String equal = "";
        int nr1AndActionLength;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9') {
                enetrCharToTextBox(e.KeyChar.ToString());
            }
            if (e.KeyChar == ',' || e.KeyChar == '.') {
                enterPointToTextBox(",");
            }
            if (e.KeyChar == '+'
                || e.KeyChar == '-'
                || e.KeyChar == '*'
                || e.KeyChar == '/'
                || e.KeyChar == '%')
            {
                addAction(e.KeyChar.ToString());
            }
            if (e.KeyChar == 8) {
                deleteOneChar();
            }
            // do zrobienia!
            if (e.KeyChar == 13)
            {
                enetrCharToTextBox("ENTER");
            }
        }

        private void equalButton_Click(object sender, EventArgs e)
        {
            if (!nr1.HasValue || !action.HasValue)
            {
                MessageBox.Show("Error: \n Nie podano liczb lub działania");
            }
            else {
                try {
                    nr2 = Convert.ToDouble(textBox1.Text.Substring(nr1AndActionLength));
                } catch (Exception ex) {
                    nr2 = 0;
                }
                // Obliczenia //
                switch (action)
                {
                    case '+':
                        equal = (nr1 + nr2).ToString();
                        setEqual(equal);
                        break;
                    case '-':
                        equal = (nr1 - nr2).ToString();
                        setEqual(equal);
                        break;
                    case '*':
                        equal = (nr1 * nr2).ToString();
                        setEqual(equal);
                        break;
                    case '/':
                        if (nr2 != 0)
                        {
                            equal = (nr1 / nr2).ToString();
                            setEqual(equal);
                            break;
                        }
                        else {
                            MessageBox.Show("Error: \n Dzielenie przez 0!");
                            break;
                        }
                        
                    case '%':
                        double? eq = nr1 + (nr1 * (nr2 / 100));
                        equal = eq.ToString();
                        setEqual(equal);
                        break;
                }
            }
        }

        #region Buttons 00 - 9

        private void button1_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("9");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("0");
        }

        private void button00_Click(object sender, EventArgs e)
        {
            enetrCharToTextBox("00");
        }

        #endregion

        #region Buttons point, +,-,*,/, %, binary

        private void pointButton_Click(object sender, EventArgs e)
        {
            enterPointToTextBox(",");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            addAction("+");
        }

        private void minusButton_Click(object sender, EventArgs e)
        {
            addAction("-");
        }

        private void multiButton_Click(object sender, EventArgs e)
        {
            addAction("*");
        }

        private void divButton_Click(object sender, EventArgs e)
        {
            addAction("/");
        }

        private void procentButton_Click(object sender, EventArgs e)
        {
            addAction("%");
        }

        private void binaryButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) {
                try
                {
                    int nr = Convert.ToInt32(textBox1.Text);
                    textBox1.Text = Convert.ToString(nr, 2);
                }
                catch (Exception ex) {
                    MessageBox.Show("Error: \n Niewłaściwe dane do konwersji!");
                }
            }
        }

        #endregion

        #region Buttons clear, back

        private void clearButton_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            deleteOneChar();
        }

        #endregion

        private void setEqual(String eq)
        {
            //wyświetlanie wyniku na ekranie
            String operation = nr1 + " " + action + " " + nr2;
            textBox1.Text = operation + " = " + eq;
            //wyświetlanie okna pamięci
            if (form == null)
            {
                generatedForm2();
                form.Show();
            }
            //dodawanie do okna pamięci
            form.addRow(eq, operation);
        }

        private void enetrCharToTextBox(String text)
        {
            if (equal.Length != 0 || textBox1.Text.Equals("0")) {
                clear();
            }
            textBox1.Text = textBox1.Text + text;
        }

        private void enterPointToTextBox(String text)
        {
            if (!nr1.HasValue)
            {
                if (textBox1.Text.IndexOf(text) == -1)
                {
                    if (textBox1.Text.Length == 0)
                    {
                        textBox1.Text = "0" + text;
                    }
                    else
                    {
                        textBox1.Text = textBox1.Text + text;
                    }
                }
            }
            else
            {
                if (textBox1.Text.Substring(nr1AndActionLength).IndexOf(text) == -1)
                {
                    if (textBox1.Text.Substring(nr1AndActionLength).Length == 0)
                    {
                        textBox1.Text = "0" + text;
                    }
                    else
                    {
                        textBox1.Text = textBox1.Text + text;
                    }
                }
            }

        }

        private void addAction(String text)
        {
            if (!action.HasValue)
            {
                nr1 = Convert.ToDouble(textBox1.Text);
                action = Convert.ToChar(text);
                enetrCharToTextBox(text);
                nr1AndActionLength = textBox1.Text.Length;
            }
        }

        private void deleteOneChar()
        {
            if (!nr1.HasValue && !nr2.HasValue)
            {
                if (textBox1.Text.Length > 1)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }
                else if (textBox1.Text.Length == 1)
                {
                    textBox1.Text = "0";
                }
            }
            else if (nr1.HasValue && action.HasValue && !nr2.HasValue)
            {
                if (textBox1.Text.Length == nr1AndActionLength)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    action = null;
                    nr1 = null;
                }
                else if (textBox1.Text.Length > nr1AndActionLength)
                {
                    textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                }
                else if (textBox1.Text.Length == nr1AndActionLength - 1)
                {
                    textBox1.Text = textBox1.Text + "0";
                }
            }
        }

        private void clear()
        {
            textBox1.Text = "";
            nr1 = null;
            nr2 = null;
            action = null;
            equal = "";
        }

        public void backFromTable(String text) {
            clear();
            textBox1.Text = text;
        }

        private void generatedForm2() {
            form = new Form2(this);
            form.StartPosition = FormStartPosition.Manual;
            Point point = new Point(500, 100); //szer, wys
            form.Location = point;
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            //okno podpowiedzi do wyświetlania wyników (gdyby był za długi)
            ToolTip toolTip1 = new ToolTip();
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(textBox1, textBox1.Text);
        }

        #region Menu

        private void stronaProjektuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/adamkot/calculatorHomework.git");
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (form != null)
            {
                String dataToSave = form.getData();
                if (dataToSave.Length == 0)
                {
                    MessageBox.Show("Error: \n Brak danych do zapisu!");
                }
                else
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    saveFileDialog1.Filter = "Text file|*.txt";
                    saveFileDialog1.Title = "Save an Text File";
                    saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.FileName != "")
                    {
                        try
                        {
                            FileStream fileStream = (FileStream)saveFileDialog1.OpenFile();
                            StreamWriter fileWriter = new StreamWriter(fileStream);
                            fileWriter.WriteLine(form.getData());
                            fileWriter.Flush();
                            fileWriter.Close();
                            MessageBox.Show("Sukces: \n Plik zapisany poprawnie");
                        }
                        catch (IOException ioe)
                        {
                            MessageBox.Show("Error: \n Nieudany zapis pliku!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Error: \n Brak danych do zapisu!");
            }
        }
    }
}
