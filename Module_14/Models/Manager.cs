using System;

namespace Module_14.Models
{
    internal class Manager : Personal
    {
        public Manager(string namePersonal) : base("Менеджер",namePersonal) { }

        public override void AddClient(string surnameClient, string namePersonal, string patronymicClient, string phoneNumberClient, string pasportClient)
        {
            base.AddClient(surnameClient, namePersonal, patronymicClient, phoneNumberClient, pasportClient);
        }

        public override void DeleteClient(string tempId)
        {
            base.DeleteClient(tempId);
        }

        public override void EditingClient(string idClient, string surnameClaient, string nameClient, string patronymicClient, string phoneNumberClient, string pasportClient)
        {
            base.EditingClient(idClient, surnameClaient, nameClient, patronymicClient, phoneNumberClient, pasportClient);
        }

        public override Client PrintClient(Client client)
        {
            return base.PrintClient(client);
        }
    }
}
