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
    class NPC
    {
        public string name;
        public int ID;
        public string text;

        public bool NPC1;
        public bool NPC2;
        public bool NPC3;
        public bool NPC4;
        public bool NPC5;

        protected Timer tm = new Timer();

        public int x;
        public int y;

        public int time;

        PictureBox npc = new PictureBox();

        PictureBox up = new PictureBox();
        PictureBox down = new PictureBox();
        PictureBox left = new PictureBox();
        PictureBox right = new PictureBox();

        PictureBox Box = new PictureBox();


        public NPC(string n, int X, int Y, int id, string t, ref int timein)
        {
            name = n;
            x = X;
            y = Y;
            ID = id;
            text = t;
            time = timein;
        

            if (ID == 1)
            {
                NPC1 = true;
            }
            if (ID == 2)
            {
                NPC2 = true;
            }
            if (ID == 3)
            {
                NPC3 = true;
            }
            if (ID == 4)
            {
                NPC4 = true;
            }
            if (ID == 5)
            {
                NPC5 = true;
            }
        }

        public void Create(Form form)
        {
            npc.Size = new Size(40, 40);
            
            npc.SizeMode = PictureBoxSizeMode.StretchImage;
            npc.Tag = "NPC";
            npc.Location = new Point(x, y);
            if (NPC1 == true)
            {
                npc.Image = Properties.Resources.NPC1Down;
            }
            if (NPC2 == true)
            {
                npc.Image = Properties.Resources.NPC2Down;
            }
            if (NPC3 == true)
            {
                npc.Image = Properties.Resources.NPC3Down;
            }
            if (NPC4 == true)
            {
                npc.Image = Properties.Resources.NPC4Down;
            }
            if (NPC5 == true)
            {
                npc.Image = Properties.Resources.NPC5Down;
            }

            up.Size = new Size(40, 40);
            left.Size = new Size(40, 40);
            right.Size = new Size(40, 40);
            down.Size = new Size(40, 40);

            up.Location = new Point(x,y - 40);
            right.Location = new Point(x - 40, y);
            down.Location = new Point(x, y + 40);
            left.Location = new Point(x + 40, y);

            up.Visible = false;
            down.Visible = false;
            left.Visible = false;
            right.Visible = false;

            up.Tag = "up";
            down.Tag = "down";
            left.Tag = "left";
            right.Tag = "right";

            form.Controls.Add(right);
            form.Controls.Add(left);
            form.Controls.Add(down);
            form.Controls.Add(up);
            form.Controls.Add(npc);
            npc.BringToFront();
            npc.BringToFront();

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

            if (NPC1 == true)
            {
                bool movement = true;


                if (up.Location == new Point(280, 280))
                {
                    movement = false;
                    npc.Image = Properties.Resources.NPC1Up;

                }
                if (down.Location == new Point(280, 280))
                {
                    movement = false;
                    npc.Image = Properties.Resources.NPC1Down;
                }
                if (left.Location == new Point(280, 280))
                {
                    movement = false;
                    npc.Image = Properties.Resources.NPC1Right;
                }
                if (right.Location == new Point(280, 280))
                {
                    movement = false;
                    npc.Image = Properties.Resources.NPC1Left;
                }

                if (movement == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }



                Random rnd = new Random();
                int rndno = rnd.Next(1, 100);
                if (rndno == 21 && movement == true)
                {
                    npc.Image = Properties.Resources.NPC1Up;
                    npc.Top = npc.Top - 40;

                    up.Top = up.Top - 40;
                    down.Top = down.Top - 40;
                    left.Top = left.Top - 40;
                    right.Top = right.Top - 40;
                }

                if (rndno == 22 && movement == true)
                {
                    npc.Image = Properties.Resources.NPC1Down;
                    npc.Top = npc.Top + 40;

                    up.Top = up.Top + 40;
                    down.Top = down.Top + 40;
                    left.Top = left.Top + 40;
                    right.Top = right.Top + 40;
                }

                if (rndno == 23 && movement == true)
                {
                    npc.Image = Properties.Resources.NPC1Right;
                    npc.Left = npc.Left + 40;

                    up.Left = up.Left + 40;
                    down.Left = down.Left + 40;
                    left.Left = left.Left + 40;
                    right.Left = right.Left + 40;
                }

                if (rndno == 24 && movement == true)
                {
                    npc.Image = Properties.Resources.NPC1Left;
                    npc.Left = npc.Left - 40;

                    up.Left = up.Left - 40;
                    down.Left = down.Left - 40;
                    left.Left = left.Left - 40;
                    right.Left = right.Left - 40;
                }

                
            }

            if (NPC2 == true)
            {
                bool movement2 = true;

                if (up.Location == new Point(280, 280))
                {
                    movement2 = false;
                    npc.Image = Properties.Resources.NPC2Up;

                }
                if (down.Location == new Point(280, 280))
                {
                    movement2 = false;
                    npc.Image = Properties.Resources.NPC2Down;
                }
                if (left.Location == new Point(280, 280))
                {
                    movement2 = false;
                    npc.Image = Properties.Resources.NPC2Right;
                }
                if (right.Location == new Point(280, 280))
                {
                    movement2 = false;
                    npc.Image = Properties.Resources.NPC2Left;
                }

                if (movement2 == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }

                Random rnd = new Random();
                int rndno = rnd.Next(1, 100);
                if (rndno == 21 && movement2 == true)
                {
                    npc.Image = Properties.Resources.NPC2Up;
                    npc.Top = npc.Top - 40;

                    up.Top = up.Top - 40;
                    down.Top = down.Top - 40;
                    left.Top = left.Top - 40;
                    right.Top = right.Top - 40;
                }

                if (rndno == 22 && movement2 == true)
                {
                    npc.Image = Properties.Resources.NPC2Down;
                    npc.Top = npc.Top + 40;

                    up.Top = up.Top + 40;
                    down.Top = down.Top + 40;
                    left.Top = left.Top + 40;
                    right.Top = right.Top + 40;
                }

                if (rndno == 23 && movement2 == true)
                {
                    npc.Image = Properties.Resources.NPC2Right;
                    npc.Left = npc.Left + 40;

                    up.Left = up.Left + 40;
                    down.Left = down.Left + 40;
                    left.Left = left.Left + 40;
                    right.Left = right.Left + 40;
                }

                if (rndno == 24 && movement2 == true)
                {
                    npc.Image = Properties.Resources.NPC2Left;
                    npc.Left = npc.Left - 40;

                    up.Left = up.Left - 40;
                    down.Left = down.Left - 40;
                    left.Left = left.Left - 40;
                    right.Left = right.Left - 40;
                }
            }

            if (NPC3 == true)
            {


                bool movement3 = true;

                if (up.Location == new Point(280, 280))
                {
                    movement3 = false;
                    npc.Image = Properties.Resources.NPC3Up;

                }
                if (down.Location == new Point(280, 280))
                {
                    movement3 = false;
                    npc.Image = Properties.Resources.NPC3Down;
                }
                if (left.Location == new Point(280, 280))
                {
                    movement3 = false;
                    npc.Image = Properties.Resources.NPC3Right;
                }
                if (right.Location == new Point(280, 280))
                {
                    movement3 = false;
                    npc.Image = Properties.Resources.NPC3Left;
                }

                if (movement3 == false)
                {
                    if (Form1.interact == true && Form1.texting == false)
                    {
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                        Box.Show();
                    }
                }



                Random rnd = new Random();
                int rndno = rnd.Next(1, 100);
                if (rndno == 21 && movement3 == true)
                {
                    npc.Image = Properties.Resources.NPC3Up;
                    npc.Top = npc.Top - 40;

                    up.Top = up.Top - 40;
                    down.Top = down.Top - 40;
                    left.Top = left.Top - 40;
                    right.Top = right.Top - 40;
                }

                if (rndno == 22 && movement3 == true)
                {
                    npc.Image = Properties.Resources.NPC3Down;
                    npc.Top = npc.Top + 40;

                    up.Top = up.Top + 40;
                    down.Top = down.Top + 40;
                    left.Top = left.Top + 40;
                    right.Top = right.Top + 40;
                }

                if (rndno == 23 && movement3 == true)
                {
                    npc.Image = Properties.Resources.NPC3Right;
                    npc.Left = npc.Left + 40;

                    up.Left = up.Left + 40;
                    down.Left = down.Left + 40;
                    left.Left = left.Left + 40;
                    right.Left = right.Left + 40;
                }

                if (rndno == 24 && movement3 == true)
                {
                    npc.Image = Properties.Resources.NPC3Left;
                    npc.Left = npc.Left - 40;

                    up.Left = up.Left - 40;
                    down.Left = down.Left - 40;
                    left.Left = left.Left - 40;
                    right.Left = right.Left - 40;
                }


            }

            if (NPC4 == true)
            {
                bool movement4 = true;
                npc.Tag = "static";

                if (up.Location == new Point(280, 280))
                {
                    movement4 = false;
                }
                if (down.Location == new Point(280, 280))
                {
                    movement4 = false;
                }
                if (left.Location == new Point(280, 280))
                {
                    movement4 = false;
                }
                if (right.Location == new Point(280, 280))
                {
                    movement4 = false;
                }

                if (movement4 == false)
                {
                    if (Form1.interact == true && Form1.texting == false)
                    {
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        time = 0;
                        Form1.textMode = true;
                        Box.Show();
                    }
                }

            }

            if (NPC5 == true)
            {
                bool movement5 = true;


                if (up.Location == new Point(280, 280))
                {
                    movement5 = false;
                    npc.Image = Properties.Resources.NPC5Up;

                }
                if (down.Location == new Point(280, 280))
                {
                    movement5 = false;
                    npc.Image = Properties.Resources.NPC5Down;
                }
                if (left.Location == new Point(280, 280))
                {
                    movement5 = false;
                    npc.Image = Properties.Resources.NPC5Right;
                }
                if (right.Location == new Point(280, 280))
                {
                    movement5 = false;
                    npc.Image = Properties.Resources.NPC5Left;
                }

                if (movement5 == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }



                Random rnd = new Random();
                int rndno = rnd.Next(1, 100);
                if (rndno == 21 && movement5 == true)
                {
                    npc.Image = Properties.Resources.NPC5Up;
                    npc.Top = npc.Top - 40;

                    up.Top = up.Top - 40;
                    down.Top = down.Top - 40;
                    left.Top = left.Top - 40;
                    right.Top = right.Top - 40;
                }

                if (rndno == 22 && movement5 == true)
                {
                    npc.Image = Properties.Resources.NPC5Down;
                    npc.Top = npc.Top + 40;

                    up.Top = up.Top + 40;
                    down.Top = down.Top + 40;
                    left.Top = left.Top + 40;
                    right.Top = right.Top + 40;
                }

                if (rndno == 23 && movement5 == true)
                {
                    npc.Image = Properties.Resources.NPC5Right;
                    npc.Left = npc.Left + 40;

                    up.Left = up.Left + 40;
                    down.Left = down.Left + 40;
                    left.Left = left.Left + 40;
                    right.Left = right.Left + 40;
                }

                if (rndno == 24 && movement5 == true)
                {
                    npc.Image = Properties.Resources.NPC5Left;
                    npc.Left = npc.Left - 40;

                    up.Left = up.Left - 40;
                    down.Left = down.Left - 40;
                    left.Left = left.Left - 40;
                    right.Left = right.Left - 40;
                }


            }


        }

        public PictureBox GetPictureBoxUp()
        {
            return up;
        }
        public PictureBox GetPictureBoxDown()
        {
            return down;
        }
        public PictureBox GetPictureBoxLeft()
        {
            return left;
        }
        public PictureBox GetPictureBoxRight()
        {
            return right;
        }
        public PictureBox GetPictureBoxNpc()
        {
            return npc;
        }
    }
}
