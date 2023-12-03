using System.Text;
using Newtonsoft.Json;
namespace Mobile_App;

public partial class HomePage : ContentPage
{
    string apiUrl = "http://10.0.0.2:5077";
    List<DateTime> weekDates;
    UserController UserController;
    private readonly OfficeDayService _officeDayService = new OfficeDayService();

    public HomePage(UserController controller)
    {
        InitializeComponent();
        DateTime today = DateTime.Today;
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
        UserController = controller;
    }
    public HomePage()
    {
        InitializeComponent();
        DateTime today = DateTime.Today;
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
        UserController = new UserController(new UserService());
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var user = UserController.CurrentUser;
        if (user != null)
        {
            var name = user.FirstName + " " + user.LastName;
            welcomeLabel.Text = "Welcome, " + name;
        }
    }
    private List<DateTime> GetCurrentWeekDates(DateTime startDate)
    {
        int delta = DayOfWeek.Monday - startDate.DayOfWeek;

        // Adjust the date to Monday of the current week
        DateTime monday = startDate.AddDays(delta);

        // Create a list to store the dates for the current week
        List<DateTime> weekDates = new List<DateTime>();

        // Populate the list with the dates for the current week
        for (int i = 0; i < 5; i++) // Assuming 5 working days (Monday to Friday)
        {
            weekDates.Add(monday.AddDays(i));
        }

        return weekDates;
    }

    private void UpdateDateLabels()
    {

        MondayDateLabel.Text = weekDates[0].ToString("MMMM dd");
        TuesdayDateLabel.Text = weekDates[1].ToString("MMMM dd");
        WednesdayDateLabel.Text = weekDates[2].ToString("MMMM dd");
        ThursdayDateLabel.Text = weekDates[3].ToString("MMMM dd");
        FridayDateLabel.Text = weekDates[4].ToString("MMMM dd");
    }

    private async void SaveSelection(object sender, EventArgs e)
    {
        List<DateTime> selectedDates = new List<DateTime>();

        // Check each checkbox to determine selected dates
        if (MondayCheckBox.IsChecked)
            selectedDates.Add(weekDates[0]);
        if (TuesdayCheckBox.IsChecked)
            selectedDates.Add(weekDates[1]);
        if (WednesdayCheckBox.IsChecked)
            selectedDates.Add(weekDates[2]);
        if (ThursdayCheckBox.IsChecked)
            selectedDates.Add(weekDates[3]);
        if (FridayCheckBox.IsChecked)
            selectedDates.Add(weekDates[4]);

        // Fetch existing OfficeDays from the server
        var existingDays = await _officeDayService.GetAllOfficeDays();
        if (existingDays != null)
        {
            var newOfficeDays = selectedDates
                .Select(date => new OfficeDay { UserId = UserController.CurrentUser.Id, Date = date })
                .ToList();

            var daysToAdd = newOfficeDays.Where(newDay =>
                !existingDays.Any(existingDay => existingDay.UserId == newDay.UserId && existingDay.Date == newDay.Date))
                .ToList();

            var daysToRemove = existingDays
                .Where(existingDay =>
                    !newOfficeDays.Any(newDay => existingDay.UserId == newDay.UserId && existingDay.Date == newDay.Date))
                .ToList();

            foreach (var dayToRemove in daysToRemove)
            {
                await _officeDayService.DeleteOfficeDay(dayToRemove.Id);
            }

            foreach (var dayToAdd in daysToAdd)
            {
                if (await _officeDayService.CreateOfficeDay(dayToAdd.Date, dayToAdd.UserId))
                {
                    await DisplayAlert("Success", "Selection saved!", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to fetch existing dates", "OK");
                }
            }
        }
    }


    private void PreviousWeekButton_Clicked(object sender, EventArgs e)
    {
        DateTime previousMonday = weekDates[0].AddDays(-7);
        DateTime today = DateTime.Today;


        if (previousMonday >= today.AddDays(-7))
        {
            weekDates = GetCurrentWeekDates(previousMonday);
            UpdateDateLabels();
        }
    }



    private void NextWeekButton_Clicked(object sender, EventArgs e)
    {
        DateTime nextMonday = weekDates[0].AddDays(7);
        DateTime today = DateTime.Today;

        if (nextMonday <= today.AddDays(7))
        {
            weekDates = GetCurrentWeekDates(nextMonday);
            UpdateDateLabels();
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        button.IsEnabled = false;
        await Navigation.PushAsync(new SettingsPage(UserController));
        button.IsEnabled = true;
    }


}