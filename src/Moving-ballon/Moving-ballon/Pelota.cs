using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_ballon
{
    internal class Pelota
    {

        private int posX;
        private int posY;
        private int radio;
        private int velocidadX;
        private int velocidadY;
        private int velocidadInicial; // nueva variable para almacenar la velocidad inicial
        private Brush color;

        public Pelota(int x, int y, int r, int vx, int vy, Brush c)
        {
            posX = x;
            posY = y;
            radio = r;
            velocidadX = vx;
            velocidadY = vy;
            velocidadInicial = (int)Math.Sqrt(Math.Pow(velocidadX, 2) + Math.Pow(velocidadY, 2)); // calcula la velocidad inicial
            color = c;
        }

        public void Dibujar(Graphics g)
        {
            g.FillEllipse(color, posX - radio, posY - radio, radio * 2, radio * 2);
        }

        public void Mover(int ancho, int alto, List<Pelota> pelotas)
        {
            posX += velocidadX;
            posY += velocidadY;

            // Comprobar si la pelota ha llegado a los bordes de la pantalla
            if (posX - radio < 0 || posX + radio > ancho)
            {
                velocidadX = -velocidadX;
            }
            if (posY - radio < 0 || posY + radio > alto)
            {
                velocidadY = -velocidadY;
            }

            // Comprobar si la pelota ha colisionado con otra pelota
            foreach (Pelota otraPelota in pelotas)
            {
                if (otraPelota != this) // Evita comprobar la colisión consigo misma
                {
                    double distancia = Math.Sqrt(Math.Pow(posX - otraPelota.posX, 2) + Math.Pow(posY - otraPelota.posY, 2));
                    if (distancia < radio + otraPelota.radio)
                    {
                        // Colisión detectada
                        double angulo = Math.Atan2(posY - otraPelota.posY, posX - otraPelota.posX);
                        double velocidadX1 = Math.Cos(angulo) * velocidadInicial;
                        double velocidadY1 = Math.Sin(angulo) * velocidadInicial;
                        double velocidadX2 = Math.Cos(angulo + Math.PI) * otraPelota.velocidadInicial;
                        double velocidadY2 = Math.Sin(angulo + Math.PI) * otraPelota.velocidadInicial;

                        velocidadX = Convert.ToInt32(velocidadX1);
                        velocidadY = Convert.ToInt32(velocidadY1);
                        otraPelota.velocidadX = Convert.ToInt32(velocidadX2);
                        otraPelota.velocidadY = Convert.ToInt32(velocidadY2);
                    }
                }
            }
        }


    }
}
