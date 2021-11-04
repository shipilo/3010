using System;

namespace Les_3010
{
    class Human : Hero
    {
        private const double heartPointsMax_adult = 1.0;
        private const double bloodPointsMax_adult = 2.0;
        private const double heartPointsMax_child = 0.5;
        private const double bloodPointsMax_child = 1.0;

        public double Sweets;
        private double bloodPoints;
        public bool KillAction;
        public bool Deathless;
        public double BloodPoints 
        { 
            get => bloodPoints;
            set
            {
                if (value >= 0)
                {                    
                    bloodPoints = value;
                    if (value == 0 && !Deathless)
                    {
                        Alive = false;
                    }
                }
            }
        }

        public Human(HeroType Type, int Sweets)
        {
            switch ((int)Type)
            {
                case Game.MonstersCount:
                    this.Type = Type;
                    this.Sweets = Sweets;
                    HeartPoints = heartPointsMax_adult;
                    BloodPoints = bloodPointsMax_adult;
                    KillAction = false;
                    Alive = true;
                    Deathless = false;
                    break;
                case Game.MonstersCount + 1:
                    this.Type = Type;
                    this.Sweets = Sweets;
                    HeartPoints = heartPointsMax_child;
                    BloodPoints = bloodPointsMax_child;
                    KillAction = false;
                    Alive = true;
                    Deathless = false;
                    break;
                case Game.MonstersCount + 2:
                    this.Type = Type;
                    KillAction = true;
                    Alive = true;
                    Deathless = false;
                    break;
                default:
                    throw new Exception(Game.ErrorTypeOfHeroExceptionMessage);
            }
        }

        public void ReplenishBlood(double points)
        {
            if (points >= bloodPointsMax_adult && Type == HeroType.Взрослый)
            {
                bloodPoints = bloodPointsMax_adult;
            }
            else if (points >= bloodPointsMax_child && Type == HeroType.Ребенок_)
            {
                bloodPoints = bloodPointsMax_adult;
            }
            else
            {
                bloodPoints += points;
            }        
        }

        public string GetStatus()
        {
            return $"{Type} Кофеты: {Sweets} - {Game.ConvertAliveStatus(Alive)} - hp: {HeartPoints} - blood: {bloodPoints}";
        }
    }
}
