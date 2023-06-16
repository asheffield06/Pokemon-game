using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POKEMON_GAME
{
    class pokemon
    {
        protected string pokename;
        protected string type;
        protected int attack;
        protected int defense;
        protected int speed;
        protected int currentHP;
        protected int HP;
        protected int level;
        protected int XP;
        protected int maxXP;

        public pokemon(string n, string t, int a, int d, int s, int h, int c, int l, int x , int m)
        {
            pokename = n;
            type = t;
            attack = a;
            defense = d;
            speed = s;
            HP = h;
            currentHP = c;
            level = l;
            XP = x;
            maxXP = m;
        }

        
        public string Name
        {
            get { return pokename; }
            set { pokename = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Defense
        {
            get { return defense; }
            set { defense = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Health
        {
            get { return HP; }
            set { HP = value; }
        }

        public int CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int EXP
        {
            get { return XP; }
            set { XP = value; }
        }

        public int MaxXP 
        {
            get { return maxXP; }
            set { maxXP = value; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }




    }
}
