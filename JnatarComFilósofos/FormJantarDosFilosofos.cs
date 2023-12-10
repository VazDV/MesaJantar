using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace JnatarComFilósofos
{
    public partial class FormJantarDosFilosofos : Form
    {
        private PictureBox mesaPictureBox;
        private PictureBox[] filosofoPictureBoxes;
        private object[] garfos;
        List<string> nomes = new List<string> { "Aristoteles", "Descartes", "Pitagoras", "Platao", "Socrates" };
        int[] eixoX = new int[5] { 300, 100, 100, 500, 500 };
        int[] eixoY = new int[5] { 100, 200, 400, 400, 200 };

        public FormJantarDosFilosofos()
        {
            InitializeComponent();
            InitializeControls();
            InitializeGarfos();
        }

        private void InitializeControls()
        {
            //configura a mesa
            mesaPictureBox = new PictureBox
            {
                Image = Properties.Resources.Mesa,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new System.Drawing.Size(300, 300),
                Location = new System.Drawing.Point(200, 200)
            };
            Controls.Add(mesaPictureBox);

            //configura os filósofos
            filosofoPictureBoxes = new PictureBox[5];

            for (int i = 0; i < filosofoPictureBoxes.Length; i++)
            {
                filosofoPictureBoxes[i] = new PictureBox
                {
                    Image = Properties.Resources.ResourceManager.GetObject($"{nomes[i]}") as Image,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new System.Drawing.Size(100, 100),
                    Location = new System.Drawing.Point(eixoX[i], eixoY[i])

                };  
                Controls.Add(filosofoPictureBoxes[i]);
            }
        }
        private void InitializeGarfos()
        {
            // Inicializar os garfos como objetos de bloqueio
            garfos = new object[filosofoPictureBoxes.Length];
            for (int i = 0; i < garfos.Length; i++)
            {
                garfos[i] = new object();
            }
        }
        public void FilosofoThread(int filosofoIndex)
        {
            if (filosofoIndex < 3)
            {
                PegarGarfos(filosofoIndex);
                PegarGarfos(filosofoIndex + 2);
                Comer(filosofoIndex);
                Comer(filosofoIndex + 2);
                LiberarGarfos(filosofoIndex);
                LiberarGarfos(filosofoIndex + 2);
            }
            else
            {
                PegarGarfos(filosofoIndex);
                PegarGarfos(filosofoIndex - 3);
                Comer(filosofoIndex);
                Comer(filosofoIndex - 3 );
                LiberarGarfos(filosofoIndex);
                LiberarGarfos(filosofoIndex - 3);
            }
        }
        private void PegarGarfos(int filosofoIndex)
        {
            // Lógica para pegar garfos
            int garfoEsquerdo = filosofoIndex;
            int garfoDireito = (filosofoIndex + 1) % filosofoPictureBoxes.Length;
            filosofoPictureBoxes[filosofoIndex].Image = Properties.Resources.ResourceManager.GetObject($"{nomes[filosofoIndex]}2") as Image;

            Monitor.Enter(garfos[garfoEsquerdo]);
            Monitor.Enter(garfos[garfoDireito]);
        }

        private void Comer(int filosofoIndex)
        {
            Thread.Sleep(500);
        }

        private void LiberarGarfos(int filosofoIndex)
        {
            // Lógica para liberar garfos
            int garfoEsquerdo = filosofoIndex;
            int garfoDireito = (filosofoIndex + 1) % filosofoPictureBoxes.Length;
            filosofoPictureBoxes[filosofoIndex].Image = Properties.Resources.ResourceManager.GetObject($"{nomes[filosofoIndex]}") as Image;
            Monitor.Exit(garfos[garfoEsquerdo]);
            Monitor.Exit(garfos[garfoDireito]);
        }
    }
}
