using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ukol3
{
    public partial class Form1 : Form
    {
        private string[] soustavy = { "Desitkova", "Hexadecimalni", "Dvojkova" };
        
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Desitkova");
            comboBox1.Items.Add("Hexadecimalni");
            comboBox1.Items.Add("Dvojkova");

            comboBox2.Items.Add("Hexadecimalni");
            comboBox2.Items.Add("Dvojkova");
            comboBox1.SelectedIndex = 0;
            richTextBox1.Text = "Děkuje za použití naší aplikace. \nAktuální datum je: " + Convert.ToString(DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year);
        }

        private int binaryToDeci(string binaryNumber)
        {
            int binNum = Convert.ToInt32(binaryNumber);
            int decNumFinal = 0;
            int base1 = 1;
            int zbytek = 0;

            while (binNum > 0)
            {
                zbytek = binNum % 10;
                binNum = binNum / 10;
                decNumFinal += zbytek * base1;
                base1 = base1 * 2;
            }

            return decNumFinal;
        }

        private string deciToBinary(int decimalNumber)
        {
            int decNum = decimalNumber;
            int zbytek = 0;
            string binNum = "";
            string binNumFinal = "";
            while (decNum != 0)
            {
                zbytek = decNum % 2;
                binNum += Convert.ToString(zbytek);
                decNum = decNum / 2;
            }
            for (int i = binNum.Length - 1; i >= 0; i--)
            {
                binNumFinal += binNum[i];
            }
            return binNumFinal;
        }

        private string deciToHexa(int decimalNumber)
        {
            int decNum = decimalNumber;
            int zbytek = 0;
            string hexNum = "";
            string hexNumFinal = "";
            while (decNum != 0)
            {
                zbytek = decNum % 16;
                switch (zbytek)
                {
                    case 10:
                        hexNum += "A";
                        break;
                    case 11:
                        hexNum += "B";
                        break;
                    case 12:
                        hexNum += "C";
                        break;
                    case 13:
                        hexNum += "D";
                        break;
                    case 14:
                        hexNum += "E";
                        break;
                    case 15:
                        hexNum += "F";
                        break;
                    default:
                        hexNum += zbytek;
                        break;
                }
                decNum = decNum / 16;
            }
            for (int i = hexNum.Length - 1; i >= 0; i--)
            {
                hexNumFinal += hexNum[i];
            }
            return hexNumFinal;
        }

        private int hexaToDeci(string hexNumber)
        {
            string hexNum = hexNumber.ToUpper();
            int decNum = 0;
            int hex = 16;
            int decNumFinal = 0;
            int index = hexNum.Length - 1;
            string znak = "";
            for (int i = 0; i < hexNum.Length; i++)
            {
                znak = Convert.ToString(hexNum[i]);
                switch (znak)
                {
                    case "A":
                        decNum = 10;
                        break;
                    case "B":
                        decNum = 11;
                        break;
                    case "C":
                        decNum = 12;
                        break;
                    case "D":
                        decNum = 13;
                        break;
                    case "E":
                        decNum = 14;
                        break;
                    case "F":
                        decNum = 15;
                        break;
                    default:
                        try
                        {
                            decNum = Convert.ToInt32(znak);
                        }catch(Exception e)
                        {
                            decNum = 0;
                        }
                        break;
                }
                hex = 16;
                for (int x = 0; x < index; x++)
                {
                    hex = hex * 16;
                }
                index--;
                if (hex == 16)
                {
                    hex = 1;
                }
                if (hex == 256)
                {
                    hex = 16;
                }
                decNumFinal += decNum * hex;
            }
            return decNumFinal;
        }

        private void zapisVysledek(string text)
        {
            textBox2.Text = text;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            int i = comboBox1.SelectedIndex;
            for (int x = 0; x < soustavy.Length; x++)
            {
                if(x != i)
                    comboBox2.Items.Add(soustavy[x]);
            }
            comboBox2.SelectedIndex = 0;
            pocitej();
        }

        private void pocitej()
        {
            int cislo = 0;
            if (textBox1.Text != "")
            {
                switch (comboBox1.SelectedItem)
                {
                    case "Dvojkova":
                        cislo = binaryToDeci(textBox1.Text);
                        break;
                    case "Hexadecimalni":
                        cislo = hexaToDeci(textBox1.Text);
                        break;
                    default:
                        cislo = Convert.ToInt32(textBox1.Text);
                        break;

                }
                switch (comboBox2.SelectedItem)
                {
                    case "Desitkova":
                        zapisVysledek(Convert.ToString(cislo));
                        break;
                    case "Dvojkova":
                        zapisVysledek(deciToBinary(cislo));
                        break;
                    case "Hexadecimalni":
                        zapisVysledek(deciToHexa(cislo));
                        break;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            pocitej();
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            pocitej();
        }
    }
}