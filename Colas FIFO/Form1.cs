using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colas_FIFO
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnComenzar_Click(object sender, EventArgs e)
        {
            Queue<Proceso> miFifo = new Queue<Proceso> { };
            int ciclosVacía = 0;
            for (int i = 0; i < 200; i++)
            {
                // 25% de probabilidades de agregar un nuevo proceso a la cola
                if (random.Next(4) == 0)
                {
                    Proceso nuevo = new Proceso(random.Next(4, 15));
                    miFifo.Enqueue(nuevo);
                }

                // Procesa
                if (miFifo.Count > 0)
                {
                    miFifo.Peek().ciclos--;
                    if (miFifo.Peek().ciclos == 0)
                        miFifo.Dequeue();
                }
                else
                    ciclosVacía++;
            }

            // Datos
            txtLog.Text = "Cantidad de ciclos en los que estuvo vacía la cola: " + ciclosVacía.ToString() + Environment.NewLine;
            txtLog.Text += "Quedaron " + miFifo.Count.ToString() + " procesos pendientes" + Environment.NewLine;
            int totalCiclosPendientes = 0;
            foreach (Proceso proceso in miFifo)
                totalCiclosPendientes += proceso.ciclos;
            txtLog.Text += "Hicieron falta " + totalCiclosPendientes.ToString() + " ciclos para terminar de procesar la cola";
        }
    }
}