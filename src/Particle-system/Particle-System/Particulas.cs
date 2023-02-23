using System;
using System.Collections.Generic;
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

        // Método para dibujar la partícula en la pantalla
        public void Dibujar(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, posX - diametro / 2, posY - diametro / 2, diametro, diametro);
        }
    }
}
