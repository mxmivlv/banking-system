using Module_14.Commands;
using Module_14.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Module_14.ViewModels
{
    internal class ViewModelMainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private Bank bank;

        #region Title, ComboBox, GroupBox
        public string Title { get { return title; } }
        public string[] ComboBoxTypeAccount { get { return comboBoxtypeAccount; } }
        public string SelectedTypeAccount 
        {
            get { return selectedTypeAccount; }
            set
            {
                if (selectedTypeAccount == value)
                    return;
                selectedTypeAccount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedTypeAccount)));
            }
        }
        public string[] ComboBoxPersonal { get { return comboBoxPersonal; } }
        public string SelectedPersonal
        {
            get { return selectedPersonal; }
            set
            {
                if (selectedPersonal == value)
                    return;
                selectedPersonal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedPersonal)));
            }
        }
        public string GroupBoxAuthorization { get { return groupBoxAuthorization; } }
        public string GroupBoxDataClient { get { return groupBoxDataClient; } }
        public string GroupBoxEnrollment { get { return groupBoxEnrollment; } }
        public string GroupBoxOpenOrCloseAccount { get { return groupBoxOpenOrCloseAccount; } }
        public string GroupBoxTransfer { get { return groupBoxTransfer; } }

        private string title = "Банк";
        private string[] comboBoxtypeAccount = new string[] { "Депозитный", "Обычный" };
        private string selectedTypeAccount;
        private string[] comboBoxPersonal = new string[] { "Консультант", "Менеджер" };
        private string selectedPersonal;
        private string groupBoxAuthorization = "Авторизация";
        private string groupBoxDataClient = "Работа с данными клиента";
        private string groupBoxEnrollment = "Зачисление на счет клиенту";
        private string groupBoxOpenOrCloseAccount = "Открытие/Закрытие счета";
        private string groupBoxTransfer = "Перевод платежей";
        #endregion

        #region Button, TextBox
        public string ButtonEditingOrDeleteClient { get { return buttonEditingOrDeleteClient; } }
        public string ButtonLoadAccountClient { get { return buttonLoadAccountClient; } }
        public string ButtonAddClient { get { return buttonAddClient; } }
        public string ButtonDeleteClient { get { return buttonDeleteClient; } }
        public string ButtonEditingDataClient { get { return buttonEditingDataClient; } }
        public string ButtonEnrollment { get { return buttonEnrollment; } }
        public string ButtonOpenAccountClient { get { return buttonOpenAccountClient; } }
        public string ButtonCloseAccountClient { get { return buttonCloseAccountClient; } }
        public string ButtonTransfer { get { return buttonTransfer; } }
        public string ButtonAuthorization { get { return buttonAuthorization; } }
        public bool ViewModelIsEnabled
        {
            get { return viewModelIsEnabled; }
            set
            {
                if (viewModelIsEnabled == value)
                    return;
                viewModelIsEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ViewModelIsEnabled)));
            }
        }
        public bool ViewModelIsEnabledButton
        {
            get { return viewModelIsEnabledButton; }
            set
            {
                if (viewModelIsEnabledButton == value)
                    return;
                viewModelIsEnabledButton = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ViewModelIsEnabledButton)));
            }
        }
        public bool ViewModelIsReadOnly
        {
            get { return viewModelIsReadOnly; }
            set
            {
                if (viewModelIsReadOnly == value)
                    return;
                viewModelIsReadOnly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ViewModelIsReadOnly)));
            }
        }
        public string TextBoxId
        {
            get { return textBoxId; }
            set
            {
                if (textBoxId == value)
                    return;
                textBoxId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxId)));
            }
        }
        public string TextBoxSurname 
        {
            get { return textBoxSurname; }
            set 
            {
                if (textBoxSurname == value)
                    return;
                textBoxSurname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxSurname)));
            }
        }
        public string TextBoxName
        {
            get { return textBoxName; }
            set
            {
                if (textBoxName == value)
                    return;
                textBoxName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxName)));
            }
        }
        public string TextBoxPatronymic
        {
            get { return textBoxPatronymic; }
            set
            {
                if (textBoxPatronymic == value)
                    return;
                textBoxPatronymic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxPatronymic)));
            }
        }
        public string TextBoxPhoneNumber
        {
            get { return textBoxPhoneNumber; }
            set
            {
                if (textBoxPhoneNumber == value)
                    return;
                textBoxPhoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxPhoneNumber)));
            }
        }
        public string TextBoxPasport
        {
            get { return textBoxPasport; }
            set
            {
                if (textBoxPasport == value)
                    return;
                textBoxPasport = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxPasport)));
            }
        }
        public string TextBoxIdClientEnrollment 
        {
            get { return textBoxIdClientEnrollment; }
            set
            {
                if (textBoxIdClientEnrollment == value)
                    return;
                textBoxIdClientEnrollment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxIdClientEnrollment)));
            }
        }
        public string TextBoxNumberAccountEnrollment 
        {
            get { return textBoxNumberAccountEnrollment; }
            set
            {
                if (textBoxNumberAccountEnrollment == value)
                    return;
                textBoxNumberAccountEnrollment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxNumberAccountEnrollment)));
            }
        }
        public string TextBoxAmountEnrollment 
        {
            get { return textBoxAmountEnrollment; }
            set
            {
                if (textBoxAmountEnrollment == value)
                    return;
                textBoxAmountEnrollment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxAmountEnrollment)));
            }
        }
        public string TextBoxIdClientWriteOff
        {
            get { return textBoxIdClientWriteOff; }
            set
            {
                if (textBoxIdClientWriteOff == value)
                    return;
                textBoxIdClientWriteOff = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxIdClientWriteOff)));
            }
        }
        public string TextBoxNumberAccountClientWriteOff
        {
            get { return textBoxNumberAccountClientWriteOff; }
            set
            {
                if (textBoxNumberAccountClientWriteOff == value)
                    return;
                textBoxNumberAccountClientWriteOff = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxNumberAccountClientWriteOff)));
            }
        }
        public string TextBoxIdClientReceiving
        {
            get { return textBoxIdClientReceiving; }
            set
            {
                if (textBoxIdClientReceiving == value)
                    return;
                textBoxIdClientReceiving = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxIdClientReceiving)));
            }
        }
        public string TextBoxNumberAccountClientReceiving
        {
            get { return textBoxNumberAccountClientReceiving; }
            set
            {
                if (textBoxNumberAccountClientReceiving == value)
                    return;
                textBoxNumberAccountClientReceiving = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxNumberAccountClientReceiving)));
            }
        }
        public string TextBoxAmountTranslation
        {
            get { return textBoxAmountTranslation; }
            set
            {
                if (textBoxAmountTranslation == value)
                    return;
                textBoxAmountTranslation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.TextBoxAmountTranslation)));
            }
        }

        private string textBoxId;
        private string textBoxSurname;
        private string textBoxName;
        private string textBoxPatronymic;
        private string textBoxPhoneNumber;
        private string textBoxPasport;

        private string textBoxIdClientEnrollment;
        private string textBoxNumberAccountEnrollment;
        private string textBoxAmountEnrollment;

        private string textBoxIdClientWriteOff;
        private string textBoxNumberAccountClientWriteOff;
        private string textBoxIdClientReceiving;
        private string textBoxNumberAccountClientReceiving;
        private string textBoxAmountTranslation;

        private string buttonEditingOrDeleteClient = "Загрузить для редактирования";
        private string buttonLoadAccountClient = "Загрузить счета клиента";
        private string buttonAddClient = "Добавить";
        private string buttonDeleteClient = "Удалить";
        private string buttonEditingDataClient = "Редактировать";
        private string buttonEnrollment = "Зачислить";
        private string buttonOpenAccountClient = "Открыть";
        private string buttonCloseAccountClient = "Закрыть";
        private string buttonTransfer = "Перевести";
        private string buttonAuthorization = "Войти";

        private bool viewModelIsEnabled;
        private bool viewModelIsEnabledButton;
        private bool viewModelIsReadOnly = false;
        #endregion

        #region Label
        public string LabelTypeAccount { get { return labelTypeAccount; } }
        public string LabelId { get { return labelId; } }
        public string LabelSurname { get { return labelSurname; } }
        public string LabelName { get { return labelName; } }
        public string LabelPatronymic { get { return labelPatronymic; } }
        public string LabelPhoneNumber { get { return labelPhoneNumber; } }
        public string LabelPasport { get { return labelPasport; } }
        public string LabelNumberAccountClient { get { return labelNumberAccountClient; } }
        public string LabelAmount { get { return labelAmount; } }
        public string LabelNumberAccountWriteOff { get { return labelNumberAccountWriteOff; } }
        public string LabelNumberAccountEnrollment { get { return labelNumberAccountEnrollment; } }

        private string labelTypeAccount = "Тип счета";
        private string labelId = "Идентификатор";
        private string labelSurname = "Фамилия клиента";
        private string labelName = "Имя клиента";
        private string labelPatronymic = "Отчество клиента";
        private string labelPhoneNumber = "Номер телефона";
        private string labelPasport = "Паспорт клиента";
        private string labelNumberAccountClient = "Номер счета";
        private string labelAmount = "Сумма";
        private string labelNumberAccountWriteOff = "Счет клиента для списания";
        private string labelNumberAccountEnrollment = "Счет клиента для зачисления";
        #endregion

        #region Collection
        public ObservableCollection<Client> CollectionClient
        {
            get { return collectionClient; }
            set
            {
                if (collectionClient == value)
                    return;
                collectionClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CollectionClient)));
            }
        }
        public ObservableCollection<Account<string, decimal>> CollectionAccountClient
        {
            get { return collectionAccountClient; }
            set
            {
                if (collectionAccountClient == value)
                    return;
                collectionAccountClient = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.CollectionAccountClient)));
            }
        }
        public Client SelectedClientListView
        {
            get { return selectedClientListView; }
            set
            {
                if (selectedClientListView == value)
                    return;
                selectedClientListView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedClientListView)));
            }
        }
        public Account<string,decimal> SelectedAccountListView 
        {
            get { return selectedAccountListView; }
            set
            {
                if (selectedAccountListView == value)
                    return;
                selectedAccountListView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.SelectedAccountListView)));
            }
        }

        private ObservableCollection<Client> collectionClient;
        private ObservableCollection<Account<string, decimal>> collectionAccountClient;
        private Client selectedClientListView;
        private Account<string, decimal> selectedAccountListView;

        #endregion

        #region Команды
        #region Команда авторизации
        public ICommand CommandAuthorization { get; }
        private void onCommandAuthorizationExecuted(object p)
        {
            bank.CreatePersonal(SelectedPersonal);
            bank.LoadViewCollectionClient(CollectionClient);
            ViewModelIsEnabled = bank.IsEnabledElements(SelectedPersonal);
            ViewModelIsReadOnly = bank.IsReadOnlyElement(SelectedPersonal);

            ViewModelIsEnabledButton = true;

            TextBoxId = null;
            TextBoxSurname = null;
            TextBoxName = null;
            TextBoxPatronymic = null;
            TextBoxPhoneNumber = null;
            TextBoxPasport = null;

            CollectionAccountClient.Clear();
        }
        private bool commandAuthorizationExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда загрузки счетов клиента 
        public ICommand CommandLoadAccountClient { get; }
        private void onCommandLoadAccountClientExecuted(object p)
        {
            bank.LoadBankAccount(SelectedClientListView, CollectionAccountClient);
        }
        private bool commandLoadAccountClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда добавление клиента 
        public ICommand CommandAddClient { get; }
        private void onCommandAddClientExecuted(object p)
        {
            bank.AddClient(TextBoxSurname, TextBoxName, TextBoxPatronymic, TextBoxPhoneNumber, TextBoxPasport, CollectionClient);

            TextBoxId = null;
            TextBoxSurname = null;
            TextBoxName = null;
            TextBoxPatronymic = null;
            TextBoxPhoneNumber = null;
            TextBoxPasport = null;
        }
        private bool commandAddClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда добавление клиента для редактирования или удаления его из коллекции
        public ICommand CommandEditingOrDeleteClient { get; }
        private void onCommandEditingOrDeleteClientExecuted(object p)
        {
            SelectedClientListView ??= new Client("nullId"," "," "," "," "," ");

            TextBoxId = SelectedClientListView.Id;
            TextBoxSurname = SelectedClientListView.Surname;
            TextBoxName = SelectedClientListView.Name;
            TextBoxPatronymic = SelectedClientListView.Patronymic;
            TextBoxPhoneNumber = SelectedClientListView.PhoneNumber;
            TextBoxPasport = SelectedClientListView.Pasport;

        }
        private bool commandEditingOrDeleteClienttExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда удаление клиента 
        public ICommand CommandDeleteClient { get; }
        private void onCommandDeleteClientExecuted(object p)
        {
            bank.DeleteClient(TextBoxId, CollectionClient);

            TextBoxId = null;
            TextBoxSurname = null;
            TextBoxName = null;
            TextBoxPatronymic = null;
            TextBoxPhoneNumber = null;
            TextBoxPasport = null;
        }
        private bool commandDeleteClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда редактирования клиента 
        public ICommand CommandEditingClient { get; }
        private void onCommandEditingClientExecuted(object p)
        {
            bank.EditingClient(TextBoxId,TextBoxSurname, TextBoxName, TextBoxPatronymic, TextBoxPhoneNumber, TextBoxPasport, CollectionClient);

            TextBoxId = null;
            TextBoxSurname = null;
            TextBoxName = null;
            TextBoxPatronymic = null;
            TextBoxPhoneNumber = null;
            TextBoxPasport = null;
        }
        private bool commandEditingClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда зачисления средств на счет клиента 
        public ICommand CommandEnrollmentAmountAccountClient { get; }
        private void onCommandEnrollmentAmountAccountClientExecuted(object p)
        {
            bank.EnrollmentAmountAccountClient(TextBoxIdClientEnrollment, TextBoxNumberAccountEnrollment, TextBoxAmountEnrollment, CollectionAccountClient);

            TextBoxIdClientEnrollment = null;
            TextBoxNumberAccountEnrollment = null;
            TextBoxAmountEnrollment = null;
        }
        private bool commandEnrollmentAmountAccountClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда открытие счета клиенту 
        public ICommand CommandOpenAccountClient { get; }
        private void onCommandOpenAccountClientExecuted(object p)
        {
            bank.OpenAccount(SelectedClientListView, SelectedTypeAccount, CollectionAccountClient);
        }
        private bool commandOpenAccountClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда закрытие счета клиента 
        public ICommand CommandCloseAccountClient { get; }
        private void onCommandCloseAccountClientExecuted(object p)
        {
            bank.CloseAccount(SelectedClientListView, SelectedAccountListView, CollectionAccountClient);
        }
        private bool commandCloseAccountClientExecute(object p)
        {
            return true;
        }
        #endregion

        #region Команда перевода средств 
        public ICommand CommandTranslation { get; }
        private void onCommandTranslationtExecuted(object p)
        {
            bank.Translation(TextBoxIdClientWriteOff, TextBoxNumberAccountClientWriteOff, TextBoxIdClientReceiving, TextBoxNumberAccountClientReceiving, TextBoxAmountTranslation, CollectionAccountClient);

            TextBoxIdClientWriteOff = null;
            TextBoxNumberAccountClientWriteOff = null;
            TextBoxIdClientReceiving = null;
            TextBoxNumberAccountClientReceiving = null;
            TextBoxAmountTranslation = null;
        }
        private bool commandTranslationExecute(object p)
        {
            return true;
        }
        #endregion
        #endregion

        public ViewModelMainWindow() 
        {
            CollectionClient = new ObservableCollection<Client>();
            CollectionAccountClient = new ObservableCollection<Account<string, decimal>>();
            bank = new Bank();
            SelectedPersonal = ComboBoxPersonal[0];
            SelectedTypeAccount = ComboBoxTypeAccount[0];
            CommandAuthorization = new Command(onCommandAuthorizationExecuted, commandAuthorizationExecute);
            CommandLoadAccountClient = new Command(onCommandLoadAccountClientExecuted, commandLoadAccountClientExecute);
            CommandAddClient = new Command(onCommandAddClientExecuted, commandAddClientExecute);
            CommandAddClient = new Command(onCommandAddClientExecuted, commandAddClientExecute);
            CommandEditingOrDeleteClient = new Command(onCommandEditingOrDeleteClientExecuted, commandEditingOrDeleteClienttExecute);
            CommandDeleteClient = new Command(onCommandDeleteClientExecuted, commandDeleteClientExecute);
            CommandEditingClient = new Command(onCommandEditingClientExecuted, commandEditingClientExecute);
            CommandEnrollmentAmountAccountClient = new Command(onCommandEnrollmentAmountAccountClientExecuted, commandEditingOrDeleteClienttExecute);
            CommandOpenAccountClient = new Command(onCommandOpenAccountClientExecuted, commandOpenAccountClientExecute);
            CommandCloseAccountClient = new Command(onCommandCloseAccountClientExecuted, commandCloseAccountClientExecute);
            CommandTranslation = new Command(onCommandTranslationtExecuted, commandTranslationExecute);
            Application.Current.MainWindow.Closing += new CancelEventHandler(bank.SaveDataClientAsync);
            bank.CreateFileClientsInBank();
        }
    }
}
