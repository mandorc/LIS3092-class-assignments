using System;

namespace Particle_System
{
    public partial class Form1 : Form
    {
        private List<Particulas> particulasFuego = new List<Particulas>();
        private Random random = new Random();

        // Lista de rectángulos que representan las áreas visibles en la pantalla
        private List<Rectangle> areasVisibles = new List<Rectangle>();

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

        private void pictureBox1_Scroll(object sender, ScrollEventArgs e)
        {
            // Actualizar la lista de áreas visibles cuando se produce un evento de desplazamiento
            areasVisibles.Clear();
            areasVisibles.Add(pictureBox1.ClientRectangle);
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
                float diametro = (float)(random.NextDouble() * 20 + 5); // Tamaño aleatorio entre 5 y 25
                Color color = Color.FromArgb(128, random.Next(256), random.Next(256), random.Next(256)); // Color aleatorio con transparencia
                particulasFuego.Add(new Particulas(posX, posY, velX, velY, tiempoVida, gravedad, diametro, color));
            }

            // Incrementar la variable de contador de partículas
            totalParticulas += 10;

            // Dibujar las partículas de fuego en el PictureBox
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Actualizar y dibujar las partículas de fuego que están dentro de las áreas visibles
            foreach (Particulas particula in particulasFuego)
            {
                if (EstaDentroDeAlgunaAreaVisible(particula))
                {
                    particula.Actualizar(0.02f); // Utiliza un deltaTime fijo de 0.02 segundos para simplificar el ejemplo
                    particula.Dibujar(e.Graphics);
                }
            }
        }

        private bool EstaDentroDeAlgunaAreaVisible(Particulas particula)
        {
            // Comprobar si la partícula está dentro de alguna de las áreas visibles
            foreach (Rectangle areaVisible in areasVisibles)
            {
                Rectangle particulaRect = new Rectangle((int)(particula.posX - particula.diametro / 2), (int)(particula.posY - particula.diametro / 2), (int)particula.diametro, (int)particula.diametro);

                if (areaVisible.IntersectsWith(particulaRect))
                {
                    return true;
                }
            }

            return false;
        }

        private int totalParticulas = 0;

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

            // Si se han creado 10 nuevas partículas, eliminar las primeras partículas creadas
            if (totalParticulas >= 10 && particulasFuego.Count >= 10)
            {
                particulasFuego.RemoveRange(0, 10);
                totalParticulas -= 10;
            }

            // Dibujar las partículas de fuego en el PictureBox
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Icon customIcon = new Icon(Resources.star_icon, new Size(32, 32));
            Cursor customCursor = new Cursor(customIcon.Handle);
            this.Cursor = customCursor;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Agregar el rectángulo inicialmente visible al iniciar la aplicación
            areasVisibles.Add(pictureBox1.ClientRectangle);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            // Actualizar la lista de áreas visibles cuando se produce un evento de cambio de tamaño
            areasVisibles.Clear();
            areasVisibles.Add(pictureBox1.ClientRectangle);
        }
    }
}