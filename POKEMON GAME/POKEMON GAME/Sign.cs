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
    class Sign
    {
        PictureBox sign = new PictureBox();

        PictureBox Box = new PictureBox();

        public string text;

        protected Timer tm = new Timer();

        public int x;
        public int y;

        public int time;

        public Sign(string t, int X, int Y, ref int timein)
        {
            text = t;
            x = X;
            y = Y;
            time = timein;
        }

        public void Create(Form form)
        {
            sign.Size = new Size(40, 40);

            sign.SizeMode = PictureBoxSizeMode.StretchImage;
            sign.Image = Properties.Resources.sign;
            sign.Tag = "sign";
            sign.Location = new Point(x, y);
            form.Controls.Add(sign);

            Box.Image = Properties.Resources.text;
            Box.Size = new Size(600, 160);
            Box.Location = new Point(0, 400);
            Box.Tag = "text";
            form.Controls.Add(Box);
            Box.BringToFront();
            Box.Hide();

            tm.Interval = 200;
            tm.Tick += new EventHandler(tm_Tick);
            tm.Start();

        }

        public void tm_Tick(object sender, EventArgs e)
        {
            bool location = true;

            if (sign.Location == new Point(240, 280))
            {
                location = false;

            }
            if (sign.Location == new Point(320, 280))
            {
                location = false;
            }
            if (sign.Location == new Point(280, 240))
            {
                location = false;
            }
            if (sign.Location == new Point(280, 320))
            {
                location = false;
            }

            if (location == false)
            {
                if (Form1.interact == true && Form1.texting == false)
                {
                    Form1.NPCtext = text;
                    time = 0;
                    Form1.signMode = true;
                    Box.Show();
                }
            }

        }
    }
}
