using ContactManager.WPF.Models;
using ContactManager.WPF.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactManager.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ContactService _contactService = new();

        public ObservableCollection<ContactDto> Contacts { get; set; } = new();

        private ContactDto _selectedContact;
        public ContactDto SelectedContact
        {
            get => _selectedContact;
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                if (value != null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    PhoneNumber = value.PhoneNumber;
                }
                else
                {
                    FirstName = LastName = PhoneNumber = string.Empty;
                }
            }
        }

        private string _firstName = "";
        public string FirstName
        {
            get => _firstName; set { _firstName = value; OnPropertyChanged(); }
        }
        private string _lastName = "";
        public string LastName
        {
            get => _lastName; set { _lastName = value; OnPropertyChanged(); }
        }
        private string _phoneNumber = "";
        public string PhoneNumber
        {
            get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); }
        }

        public ICommand LoadContactsCommand { get; }
        public ICommand CreateContactCommand { get; }
        public ICommand UpdateContactCommand { get; }
        public ICommand DeleteContactCommand { get; }
        public ICommand ClearFormCommand { get; }

        public MainViewModel()
        {
            LoadContactsCommand = new RelayCommand(async _ => await LoadContactsAsync());
            CreateContactCommand = new RelayCommand(async _ => await CreateContactAsync(), _ => CanCreateOrUpdate());
            UpdateContactCommand = new RelayCommand(async _ => await UpdateContactAsync(), _ => SelectedContact != null);
            DeleteContactCommand = new RelayCommand(async _ => await DeleteContactAsync(), _ => SelectedContact != null);
            ClearFormCommand = new RelayCommand(_ => ClearForm());
            Task.Run(async () => await LoadContactsAsync());
        }

        private async Task LoadContactsAsync()
        {
            Contacts.Clear();
            var items = await _contactService.GetAllAsync();
            foreach (var item in items) Contacts.Add(item);
            ClearForm();
        }

        private async Task CreateContactAsync()
        {
            var dto = new CreateContactDto
            {
                FirstName = FirstName?.Trim() ?? "",
                LastName = LastName?.Trim() ?? "",
                PhoneNumber = PhoneNumber?.Trim() ?? ""
            };
            await _contactService.CreateAsync(dto);
            await LoadContactsAsync();
        }

        private async Task UpdateContactAsync()
        {
            if (SelectedContact == null) return;
            var dto = new UpdateContactDto
            {
                FirstName = FirstName?.Trim() ?? "",
                LastName = LastName?.Trim() ?? "",
                PhoneNumber = PhoneNumber?.Trim() ?? ""
            };
            await _contactService.UpdateAsync(SelectedContact.Id, dto);
            await LoadContactsAsync();
        }

        private async Task DeleteContactAsync()
        {
            if (SelectedContact == null) return;
            await _contactService.DeleteAsync(SelectedContact.Id);
            await LoadContactsAsync();
        }

        private void ClearForm()
        {
            FirstName = "";
            LastName = "";
            PhoneNumber = "";
            SelectedContact = null;
        }

        private bool CanCreateOrUpdate()
            => !string.IsNullOrWhiteSpace(FirstName)
            && !string.IsNullOrWhiteSpace(LastName)
            && !string.IsNullOrWhiteSpace(PhoneNumber);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string p = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
    }
}