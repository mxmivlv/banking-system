using System;
using System.Collections.ObjectModel;

namespace Module_14.Models
{
    internal class Client
    {
        public string Id { get { return id; } }
        public string Surname { get { return surname; } }
        public string Name { get { return name; } }
        public string Patronymic { get { return patronymic; } }
        public string PhoneNumber { get { return phoneNumber; } }
        public string Pasport { get { return pasport; } }
        public ObservableCollection<Account<string, decimal>> Accounts { get { return accounts; } }

        private string id;
        private string surname;
        private string name;
        private string patronymic;
        private string phoneNumber;
        private string pasport;
        private ObservableCollection<Account<string, decimal>> accounts = new ObservableCollection<Account<string, decimal>>();

        public Client(string id, string surname, string name, string patronymic, string phoneNumber, string pasport)
        {
            this.id = SetId(id);
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.phoneNumber = phoneNumber;
            this.pasport = pasport;
        }

        /// <summary>
        /// Перегрузка конструктора класса. Основной для использования. Id генерируется автоматически
        /// </summary>
        /// <param name="surname">Фамилия клиента</param>
        /// <param name="name">Имя клиента</param>
        /// <param name="patronymic">Отчество клиента</param>
        public Client(string surname, string name, string patronymic, string phoneNumber, string pasport) :
            this("", surname, name, patronymic, phoneNumber, pasport)
        {
            id = Guid.NewGuid().ToString("N").Substring(0, 5);
        }

        /// <summary>
        /// Метод установки идентификатора
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        private string SetId(string id)
        {
            char[] charId = id.ToCharArray();
            char[] tempCharId = new char[5];
            string tempId = string.Empty;

            if(id == "nullId") 
            {
                return tempId;
            }
            else 
            {
                if ((charId.Length != tempCharId.Length) || (id == "0"))
                {
                    tempId = Guid.NewGuid().ToString("N").Substring(0, 5);
                    return tempId;
                }
                else
                    return id;
            }
        }
    }
}
