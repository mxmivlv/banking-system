using System;


namespace Module_14.Models
{
    internal interface IBankOutBalance<in T>
        where T : Account<string, decimal>
    {
        abstract public void WriteOffBalance(string idClientWriteOff, long numberAccountClientWriteOff, decimal amount);
    }
}
