using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les_3010
{
    enum HeroType
    {
        Вампир______,
        Ведьма______,
        Оборотень___,
        Призрак_____,
        Демон_______,
        Зомби_______,
        Черная_вдова,
        Взрослый,
        Ребенок_,
        Ведьмак_
    }    
    enum FoodType
    {
        Жизнь,
        Конфета,
        Кровь
    }
    enum Gift
    {
        Бессмертие,
        Восстановление_крови,
        Конфета,
        Ничего
    }
    enum ActionType
    {
        Встретил,
        Убил,
        Умер,
        Подарил,
        Получил
    }
    class Game
    {
        public const string ErrorTypeOfHeroExceptionMessage = "This type of hero does not valid.";
        public const int MonstersCount = 7;
        public const int HumansCount = 3;
        private const double chanceOfGift = 30;

        public static string ConvertAliveStatus(bool status)
        {
            if (status) return "Жив";
            else return "Мертв";
        }
        private static bool Randomizer(double chance)
        {
            if (chance == 0) return false;
            else return (rnd.NextDouble() < chance / 100) ? true : false;
        }
        private static int RandomNumbers(int max, ref int num)
        {
            num = rnd.Next(0, max);
            if (Randomizer(num / (max - 1) * 100))
            {
                return rnd.Next(0, num);
            }
            else
            {
                return rnd.Next(num + 1, max);
            }
        }
        private static void TimerTick(int mSeconds)
        {
            DateTime startPosition = DateTime.Now;
            while ((DateTime.Now - startPosition).TotalMilliseconds < mSeconds) { }
        }
        private static string PrintMeeting(Hero hero1, Hero hero2, ActionType action)
        {
            switch ((int)action)
            {
                case 0:
                    return $"{hero1.Type} встретил {hero2.Type}";
                case 1:
                    return $"{hero1.Type} убил {hero2.Type}";
                case 2:
                    return $"{hero1.Type} умер от {hero2.Type}";
                default:
                    return "Error";
            }
        }
        private static string PrintMeeting(Hero hero1, Hero hero2, ActionType action, Gift gift, double points)
        {
            switch ((int)action)
            {
                case 3:
                    return $"{hero1.Type} вручил {hero2.Type} подарок: {gift} в размере {points}";
                case 4:
                    return $"{hero1.Type} получил от {hero2.Type} подарок: {gift} в размере {points}";
                default:
                    return "Error";
            }
        }

        private static Random rnd = new Random();
        private static Monster[] monsters;
        private static Human[] humans;

        public static void CreateRandomMonsters(int count)
        {
            monsters = new Monster[count];
            for(int i = 0; i < count; i++)
            {
                monsters[i] = new Monster((HeroType)rnd.Next(MonstersCount));
            }
        }
        
        public static void CreateRandomHumans(int humanCount, int sweetCountRange)
        {
            humans = new Human[humanCount];
            for (int i = 0; i < humanCount; i++)
            {
                humans[i] = new Human((HeroType)rnd.Next(MonstersCount, HumansCount + MonstersCount), rnd.Next(0, sweetCountRange + 1));
            }
        }

        public static void Start(int iterations, int speed)
        {
            List<Hero> pool = new List<Hero>();
            pool.AddRange(monsters);
            pool.AddRange(humans);

            for (int i = 0; i < iterations; i++)
            {
                int num2 = 0;
                Hero hero1 = pool[RandomNumbers(pool.Count, ref num2)];
                Hero hero2 = pool[num2];

                string result = "";

                if (hero1 is Monster && hero2 is Monster || hero1 is Human && hero2 is Human)
                {
                    result = PrintMeeting(hero1, hero2, ActionType.Встретил);
                    if (hero1 is Monster)
                    {
                        hero1.HeartPoints--;
                        hero2.HeartPoints--;
                    }
                }
                else if (hero1 is Monster)
                {
                    if (Randomizer(chanceOfGift) && (hero1 as Monster).Gift != Gift.Ничего)
                    {
                        result = PrintMeeting(hero1, hero2, ActionType.Подарил, (hero1 as Monster).Gift, (hero1 as Monster).GiftPoints);
                        if ((hero1 as Monster).Gift == Gift.Бессмертие)
                        {
                            (hero2 as Human).Deathless = true;
                        }
                        else if ((hero1 as Monster).Gift == Gift.Восстановление_крови)
                        {
                            (hero2 as Human).Replenish(Gift.Восстановление_крови, (hero1 as Monster).GiftPoints);
                        }
                    }
                    else
                    {
                        if ((hero2 as Human).KillAction)
                        {
                            result = PrintMeeting(hero1, hero2, ActionType.Умер);
                            (hero1 as Monster).HeartPoints = 0;
                        }
                        else
                        {
                            double points = (hero1 as Monster).FoodPoints;
                            double max = (hero1 as Monster).FoodPointsMax;
                            switch ((int)(hero1 as Monster).Food)
                            {
                                case 0:
                                    points = (max - points) >= (hero2 as Human).HeartPoints ? (hero2 as Human).HeartPoints : max;
                                    (hero2 as Human).HeartPoints -= points;
                                    break;
                                case 1:
                                    points = (max - points) >= (hero2 as Human).BloodPoints ? (hero2 as Human).BloodPoints : max;
                                    (hero2 as Human).BloodPoints -= points;
                                    break;
                                case 2:
                                    points = (max - points) >= (hero2 as Human).Sweets ? (hero2 as Human).Sweets : max;
                                    (hero2 as Human).Sweets -= points;
                                    break;
                            }
                            (hero1 as Monster).FoodPoints += points;
                            if (!(hero2 as Human).Alive)
                            {
                                result = PrintMeeting(hero1, hero2, ActionType.Умер);
                            }
                            else
                            {
                                result = PrintMeeting(hero1, hero2, ActionType.Встретил);
                            }
                        }
                    }
                }
                else
                {
                    if (Randomizer(chanceOfGift) && (hero2 as Monster).Gift != Gift.Ничего)
                    {
                        result = PrintMeeting(hero1, hero2, ActionType.Получил, (hero2 as Monster).Gift, (hero2 as Monster).GiftPoints);
                        if ((hero2 as Monster).Gift == Gift.Бессмертие)
                        {
                            (hero1 as Human).Deathless = true;
                        }
                        else if ((hero2 as Monster).Gift == Gift.Восстановление_крови)
                        {
                            (hero1 as Human).Replenish(Gift.Восстановление_крови, (hero2 as Monster).GiftPoints);
                        }
                    }
                    else
                    {
                        if ((hero1 as Human).KillAction)
                        {
                            result = PrintMeeting(hero1, hero2, ActionType.Убил);
                            (hero2 as Monster).HeartPoints = 0;
                        }
                        else
                        {
                            double points = (hero2 as Monster).FoodPoints;
                            double max = (hero2 as Monster).FoodPointsMax;
                            switch ((int)(hero2 as Monster).Food)
                            {
                                case 0:
                                    points = (max - points) >= (hero1 as Human).HeartPoints ? (hero1 as Human).HeartPoints : max;
                                    (hero1 as Human).HeartPoints -= points;
                                    break;
                                case 1:
                                    points = (max - points) >= (hero1 as Human).BloodPoints ? (hero1 as Human).BloodPoints : max;
                                    (hero1 as Human).BloodPoints -= points;
                                    break;
                                case 2:
                                    points = (max - points) >= (hero1 as Human).Sweets ? (hero1 as Human).Sweets : max;
                                    (hero1 as Human).Sweets -= points;
                                    break;
                            }
                            (hero2 as Monster).FoodPoints += points;
                            if (!(hero1 as Human).Alive)
                            {
                                result = PrintMeeting(hero1, hero2, ActionType.Умер);
                            }
                            else
                            {
                                result = PrintMeeting(hero1, hero2, ActionType.Встретил);
                            }
                        }
                    }
                }

                if(!hero1.Alive)
                {
                    pool.Remove(hero1);                    
                }
                if (!hero2.Alive)
                {
                    pool.Remove(hero2);
                }
                if ((hero1 is Monster) && (hero1 as Monster).FoodFull)
                {
                    pool.Remove(hero1);
                }
                if ((hero2 is Monster) && (hero2 as Monster).FoodFull)
                {
                    pool.Remove(hero2);
                }

                Console.WriteLine(result + "\n");
                TimerTick(speed);
            }
        }

        public static string GetInfo()
        {
            string strout = "";
            foreach (Monster monster in monsters)
            {
                strout += monster.GetStatus() + "\n";
            }
            foreach (Human human in humans)
            {
                strout += human.GetStatus() + "\n";
            }
            return strout;
        }        
    }
}
