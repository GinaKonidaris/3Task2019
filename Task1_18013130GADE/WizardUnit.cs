using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_18013130GADE
{
    class WizardUnit : Unit
    {
        public int Attack { get; internal set; }
        public int Team { get; internal set; }
        public int Xpos { get; internal set; }
        public int Ypos { get; internal set; }
        public int Health { get; internal set; }
        public string Symbol { get; internal set; }
        public override void Move(Direction d)
        { //moves unit in another direction
            switch (d)
            {
                case Direction.North:
                    {
                        ypos -= speed;
                        break;
                    }
                case Direction.East:
                    {
                        xpos += speed;
                        break;
                    }
                case Direction.West:
                    {
                        xpos -= speed;
                        break;
                    }
                case Direction.South:
                    {
                        ypos += speed;
                        break;
                    }
            }
        }
        public WizardUnit(int x, int y, int Speed, int Range, int Health, int Team, string Symbol, int Attack)
        {
            xpos -= x;
            ypos = y;
            Health = this.Health;
            Speed = speed;
            Range = range;
            Team = team;
            Symbol = this.Symbol;
            Attack = attack;
        }

        private int DistanceTo(Unit u)
        { //checks the distance between units
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit n = (MeleeUnit)u;
                int d = (xpos - n.Xpos) + Math.Abs(ypos - n.Ypos);
                return d;
            }
            else
            {
                return 0;
            }
        }

        public Direction DirectionTo(Unit u)
        {    //changes direction
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit n = (MeleeUnit)u;
                if (n.Xpos < n.Ypos)
                {
                    return Direction.North;
                }
                else if (n.Xpos > xpos)
                {
                    return Direction.South;
                }
                else if (n.Ypos < ypos)
                {
                    return Direction.West;
                }
                else
                {
                    return Direction.East;
                }
            }
            else
            {
                return Direction.North;
            }
        }
        public override void Combat(Unit u, Building b)
        {  //starts combat
            if (u.GetType() == typeof(MeleeUnit))
            {
                Health -= ((MeleeUnit)u).Attack;

            }
            else if (u.GetType() == typeof(RangedUnit))
            {
                Health -= ((RangedUnit)u).Attack;
                Health -= ((RangedUnit)u).Attack;
            }
            else if (u.GetType()== typeof(WizardUnit))
            {// will not harm wizards
                Health = ((WizardUnit)u).health;
               
            }
            else if(u.GetType()== typeof(BuildingFactory))
            { //attack will do no damage
                attack = 0;
            }
            else if(health <50)
            {//hopefully will make wizard runaway -_-
                xpos += ((WizardUnit)u).ypos;
            }
            else if (u.GetType() == typeof(BuildingFactory))
            {
                Health -= ((BuildingFactory)b).Health;
                Health -= ((BuildingFactory)b).Health;
            }

        }
        public override bool Inranged(Unit u)
        { //checks if unit is in range
            if (u.GetType() == typeof(MeleeUnit))
            {
                MeleeUnit n = (MeleeUnit)u;
                if (DistanceTo(u) <= range)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public override Unit Closest(Unit[] units)
        {   //checks for the closest unit in a block radius.
            Unit closest = this;
            int closestDistance = 100;

            foreach (Unit u in units)
            {
                if (((MeleeUnit)u).Team == team)
                {
                    if (DistanceTo(u) < closestDistance)
                    {
                        closest = u;
                        closestDistance = DistanceTo((MeleeUnit)u);
                    }
                }
            }
            return closest;
        }      
        public override bool Isdead()
        {  //checks if unit is alive
            if (Health < +0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override string Tostring()
        {   //gives information on MeleeUnit
            return "MU" + xpos + "," + ypos + "," + Health + "," + Name + team;
        }
    }
}
