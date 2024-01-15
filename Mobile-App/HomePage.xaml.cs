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

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        var user = UserController.CurrentUser;
        if (user != null)
        {
            var name = user.FirstName + " " + user.LastName;
            welcomeLabel.Text = "Welcome, " + name;
        }

        // huidige datum
        DateTime currentDate = DateTime.Today;

        // huidige weeknummer
        int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            currentDate,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );

        WeekLabel.Text = $"Week {weekNumber}";
        MonthLabel.Text = currentDate.ToString("MMMM");

        UpdateCheckboxes();
       
    }

    private async void UpdateCheckboxes()
    {
        var allExistingDays = await _officeDayService.GetAllOfficeDays();
        if (allExistingDays != null)
        {
            var existingDaysForUser = allExistingDays
                .Where(day => day.UserId == UserController.CurrentUser.Id)
                .ToList();
            // Filter existing days for the current week
            var existingDays = existingDaysForUser
                .Where(day => weekDates.Contains(day.Date))
                .ToList();

        MondayCheckBox.IsChecked = existingDays.Any(day => day.Date == weekDates[0]);
        TuesdayCheckBox.IsChecked = existingDays.Any(day => day.Date == weekDates[1]);
        WednesdayCheckBox.IsChecked = existingDays.Any(day => day.Date == weekDates[2]);
        ThursdayCheckBox.IsChecked = existingDays.Any(day => day.Date == weekDates[3]);
        FridayCheckBox.IsChecked = existingDays.Any(day => day.Date == weekDates[4]);
    }
    }

    private List<DateOnly> GetCurrentWeekDates(DateOnly startDate)
    {
        int delta = DayOfWeek.Monday - startDate.DayOfWeek;

        // datum wordt maandag van deze week
        DateOnly monday = startDate.AddDays(delta);

        // list voor dagen van deze week
        List<DateOnly> weekDates = new List<DateOnly>();

        // vul de lijst met maandag - vrijdag
        for (int i = 0; i < 5; i++)
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

        UpdateCheckboxes();
    }

    private async void SaveSelection(object sender, EventArgs e)
    {
        List<DateOnly> selectedDates = new List<DateOnly>();

        // check welke dagen zijn aangekruist
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

        // verkrijgen van alle officedays
        var allExistingDays = await _officeDayService.GetAllOfficeDays();
        if (allExistingDays != null)
        {
            // filter op current user
            var existingDaysForUser = allExistingDays
                .Where(day => day.UserId == UserController.CurrentUser.Id)
                .ToList();

            // filter op dagen die geselecteerd zijn en niet in de database zitten
            var daysToAdd = selectedDates
                .Where(newDay => !existingDaysForUser.Any(existingDay => existingDay.Date == newDay))
                .Select(date => new OfficeDay { UserId = UserController.CurrentUser.Id, Date = date })
                .ToList();

            // filter op dagen die niet geselecteerd zijn in deze week om te verwijderen
            var daysToRemove = existingDaysForUser
            .Where(existingDay =>
                !selectedDates.Contains(existingDay.Date) &&
                weekDates.Contains(existingDay.Date))
            .ToList();

            int counter = 0;

            foreach (var dayToRemove in daysToRemove)
            {
                if(await _officeDayService.DeleteOfficeDay(dayToRemove.Id))
                {
                    counter += 1;
                }
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

            // als er een wijziging is, wordt counter > 0, en komt er een popup op het scherm
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

    private void UpdateWeekNumber()
    {
        // bereken weeknummer met datum van eerste dag van de week
        int updatedWeekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            new DateTime(weekDates[0].Year, weekDates[0].Month, weekDates[0].Day),
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );

        WeekLabel.Text = $"Week {updatedWeekNumber}";
    }

    private void PreviousWeekButton_Clicked(object sender, EventArgs e)
    {
        DateOnly previousMonday = weekDates[0].AddDays(-7);
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        // Controleert of de vorige maandag gelijk is aan of na de start van de vorige week
        if (previousMonday >= today.AddDays(-7))
        {
            weekDates = GetCurrentWeekDates(previousMonday);
            UpdateDateLabels();
            UpdateWeekNumber();
        }
    }


    private void NextWeekButton_Clicked(object sender, EventArgs e)
    {
        DateOnly nextMonday = weekDates[0].AddDays(7);
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        // Controleert of de volgende maandag gelijk is aan of voor de datum die 7 dagen na vandaag ligt
        if (nextMonday <= today.AddDays(7))
        {
            weekDates = GetCurrentWeekDates(nextMonday);
            UpdateDateLabels();
            UpdateWeekNumber();
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