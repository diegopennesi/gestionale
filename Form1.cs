using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestionale
{
    public partial class Form1 : Form
    {
        Engine engine = new Engine();
        public Form1()
        {
           
            InitializeComponent();
           tabControl1.Hide();
            panel3.Hide();
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String[] a = { textBox1.Text, textBox2.Text };
           
          if(engine.login(a))
            {
                panel1.Hide();
                panel3.Show();
                tabControl1.Show();
            }
            else
            {
                MessageBoxIcon icon = MessageBoxIcon.Error;
                
                MessageBox.Show("username o Password errata","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Panel_LavoratoriIns.Visible) {
                Panel_LavoratoriIns.Show();
                    };
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string nome= Lavoratore_nome.Text;
            string cognome = Lavoratore_cognome.Text;
            string email= Lavoratore_email.Text;
            ArrayList lingue= new ArrayList();
            foreach (string item in Lavoratore_language_list.CheckedItems)
            {
                lingue.Add(item);
            }
            string extrafield = Lavoratore_extraFiled.Text;
            switch (engine.insertNewWorker(nome, cognome, email, lingue, extrafield))
            {
                case true:
                    MessageBox.Show("Inserimento a buon fine", "Inserimento", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case false: 
                    MessageBox.Show("Inserimento Fallito Utente già inserito in DB", "Inserimento", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    break;
                default:
                    MessageBox.Show("Inserimento Fallito", "Inserimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel_LavoratoriIns_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
    