using System;

namespace Les_3010
{
    class Monster : Hero
    {
        private double[,] monster_specs =
        {
            { 2, 2, 0, 0, 10 },
            { 2, 1, 2, 5, 5 },
            { 0, 1, 3, 0, 5 },
            { 1, 5, 2, 5, 5 },
            { 0, 1, 1, 1.5, 10 },
            { 2, 1.5, 3, 0, 5 },
            { 1, 3, 1, 1.5, 10 }
        };

        public FoodType Food;
        public readonly double FoodPointsMax;
        public bool FoodFull;

        public Gift Gift;
        public readonly double GiftPoints;

        private double foodPoints;
        public double FoodPoints 
        { 
            get => foodPoints; 
            set
            {
                if (value >= 0 && value <= FoodPointsMax)
                {
                    foodPoints = value;
                    if (value == FoodPointsMax)
                    {
                        FoodFull = true;
                    }
                }
            }
        }

        public Monster(HeroType Type)
        {
            if ((int)Type < Game.MonstersCount)
            {
                this.Type = Type;

                Food = (FoodType)monster_specs[(int)Type, 0];
                FoodPointsMax = monster_specs[(int)Type, 1];
                FoodFull = false;

                Gift = (Gift)monster_specs[(int)Type, 2];
                GiftPoints = monster_specs[(int)Type, 3];

                HeartPoints = monster_specs[(int)Type, 4];

                FoodPoints = 0;

                Alive = true;
            }
            else
            {
                throw new Exception(Game.ErrorTypeOfHeroExceptionMessage);
            }
        }
        
        public string GetStatus()
        {
            return $"{Type} hp: {HeartPoints} - {Game.ConvertAliveStatus(Alive)}";
        }
    }
}
