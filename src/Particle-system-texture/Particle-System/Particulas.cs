using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
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
            Bitmap imagen = Resources.star_min; // Reemplaza "miImagen" con el nombre de tu imagen de Resources.resx
            TextureBrush brush = new TextureBrush(imagen);
            brush.TranslateTransform(posX - diametro / 2, posY - diametro / 2);
            brush.ScaleTransform(diametro / imagen.Width, diametro / imagen.Height);
            Matrix matrizOpacidad = new Matrix();
            matrizOpacidad.Scale(1, 1);
            matrizOpacidad.Translate(0, 0);
            matrizOpacidad.Multiply(new Matrix(1, 0, 0, 0.5f, 0, 0)); // Establece el nivel de opacidad del pincel
            brush.MultiplyTransform(matrizOpacidad);

            g.FillEllipse(brush, posX - diametro / 2, posY - diametro / 2, diametro, diametro);
        }
    }
}
