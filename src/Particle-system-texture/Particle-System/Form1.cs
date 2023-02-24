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

            timer.Interval = 10; // Intervalo de 100 milisegundos
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Crear nuevas partículas de fuego en la posición del mouse
            for (int i = 0; i < 1; i++)
            {
                float posX = e.X;
                float posY = e.Y;
                float velX = (float)(random.NextDouble() * 100 - 50);
                float velY = (float)(random.NextDouble() * 100 - 50);
                float tiempoVida = 1.0f;
                float gravedad = 30.0f;
                float diametro = (float)(random.NextDouble() * 20 + 5); // Tamaño aleatorio entre 5 y 25
                Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)); // Color aleatorio
                particulasFuego.Add(new Particulas(posX, posY, velX, velY, tiempoVida, gravedad, diametro, color));
            }

            // Dibujar las partículas de fuego en el PictureBox
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Obtener la región visible del PictureBox
            Rectangle visibleRegion = e.ClipRectangle;

            // Actualizar y dibujar solo las partículas que están dentro de la región visible
            foreach (Particulas particula in particulasFuego)
            {
                particula.Actualizar(0.02f); // Utiliza un deltaTime fijo de 0.02 segundos para simplificar el ejemplo

                // Dibujar la partícula solo si está dentro de la región visible del PictureBox
                if (particula.posX - particula.diametro / 2 >= visibleRegion.Left &&
                    particula.posX + particula.diametro / 2 <= visibleRegion.Right &&
                    particula.posY - particula.diametro / 2 >= visibleRegion.Top &&
                    particula.posY + particula.diametro / 2 <= visibleRegion.Bottom)
                {
                    particula.Dibujar(e.Graphics);
                }
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

            // Liberar memoria no utilizada
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor cursorPersonalizado = new Cursor(Resources.star.GetHicon());
            Cursor.Current = cursorPersonalizado;
        }
    }
}