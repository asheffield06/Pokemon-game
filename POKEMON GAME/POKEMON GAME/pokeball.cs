using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POKEMON_GAME
{
    class pokeball
    {
        public string name;
        public string type;

        public int x;
        public int y;

        public int time;

        PictureBox ball = new PictureBox();

        PictureBox Box = new PictureBox();

        protected Timer tm3 = new Timer();

        public pokeball(string n, string t ,int X, int Y, ref int timein)
        {
            name = n;
            type = t;
            x = X;
            y = Y;
            time = timein;
        }

        public void Create(Form form)
        {
            ball.Size = new Size(40, 40);
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            ball.Image = Properties.Resources.pokeball;
            ball.Tag = "ball";
            ball.Location = new Point(x, y);
            form.Controls.Add(ball);
            ball.BringToFront();

            Box.Image = Properties.Resources.text;
            Box.Size = new Size(600, 160);
            Box.Location = new Point(0, 400);
            Box.Tag = "text";
            form.Controls.Add(Box);
            Box.BringToFront();
            Box.Hide();

            tm3.Interval = 200;
            tm3.Tick += new EventHandler(tm3_Tick);
            tm3.Start();
        }

        public void tm3_Tick(object sender, EventArgs e)
        {
            

            bool location = true;

            if (ball.Location == new Point(240, 280))
            {
                location = false;
            }
            if (ball.Location == new Point(320, 280))
            {
                location = false;
            }
            if (ball.Location == new Point(280, 240))
            {
                location = false;
            }
            if (ball.Location == new Point(280, 320))
            {
                location = false;
            }

            if (location == false)
            {
                if (Form1.interact == true && Form1.texting == false && Form1.firstPokemon == false)
                {
                    Form1.NPCtext = "would you like " + name + "?";
                    Form1.NPCname = name;
                    Form1.Poketype = type;
                    time = 0;
                    Form1.ballMode = true;
                    Box.Show();
                }
            }

        }
    }
}
