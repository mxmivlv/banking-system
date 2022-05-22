using System;

namespace Module_14.Models
{
    internal class Personal : Bank
    {
        public string PostPersonal { get { return postPersonal; } }
        public string NamePersonal { get { return namePersonal; } }

        private string postPersonal;
        private string namePersonal;
        protected Personal(string postPersonal, string namePersonal)
        {
            this.postPersonal = postPersonal;
            this.namePersonal = namePersonal;
        }

        /// <summary>
        /// Создание и добавление нового клиента
        /// </summary>
        /// <param name="surnameClient"> фамилия клиента</param>
        /// <param name="nameClient"> имя клиента</param>
        /// <param name="patronymicClient"> отчество клиента</param>
        /// <param name="phoneNumberClient"> номер телефона клиента</param>
        /// <param name="pasportClient"> паспорт клиента</param>
        public virtual void AddClient(string surnameClient, string nameClient, string patronymicClient, string phoneNumberClient, string pasportClient)
        {
            try 
            {
                long phoneNumber;
                long pasport;

                bool flagAddClient = true;
                bool flagPhoneNumber = long.TryParse(phoneNumberClient, out phoneNumber);
                bool flagPasport = long.TryParse(pasportClient, out pasport);

                char[] tempArrayPhoneNumber = new char[11];
                char[] lenghtNumberPhone = phoneNumber.ToString().ToCharArray();

                char[] tempArrayPasport = new char[10];
                char[] lenghtPasport = pasport.ToString().ToCharArray();

                //Проверка на заполненность всех полей
                if ((surnameClient != String.Empty) && (nameClient != String.Empty) && (patronymicClient != String.Empty) && (phoneNumberClient != String.Empty) && (pasportClient != String.Empty))
                {
                    //Если коллекция пустая, то переменной присваем истинну для добавления клиента
                    if (collectionDataClients.Count == 0)
                    {
                        flagAddClient = true;
                    }
                    if (flagAddClient)
                    {
                        //Бежим по коллекции
                        foreach (Client client in collectionDataClients)
                        {
                            //Проверяем на точно такого же клиента
                            if ((surnameClient == client.Surname) && (nameClient == client.Name) && (patronymicClient == client.Patronymic) && (phoneNumberClient == client.PhoneNumber) && (pasportClient == client.Pasport))
                            {
                                //Если такой клиент есть
                                CallEventLog($"{postPersonal}", $"{namePersonal}", "попытка добавить клиента, который уже есть в бд");
                                CallEventError($"{postPersonal}", $"{namePersonal}", "клиент есть уже в системе");
                                flagAddClient = false;
                                break;
                            }
                        }
                        //Если такого клиента нет в коллекции
                        if (flagAddClient)
                        {
                            //Номер и паспорт соответствует конвертации
                            if (flagPhoneNumber && flagPasport)
                            {
                                //Номер и паспорт соответствует длине
                                if ((lenghtNumberPhone.Length == tempArrayPhoneNumber.Length) && (lenghtPasport.Length == tempArrayPasport.Length))
                                {
                                    Client tempClient = new Client(surnameClient, nameClient, patronymicClient, phoneNumberClient, pasportClient);
                                    collectionDataClients.Add(tempClient);

                                    CallEventLog($"{postPersonal}", $"{namePersonal}", $"клиент {tempClient.Surname} {tempClient.Name} добавлен");
                                    CallEventSuccess($"{postPersonal}", $"{namePersonal}", $"клиент {tempClient.Surname} {tempClient.Name} добавлен");
                                }
                                //Если номер и паспорт не соответствует длине
                                else
                                {
                                    CallEventLog($"{postPersonal}", $"{namePersonal}", "неверная длина номера телефона или паспорта");
                                    CallEventError($"{postPersonal}", $"{namePersonal}", "неверная длина номера телефона или паспорта");
                                }
                            }
                            //Номер и паспорт не соответствует конвертации
                            else
                            {
                                CallEventLog($"{postPersonal}", $"{namePersonal}", "введен не верный номер паспорта");
                                CallEventError($"{postPersonal}", $"{namePersonal}", "введен не верный номер паспорта");
                            }
                        }
                    }
                }
                //Если поля не заполнены
                else
                {
                    CallEventLog($"{postPersonal}", $"{namePersonal}", "поля не заполнены");
                    CallEventError($"{postPersonal}", $"{namePersonal}", "поля не заполнены");
                }
            }
            catch (Exception ex) 
            {
                CallEventLog($"{postPersonal}", $"{namePersonal}", $"{ex.Message}");
            }
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="idClient"> идентификатор клиента</param>
        public virtual void DeleteClient(string idClient)
        {
            try 
            {
                //Если коллекция пуста
                if (collectionDataClients.Count == 0)
                {
                    CallEventLog($"{postPersonal}", $"{namePersonal}", "нет клиентов в коллекции");
                    CallEventError($"{postPersonal}", $"{namePersonal}", "нет клиентов в коллекции");
                }
                else
                {
                    //Если не выбран клиент
                    if (idClient == null)
                    {
                        CallEventLog($"{postPersonal}", $"{namePersonal}", "не введен идентификатор клиента для удаления");
                        CallEventError($"{postPersonal}", $"{namePersonal}", "не введен идентификатор клиента для удаления");
                    }
                    else
                    {
                        //Итерируемся по коллекции в поиске клиента
                        for (int i = 0; i < collectionDataClients.Count; i++)
                        {
                            if (idClient == collectionDataClients[i].Id)
                            {
                                CallEventLog($"{postPersonal}", $"{namePersonal}", $"клиент {collectionDataClients[i].Surname} {collectionDataClients[i].Name} удален");
                                CallEventSuccess($"{postPersonal}", $"{namePersonal}", $"клиент {collectionDataClients[i].Surname} {collectionDataClients[i].Name} удален");

                                collectionDataClients.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                CallEventLog($"{postPersonal}", $"{namePersonal}", $"{ex.Message}");
            }
        }

        /// <summary>
        /// Изменение данных клиента
        /// </summary>
        /// <param name="surnameClient"> фамилия клиента</param>
        /// <param name="nameClient"> имя клиента</param>
        /// <param name="patronymicClient"> отчество клиента</param>
        /// <param name="phoneNumberClient"> номер телефона клиента</param>
        /// <param name="pasportClient"> паспорт клиента</param>
        public virtual void EditingClient(string idClient, string surnameClient, string nameClient, string patronymicClient, string phoneNumberClient, string pasportClient)
        {
            try 
            {
                long phoneNumber;
                long pasport;

                bool flagPhoneNumber = long.TryParse(phoneNumberClient, out phoneNumber);
                bool flagPasport = long.TryParse(pasportClient, out pasport);

                //Номер и паспорт соответствует конвертации
                if (flagPhoneNumber && flagPasport)
                {
                    char[] tempArrayPhoneNumber = new char[11];
                    char[] lenghtNumberPhone = phoneNumber.ToString().ToCharArray();

                    char[] tempArrayPasport = new char[10];
                    char[] lenghtPasport = pasport.ToString().ToCharArray();

                    //Номер и паспорт соответствуют длине
                    if ((lenghtNumberPhone.Length == tempArrayPhoneNumber.Length) && (lenghtPasport.Length == tempArrayPasport.Length))
                    {
                        for (int i = 0; i < collectionDataClients.Count; i++)
                        {
                            //Идентификатор найден в коллекции клиентов
                            if (idClient == collectionDataClients[i].Id)
                            {
                                Client tempClient = new Client(collectionDataClients[i].Id, surnameClient, nameClient, patronymicClient, phoneNumberClient, pasportClient);

                                //Измененному клиенту добавляем счета
                                for (int q = 0; q < collectionDataClients[i].Accounts.Count; q++)
                                {
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Депозитный")
                                    {
                                        Deposit<string, decimal> deposit = new Deposit<string, decimal>(collectionDataClients[i].Accounts[q].NameAccount, collectionDataClients[i].Accounts[q].BalanceAccount, collectionDataClients[i].Accounts[q].NumberAccount);
                                        tempClient.Accounts.Add(deposit);

                                    }
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Обычный")
                                    {
                                        NoDeposit<string, decimal> noDeposit = new NoDeposit<string, decimal>(collectionDataClients[i].Accounts[q].NameAccount, collectionDataClients[i].Accounts[q].BalanceAccount, collectionDataClients[i].Accounts[q].NumberAccount);
                                        tempClient.Accounts.Add(noDeposit);
                                    }
                                }
                                //В коллекцию добавляем измененного клиента с счетами
                                collectionDataClients[i] = tempClient;

                                CallEventLog($"{postPersonal}", $"{namePersonal}", "изменение данных клиента");
                                CallEventSuccess($"{postPersonal}", $"{namePersonal}", "клиент изменен");
                            }
                        }
                    }
                    //Номер и паспорт не соответствуют длине
                    else
                    {
                        CallEventLog($"{postPersonal}", $"{namePersonal}", "неверный размер номера или паспорта");
                        CallEventError($"{postPersonal}", $"{namePersonal}", "неверный размер номера или паспорта");
                    }
                }
                //Номер и паспорт не соответствуют конвертации
                else
                {
                    CallEventLog($"{postPersonal}", $"{namePersonal}", "некорректный формат номера или паспорта");
                    CallEventError($"{postPersonal}", $"{namePersonal}", "некорректный формат номера или паспорта");
                }
            }
            catch (Exception ex) 
            {
                CallEventLog($"{postPersonal}", $"{namePersonal}", $"{ex.Message}");
            }
        }

        /// <summary>
        /// Вывод данных клиента
        /// </summary>
        /// <param name="client"> экземпляр клиента</param>
        /// <returns> возвращаем экземпляр клиента с данными</returns>
        public virtual Client PrintClient(Client client)
        {
            return new Client(client.Id, client.Surname, client.Name, client.Patronymic, client.PhoneNumber, client.Pasport);
        }
    }
}
