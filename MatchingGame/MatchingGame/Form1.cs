using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {

        Joc tabla;

        public Form1()
        {
            InitializeComponent();
            tabla = new Joc();
            punereIconite();
        }

        public void punereIconite()
        {
            List<string> lista = tabla.getIcons();
            Label auxlabel;
            for (int i=0; i<tableLayoutPanel1.Controls.Count; ++i)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                    auxlabel = (Label)tableLayoutPanel1.Controls[i];
                else
                    continue;
                auxlabel.Text = lista[i];
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label apasat = sender as Label;
            tabla.verificare(apasat,timer1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            tabla.reset_last_data();
        }
    }


    public class Joc
    {
        private Random random = new Random();
        Label apasat1, apasat2;
        int nr_corecte, nr_incercari;
        private List<string> icons = new List<string>()
        {
            "a","a","b","b","c","c","d","d","e","e","f","f","g","g","h","h"
        };

        public List<string>getIcons()
        {
            return icons;
        }

        public void verificare(Label label, Timer timer)
        {
            if (apasat1 != null && apasat2 != null)
                return;
            if (label == null)
                return;
            if (label.ForeColor == Color.Black)
                return;
            if (apasat1==null)
            {
                apasat1 = label;
                apasat1.ForeColor = Color.Black;
                return;
            }
            apasat2 = label;
            apasat2.ForeColor = Color.Black;
            nr_incercari++;

            if (apasat1.Text==apasat2.Text)
            {
                apasat1 = null;
                apasat2 = null;
                nr_corecte++;
                if (nr_corecte==8)
                {
                    MessageBox.Show("Felicitari. Ai terminat jocul in "+nr_incercari+" incercari!");
                }
            }
            else
                timer.Start();
        }

        public void reset_last_data()
        {
            apasat1.ForeColor = apasat1.BackColor;
            apasat2.ForeColor = apasat2.BackColor;

            apasat1 = null;
            apasat2 = null;
        }

        public Joc()
        {
            nr_corecte = 0;
            nr_incercari = 0;
            int nr;
            string aux;
            for (int i=0; i<16; ++i)
            {
                nr = random.Next(0, 16);
                aux = icons[i];
                icons[i] = icons[nr];
                icons[nr] = aux;
            }
        }
    }
}
