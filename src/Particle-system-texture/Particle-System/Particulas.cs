using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particle_System
{
    internal class Particulas
    {
        // Propiedades de las partículas
        public float posX { get; set; }
        public float posY { get; set; }
        public float velX { get; set; }
        public float velY { get; set; }
        public float tiempoVida { get; set; }
        public float tiempoActual { get; set; }
        public float gravedad { get; set; }
        public float diametro { get; set; }
        public Color color { get; set; }

        // Agrega una propiedad para almacenar la imagen de la partícula
        private Image imagenParticula;

        // Constructor de la clase Particulas
        public Particulas(float posX, float posY, float velX, float velY, float tiempoVida, float gravedad, float diametro, Color color)
        {
            this.posX = posX;
            this.posY = posY;
            this.velX = velX;
            this.velY = velY;
            this.tiempoVida = tiempoVida;
            this.gravedad = gravedad;
            this.diametro = diametro;
            this.color = color;

            
        }

        // Método para actualizar las propiedades de la partícula
        public void Actualizar(float deltaTime)
        {
            tiempoActual += deltaTime;
            posX += velX * deltaTime;
            posY += velY * deltaTime;
            velY += gravedad * deltaTime;
        }

        private const int BrilloMaximo = 200;

        // Método para dibujar la partícula en la pantalla
        public void Dibujar(Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Yellow);

            // Dibuja la partícula como un círculo amarillo
            g.FillEllipse(brush, posX - diametro / 2, posY - diametro / 2, diametro, diametro);

            // Calcula el tamaño del brillo de fondo en función del radio de la partícula
            int brillo = Math.Min(BrilloMaximo, (int)(BrilloMaximo * (50 / diametro)));

            // Crea una máscara que sólo permita el brillo del fondo en las zonas cercanas a la partícula
            int mascaraRadio = (int)(diametro / 2) + 10;
            GraphicsPath mascara = new GraphicsPath();
            mascara.AddEllipse(posX - mascaraRadio, posY - mascaraRadio, diametro + 20, diametro + 20);
            Region mascaraRegion = new Region(mascara);

            // Dibuja el brillo del fondo
            Color backgroundColor = Color.FromArgb(brillo, Color.White);
            Brush backgroundBrush = new SolidBrush(backgroundColor);
            g.FillRegion(backgroundBrush, mascaraRegion);
        }
    }
}
