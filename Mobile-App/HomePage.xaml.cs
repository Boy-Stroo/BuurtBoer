namespace Mobile_App;

public partial class HomePage : ContentPage
{
    List<DateTime> weekDates;
    public HomePage()
    {
        InitializeComponent();
        DateTime today = DateTime.Today;
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
    }
    private List<DateTime> GetCurrentWeekDates()
    {
        DateTime today = DateTime.Today;
        int delta = today.DayOfWeek - DayOfWeek.Monday;

        // Adjust the date to Monday of the current week
        DateTime monday = today.AddDays(-delta);

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
        List<DateTime> weekDates = GetCurrentWeekDates();

        MondayDateLabel.Text = weekDates[0].ToString("MMMM dd");
        TuesdayDateLabel.Text = weekDates[1].ToString("MMMM dd");
        WednesdayDateLabel.Text = weekDates[2].ToString("MMMM dd");
        ThursdayDateLabel.Text = weekDates[3].ToString("MMMM dd");
        FridayDateLabel.Text = weekDates[4].ToString("MMMM dd");
    }

    private async void CheckBox_Clicked(object sender, EventArgs e)
    {
        if (sender is CheckBox checkBox)
        {
            int index = int.Parse(checkBox.ClassId);
            DateTime selectedDate = weekDates[index];

            // Here you can save the attendance for the selected date in your database
            // Create an OfficeDay object and save it to your database using Entity Framework
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
        await Navigation.PushAsync(new SettingsPage());
        button.IsEnabled = true;
    }


}