using System;

namespace Module_14.Models
{
    internal class Consultant : Personal
    {
        public Consultant(string namePersonal) : base("Консультант", namePersonal) { }

        /// <summary>
        /// Метод изменения данных клиента
        /// </summary>
        /// <param name="idClient">Идентификатор клиента</param>
        /// <param name="surnameClient">Фамилия клиента</param>
        /// <param name="nameClient">Имя клиента</param>
        /// <param name="patronymicClient">Отчество клиента</param>
        /// <param name="phoneNumberClient">Номер клиента</param>
        public override void EditingClient(string idClient, string surnameClient, string nameClient, string patronymicClient, string phoneNumberClient, string pasportClient)
        {
            for (int i = 0; i < collectionDataClients.Count; i++)
            {
                if (idClient == collectionDataClients[i].Id)
                {
                    pasportClient = collectionDataClients[i].Pasport;
                    base.EditingClient(idClient, surnameClient, nameClient, patronymicClient, phoneNumberClient, pasportClient);
                    break;
                }
            }
        }

        /// <summary>
        /// Запись данных клиента в коллекцию
        /// </summary>
        /// <param name="client">Экземпляр клиента</param>
        /// <returns>Возвращаем экземпляр клиента со скрытым паспортом</returns>
        public override Client PrintClient(Client client)
        {
            string closePasport = "**** ******";
            return new Client(client.Id, client.Surname, client.Name, client.Patronymic, client.PhoneNumber, closePasport);
        }
    }
}
