using System;
using System.Collections.Generic;
using System.IO;

namespace Met_3010
{
	class Account
	{
		public enum Type
		{
			Current,
			Saving
		}
		private int index;
		private Type accountType;
		private int balance;
		private Queue<BankTransaction> transactions;

		static int indexer = 0;

        public Account()
        {
			index = indexer++;
			transactions = new Queue<BankTransaction>();
        }

        public Account(int balance)
        {
			index = indexer++;
			this.balance = balance;
			transactions = new Queue<BankTransaction>();
		}

		public Account(Type accountType)
        {
			index = indexer++;
			this.accountType = accountType;
			transactions = new Queue<BankTransaction>();
		}

		public Account(Type accountType, int balance) : this(accountType)
        {
            this.balance = balance;
        }

        public bool Withdraw(int sum)
		{
			if (sum <= balance)
			{
				balance -= sum;
				transactions.Enqueue(new BankTransaction(-sum));
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool PutInBalance(int sum)
		{
			if (sum > 0)
			{
				balance += sum;
				transactions.Enqueue(new BankTransaction(sum));
				return true;
			}
			else
			{
				return false;
			}
		}
		public bool MakeTransfer(Account accPaymentReceiver, int sum)
		{
			if (Withdraw(sum))
			{
				accPaymentReceiver.PutInBalance(sum);
				return true;
			}
			else
			{
				return false;
			}
		}
		public void Dispose(string file)
        {
			StreamWriter sw = new StreamWriter(file);
			sw.Write(string.Join("\n", transactions));
			sw.Close();
			GC.SuppressFinalize(sw);
        }
	}
}
