using System;

namespace Module_14.Models
{
    internal class NoDeposit<T1, T2> : Account<T1, T2>
    {
        public NoDeposit(T1 name, T2 balance, long number = 0) :
            base(name, balance, number)
        { }

        /// <summary>
        /// Зачисление суммы клиенту на счет
        /// </summary>
        /// <param name="idClientReceiving">Идентификатор клиента</param>
        /// <param name="numberAccountClientReceiving">Номер счета клиента</param>
        /// <param name="amount">Сумма для зачисления</param>
        /// <returns>Счет клиента</returns>
        public override Account<string, decimal> InBalance(string idClientReceiving, long numberAccountClientReceiving, decimal amount)
        {
            return base.InBalance(idClientReceiving, numberAccountClientReceiving, amount);
        }
    }
}
