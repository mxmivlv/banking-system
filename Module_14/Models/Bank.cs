using Notifications.Wpf.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Module_14.Models
{
    internal class Bank
    {
        private Personal personal;
        private Account<string, decimal> account;

        private event Action<string, string, string> EventError; // Событие ошибки
        private event Action<string, string, string> EventSuccess; // Событие успешного выполнения
        private event Action<string, string, string> EventLog; // Событие записи действий персонала в фаил
        private NotificationManager notificationManager; // Переменная для работы с уведомлениями

        protected static ObservableCollection<Client> collectionDataClients; //Коллекция клиентов
        static string pathFile; // Путь к текстовому фаилу
        static string pathLog; // Путь к текстовому фаилу для записи логов
        static Bank()
        {
            collectionDataClients = new ObservableCollection<Client>();
            pathFile = "ClientsInBank.txt";
            pathLog = "Log.txt";
        }

        public Bank() 
        {
            notificationManager = new NotificationManager();
            EventError += ErrorAsync;
            EventSuccess += SuccessAsync;
            EventLog += LogAsync;
        }

        /// <summary>
        /// Асинхронный метод уведомления (успешного)
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textSuccess">Текст успешного выполнения уведомления</param>
        private async void SuccessAsync(string postPersonal, string namePersonal, string textSuccess)
        {
            await Task.Run(() => notificationManager.ShowAsync(new NotificationContent
            {
                Title = $"{postPersonal}",
                Message = $"{namePersonal}: {textSuccess}",
                Type = NotificationType.Success
            }));
        }

        /// <summary>
        /// Вызов события вне класса (успешный)
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textSuccess">Текст успешного выполнения</param>
        protected void CallEventSuccess(string postPersonal, string namePersonal, string textSuccess)
        {
            EventSuccess?.Invoke($"{postPersonal}", $"{namePersonal}", $"{textSuccess}");
        }

        /// <summary>
        /// Асинхронный метод уведомления (ошибки)
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textError">Текст ошибки</param>
        private async void ErrorAsync(string postPersonal, string namePersonal, string textError)
        {
            await Task.Run(() => notificationManager.ShowAsync(new NotificationContent
            {
                Title = $"{postPersonal}",
                Message = $"{namePersonal}: {textError}",
                Type = NotificationType.Error
            }));
        }

        /// <summary>
        /// Вызов события вне класса (ошибка)
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textError">Текст ошибки</param>
        protected void CallEventError(string postPersonal, string namePersonal, string textError)
        {
            EventError?.Invoke($"{postPersonal}", $"{namePersonal}", $"{textError}");
        }

        /// <summary>
        /// Асинхронный метод записи действии персонала в системе
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textWrite">Текст действий</param>
        private async void LogAsync(string postPersonal, string namePersonal, string textWrite)
        {
            await Task.Run(() =>
            {
                using (var fileLog = new StreamWriter(pathLog, true))
                {
                    fileLog.WriteLine($"{postPersonal} {namePersonal}: {textWrite} {DateTime.Now};");
                }
            });
        }

        /// <summary>
        /// Вызов события вне класса (запись логов)
        /// </summary>
        /// <param name="postPersonal">Должность персонала</param>
        /// <param name="namePersonal">Имя персонала</param>
        /// <param name="textWrite">Текст действий</param>
        protected void CallEventLog(string postPersonal, string namePersonal, string textWrite)
        {
            EventLog?.Invoke($"{postPersonal}", $"{namePersonal}", $"{textWrite}");
        }

        /// <summary>
        /// Создание персонала
        /// </summary>
        /// <param name="viewPersonalAuthorization">Должность персонала</param>
        public void CreatePersonal(string viewPersonalAuthorization) 
        {
            switch (viewPersonalAuthorization)
            {
                case "Консультант":
                    {
                        personal = new Consultant("Имя консультанта");
                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "вошел в систему");
                        break;
                    }
                case "Менеджер":
                    {
                        personal = new Manager("Имя Менеджера");
                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "вошел в систему");
                        break;
                    }
                default:
                    break;
            }
        }

        /// <summary>
        /// Отключение кнопок на форме
        /// </summary>
        /// <param name="viewPersonalAuthorization">Должность персонала</param>
        /// <returns>Возвращает истину если в системе работает персонал у которого больше прав</returns>
        public bool IsEnabledElements(string viewPersonalAuthorization)
        {
            if (viewPersonalAuthorization == "Консультант")
                return false;
            else
                return true;
        }

        /// <summary>
        /// Отключение TexBox на форме
        /// </summary>
        /// <param name="viewPersonalAuthorization">Должность персонала</param>
        /// <returns>Возвращает истину для запрета редактирования TexBox данных</returns>
        public bool IsReadOnlyElement(string viewPersonalAuthorization) 
        {
            if (viewPersonalAuthorization == "Консультант")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Создание фаила. Если фаил есть, передаем выполнение другому методу, если нет, то его создаем
        /// </summary>
        public void CreateFileClientsInBank()
        {
            if (File.Exists(pathFile))
            {
                LoadDataClient(pathFile);
            }
            else 
            {
                using (var tempFile = new FileStream(pathFile, FileMode.Create)) { }
            }
        }

        /// <summary>
        /// Загрузка данных из фаила
        /// </summary>
        private void LoadDataClient(string pathFile)
        {
            string tempStringDocument; //Строка содержащая все данные из документа
            int indexClient = -1;
            using (var document = new StreamReader(pathFile, true))
            {
                tempStringDocument = document.ReadToEnd();
            }

            string[] line = tempStringDocument.Split(";"); //Массив строк

            for (int i = 0; i < line.Length - 1; i++)
            {
                string[] words = line[i].Split(" "); //Массив слов в строке
                string tempId = words[0];
                tempId = tempId.Trim();

                //Создаем клеинта и добавляем в коллекцию
                if (tempId != "-")
                {

                    string tempSurname = words[1];
                    string tempName = words[2];
                    string tempPatronymic = words[3];
                    string tempPhoneNumber = words[4];
                    string tempPasport = words[5];
                    Client tempClient = new Client(tempId, tempSurname, tempName, tempPatronymic, tempPhoneNumber, tempPasport);
                    collectionDataClients.Add(tempClient);
                    indexClient++;
                }

                //Создаем счет и добавляем его к клиенту
                else
                {
                    string tempNameAccount = words[1];
                    tempNameAccount = tempNameAccount.Trim();
                    long tempNumberAccount = Convert.ToInt64(words[2]);
                    decimal tempBalanse = Convert.ToDecimal(words[3]);

                    if (tempNameAccount == "Депозитный")
                    {
                        /*deposit*/ account = new Deposit<string, decimal>(tempNameAccount, tempBalanse, tempNumberAccount);
                        collectionDataClients[indexClient].Accounts.Add(account);
                    }
                    if (tempNameAccount == "Обычный")
                    {
                        /*noDeposit*/ account = new NoDeposit<string, decimal>(tempNameAccount, tempBalanse, tempNumberAccount);
                        collectionDataClients[indexClient].Accounts.Add(account);
                    }
                }
            }
        }

        /// <summary>
        /// Асинхронный метод сохранение данных в фаил при закрытии программы
        /// </summary>
        public async void SaveDataClientAsync(object sender, CancelEventArgs e)
        {
            await Task.Run(() => 
            {
                File.Delete(pathFile);
                string tempClient = string.Empty;
                string tempAccountInClient = string.Empty;
                using (var document = new StreamWriter(pathFile, true))
                {
                    for (int i = 0; i < collectionDataClients.Count; i++)
                    {
                        tempClient = $"{collectionDataClients[i].Id} {collectionDataClients[i].Surname} {collectionDataClients[i].Name} {collectionDataClients[i].Patronymic} {collectionDataClients[i].PhoneNumber} {collectionDataClients[i].Pasport};";
                        document.WriteLine(tempClient);

                        for (int q = 0; q < collectionDataClients[i].Accounts.Count; q++)
                        {
                            tempAccountInClient = $"\t- {collectionDataClients[i].Accounts[q].NameAccount} {collectionDataClients[i].Accounts[q].NumberAccount} {collectionDataClients[i].Accounts[q].BalanceAccount};";
                            document.WriteLine(tempAccountInClient);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Загрузка клиентов в коллекцию для показа
        /// </summary>
        /// <param name="viewCollectionClient">Коллекция для вывода клиентов в форму</param>
        public void LoadViewCollectionClient(ObservableCollection<Client> viewCollectionClient) 
        {
            viewCollectionClient.Clear();

            foreach (var item in collectionDataClients)
            {
                viewCollectionClient.Add(personal.PrintClient(item));
            }
        }

        /// <summary>
        /// Загрузка счетов у клиента для показа
        /// </summary>
        /// <param name="client">Выбранный клиента</param>
        /// <param name="viewCollectionAccountClient">Коллекция для вывода информации о счетах в форму</param>
        public void LoadBankAccount(Client client, ObservableCollection<Account<string,decimal>> viewCollectionAccountClient)
        {
            viewCollectionAccountClient.Clear();

            foreach (var item in collectionDataClients)
            {
                if (item.Id == client.Id)
                {
                    foreach (var account in item.Accounts)
                    {
                        viewCollectionAccountClient.Add(account);
                    }
                }

            }
        }

        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="viewClientSurname">Фамилия клиента</param>
        /// <param name="viewClientName">Имя клиента</param>
        /// <param name="viewClientPatronymic">Отчество клиента</param>
        /// <param name="viewClientPhoneNumber">Номер телефона клиента</param>
        /// <param name="viewClientPasport">Паспорт клиента</param>
        /// <param name="viewCollectionClient">Коллекция для вывода клиентов в форму</param>
        public void AddClient(string viewClientSurname, string viewClientName, string viewClientPatronymic, string viewClientPhoneNumber, string viewClientPasport, 
            ObservableCollection<Client> viewCollectionClient) 
        {
            personal.AddClient(viewClientSurname, viewClientName, viewClientPatronymic, viewClientPhoneNumber, viewClientPasport);
            LoadViewCollectionClient(viewCollectionClient);
        }

        /// <summary>
        /// Редактирование клиента
        /// </summary>
        /// <param name="viewClientId">Идентификатор клиента</param>
        /// <param name="viewClientSurname">Фамилия клиента</param>
        /// <param name="viewClientName">Имя клиента</param>
        /// <param name="viewClientPatronymic">Отчество клиента</param>
        /// <param name="viewClientPhoneNumber">Номер телефона клиента</param>
        /// <param name="viewClientPasport">Паспорт клиента</param>
        /// <param name="viewCollectionClient">Коллекция для вывода клиентов в форму</param>
        public void EditingClient(string viewClientId, string viewClientSurname, string viewClientName, string viewClientPatronymic, string viewClientPhoneNumber, string viewClientPasport, 
            ObservableCollection<Client> viewCollectionClient) 
        {
            personal.EditingClient(viewClientId, viewClientSurname, viewClientName, viewClientPatronymic, viewClientPhoneNumber, viewClientPasport);
            LoadViewCollectionClient(viewCollectionClient);
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="viewClientId">Идентификатор клиента, нужен для поиска и удаления клиента</param>
        /// <param name="viewCollectionClient">Коллекция для вывода клиентов в форму</param>
        public void DeleteClient(string viewClientId, ObservableCollection<Client> viewCollectionClient) 
        {
            personal.DeleteClient(viewClientId);
            LoadViewCollectionClient(viewCollectionClient);
        }

        /// <summary>
        /// Зачисление средств на счет клиенту
        /// </summary>
        /// <param name="viewClientId">Идентификатор клиента</param>
        /// <param name="viewClientNumberAccount">Номер счета клиента</param>
        /// <param name="viewClientBalanceAccount">Сумма для зачисления</param>
        /// <param name="viewCollectionAccount">Коллекция для вывода информации о счетах клиента в форму</param>
        public void EnrollmentAmountAccountClient(string viewClientId, string viewClientNumberAccount, string viewClientBalanceAccount, 
            ObservableCollection<Account<string, decimal>> viewCollectionAccount) 
        {
            try
            {
                long numberAccountClient = Int64.Parse(viewClientNumberAccount);
                decimal balanceAccountClient = Convert.ToDecimal(viewClientBalanceAccount);

                //Если выбран клиент, выбран счет у клиента и сумма для переведа больше нуля
                if (viewClientId != null && numberAccountClient > 0 && balanceAccountClient > 0)
                {
                    //Итерируемся по коллекции
                    for (int i = 0; i < collectionDataClients.Count; i++)
                    {
                        //Если идентификатор клиента равен идентификатору клиента в коллекции
                        if (viewClientId == collectionDataClients[i].Id)
                        {
                            //Итерируемся по коллекции счетов у данного клиента
                            for (int q = 0; q < collectionDataClients[i].Accounts.Count; q++)
                            {
                                //Если номер счета равен номеру счета в коллекции у клиента
                                if (numberAccountClient == collectionDataClients[i].Accounts[q].NumberAccount)
                                {
                                    //Если депозитный
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Депозитный")
                                    {
                                        IBankInBalance<Account<string, decimal>> tempBank = collectionDataClients[i].Accounts[q]; //Ковариантный интерфейс
                                        collectionDataClients[i].Accounts[q] = tempBank.InBalance(viewClientId, numberAccountClient, balanceAccountClient);

                                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"сумма в размере {balanceAccountClient} зачислена");
                                        EventSuccess?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"сумма в размере {balanceAccountClient} зачислена");
                                    }

                                    //Если обычный
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Обычный")
                                    {
                                        IBankInBalance<Account<string, decimal>> tempBank = collectionDataClients[i].Accounts[q]; //Ковариантный интерфейс
                                        collectionDataClients[i].Accounts[q] = tempBank.InBalance(viewClientId, numberAccountClient, balanceAccountClient);

                                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"сумма в размере {balanceAccountClient} зачислена");
                                        EventSuccess?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"сумма в размере {balanceAccountClient} зачислена");
                                    }
                                    LoadBankAccount(collectionDataClients[i], viewCollectionAccount);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                //Если не выбран клиент или не выбран счет у клиента, или сумма для перевода меньше нуля
                else
                {
                    EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введите идентификатор клиента, номер счета клиента. Сумма для зачисления должна быть больше 0");
                    EventError?.Invoke($"{personal.PostPersonal}",$"{personal.NamePersonal}", "введите идентификатор клиента, номер счета клиента. Сумма для зачисления должна быть больше 0");
                }
            }
            //Если введен неверный баланса
            catch (FormatException)
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "сумма для зачисления некорректна");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "сумма для зачисления некорректна");
            }
            //Если введено слишком длинное число
            catch (OverflowException)
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введено слишком длинное число");
                EventError?.Invoke($"{personal.PostPersonal}",$"{personal.NamePersonal}" , "введено слишком длинное число");
            }
            //Если поля не заполнены
            catch (ArgumentNullException) 
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "поля не заполнены");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "поля не заполнены");
            }
        }

        /// <summary>
        /// Открытие счета у клиента
        /// </summary>
        /// <param name="viewClient">Идентификатор клиента</param>
        /// <param name="viewTypeAccount">Тип счета</param>
        /// <param name="viewCollectionAccount">Коллекция для вывода информации о счетах клиента в форму</param>
        public void OpenAccount(Client viewClient, string viewTypeAccount, ObservableCollection<Account<string, decimal>> viewCollectionAccount) 
        {
            try 
            {
                //Если выбран клиент для открытия счета и выбран тип счета
                if (viewClient.Id != null && viewTypeAccount != null)
                {
                    switch (viewTypeAccount)
                    {
                        case "Депозитный":
                            {
                                for (int i = 0; i < collectionDataClients.Count; i++)
                                {
                                    if (viewClient.Id == collectionDataClients[i].Id)
                                    {
                                        account = new Deposit<string, decimal>(viewTypeAccount, 0);
                                        collectionDataClients[i].Accounts.Add(account);

                                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет открыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                        EventSuccess?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет открыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                        break;
                                    }
                                }
                                break;
                            }
                        case "Обычный":
                            {
                                for (int i = 0; i < collectionDataClients.Count; i++)
                                {
                                    if (viewClient.Id == collectionDataClients[i].Id)
                                    {
                                        account = new NoDeposit<string, decimal>(viewTypeAccount, 0);
                                        collectionDataClients[i].Accounts.Add(account);

                                        EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет открыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                        EventSuccess?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет открыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                        break;
                                    }
                                }
                                break;
                            }
                    }
                    LoadBankAccount(viewClient, viewCollectionAccount);
                }
                else
                {
                    EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "не выбран клиент или тип счета для открытие");
                    EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "не выбран клиент или тип счета для открытие");
                }
            }
            catch (Exception ex) 
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"{ex.Message}");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"{ex.Message}");
            }
        }

        /// <summary>
        /// Закрытие счета у клиента
        /// </summary>
        /// <param name="viewClient">Идентификатор клиента</param>
        /// <param name="viewClientNumberAccount">Номер счета клиента</param>
        /// <param name="viewCollectionAccount">Коллекция для вывода информации о счетах клиента в форму</param>
        public void CloseAccount(Client viewClient, Account<string, decimal> viewClientNumberAccount, ObservableCollection<Account<string, decimal>> viewCollectionAccount) 
        {
            try 
            {
                //Если выбран клиент и счет для закрытия
                if (viewClient.Id != null && viewClientNumberAccount.NumberAccount != null)
                {
                    for (int i = 0; i < collectionDataClients.Count; i++)
                    {
                        if (viewClient.Id == collectionDataClients[i].Id)
                        {
                            for (int e = 0; e < collectionDataClients[i].Accounts.Count; e++)
                            {
                                if (viewClientNumberAccount.NumberAccount == collectionDataClients[i].Accounts[e].NumberAccount)
                                {
                                    collectionDataClients[i].Accounts.RemoveAt(e);
                                    LoadBankAccount(viewClient, viewCollectionAccount);

                                    EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет закрыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                    EventSuccess?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", $"счет закрыт у клиента: {collectionDataClients[i].Surname} {collectionDataClients[i].Name}");
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (NullReferenceException) 
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "не выбран клиент или счет для закрытия");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "не выбран клиент или счет для закрытия");
            }
        }

        /// <summary>
        /// Перевод средств между счетами
        /// </summary>
        /// <param name="viewClientIdWriteOff">Идентификатор клиента у которого средства спишутся</param>
        /// <param name="viewClientNumberAccountWriteOff">Номер счета для списания</param>
        /// <param name="viewClientIdReceiving">Идентификатор клиента для зачисления средств</param>
        /// <param name="viewClientNumberAccountReceiving">Номер счета для зачисления</param>
        /// <param name="viewAmountTranslation">Сумма</param>
        /// <param name="collectionAccountClient">Коллекция для вывода информации о счетах клиента в форму</param>
        public void Translation(string viewClientIdWriteOff, string viewClientNumberAccountWriteOff, string viewClientIdReceiving, string viewClientNumberAccountReceiving, string viewAmountTranslation, 
            ObservableCollection<Account<string, decimal>> collectionAccountClient) 
        {
            try
            {
                long tempNumberAccountWriteOff = Convert.ToInt64(viewClientNumberAccountWriteOff);
                long tempNumberAccountClientReceiving = Convert.ToInt64(viewClientNumberAccountReceiving);
                decimal tempAmountTranslation = Convert.ToDecimal(viewAmountTranslation);

                try
                {
                    for (int i = 0; i < collectionDataClients.Count; i++)
                    {
                        if (viewClientIdWriteOff == collectionDataClients[i].Id)
                        {
                            for (int q = 0; q < collectionDataClients[i].Accounts.Count; q++)
                            {
                                if (tempNumberAccountWriteOff == collectionDataClients[i].Accounts[q].NumberAccount)
                                {
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Депозитный")
                                    {
                                        IBankOutBalance<Deposit<string, decimal>> outAmount = new OutBalance<Deposit<string, decimal>>(); //Контрвариантный интерфейс
                                        outAmount.WriteOffBalance(collectionDataClients[i].Id, collectionDataClients[i].Accounts[q].NumberAccount, tempAmountTranslation);
                                    }
                                    if (collectionDataClients[i].Accounts[q].NameAccount == "Обычный")
                                    {
                                        IBankOutBalance<NoDeposit<string, decimal>> outAmount = new OutBalance<NoDeposit<string, decimal>>(); //Контрвариантный интерфейс
                                        outAmount.WriteOffBalance(collectionDataClients[i].Id, collectionDataClients[i].Accounts[q].NumberAccount, tempAmountTranslation);
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }

                }
                catch (CodeException ex) when (ex.Code == 0)
                {
                    EnrollmentAmountAccountClient(viewClientIdReceiving, viewClientNumberAccountReceiving, viewAmountTranslation, collectionAccountClient);
                }
                catch (CodeException ex) when (ex.Code == 1)
                {
                    EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "баланс после списания не может быть отрицательным");
                    EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "баланс после списания не может быть отрицательным");
                }
            }
            //Если конвертация не удалась
            catch (FormatException)
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введенны не верные данные");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введенны не верные данные");
            }
            //Если числа слишком длинные
            catch (OverflowException)
            {
                EventLog?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введено слишком длинное число");
                EventError?.Invoke($"{personal.PostPersonal}", $"{personal.NamePersonal}", "введено слишком длинное число");
            }
        }
    }
}
