using System;
using System.Collections.ObjectModel;

namespace Module_14.Models
{
    internal class Account<T1, T2> : Bank, IBankInBalance<Account<string, decimal>>
    {
        public T1 NameAccount { get { return nameAccount; } }
        public long NumberAccount { get { return numberAccount; } }
        public T2 BalanceAccount { get { return balanceAccount; } }

        private T1 nameAccount;
        private long numberAccount;
        private T2 balanceAccount;

        static ObservableCollection<long> collectionNumberAccount;
        static Account()
        {
            collectionNumberAccount = new ObservableCollection<long>();
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="nameAccount">Имя счета</param>
        /// <param name="balanceAccount">Баланс счета</param>
        /// <param name="numberAccount">Номер счета. Если номер не указывать, он сгенирируется автоматически и будет индивидуальным</param>
        public Account(T1 nameAccount, T2 balanceAccount, long numberAccount = 0)
        {
            this.nameAccount = nameAccount;
            this.balanceAccount = balanceAccount;
            this.numberAccount = SetNumberAccount(numberAccount);
        }

        /// <summary>
        /// Метод проверки и генерации номера счета
        /// </summary>
        /// <param name="numberAccount">Номер счета</param>
        /// <returns>Номер счета</returns>
        private long SetNumberAccount(long numberAccount)
        {
            if (numberAccount == 0)
            {
                Random randomNumber = new Random();
                long tempNumberAccount = (long)(randomNumber.NextDouble() * long.MaxValue);

                for (int i = 0; i < collectionNumberAccount.Count; i++)
                {
                    if (tempNumberAccount == collectionNumberAccount[i])
                    {
                        tempNumberAccount = SetNumberAccount(numberAccount);
                        break;
                    }
                }
                collectionNumberAccount.Add(tempNumberAccount);
                return tempNumberAccount;
            }
            else
                return numberAccount;
        }

        /// <summary>
        /// Зачисление суммы клиенту на счет
        /// </summary>
        /// <param name="idClientReceiving">Идентификатор клиента</param>
        /// <param name="numberAccountClientReceiving">Номер счета клиента</param>
        /// <param name="amount">Сумма для зачисления</param>
        /// <returns>Счет клиента</returns>
        public virtual Account<string, decimal> InBalance(string idClientReceiving, long numberAccountClientReceiving, decimal amount)
        {
            string tempNameAccount = string.Empty;
            decimal tempBalanceAccount = 0;
            long tempNumberAccount = 0;

            for (int i = 0; i < collectionDataClients.Count; i++)
            {
                if (idClientReceiving == collectionDataClients[i].Id)
                {
                    for (int e = 0; e < collectionDataClients[i].Accounts.Count; e++)
                    {
                        if (numberAccountClientReceiving == collectionDataClients[i].Accounts[e].NumberAccount)
                        {
                            tempNameAccount = collectionDataClients[i].Accounts[e].NameAccount;
                            tempBalanceAccount = collectionDataClients[i].Accounts[e].BalanceAccount;
                            tempBalanceAccount += amount;
                            tempNumberAccount = collectionDataClients[i].Accounts[e].NumberAccount;
                        }
                    }
                }
            }
            return new Account<string, decimal>(tempNameAccount, tempBalanceAccount, tempNumberAccount);
        }
    }
}
