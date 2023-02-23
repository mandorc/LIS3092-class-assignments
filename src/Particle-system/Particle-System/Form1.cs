using System;

namespace Particle_System
{
    public partial class Form1 : Form
    {
        private List<Particulas> particulasFuego = new List<Particulas>();
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();

            timer.Interval = 100; // Intervalo de 100 milisegundos
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Crear nuevas partículas de fuego en la posición del mouse
            for (int i = 0; i < 10; i++)
            {
                float posX = e.X;
                float posY = e.Y;
                float velX = (float)(random.NextDouble() * 100 - 50);
                float velY = (float)(random.NextDouble() * 100 - 50);
                float tiempoVida = 1.0f;
                float gravedad = 30.0f;
                float diametro = 10.0f;
                Color color = Color.OrangeRed;
                particulasFuego.Add(new Particulas(posX, posY, velX, velY, tiempoVida, gravedad, diametro, color));
            }

            // Dibujar las partículas de fuego en el PictureBox
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Actualizar y dibujar las partículas de fuego
            foreach (Particulas particula in particulasFuego)
            {
                particula.Actualizar(0.02f); // Utiliza un deltaTime fijo de 0.02 segundos para simplificar el ejemplo
                particula.Dibujar(e.Graphics);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Eliminar las partículas que han superado su tiempo de vida
            for (int i = 0; i < particulasFuego.Count; i++)
            {
                Particulas particula = particulasFuego[i];

                if (particula.tiempoActual >= particula.tiempoVida)
                {
                    particulasFuego.RemoveAt(i);
                    i--;
                }
            }

            // Dibujar las partículas de fuego en el PictureBox
            pictureBox1.Invalidate();
        }
    }
}