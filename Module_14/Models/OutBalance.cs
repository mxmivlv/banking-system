using System;

namespace Module_14.Models
{
    internal class OutBalance<T> : Bank, IBankOutBalance<T>
       where T : Account<string, decimal>
    {
        /// <summary>
        /// Списание суммы со счета
        /// </summary>
        /// <param name="idClientWriteOff">Номер клиента по списку в базе у которого средства спишутся</param>
        /// <param name="numberAccountClientWriteOff">Номер счета по списку в базе у клиента, у которого спишутся средства</param>
        /// <param name="amount">Сумма для списания</param>
        /// <returns></returns>
        public void WriteOffBalance(string idClientWriteOff, long numberAccountClientWriteOff, decimal amount)
        {
            string tempNameAccount = string.Empty;
            decimal tempBalanceAccount = 0;
            long tempNumberAccount = 0;

            for (int i = 0; i < collectionDataClients.Count; i++)
            {
                if (idClientWriteOff == collectionDataClients[i].Id)
                {
                    for (int e = 0; e < collectionDataClients[i].Accounts.Count; e++)
                    {
                        if (numberAccountClientWriteOff == collectionDataClients[i].Accounts[e].NumberAccount)
                        {
                            tempNameAccount = collectionDataClients[i].Accounts[e].NameAccount;
                            tempBalanceAccount = collectionDataClients[i].Accounts[e].BalanceAccount;
                            tempBalanceAccount -= amount;
                            tempNumberAccount = collectionDataClients[i].Accounts[e].NumberAccount;
                            if (tempBalanceAccount < 0)
                            {
                                throw new CodeException("Баланс после списания меньше нуля", 1);
                            }
                            collectionDataClients[i].Accounts[e] = new Account<string, decimal>(tempNameAccount, tempBalanceAccount, tempNumberAccount);
                            throw new CodeException("Успешно", 0);
                        }
                    }
                }
            }
        }
    }
}
