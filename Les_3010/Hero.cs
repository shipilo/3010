namespace Les_3010
{
    class Hero
    {
        public HeroType Type;
        private double heartPoints;
        public bool Alive;

        public double HeartPoints 
        { 
            get => heartPoints; 
            set
            {
                if (value >= 0)
                {
                    heartPoints = value;
                    if (value == 0)
                    {
                        Alive = false;
                    }
                }
            }
        }
    }
}
