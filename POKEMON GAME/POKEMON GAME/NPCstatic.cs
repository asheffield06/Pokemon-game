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
    class NPCstatic
    {
        public string name;
        public int ID;
        public string text;

        public bool NPC1;
        public bool NPC2;
        public bool NPC3;


        protected Timer tm2 = new Timer();

        public int x;
        public int y;

        public int time;

        PictureBox npc = new PictureBox();

        PictureBox Box = new PictureBox();

        public NPCstatic(string n, int X, int Y, int id, string t, ref int timein)
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
        }

        public void Create(Form form)
        {
            npc.Size = new Size(40, 40);

            npc.SizeMode = PictureBoxSizeMode.StretchImage;
            npc.Tag = "static";
            npc.Location = new Point(x, y);
            if (NPC1 == true)
            {
                npc.Image = Properties.Resources.NPC4Down;
            }
            if (NPC2 == true)
            {
                npc.Image = Properties.Resources.NPC2Left;
            }
            if (NPC3 == true)
            {
                npc.Image = Properties.Resources.NPC5Down;
            }
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


            tm2.Interval = 200;
            tm2.Tick += new EventHandler(tm2_Tick);
            tm2.Start();
        }

        public void tm2_Tick(object sender, EventArgs e)
        {
            

            if (NPC1 == true)
            {
                bool location = true;


                if (npc.Location == new Point(280, 320))
                {
                    location = false;

                }
                if (npc.Location == new Point(280, 240))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC4Down;
                }
                if (npc.Location == new Point(320, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC4Left;
                }
                if (npc.Location == new Point(240, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC4Right;
                }

                if (location == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }
            }

            if (NPC2 == true)
            {
                bool location = true;


                if (npc.Location == new Point(280, 320))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC2Up;

                }
                if (npc.Location == new Point(280, 240))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC2Down;
                }
                if (npc.Location == new Point(320, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC2Left;
                }
                if (npc.Location == new Point(240, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC2Right;
                }

                if (location == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }
            }

            if (NPC3 == true)
            {
                bool location = true;


                if (npc.Location == new Point(280, 320))
                {
                    location = false;

                }
                if (npc.Location == new Point(280, 240))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC5Down;
                }
                if (npc.Location == new Point(320, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC5Left;
                }
                if (npc.Location == new Point(240, 280))
                {
                    location = false;
                    npc.Image = Properties.Resources.NPC5Right;
                }

                if (location == false && Form1.texting == false)
                {
                    if (Form1.interact == true)
                    {
                        Box.Show();
                        Form1.NPCtext = text;
                        Form1.NPCname = name;
                        Form1.textMode = true;
                    }
                }
            }
        }

        public PictureBox GetPictureBoxNpc()
        {
            return npc;
        }
    }
}
