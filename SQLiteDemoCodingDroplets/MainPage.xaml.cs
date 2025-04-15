namespace SQLiteDemoCodingDroplets
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int editcus;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listView.ItemsSource = await _dbService.GetCustomers());
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (editcus == 0)
            { //add
                await _dbService.Create(new Customer
                {
                    CustomerName = name.Text,
                    Email = email.Text,
                    Mobile = mobile.Text,
                });
            }
            else
            {
                //update
                await _dbService.Update(new Customer
                {
                    Id = editcus,
                    CustomerName = name.Text,
                    Email = email.Text,
                    Mobile = mobile.Text,
                });

                editcus = 0;
            }
            name.Text = string.Empty;
            email.Text = string.Empty;
            mobile.Text = string.Empty;

            listView.ItemsSource = await _dbService.GetCustomers();
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var customer = (Customer)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    editcus = customer.Id;
                    name.Text = customer.CustomerName;
                    email.Text = customer.Email;
                    mobile.Text = customer.Mobile;
                    break;

                case "Delete":
                    await _dbService.Delete(customer);
                    listView.ItemsSource = await _dbService.GetCustomers();
                    break;
            }
        }
    }
}