using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;


namespace POKEMON_GAME
{
    public partial class Form1 : Form
    {
        // Timer 

        int timer = 0;

        // Movement related variables & sprite related variables

        int movementInt = 0;
        bool movement = false;
        bool warping = false;
        bool inGrass = false;
        bool goLeft = false;
        bool goRight = false;
        bool goDown = false;
        bool goUp = false;
        bool goLeftCheck = true;
        bool goRightCheck = true;
        bool goDownCheck = true;
        bool goUpCheck = true;
        string facing = "down";

        // Cutscene related variables 

        bool blockCutScene = false;
        bool interacted = false;
        bool location = false;
        int steps = 0;

        // Menu related variables 

        bool menuMode = false;
        bool arrowLocation = false;
        bool activate = false;
        bool switching = false;
        bool skip = false;
        int swapNumber = 0;

        // Encounter variables 

        int randomEncounter;
        bool encountering = false;
        Random rnd = new Random();
        int ID;

        // Dialougue related variables

        bool linesComplete = false;
        bool secondline;
        public static string NPCtext = "";
        public static string NPCname = "";
        public static string Poketype = "";

        // Public variables for classes 

        public static bool firstPokemon = false;
        public static bool interact = false;
        public static bool textMode = false;
        public static bool signMode = false;
        public static bool ballMode = false;
        public static bool texting = false;
        public static bool battleMode = false;

        // Battle variables 

        bool run = false;
        bool attacking = false;
        bool wildAttacking = false;
        bool wildFaint = false;
        bool playerFaint = false;
        bool levelUp = false;
        bool fullParty = false;
        bool partyCheck = false;
        bool captureMode = false;
        int attackSum = 0;

        // Creating Pokemon objects 

        pokemon MyPokemonOne = new pokemon("", "", 8, 6, 6, 35, 35, 5, 0, 500);
        pokemon MyPokemonTwo = new pokemon("", "", 50, 5, 5, 0, 0, 5, 0, 500);
        pokemon MyPokemonThree = new pokemon("", "", 50, 5, 5, 0, 0, 5, 0, 500);
        pokemon Wild = new pokemon("", "", 15, 15, 15, 30, 30, 5, 0, 500);
        pokemon switchPokemon;

        // Creating NPC and staticNPC objects 

        NPC kevin;
        NPC mum;
        NPC bruce;
        NPCstatic jeff;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        // For when a key is pressed 

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                movement = true;
                facing = "left";
                if (movementInt == 1)
                {
                    player.Image = Properties.Resources.movingleft;
                }
                if (movementInt == 2)
                {
                    player.Image = Properties.Resources.left;
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                movement = true;
                facing = "right";
                if (movementInt == 1)
                {
                    player.Image = Properties.Resources.movingright;
                }
                if (movementInt == 2)
                {
                    player.Image = Properties.Resources.right;
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                movement = true;
                facing = "up";
                if (movementInt == 1)
                {
                    player.Image = Properties.Resources.movingup;
                }
                if (movementInt == 2)
                {
                    player.Image = Properties.Resources.movingup2;
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                movement = true;
                facing = "down";
                if (movementInt == 1)
                {
                    player.Image = Properties.Resources.movingdown1;
                }
                if (movementInt == 2)
                {
                    player.Image = Properties.Resources.movingdown2;
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                interact = true;

            }
            if (e.KeyCode == Keys.D1)
            {
                menuMode = true;
                skip = true;
            }
        }

        // For when a key isnt being pressed 

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            interact = false;

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
                goLeftCheck = true;
                movement = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
                goRightCheck = true;
                movement = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
                goUpCheck = true;
                movement = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
                goDownCheck = true;
                movement = false;
            }
            if (e.KeyCode == Keys.Space)
            {

                interact = false;
            }
            if (e.KeyCode == Keys.D1)
            {
                menuMode = false;
            }
            if (e.KeyCode == Keys.D2)
            {
                menuMode = false;
            }
        }

        // Main timer for overworld 

        public void MainGameTimer_Tick(object sender, EventArgs e)
        {
            texting = false;

            // Deals with setting sprites for stationary player

            if (facing == "down" && goDown == false && movement == false)
            {
                player.Image = Properties.Resources.down1;
            }
            if (facing == "up" && goUp == false && movement == false)
            {
                player.Image = Properties.Resources.up;
            }
            if (facing == "right" && goRight == false && movement == false)
            {
                player.Image = Properties.Resources.right;
            }
            if (facing == "left" && goLeft == false && movement == false)
            {
                player.Image = Properties.Resources.left;
            }

            // Enabling collsions when the player moves

            if (movement == true)
            {
                goLeftCheck = true;
                goRightCheck = true;
                goDownCheck = true;
                goUpCheck = true;
            }

            // Starting the cutscene

            if (downCheck.Bounds.IntersectsWith(roadBlock.Bounds) && !firstPokemon)
            {
                blockCutScene = true;
                cutSceneTimer.Start();
                MainGameTimer.Stop();
            }

            // Removing cutscene condition

            if (firstPokemon == true)
            {
                roadBlock.Hide();
                roadBlock2.Hide();
            }

            // Enables the text timer

            if (textMode == true)
            {
                TextTimer.Start();
                MainGameTimer.Stop();
            }

            // Enables the sign timer 

            if (signMode == true)
            {
                SignTimer.Start();
                MainGameTimer.Stop();
            }

            // Enables pokeball selection mode

            if (ballMode == true)
            {
                BallTimer.Start();
                arrow.Location = new Point(80, 500);
                MainGameTimer.Stop();
            }

            // Enables the menu mode

            if (menuMode == true)
            {
                MenuTimer.Start();
                arrow.Location = new Point(340, 40);
                MainGameTimer.Stop();
            }

            // Enables the battle mode

            if (battleMode == true)
            {
                if (timer > 80)
                {
                    encounterAnimation.Hide();
                    BattleTimer.Start();
                    MainGameTimer.Stop();
                }

            }

            // UP DIRECTION

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == true && x.Tag != "player" && x.Tag != "floor" && x.Tag != "grass")
                {
                    if (upCheck.Bounds.IntersectsWith(x.Bounds))
                    {
                        goUpCheck = false;
                    }
                }
            }

            if (goUpCheck == true)
            {
                if (goUp)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                        {
                            x.Top = x.Top + 40;
                            Application.DoEvents();
                        }
                    }
                }
            }

            // DOWN DIRECTION

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == true && x.Tag != "player" && x.Tag != "floor" && x.Tag != "grass")
                {
                    if (downCheck.Bounds.IntersectsWith(x.Bounds))
                    {
                        goDownCheck = false;
                    }
                }
            }
            if (goDownCheck == true)
            {
                if (goDown)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                        {
                            x.Top = x.Top - 40;
                            Application.DoEvents();
                        }
                    }
                }
            }


            // RIGHT DIRECTION

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == true && x.Tag != "player" && x.Tag != "floor" && x.Tag != "grass")
                {
                    if (rightCheck.Bounds.IntersectsWith(x.Bounds))
                    {
                        goRightCheck = false;
                    }
                }
            }
            if (goRightCheck == true)
            {
                if (goRight)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                        {
                            x.Left = x.Left - 40;
                            Application.DoEvents();
                        }
                    }
                }
            }


            // LEFT DIRECTION

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == true && x.Tag != "player" && x.Tag != "floor" && x.Tag != "grass")
                {
                    if (leftCheck.Bounds.IntersectsWith(x.Bounds))
                    {
                        goLeftCheck = false;
                    }
                }
            }
            if (goLeftCheck == true)
            {
                if (goLeft)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                        {
                            x.Left = x.Left + 40;
                            Application.DoEvents();
                        }
                    }
                }
            }

            // Collisions for all NPCs

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag != "NPC" && y.Tag != "text" && y.Visible == true && y != player)
                {
                    if (kevin.GetPictureBoxRight().Bounds.IntersectsWith(y.Bounds))
                    {
                        kevin.GetPictureBoxRight().Left = kevin.GetPictureBoxRight().Left + 40;
                        kevin.GetPictureBoxLeft().Left = kevin.GetPictureBoxLeft().Left + 40;
                        kevin.GetPictureBoxDown().Left = kevin.GetPictureBoxDown().Left + 40;
                        kevin.GetPictureBoxUp().Left = kevin.GetPictureBoxUp().Left + 40;
                        kevin.GetPictureBoxNpc().Left = kevin.GetPictureBoxNpc().Left + 40;
                    }

                    if (kevin.GetPictureBoxLeft().Bounds.IntersectsWith(y.Bounds))
                    {
                        kevin.GetPictureBoxRight().Left = kevin.GetPictureBoxRight().Left - 40;
                        kevin.GetPictureBoxLeft().Left = kevin.GetPictureBoxLeft().Left - 40;
                        kevin.GetPictureBoxDown().Left = kevin.GetPictureBoxDown().Left - 40;
                        kevin.GetPictureBoxUp().Left = kevin.GetPictureBoxUp().Left - 40;
                        kevin.GetPictureBoxNpc().Left = kevin.GetPictureBoxNpc().Left - 40;
                    }

                    if (kevin.GetPictureBoxUp().Bounds.IntersectsWith(y.Bounds))
                    {
                        kevin.GetPictureBoxRight().Top = kevin.GetPictureBoxRight().Top + 40;
                        kevin.GetPictureBoxLeft().Top = kevin.GetPictureBoxLeft().Top + 40;
                        kevin.GetPictureBoxDown().Top = kevin.GetPictureBoxDown().Top + 40;
                        kevin.GetPictureBoxUp().Top = kevin.GetPictureBoxUp().Top + 40;
                        kevin.GetPictureBoxNpc().Top = kevin.GetPictureBoxNpc().Top + 40;
                    }

                    if (kevin.GetPictureBoxDown().Bounds.IntersectsWith(y.Bounds))
                    {
                        kevin.GetPictureBoxRight().Top = kevin.GetPictureBoxRight().Top - 40;
                        kevin.GetPictureBoxLeft().Top = kevin.GetPictureBoxLeft().Top - 40;
                        kevin.GetPictureBoxDown().Top = kevin.GetPictureBoxDown().Top - 40;
                        kevin.GetPictureBoxUp().Top = kevin.GetPictureBoxUp().Top - 40;
                        kevin.GetPictureBoxNpc().Top = kevin.GetPictureBoxNpc().Top - 40;
                    }
                }
            }
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag != "NPC" && y.Tag != "text" && y.Tag != "floor" && y.Visible == true && y != player)
                {
                    if (mum.GetPictureBoxRight().Bounds.IntersectsWith(y.Bounds))
                    {
                        mum.GetPictureBoxRight().Left = mum.GetPictureBoxRight().Left + 40;
                        mum.GetPictureBoxLeft().Left = mum.GetPictureBoxLeft().Left + 40;
                        mum.GetPictureBoxDown().Left = mum.GetPictureBoxDown().Left + 40;
                        mum.GetPictureBoxUp().Left = mum.GetPictureBoxUp().Left + 40;
                        mum.GetPictureBoxNpc().Left = mum.GetPictureBoxNpc().Left + 40;
                    }

                    if (mum.GetPictureBoxLeft().Bounds.IntersectsWith(y.Bounds))
                    {
                        mum.GetPictureBoxRight().Left = mum.GetPictureBoxRight().Left - 40;
                        mum.GetPictureBoxLeft().Left = mum.GetPictureBoxLeft().Left - 40;
                        mum.GetPictureBoxDown().Left = mum.GetPictureBoxDown().Left - 40;
                        mum.GetPictureBoxUp().Left = mum.GetPictureBoxUp().Left - 40;
                        mum.GetPictureBoxNpc().Left = mum.GetPictureBoxNpc().Left - 40;
                    }

                    if (mum.GetPictureBoxUp().Bounds.IntersectsWith(y.Bounds))
                    {
                        mum.GetPictureBoxRight().Top = mum.GetPictureBoxRight().Top + 40;
                        mum.GetPictureBoxLeft().Top = mum.GetPictureBoxLeft().Top + 40;
                        mum.GetPictureBoxDown().Top = mum.GetPictureBoxDown().Top + 40;
                        mum.GetPictureBoxUp().Top = mum.GetPictureBoxUp().Top + 40;
                        mum.GetPictureBoxNpc().Top = mum.GetPictureBoxNpc().Top + 40;
                    }

                    if (mum.GetPictureBoxDown().Bounds.IntersectsWith(y.Bounds))
                    {
                        mum.GetPictureBoxRight().Top = mum.GetPictureBoxRight().Top - 40;
                        mum.GetPictureBoxLeft().Top = mum.GetPictureBoxLeft().Top - 40;
                        mum.GetPictureBoxDown().Top = mum.GetPictureBoxDown().Top - 40;
                        mum.GetPictureBoxUp().Top = mum.GetPictureBoxUp().Top - 40;
                        mum.GetPictureBoxNpc().Top = mum.GetPictureBoxNpc().Top - 40;
                    }
                }
            }
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag != "NPC" && y.Tag != "text" && y.Visible == true && y != player)
                {
                    if (bruce.GetPictureBoxRight().Bounds.IntersectsWith(y.Bounds))
                    {
                        bruce.GetPictureBoxRight().Left = bruce.GetPictureBoxRight().Left + 40;
                        bruce.GetPictureBoxLeft().Left = bruce.GetPictureBoxLeft().Left + 40;
                        bruce.GetPictureBoxDown().Left = bruce.GetPictureBoxDown().Left + 40;
                        bruce.GetPictureBoxUp().Left = bruce.GetPictureBoxUp().Left + 40;
                        bruce.GetPictureBoxNpc().Left = bruce.GetPictureBoxNpc().Left + 40;
                    }

                    if (bruce.GetPictureBoxLeft().Bounds.IntersectsWith(y.Bounds))
                    {
                        bruce.GetPictureBoxRight().Left = bruce.GetPictureBoxRight().Left - 40;
                        bruce.GetPictureBoxLeft().Left = bruce.GetPictureBoxLeft().Left - 40;
                        bruce.GetPictureBoxDown().Left = bruce.GetPictureBoxDown().Left - 40;
                        bruce.GetPictureBoxUp().Left = bruce.GetPictureBoxUp().Left - 40;
                        bruce.GetPictureBoxNpc().Left = bruce.GetPictureBoxNpc().Left - 40;
                    }

                    if (bruce.GetPictureBoxUp().Bounds.IntersectsWith(y.Bounds))
                    {
                        bruce.GetPictureBoxRight().Top = bruce.GetPictureBoxRight().Top + 40;
                        bruce.GetPictureBoxLeft().Top = bruce.GetPictureBoxLeft().Top + 40;
                        bruce.GetPictureBoxDown().Top = bruce.GetPictureBoxDown().Top + 40;
                        bruce.GetPictureBoxUp().Top = bruce.GetPictureBoxUp().Top + 40;
                        bruce.GetPictureBoxNpc().Top = bruce.GetPictureBoxNpc().Top + 40;
                    }

                    if (bruce.GetPictureBoxDown().Bounds.IntersectsWith(y.Bounds))
                    {
                        bruce.GetPictureBoxRight().Top = bruce.GetPictureBoxRight().Top - 40;
                        bruce.GetPictureBoxLeft().Top = bruce.GetPictureBoxLeft().Top - 40;
                        bruce.GetPictureBoxDown().Top = bruce.GetPictureBoxDown().Top - 40;
                        bruce.GetPictureBoxUp().Top = bruce.GetPictureBoxUp().Top - 40;
                        bruce.GetPictureBoxNpc().Top = bruce.GetPictureBoxNpc().Top - 40;
                    }
                }
            }

            // Warping situations

            if (upCheck.Bounds.IntersectsWith(warp1.Bounds))
            {
                if (goUp)
                {
                    foreach (Control x in this.Controls)
                    {

                        if (!warping)
                        {
                            warping = true;
                        }
                        else
                        {
                            if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                            {
                                x.Top = x.Top - 5000;

                            }
                        }
                    }
                    warping = false;
                }

            }
            if (downCheck.Bounds.IntersectsWith(warp2.Bounds))
            {
                if (goDown)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (!warping)
                        {
                            warping = true;
                        }
                        else
                        {
                            if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                            {
                                x.Top = x.Top + 5000;
                                Application.DoEvents();
                            }

                        }

                    }
                    warping = false;
                }

            }
            if (upCheck.Bounds.IntersectsWith(warp3.Bounds))
            {
                if (goUp)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (!warping)
                        {
                            warping = true;
                        }
                        else
                        {
                            if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                            {
                                x.Top = x.Top + 920;
                                Application.DoEvents();
                            }

                        }
                    }
                    warping = false;
                }
            }
            if (downCheck.Bounds.IntersectsWith(warp4.Bounds))
            {
                if (goDown)
                {
                    foreach (Control x in this.Controls)
                    {
                        if (!warping)
                        {
                            warping = true;
                        }
                        else
                        {
                            if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                            {
                                x.Top = x.Top - 920;
                                Application.DoEvents();
                            }
                        }
                    }
                    warping = false;
                }
            }

            // Encounters set up

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "grass")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && movement && encountering == false)
                    {
                        Random rand = new Random();
                        randomEncounter = rand.Next(1, 4);
                    }
                }
            }
            if (randomEncounter == 2)
            {
                ID = rnd.Next(1, 9);
                encountering = true;
                encounter();
                randomEncounter = 0;
            }

            // sets the reset for health when interacted with bed

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "player")
                {
                    if (x.Bounds.IntersectsWith(bed2.Bounds) || x.Bounds.IntersectsWith(bed1.Bounds))
                    {
                        MyPokemonOne.CurrentHP = MyPokemonOne.Health;
                        MyPokemonTwo.CurrentHP = MyPokemonTwo.Health;
                        MyPokemonThree.CurrentHP = MyPokemonThree.Health;
                    }
                }
            }
        }

        private void TextTimer_Tick(object sender, EventArgs e)
        {
            texting = true;
            interacted = true;

            if (NPCtext.Length >= 40)
            {
                string part1 = NPCtext.Substring(0, 17);
                string part2 = NPCtext.Substring(17);

                int idx = part1.LastIndexOf(' ');

                textLabel.Text = part1.Substring(0, idx);

                string part3 = part1.Substring(idx) + part2;

                string part4 = part3.Substring(0, 24);
                string part5 = part3.Substring(24);

                int idx2 = part4.LastIndexOf(' ');
                textLabel2.Text = part4.Substring(0, idx2);



                if (interact && !secondline)
                {
                    secondline = true;
                    timer = 0;
                }
            }
            else if (NPCtext.Length >= 17 && NPCtext.Length < 40)
            {
                string part1 = NPCtext.Substring(0, 17);
                string part2 = NPCtext.Substring(17);
                int idx = part1.LastIndexOf(' ');
                textLabel.Text = part1.Substring(0, idx);
                textLabel2.Text = part1.Substring(idx) + part2;
                linesComplete = true;
            }
            else
            {
                textLabel.Text = NPCtext;
                textLabel2.Text = " ";
                linesComplete = true;
            }

            if (secondline == true && timer > 30)
            {
                textLabel.Location = new Point(20, 440);
                textLabel.BringToFront();
                nameLabel.Hide();
                string part1 = NPCtext.Substring(0, 17);
                string part2 = NPCtext.Substring(17);
                int idx = part1.LastIndexOf(' ');
                string part3 = part1.Substring(idx) + part2;
                string part4 = part3.Substring(0, 24);
                string part5 = part3.Substring(24);
                int idx2 = part4.LastIndexOf(' ');
                textLabel.Text = part4.Substring(0, idx2);
                textLabel2.Text = part4.Substring(idx2) + part5;
                linesComplete = true;
            }

            textLabel.Show();
            textLabel2.Show();
            nameLabel.Show();
            nameLabel.BringToFront();
            textLabel2.BringToFront();
            nameLabel.Text = NPCname + ":";
            textLabel.BringToFront();


            if (interact == true && linesComplete == true && timer > 60 && !blockCutScene)
            {
                textMode = false;
                secondline = false;
                linesComplete = false;

                textLabel.Location = new Point(160, 440);
                textLabel.Hide();
                textLabel2.Hide();
                nameLabel.Hide();
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "text")
                    {
                        x.Hide();
                    }
                }
                MainGameTimer.Start();
                TextTimer.Stop();
            }

            if (interact == true && linesComplete == true && timer > 60 && blockCutScene)
            {
                textMode = false;
                secondline = false;
                linesComplete = false;

                textLabel.Location = new Point(160, 440);
                textLabel.Hide();
                textLabel2.Hide();
                nameLabel.Hide();
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "text")
                    {
                        x.Hide();
                    }
                }
                blockCutScene = false;
                timer = 0;
                cutSceneTimer.Start();
                TextTimer.Stop();
            }
        }

        private void SignTimer_Tick(object sender, EventArgs e)
        {
            nameLabel.Text = NPCtext;
            nameLabel.Show();
            nameLabel.BringToFront();
            textLabel.Text = " ";
            textLabel2.Text = " ";

            if (interact == true && timer > 20)
            {
                signMode = false;

                nameLabel.Hide();
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "text")
                    {
                        x.Hide();
                    }
                }
                MainGameTimer.Start();
                interact = false;
                SignTimer.Stop();
            }
        }

        private void BallTimer_Tick(object sender, EventArgs e)
        {
            nameLabel.Text = NPCtext;
            nameLabel.Show();
            nameLabel.BringToFront();
            textLabel.Text = " ";
            textLabel2.Text = " ";
            yesLabel.Show();
            noLabel.Show();
            yesLabel.BringToFront();
            noLabel.BringToFront();

            arrow.Show();

            arrow.BringToFront();

            if (goRight)
            {
                arrow.Left = 360;
            }
            if (goLeft)
            {
                arrow.Left = 80;
            }

            pokemonSelectBack.Location = new Point(160, 40);
            pokemonSelect.Location = new Point(180, 60);
            pokemonSelectBack.BringToFront();
            pokemonSelect.BringToFront();

            if (activate == false)
            {
                timer = 0;
                activate = true;
            }

            if (NPCname == "Charmander")
            {
                pokemonSelect.Image = Properties.Resources.battleCharmander;
            }
            if (NPCname == "Bulbasaur")
            {
                pokemonSelect.Image = Properties.Resources.battleBulbasaur;
            }
            if (NPCname == "Squirtle")
            {
                pokemonSelect.Image = Properties.Resources.battleSquirtle;
            }

            if (interact && arrow.Left == 80 && activate == true && timer > 50)
            {
                ballMode = false;
                MyPokemonOne.Name = NPCname;
                MyPokemonOne.Type = Poketype;
                firstPokemon = true;

                nameLabel.Hide();
                yesLabel.Hide();
                noLabel.Hide();
                arrow.Location = new Point(340, 40);
                arrow.Hide();
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "text")
                    {
                        x.Hide();
                    }
                    if (x is PictureBox && x.Tag == "ball")
                    {
                        if (x.Bounds.IntersectsWith(upCheck.Bounds))
                        {
                            x.Hide();
                        }
                    }
                }
                pokemonSelectBack.Location = new Point(2000, 2000);
                pokemonSelect.Location = new Point(2000, 2000);

                activate = false;
                interact = false;
                MainGameTimer.Start();
                BallTimer.Stop();
            }

            if (interact && arrow.Left == 360 && activate == true && timer > 20)
            {
                ballMode = false;
                nameLabel.Hide();
                arrow.Location = new Point(340, 40);
                arrow.Hide();
                yesLabel.Hide();
                noLabel.Hide();
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag == "text")
                    {
                        x.Hide();
                    }
                }
                activate = false;
                pokemonSelectBack.Location = new Point(2000, 2000);
                pokemonSelect.Location = new Point(2000, 2000);
                interact = false;
                MainGameTimer.Start();
                BallTimer.Stop();
            }
        }

        private void spawnNPC(string name, int X, int Y, int id, string text)
        {
            NPC npc = new NPC(name, X, Y, id, text, ref timer);
            npc.Create(this);
        }
        private void spawnball(string name, int ID, string type, int X, int Y)
        {
            pokeball ball = new pokeball(name, type, X, Y, ref timer);
            ball.Create(this);
        }

        private void encounter()
        {
            Random rnd = new Random();

            string name = "";
            string type = "";
            int attack = 0;
            int defense = 0;
            int speed = 0;
            int HP = 0;
            int level = 0;
            Debug.WriteLine(ID);

            if ((ID == 1) || (ID == 3))
            {
                name = "Pidgey";
                type = "flying";
                attack = rnd.Next(1, 3);
                defense = rnd.Next(1, 3);
                speed = rnd.Next(2, 4);
                HP = rnd.Next(25, 31);
                level = rnd.Next(2, 7);
                wildPokemon.Image = Properties.Resources.battlePidgey;

            }
            if ((ID == 2) || (ID == 4))
            {
                name = "Rattata";
                type = "normal";
                attack = rnd.Next(3, 5);
                defense = rnd.Next(0, 2);
                speed = rnd.Next(4, 7);
                HP = rnd.Next(20, 25);
                level = rnd.Next(2, 6);
                wildPokemon.Image = Properties.Resources.battleRattata;
            }
            if (ID == 5)
            {
                name = "Pikachu";
                type = "electric";
                attack = rnd.Next(5, 7);
                defense = rnd.Next(3, 5);
                speed = rnd.Next(5, 8);
                HP = rnd.Next(25, 35);
                level = rnd.Next(3, 7);
                wildPokemon.Image = Properties.Resources.battlePikachu;
            }
            if (ID == 6)
            {
                name = "Slowpoke";
                type = "water";
                attack = rnd.Next(3, 6);
                defense = rnd.Next(4, 6);
                speed = rnd.Next(1, 2);
                HP = rnd.Next(30, 40);
                level = rnd.Next(3, 5);
                wildPokemon.Image = Properties.Resources.battleSlowpoke;
            }
            if (ID == 7)
            {
                name = "Psyduck";
                type = "water";
                attack = rnd.Next(3, 5);
                defense = rnd.Next(3, 5);
                speed = rnd.Next(3, 5);
                HP = rnd.Next(25, 30);
                level = rnd.Next(4, 8);
                wildPokemon.Image = Properties.Resources.battlePsyduck;
            }
            if (ID == 8)
            {
                name = "Magikarp";
                type = "water";
                attack = rnd.Next(0, 1);
                defense = rnd.Next(0, 1);
                speed = rnd.Next(0, 1);
                HP = rnd.Next(15, 25);
                level = rnd.Next(6, 10);
                wildPokemon.Image = Properties.Resources.battleMagikarp;
            }


            Wild.Name = name;
            Wild.Type = type;
            Wild.Attack = attack + level;
            Wild.Defense = defense + level;
            Wild.Speed = speed + level;
            Wild.Health = HP + level;
            Wild.CurrentHP = HP + level;
            Wild.Level = level;

            wildHealth.Maximum = Wild.Health;
            wildHealth.Value = Wild.CurrentHP;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "battle")
                {
                    x.Left = x.Left - 600;
                }
            }

            wildHealth.Left = wildHealth.Left - 600;
            playerHealth.Left = playerHealth.Left - 600;

            wildName.Show();
            wildHP.Show();
            wildLevel.Show();
            playerName.Show();
            playerHP.Show();
            playerLevel.Show();
            battleLabel1.Show();
            battleLabel2.Show();
            battleLabel3.Show();
            battleLabel4.Show();

            background.Dock = DockStyle.Fill;
            background.BringToFront();
            battleBox1.BringToFront();
            wildName.BringToFront();
            wildHealth.BringToFront();
            wildLevel.BringToFront();
            wildHP.BringToFront();
            wildPokemon.BringToFront();
            battleBox2.BringToFront();
            playerName.BringToFront();
            playerHealth.BringToFront();
            playerLevel.BringToFront();
            playerHP.BringToFront();
            playerPokemon.BringToFront();
            battleBox.BringToFront();
            battleLabel1.BringToFront();
            battleLabel3.BringToFront();
            battleLabel2.BringToFront();
            battleLabel4.BringToFront();

            arrow.Location = new Point(40, 400);
            arrow.Show();
            arrow.BringToFront();
            timer = 0;
            encounterAnimation.Show();
            encounterAnimation.Image = Properties.Resources.encounter;
            encounterAnimation.BringToFront();
            encounterAnimation.Dock = DockStyle.Fill;
            battleMode = true;

        }

        private void spawnNPCstatic(string name, int X, int Y, int id, string text)
        {
            NPCstatic npc = new NPCstatic(name, X, Y, id, text, ref timer);
            npc.Create(this);
        }

        private void spawnSign(string text, int X, int Y)
        {
            Sign sign = new Sign(text, X, Y, ref timer);
            sign.Create(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            encounterAnimation.Hide();


            TextTimer.Stop();
            textLabel.Hide();
            textLabel.Text = null;
            textLabel2.Hide();
            nameLabel.Hide();

            arrow.Hide();
            menuLabel1.Hide();
            menuLabel2.Hide();
            menuLabel3.Hide();

            pokemonOneName.Hide();
            pokemonTwoName.Hide();
            pokemonThreeName.Hide();

            pokemonOneHP.Hide();
            pokemonTwoHP.Hide();
            pokemonThreeHP.Hide();

            pokemonOneLevel.Hide();
            pokemonTwoLevel.Hide();
            pokemonThreeLevel.Hide();

            pokemonOneHealth.Hide();
            pokemonTwoHealth.Hide();
            pokemonThreeHealth.Hide();

            pokemonMenuBack.Hide();

            yesLabel.Hide();
            noLabel.Hide();

            wildName.Hide();
            wildHP.Hide();
            wildLevel.Hide();
            playerName.Hide();
            playerHP.Hide();
            playerLevel.Hide();
            battleLabel1.Hide();
            battleLabel2.Hide();
            battleLabel3.Hide();
            battleLabel4.Hide();

            blackOutLabel.Hide();

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Font\PKMN RBYGSC.ttf"));
            foreach (Control x in this.Controls)
            {
                if (x is Label && x.Tag != "nameLabel" && x.Tag != "blackOut" && x.Tag != "mainMenu")
                {
                    x.Font = new Font(pfc.Families[0], 20, FontStyle.Regular);
                }
                if (x is Label && x.Tag == "nameLabel")
                {
                    x.Font = new Font(pfc.Families[0], 16, FontStyle.Regular);
                }
                if (x is Label && x.Tag == "blackOut")
                {
                    x.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
                }
                if (x is Label && x.Tag == "mainMenu")
                {
                    x.Font = new Font(pfc.Families[0], 8, FontStyle.Regular);
                }
            }

            kevin = new NPC("Kevin", 360, 480, 1, "hey, I often pull attractive women, they love my rolls of fat", ref timer);
            kevin.Create(this);
            mum = new NPC(" Mum", 280, -600, 3, "Our furniture is so bad, the bed is too small for me and Kevin", ref timer);
            mum.Create(this);
            bruce = new NPC("Bruce", 1280, 1680, 5, "I have two brothers who sit at the end of this route, say hi!", ref timer);
            bruce.Create(this);

            jeff = new NPCstatic("Jeff", 920, 1320, 2, "Go to professor tree for a pokemon!", ref timer);
            jeff.Create(this);

            spawnNPCstatic("Tree", 1120, 5760, 1, "Hello, Pick one of these pokemans for me");

            spawnNPCstatic("Shane", 2000, 200, 3, "You cant go up here, this area hasnt been coded!");
            spawnNPCstatic("Seth", 2040, 200, 3, "Talk to my brother about why this fence is here");


            spawnSign("Mums House", 640, 480);
            spawnSign("The Locked House", 1000, 480);
            spawnSign("Professors Pokemon Lab", 960, 1080);

            spawnball("Charmander", 3, "fire", 1240, 5760);
            spawnball("Squirtle", 2, "water", 1280, 5760);
            spawnball("Bulbasaur", 1, "grass", 1320, 5760);
            spawnball("Bulbasaur", 1, "grass", 1320, 5760);

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                {
                    x.Top = x.Top + 1040;
                    x.Left = x.Left - 240;
                    Application.DoEvents();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer++;
            //Debug.WriteLine(timer);
        }

        private void instantTimer_Tick(object sender, EventArgs e)
        {
            inGrass = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "grass")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        inGrass = true;
                    }
                }
            }
            if (inGrass)
            {
                playerGrass.Location = new Point(280, 300);
            }
            if (!inGrass)
            {
                playerGrass.Location = new Point(5000, 5000);
            }


            if (MyPokemonOne.Name == "Charmander")
            {
                pokemonOne.Image = Properties.Resources.charmander;
                playerPokemon.Image = Properties.Resources.battleCharmander2;
            }
            if (MyPokemonOne.Name == "Squirtle")
            {
                pokemonOne.Image = Properties.Resources.squirtle;
                playerPokemon.Image = Properties.Resources.battleSquirtle2;
            }
            if (MyPokemonOne.Name == "Bulbasaur")
            {
                pokemonOne.Image = Properties.Resources.bulbasaur;
                playerPokemon.Image = Properties.Resources.battleBulbasaur2;
            }
            if (MyPokemonOne.Name == "Rattata")
            {
                pokemonOne.Image = Properties.Resources.rattata;
                playerPokemon.Image = Properties.Resources.battleRattata2;
            }
            if (MyPokemonOne.Name == "Pidgey")
            {
                pokemonOne.Image = Properties.Resources.pidgey;
                playerPokemon.Image = Properties.Resources.battlePidgey2;
            }
            if (MyPokemonOne.Name == "Pikachu")
            {
                pokemonOne.Image = Properties.Resources.pikachu;
                playerPokemon.Image = Properties.Resources.battlepikachu2;
            }
            if (MyPokemonOne.Name == "Psyduck")
            {
                pokemonOne.Image = Properties.Resources.psyduck;
                playerPokemon.Image = Properties.Resources.battlePsyduck2;
            }
            if (MyPokemonOne.Name == "Magikarp")
            {
                pokemonOne.Image = Properties.Resources.magikarp;
                playerPokemon.Image = Properties.Resources.battleMagikarp2;
            }
            if (MyPokemonOne.Name == "Slowpoke")
            {
                pokemonOne.Image = Properties.Resources.slowpoke;
                playerPokemon.Image = Properties.Resources.battleSlowpoke2;
            }



            if (MyPokemonTwo.Name == "Charmander")
            {
                pokemonTwo.Image = Properties.Resources.charmander;
            }
            if (MyPokemonTwo.Name == "Squirtle")
            {
                pokemonTwo.Image = Properties.Resources.squirtle;
            }
            if (MyPokemonTwo.Name == "Bulbasaur")
            {
                pokemonTwo.Image = Properties.Resources.bulbasaur;
            }
            if (MyPokemonTwo.Name == "Rattata")
            {
                pokemonTwo.Image = Properties.Resources.rattata;
            }
            if (MyPokemonTwo.Name == "Pidgey")
            {
                pokemonTwo.Image = Properties.Resources.pidgey;
            }
            if (MyPokemonTwo.Name == "Pikachu")
            {
                pokemonTwo.Image = Properties.Resources.pikachu;
            }
            if (MyPokemonTwo.Name == "Psyduck")
            {
                pokemonTwo.Image = Properties.Resources.psyduck;
            }
            if (MyPokemonTwo.Name == "Magikarp")
            {
                pokemonTwo.Image = Properties.Resources.magikarp;
            }
            if (MyPokemonTwo.Name == "Slowpoke")
            {
                pokemonTwo.Image = Properties.Resources.slowpoke;
            }

            if (MyPokemonThree.Name == "Charmander")
            {
                pokemonThree.Image = Properties.Resources.charmander;
            }
            if (MyPokemonThree.Name == "Squirtle")
            {
                pokemonThree.Image = Properties.Resources.squirtle;
            }
            if (MyPokemonThree.Name == "Bulbasaur")
            {
                pokemonThree.Image = Properties.Resources.bulbasaur;
            }
            if (MyPokemonThree.Name == "Rattata")
            {
                pokemonThree.Image = Properties.Resources.rattata;
            }
            if (MyPokemonThree.Name == "Pidgey")
            {
                pokemonThree.Image = Properties.Resources.pidgey;
            }
            if (MyPokemonThree.Name == "Pikachu")
            {
                pokemonThree.Image = Properties.Resources.pikachu;
            }
            if (MyPokemonThree.Name == "Psyduck")
            {
                pokemonThree.Image = Properties.Resources.psyduck;
            }
            if (MyPokemonThree.Name == "Magikarp")
            {
                pokemonThree.Image = Properties.Resources.magikarp;
            }
            if (MyPokemonThree.Name == "Slowpoke")
            {
                pokemonThree.Image = Properties.Resources.slowpoke;
            }


            pokemonOneName.Text = MyPokemonOne.Name;
            pokemonTwoName.Text = MyPokemonTwo.Name;
            pokemonThreeName.Text = MyPokemonThree.Name;

            pokemonOneHP.Text = MyPokemonOne.CurrentHP + "/" + MyPokemonOne.Health;
            pokemonTwoHP.Text = MyPokemonTwo.CurrentHP + "/" + MyPokemonTwo.Health;
            pokemonThreeHP.Text = MyPokemonThree.CurrentHP + "/" + MyPokemonThree.Health;

            pokemonOneLevel.Text = "Lv" + MyPokemonOne.Level;
            pokemonTwoLevel.Text = "Lv" + MyPokemonTwo.Level;
            pokemonThreeLevel.Text = "Lv" + MyPokemonThree.Level;

            pokemonOneHealth.Maximum = MyPokemonOne.Health;
            pokemonOneHealth.Value = Convert.ToInt32(MyPokemonOne.CurrentHP);
            pokemonTwoHealth.Maximum = MyPokemonTwo.Health;
            pokemonTwoHealth.Value = Convert.ToInt32(MyPokemonTwo.CurrentHP);
            pokemonThreeHealth.Maximum = MyPokemonThree.Health;
            pokemonThreeHealth.Value = Convert.ToInt32(MyPokemonThree.CurrentHP);

            wildHP.Text = Wild.CurrentHP + "/" + Wild.Health;
            wildLevel.Text = "Lv" + Wild.Level;
            wildName.Text = Wild.Name;
            wildHealth.Value = Wild.CurrentHP;

            playerHP.Text = MyPokemonOne.CurrentHP + "/" + MyPokemonOne.Health;
            playerLevel.Text = "Lv" + MyPokemonOne.Level;
            playerName.Text = MyPokemonOne.Name;
            playerHealth.Maximum = MyPokemonOne.Health;
            playerHealth.Value = MyPokemonOne.CurrentHP;

            pokemonOneHealth.Value = MyPokemonOne.CurrentHP;
            pokemonOneHealth.Maximum = MyPokemonOne.Health;

            MyPokemonOne.MaxXP = MyPokemonOne.Level * 200;

            if (MyPokemonThree.Name != "")
            {
                fullParty = true;
            }
        }

        private void MenuTimer_Tick(object sender, EventArgs e)
        {
            menu.Dock = DockStyle.Right;
            menu.BringToFront();
            menuLabel1.BringToFront();
            menuLabel2.BringToFront();
            menuLabel3.BringToFront();
            menuLabel1.Show();
            menuLabel2.Show();
            menuLabel3.Show();
            arrow.Show();
            arrow.BringToFront();



            if (goUp && arrow.Top > 40)
            {
                arrow.Top = arrow.Top - 80;
            }
            if (goDown && arrow.Top < 200)
            {
                arrow.Top = arrow.Top + 80;
            }

            if (arrow.Top == 200 && interact == true)
            {
                arrow.Hide();
                menuLabel1.Hide();
                menuLabel2.Hide();
                menuLabel3.Hide();
                menu.Dock = DockStyle.None;
                MainGameTimer.Start();
                MenuTimer.Stop();
            }

            if (arrow.Top == 40 && interact == true && firstPokemon)
            {

                pokemonMenu.Dock = DockStyle.Fill;
                pokemonMenu.BringToFront();
                pokemonOneName.Show();
                pokemonTwoName.Show();
                pokemonThreeName.Show();
                pokemonOneHP.Show();
                pokemonTwoHP.Show();
                pokemonThreeHP.Show();
                pokemonOneLevel.Show();
                pokemonTwoLevel.Show();
                pokemonThreeLevel.Show();
                pokemonOneHealth.Show();
                pokemonTwoHealth.Show();
                pokemonThreeHealth.Show();
                pokemonMenuBack.Show();
                pokemonOne.Location = new Point(80, 80);
                pokemonTwo.Location = new Point(80, 240);
                pokemonThree.Location = new Point(80, 400);
                menuMode = false;
                menu.Dock = DockStyle.None;
                pokemonMenuBack.BringToFront();
                arrow.Location = new Point(40, 100);
                pokemonMenuTimer.Start();
                MenuTimer.Stop();
            }
        }

        private void pokemonMenuTimer_Tick(object sender, EventArgs e)
        {

            pokemonOne.BringToFront();
            pokemonTwo.BringToFront();
            pokemonThree.BringToFront();
            pokemonOneName.BringToFront();
            pokemonTwoName.BringToFront();
            pokemonThreeName.BringToFront();
            pokemonOneHP.BringToFront();
            pokemonTwoHP.BringToFront();
            pokemonThreeHP.BringToFront();
            pokemonOneLevel.BringToFront();
            pokemonTwoLevel.BringToFront();
            pokemonThreeLevel.BringToFront();
            pokemonOneHealth.BringToFront();
            pokemonTwoHealth.BringToFront();
            pokemonThreeHealth.BringToFront();

            arrow.BringToFront();

            if (MyPokemonOne.Name == "")
            {
                pokemonOneName.Hide();
                pokemonOneHP.Hide();
                pokemonOneLevel.Hide();
                pokemonOneHealth.Hide();
                pokemonOne.Image = null;
            }
            if (MyPokemonTwo.Name == "")
            {
                pokemonTwoName.Hide();
                pokemonTwoHP.Hide();
                pokemonTwoLevel.Hide();
                pokemonTwoHealth.Hide();
                pokemonOne.Image = null;
            }
            if (MyPokemonThree.Name == "")
            {
                pokemonThreeName.Hide();
                pokemonThreeHP.Hide();
                pokemonThreeLevel.Hide();
                pokemonThreeHealth.Hide();
                pokemonThree.Image = null;
            }


            if (goUp && arrow.Top > 100 && arrowLocation == false)
            {
                arrow.Top = arrow.Top - 160;
            }
            if (goDown && arrow.Top < 420 && arrowLocation == false)
            {
                arrow.Top = arrow.Top + 160;
            }
            if (arrow.Top == 420 && arrowLocation == false)
            {
                goDown = false;
                arrowLocation = true;
            }
            if (goDown && arrowLocation == true && arrow.Top == 420)
            {
                arrow.Top = 500;
            }
            if (goUp && arrowLocation == true)
            {
                arrow.Top = 420;
                arrowLocation = false;
            }

            if (MyPokemonThree.Name != "" && firstPokemon == true)
            {
                if (arrow.Top == 100 && interact && switching == false)
                {
                    switchPokemon = (pokemon)MyPokemonOne.Clone();
                    swapNumber = 1;
                    arrow.Image = Properties.Resources.switchArrow;
                    switching = true;
                    interact = false;
                }
                if (arrow.Top == 260 && interact && switching == false)
                {
                    switchPokemon = (pokemon)MyPokemonTwo.Clone();
                    swapNumber = 2;
                    arrow.Image = Properties.Resources.switchArrow;
                    switching = true;
                    interact = false;
                }
                if (arrow.Top == 420 && interact && switching == false)
                {
                    switchPokemon = (pokemon)MyPokemonThree.Clone();
                    swapNumber = 3;
                    arrow.Image = Properties.Resources.switchArrow;
                    switching = true;
                    interact = false;
                }
                if (arrow.Top == 100 && interact && switching == true)
                {
                    if (swapNumber == 1)
                    {
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 2)
                    {
                        MyPokemonTwo = (pokemon)MyPokemonOne.Clone();
                        MyPokemonOne = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 3)
                    {
                        MyPokemonThree = (pokemon)MyPokemonOne.Clone();
                        MyPokemonOne = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    arrow.Image = Properties.Resources.arrow;
                }
                if (arrow.Top == 260 && interact && switching == true)
                {
                    if (swapNumber == 1)
                    {
                        MyPokemonOne = (pokemon)MyPokemonTwo.Clone();
                        MyPokemonTwo = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 2)
                    {
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 3)
                    {
                        MyPokemonThree = (pokemon)MyPokemonTwo.Clone();
                        MyPokemonTwo = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    arrow.Image = Properties.Resources.arrow;
                }
                if (arrow.Top == 420 && interact && switching == true)
                {
                    if (swapNumber == 1)
                    {
                        MyPokemonOne = (pokemon)MyPokemonThree.Clone();
                        MyPokemonThree = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 2)
                    {
                        MyPokemonTwo = (pokemon)MyPokemonThree.Clone();
                        MyPokemonThree = (pokemon)switchPokemon.Clone();
                        switching = false;
                        interact = false;
                    }
                    if (swapNumber == 3)
                    {
                        switching = false;
                        interact = false;
                    }
                    arrow.Image = Properties.Resources.arrow;
                }

            }

            if (arrow.Top == 500 && interact)
            {
                pokemonOneName.Hide();
                pokemonTwoName.Hide();
                pokemonThreeName.Hide();
                pokemonOneHP.Hide();
                pokemonTwoHP.Hide();
                pokemonThreeHP.Hide();
                pokemonOneLevel.Hide();
                pokemonTwoLevel.Hide();
                pokemonThreeLevel.Hide();
                pokemonOneHealth.Hide();
                pokemonTwoHealth.Hide();
                pokemonThreeHealth.Hide();
                pokemonMenu.Dock = DockStyle.None;
                pokemonMenuBack.Hide();

                pokemonOne.Location = new Point(20000, 20000);
                pokemonTwo.Location = new Point(20000, 20000);
                pokemonThree.Location = new Point(20000, 20000);

                arrow.Location = new Point(340, 40);
                menu.Dock = DockStyle.Right;
                menuLabel1.Show();
                menuLabel2.Show();
                menuLabel3.Show();
                arrow.Show();

                arrowLocation = false;
                arrow.Image = Properties.Resources.arrow;
                switching = false;

                MenuTimer.Start();
                pokemonMenuTimer.Stop();
            }
        }

        private void BattleTimer_Tick(object sender, EventArgs e)
        {
            menuMode = false;

            if (goLeft && arrow.Left > 40)
            {
                arrow.Left = arrow.Left - 280;
            }
            if (goRight && arrow.Left < 320)
            {
                arrow.Left = arrow.Left + 280;
            }
            if (goUp && arrow.Top > 400)
            {
                arrow.Top = arrow.Top - 80;
            }
            if (goDown && arrow.Top < 480)
            {
                arrow.Top = arrow.Top + 80;
            }

            if (arrow.Top == 480 && arrow.Left == 320 && interact == true)
            {
                interact = false;
                arrow.Hide();
                battleLabel1.Text = "Got away safely.";
                run = true;
                battleLabel2.Hide();
                battleLabel3.Hide();
                battleLabel4.Hide();
                timer = 0;
            }

            if (timer < 100 && timer > 80 && run)
            {
                endBattle();
            }

            if (arrow.Top == 400 && arrow.Left == 40 && interact)
            {
                interact = false;

                arrow.Location = new Point(1111111, 1111111);
                if (MyPokemonOne.Speed >= Wild.Speed)
                {
                    attack();
                }
                else
                {
                    wildAttack();
                }
            }

            if (timer < 60 && attacking == true && wildAttacking == false && Wild.CurrentHP != 0)
            {
                interact = false;
                arrow.Top = 1000;
                battleLabel1.Text = MyPokemonOne.Name + " used";
                battleLabel3.Text = "their attack";
                battleLabel2.Hide();
                battleLabel4.Hide();
            }

            if (timer >= 60 && attacking == true && wildAttacking == false && Wild.CurrentHP != 0)
            {
                interact = false;
                attacking = false;
                attackSum = attackSum + 1;
                if (attackSum < 2 && Wild.CurrentHP != 0)
                {
                    wildAttack();
                }
                else
                {
                    attackSum = 0;
                    wildAttacking = false;
                    attacking = false;
                    arrow.Top = 400;
                    battleLabel1.Text = "FIGHT";
                    battleLabel3.Text = "CATCH";
                    arrow.Location = new Point(40, 400);
                    battleLabel2.Show();
                    battleLabel4.Show();
                }
            }

            if (timer < 60 && wildAttacking == true && attacking == false && Wild.CurrentHP != 0)
            {
                interact = false;
                arrow.Top = 1000;
                battleLabel1.Text = Wild.Name + " used";
                battleLabel3.Text = "their attack";
                battleLabel2.Hide();
                battleLabel4.Hide();
            }
            if (timer >= 60 && wildAttacking == true && attacking == false && Wild.CurrentHP != 0)
            {
                interact = false;
                wildAttacking = false;
                attackSum = attackSum + 1;
                if (attackSum < 2 && MyPokemonOne.CurrentHP != 0)
                {
                    attack();
                }
                else
                {
                    attackSum = 0;
                    wildAttacking = false;
                    attacking = false;
                    arrow.Top = 400;
                    arrow.Location = new Point(40, 400);
                    battleLabel1.Text = "FIGHT";
                    battleLabel3.Text = "CATCH";
                    battleLabel2.Show();
                    battleLabel4.Show();
                }

            }

            if (Wild.CurrentHP == 0 && wildFaint == false)
            {
                interact = false;
                arrow.Location = new Point(4000, 40000);
                arrow.Hide();
                battleLabel1.Text = " Foe " + Wild.Name + " fainted";
                battleLabel3.Hide();
                battleLabel2.Hide();
                battleLabel4.Hide();
                timer = 0;
                wildFaint = true;
            }

            if (wildFaint == true && timer > 90 && timer < 110)
            {
                interact = false;
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Show();
                battleLabel1.Text = MyPokemonOne.Name + " gained " + Wild.Level * 40;
                battleLabel3.Text = "exp";
                MyPokemonOne.EXP = MyPokemonOne.EXP + (Wild.Level * 40);
                if (MyPokemonOne.EXP >= MyPokemonOne.MaxXP)
                {
                    levelUp = true;
                }

            }

            if (levelUp == true && timer > 140 && timer < 150)
            {
                interact = false;
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Hide();
                battleLabel1.Text = MyPokemonOne.Name + " levelled up! ";
                MyPokemonOne.Level = MyPokemonOne.Level + 1;
                levellingUp();
            }

            if ((wildFaint == true && timer > 160 && timer < 180 && levelUp == false) || (wildFaint == true && timer > 230 && timer < 250 && levelUp == true))
            {
                interact = false;
                levelUp = false;
                wildFaint = false;
                endBattle();
            }

            if (MyPokemonOne.CurrentHP == 0 && playerFaint == false && attacking == false && wildAttacking == false)
            {
                interact = false;
                arrow.Location = new Point(40000, 40000);
                arrow.Hide();
                battleLabel1.Text = MyPokemonOne.Name + " fainted";
                battleLabel3.Hide();
                battleLabel2.Hide();
                battleLabel4.Hide();
                timer = 0;
                playerFaint = true;
            }

            if (playerFaint == true && timer > 120 && timer < 140 && MyPokemonTwo.CurrentHP != 0)
            {
                interact = false;
                pokemon swapPokemon = (pokemon)MyPokemonOne.Clone();
                MyPokemonOne = (pokemon)MyPokemonTwo.Clone();
                MyPokemonTwo = (pokemon)swapPokemon.Clone();
                battleLabel1.Text = "FIGHT";
                battleLabel3.Show();
                battleLabel2.Show();
                battleLabel4.Show();
                arrow.Location = new Point(40, 400);
                arrow.Show();
                playerFaint = false;

            }
            else if (playerFaint == true && timer > 120 && timer < 140 && MyPokemonThree.CurrentHP != 0)
            {
                //
                interact = false;
                //
                pokemon swapPokemon = (pokemon)MyPokemonOne.Clone();
                MyPokemonOne = (pokemon)MyPokemonThree.Clone();
                MyPokemonThree = (pokemon)swapPokemon.Clone();
                battleLabel1.Text = "FIGHT";
                battleLabel3.Show();
                battleLabel2.Show();
                battleLabel4.Show();
                arrow.Location = new Point(40, 400);
                arrow.Show();
                playerFaint = false;
            }
            else if (playerFaint == true && timer > 120 && timer < 140)
            {
                interact = false;
                battleLabel1.Text = "You are out of";
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Show();
                battleLabel3.Text = "usable pokemon";
            }
            if (playerFaint == true && timer > 240 && timer < 260)
            {
                interact = false;
                battleLabel1.Text = "You blacked out";
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Hide();
            }

            if (playerFaint == true && timer > 340 && timer < 360)
            {
                interact = false;
                black1.Dock = DockStyle.Fill;
                black1.BringToFront();
                blackOutLabel.Show();
                blackOutLabel.BringToFront();
            }

            if (playerFaint == true && timer > 500)
            {
                interact = false;
                endBattle();
                blackOut();
                playerFaint = false;
            }



            if (arrow.Left == 40 && arrow.Top == 480 && interact)
            {
                interact = false;
                arrow.Top = arrow.Top + 1000;
                if (fullParty == true)
                {
                    partyCheck = true;
                    timer = 0;
                    battleLabel1.Text = "Your party is full.";
                    battleLabel2.Hide();
                    battleLabel4.Hide();
                    battleLabel3.Hide();
                }
                else
                {
                    pokeball.Show();
                    pokeball.BringToFront();
                    pokeballTimer.Start();
                }
            }

            if (captureMode == true && timer > 0 && timer < 20)
            {
                interact = false;
                arrow.Location = new Point(40000, 40000);
                battleLabel1.Text = Wild.Name + " was captured !";
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Hide();
            }

            if (captureMode == true && timer > 110 && timer < 130)
            {
                interact = false;
                battleLabel1.Text = Wild.Name + " was added to";
                battleLabel2.Hide();
                battleLabel4.Hide();
                battleLabel3.Show();
                battleLabel3.Text = "your party";
            }

            if (captureMode == true && timer > 210 && timer < 230 && MyPokemonTwo.Name == "")
            {
                MyPokemonTwo = (pokemon)Wild.Clone();
                endBattle();
            }
            else if (captureMode == true && timer > 210 && timer < 230)
            {
                MyPokemonThree = (pokemon)Wild.Clone();
                endBattle();
            }

            if (partyCheck == true && timer > 90 && timer < 110)
            {
                interact = false;
                battleLabel1.Text = "FIGHT";
                arrow.Top = arrow.Top - 1000;
                partyCheck = false;
                battleLabel2.Show();
                battleLabel4.Show();
                battleLabel3.Show();
            }
        }

        private void blackOut()
        {
            int x;
            int y;
            int x2;
            int y2;
            int movex;
            int movey;
            x = bed1.Left + 40;
            y = bed1.Top;
            x2 = player.Left;
            y2 = player.Top;
            movex = x - x2;
            movey = y - y2;
            foreach (Control z in this.Controls)
            {
                if (z is PictureBox && z.Tag != "player" && z.Tag != "text" && z.Tag != "battle")
                {
                    z.Top = z.Top - movey;
                    z.Left = z.Left - movex;
                    Application.DoEvents();
                }

            }

            MyPokemonOne.CurrentHP = MyPokemonOne.Health;
            MyPokemonTwo.CurrentHP = MyPokemonTwo.Health;
            MyPokemonThree.CurrentHP = MyPokemonThree.Health;

        }
        private void levellingUp()
        {
            MyPokemonOne.Attack = MyPokemonOne.Attack + 1;
            MyPokemonOne.Defense = MyPokemonOne.Defense + 1;
            MyPokemonOne.Speed = MyPokemonOne.Speed + 1;
            MyPokemonOne.CurrentHP = MyPokemonOne.CurrentHP + 3;
            MyPokemonOne.Health = MyPokemonOne.Health + 3;
            MyPokemonOne.MaxXP = Convert.ToInt32(Math.Round(MyPokemonOne.MaxXP * 1.5));
            MyPokemonOne.EXP = 0;
        }
        private void endBattle()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "battle")
                {
                    x.Left = x.Left + 600;
                }

            }

            wildHealth.Left = wildHealth.Left + 600;
            playerHealth.Left = playerHealth.Left + 600;
            black1.Dock = DockStyle.None;
            blackOutLabel.Hide();
            wildName.Hide();
            wildHP.Hide();
            wildLevel.Hide();
            playerName.Hide();
            playerHP.Hide();
            playerLevel.Hide();
            battleLabel1.Hide();
            battleLabel2.Hide();
            battleLabel3.Hide();
            battleLabel4.Hide();
            background.Dock = DockStyle.None;

            arrow.Location = new Point(340, 40);
            arrow.Hide();

            battleLabel1.Text = "FIGHT";
            battleLabel3.Text = "CATCH";

            wildPokemon.Show();

            pokeball.Location = new Point(800, 200);

            attackSum = 0;
            wildAttacking = false;
            attacking = false;
            battleMode = false;
            run = false;
            captureMode = false;
            encountering = false;
            MainGameTimer.Start();
            BattleTimer.Stop();
        }

        private void attack()
        {
            Random rndno = new Random();
            int rand = rndno.Next(0, 3);
            double x = ((2 * Wild.Level) / 5) + 2;
            double y = (MyPokemonOne.Attack / Wild.Defense);
            if (Wild.CurrentHP > 0)
            {
                Wild.CurrentHP = Wild.CurrentHP - ((Convert.ToInt32((Math.Round(x)) * (Math.Round(y))) + 2) + rand);
            }
            if (Wild.CurrentHP <= 0)
            {
                Wild.CurrentHP = 0;
            }
            attacking = true;
            timer = 0;
        }
        private void wildAttack()
        {
            Random rndno = new Random();
            int rand = rndno.Next(0, 3);
            double x = ((2 * MyPokemonOne.Level) / 5) + 2;
            double y = (Wild.Attack / MyPokemonOne.Defense);

            if (Wild.CurrentHP > 0)
            {
                MyPokemonOne.CurrentHP = MyPokemonOne.CurrentHP - ((Convert.ToInt32((Math.Round(x)) * (Math.Round(y))) + 2) + rand);
            }
            if (MyPokemonOne.CurrentHP <= 0)
            {
                MyPokemonOne.CurrentHP = 0;
            }
            wildAttacking = true;
            timer = 0;


        }

        private void pokeballTimer_Tick(object sender, EventArgs e)
        {
            pokeball.Top = pokeball.Top - 2;
            pokeball.Left = pokeball.Left + 4;

            if (pokeball.Bounds.IntersectsWith(wildPokemon.Bounds))
            {
                captureMode = true;
                wildPokemon.Hide();
                pokeball.Location = new Point(480, 160);
                timer = 0;
                pokeballTimer.Stop();
            }
        }

        private void infoTimer_Tick(object sender, EventArgs e)
        {
            background.Show();
            background.Dock = DockStyle.Fill;
            textBox.Show();
            textBox.Location = new Point(0, 400);
            nameLabel.Show();
            textLabel2.Show();
            background.BringToFront();
            textBox.BringToFront();
            nameLabel.BringToFront();
            textLabel2.BringToFront();
            introBox.Location = new Point(200, 40);
            introBox.BringToFront();
            skipLabel.BringToFront();


            if (interact)
            {
                skipLabel.Show();
            }
            if (skip)
            {
                introBox.Hide();
                background.Dock = DockStyle.None;
                textBox.Hide();
                nameLabel.Hide();
                textLabel2.Hide();
                introBox.Location = new Point(20000, 40000);
                skipLabel.Hide();
                MainGameTimer.Start();
                infoTimer.Stop();
            }

            if (timer < 140)
            {
                nameLabel.Text = "Hello, I am professor tree,";
                textLabel2.Text = " ";
            }
            if (timer > 150 && timer < 170)
            {
                nameLabel.Text = "And welcome to the world";
                textLabel2.Text = "of Pokemon!";
            }
            if (timer > 350 && timer < 370)
            {
                nameLabel.Text = "the tall grass is inhabited";
                textLabel2.Text = "by creatures called Pokemon";
            }
            if (timer > 550 && timer < 570)
            {
                nameLabel.Text = "to be safe we use our";
                textLabel2.Text = "own pokemon to protect us";
            }
            if (timer > 750 && timer < 770)
            {
                nameLabel.Text = "you can catch wild Pokemon";
                textLabel2.Text = "and use them for yourself.";
            }
            if (timer > 950 && timer < 970)
            {
                nameLabel.Text = "If you come see me in my lab";
                textLabel2.Text = "I will give you your very own";
            }
            if (timer > 1150 && timer < 1170)
            {
                nameLabel.Text = "You may then use your";
                textLabel2.Text = "Pokemon in battle";
            }
            if (timer > 1350 && timer < 1370)
            {
                nameLabel.Text = "Doing this will make them";
                textLabel2.Text = "stronger and happier.";
            }
            if (timer > 1550 && timer < 1570)
            {
                nameLabel.Text = "Use the 'space bar' to";
                textLabel2.Text = "interact with people";
            }
            if (timer > 1750 && timer < 1770)
            {
                nameLabel.Text = "Pressing the 'one key'";
                textLabel2.Text = "will give access to the menu";
            }
            if (timer > 1950 && timer < 1970)
            {
                nameLabel.Text = "allowing you to order ";
                textLabel2.Text = "and see your pokemon!";
            }
            if (timer > 2150 && timer < 2170)
            {
                introBox.Image = Properties.Resources.red1;
                nameLabel.Text = "I expect big things from you";
                textLabel2.Text = " ";
            }
            if (timer > 2350 && timer < 2370)
            {
                nameLabel.Text = "GoodLuck!";
                textLabel2.Text = " ";
                introBox.Image = Properties.Resources.red;
            }
            if (timer > 2540 && timer < 2560)
            {
                introBox.Hide();
                background.Dock = DockStyle.None;
                textBox.Hide();
                nameLabel.Hide();
                skipLabel.Hide();
                textLabel2.Hide();
                introBox.Location = new Point(20000, 40000);
                MainGameTimer.Start();
                infoTimer.Stop();
            }
        }

        private void MainGameMenu_Tick(object sender, EventArgs e)
        {
            skipLabel.Hide();
            background.BringToFront();
            mainMenuLabel.BringToFront();
            background.Image = Properties.Resources.title;
            background.Dock = DockStyle.Fill;

            if (interact == true)
            {
                background.Image = null;
                mainMenuLabel.Hide();
                timer = 0;
                MainGameMenu.Stop();
                interact = false;
                infoTimer.Start();
            }
        }

        private void cutSceneTimer_Tick(object sender, EventArgs e)
        {
            goDown = false;

            if (textMode == true)
            {
                TextTimer.Start();
                MainGameTimer.Stop();
            }

            if (jeff.GetPictureBoxNpc().Left != player.Left + 40 && blockCutScene)
            {
                jeff.GetPictureBoxNpc().Left = jeff.GetPictureBoxNpc().Left - 40;
                steps = steps + 1;
            }
            if (jeff.GetPictureBoxNpc().Left == player.Left + 40 && blockCutScene && !location)
            {
                timer = 0;
                location = true;
            }
            if (location == true && !interacted)
            {
                interact = true;
            }
            else
            {
                interact = false;
            }
            if (timer > 40 && timer < 60 && !blockCutScene && interacted)
            {
                player.Image = Properties.Resources.up;
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && x.Tag != "player" && x.Tag != "text" && x.Tag != "battle")
                    {

                        x.Top = x.Top + 40;


                    }
                }
                jeff.GetPictureBoxNpc().Left = jeff.GetPictureBoxNpc().Left + (40 * steps);
                interacted = false;
                location = false;
                steps = 0;
                facing = "up";
                MainGameTimer.Start();
                cutSceneTimer.Stop();
            }

        }

        private void MovementTimer_Tick_1(object sender, EventArgs e)
        {
            movementInt = movementInt + 1;
            if (movementInt == 3)
            {
                movementInt = 1;
            }
        }
    }

}

