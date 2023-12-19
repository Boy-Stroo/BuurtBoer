using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
namespace Mobile_App;

public partial class HomePage : ContentPage
{
    List<DateOnly> weekDates;
    UserController UserController;
    private readonly OfficeDayService _officeDayService = new OfficeDayService();

    public HomePage(UserController controller)
    {
        InitializeComponent();
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
        UserController = controller;
    }
    public HomePage()
    {
        InitializeComponent();
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
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

        // Get the current date
        DateTime currentDate = DateTime.Today;

        // Get the current week number
        int currentWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            currentDate,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );

        // Set the week label text
        WeekLabel.Text = $"Week {currentWeek}";

        // Set the month label text
        MonthLabel.Text = currentDate.ToString("MMMM");
    }
    private List<DateOnly> GetCurrentWeekDates(DateOnly startDate)
    {
        int delta = DayOfWeek.Monday - startDate.DayOfWeek;

        // Adjust the date to Monday of the current week
        DateOnly monday = startDate.AddDays(delta);

        // Create a list to store the dates for the current week
        List<DateOnly> weekDates = new List<DateOnly>();

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
        List<DateOnly> selectedDates = new List<DateOnly>();

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

        // Fetch all existing OfficeDays
        var allExistingDays = await _officeDayService.GetAllOfficeDays();
        if (allExistingDays != null)
        {
            var existingDaysForUser = allExistingDays
                .Where(day => day.UserId == UserController.CurrentUser.Id)
                .ToList();

            var daysToAdd = selectedDates
                .Where(newDay => !existingDaysForUser.Any(existingDay => existingDay.Date == newDay))
                .Select(date => new OfficeDay { UserId = UserController.CurrentUser.Id, Date = date })
                .ToList();

            var daysToRemove = existingDaysForUser
            .Where(existingDay =>
                !selectedDates.Contains(existingDay.Date) &&
                weekDates.Contains(existingDay.Date))
            .ToList();

            int counter = 0;

            foreach (var dayToRemove in daysToRemove)
            {
                await _officeDayService.DeleteOfficeDay(dayToRemove.Id);

            }

            foreach (var dayToAdd in daysToAdd)
            {
                if (await _officeDayService.CreateOfficeDay(dayToAdd.Date, dayToAdd.UserId))
                {
                    counter += 1;
                }
            
                else
                {
                    await DisplayAlert("Error", "Selection not saved!", "FAILED");
                }
            }

            if (counter > 0)
            {
                await DisplayAlert("Changes", "Selection saved!", "OK");
            }
        }
        else
        {
            await DisplayAlert("Error", "Failed to fetch existing dates", "OK");
        }
    }

    private void PreviousWeekButton_Clicked(object sender, EventArgs e)
    {
        DateOnly previousMonday = weekDates[0].AddDays(-7);
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);


        if (previousMonday >= today.AddDays(-7))
        {
            weekDates = GetCurrentWeekDates(previousMonday);
            UpdateDateLabels();
        }
    }



    private void NextWeekButton_Clicked(object sender, EventArgs e)
    {
        DateOnly nextMonday = weekDates[0].AddDays(7);
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

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