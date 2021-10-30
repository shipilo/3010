using System;

namespace Met_3010
{
    class BankTransaction
    {
        public readonly DateTime date;
        public readonly TimeSpan time;
        public readonly int sum;
        public BankTransaction(int sum)
        {
            date = DateTime.Now.Date;
            time = DateTime.Now.TimeOfDay;
            this.sum = sum;
        }
        public override string ToString()
        {
            return $"{date.ToShortDateString()} {time} {sum}";
        }
    }
}
