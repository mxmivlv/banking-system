using System;

namespace Module_14.Models
{
    internal interface IBankInBalance<out T>
        where T : Account<string, decimal>
    {
        abstract public T InBalance(string idClientReceiving, long numberAccountClientReceiving, decimal amount);
    }
}
