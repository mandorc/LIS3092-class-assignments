namespace Moving_ballon
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;

        private List<Pelota> pelotas = new List<Pelota>();

        private Pelota pelota;

        public Form1()
        {
            InitializeComponent();

            bmp=new Bitmap (pictureBox1.Width, pictureBox1.Height);

            g= Graphics.FromImage (bmp);
            pictureBox1.Image = bmp;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();

            int x = rnd.Next(0, pictureBox1.Width);
            int y = rnd.Next(0, pictureBox1.Height);
            int r = rnd.Next(10, 50);
            int vx = rnd.Next(-10, 10);
            int vy = rnd.Next(-10, 10);

            Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Brush brush = new SolidBrush(color);

            Pelota pelota = new Pelota(x, y, r, vx, vy, brush);
            pelotas.Add(pelota);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Pelota pelota in pelotas)
            {
                pelota.Dibujar(e.Graphics);
            }
        }

        private void timerPic_Tick(object sender, EventArgs e)
        {
            foreach (Pelota pelota in pelotas)
            {
                pelota.Mover(pictureBox1.Width, pictureBox1.Height, pelotas);
            }

            pictureBox1.Invalidate();
        }

        private void generateTimer_Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();

            int x = rnd.Next(0, pictureBox1.Width);
            int y = rnd.Next(0, pictureBox1.Height);
            int r = rnd.Next(10, 50);
            int vx = rnd.Next(-10, 10);
            int vy = rnd.Next(-10, 10);

            Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Brush brush = new SolidBrush(color);

            Pelota pelota = new Pelota(x, y, r, vx, vy, brush);
            pelotas.Add(pelota);
        }
    }
}