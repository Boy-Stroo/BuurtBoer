using System.Text;
using Newtonsoft.Json;
namespace Mobile_App;

public partial class HomePage : ContentPage
{
    string apiUrl = "http://localhost:5077";
    List<DateTime> weekDates;
    UserController _userController;
   
    public HomePage(UserController controller)
    {
        InitializeComponent();
        DateTime today = DateTime.Today;
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
    }
    public HomePage()
    {
        InitializeComponent();
        DateTime today = DateTime.Today;
        weekDates = GetCurrentWeekDates(today);
        UpdateDateLabels();
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

        string jsonSelectedDates = JsonConvert.SerializeObject(selectedDates);

        using (var client = new HttpClient())
        {
            // Retrieve existing dates from the database
            var existingDatesResponse = await client.GetAsync(apiUrl); // Replace with your actual endpoint
            if (existingDatesResponse.IsSuccessStatusCode)
            {
                var existingDatesJson = await existingDatesResponse.Content.ReadAsStringAsync();
                List<DateTime> existingDates = JsonConvert.DeserializeObject<List<DateTime>>(existingDatesJson);

                // Find dates to add (not already in the database)
                var datesToAdd = selectedDates.Except(existingDates).ToList();

                // Find dates to remove (in the database but not selected)
                var datesToRemove = existingDates.Except(selectedDates).ToList();

                // Perform addition of new dates
                var addContent = new StringContent(JsonConvert.SerializeObject(datesToAdd), Encoding.UTF8, "application/json");
                var addResponse = await client.PostAsync(apiUrl, addContent); // Replace with your actual endpoint

                // Perform removal of unselected dates
                var removeContent = new StringContent(JsonConvert.SerializeObject(datesToRemove), Encoding.UTF8, "application/json");
                var removeResponse = await client.PostAsync(apiUrl, removeContent); // Replace with your actual endpoint

                if (addResponse.IsSuccessStatusCode && removeResponse.IsSuccessStatusCode)
                {
                    // Handle success (data saved and removed successfully)
                    await DisplayAlert("Success", "Selection saved!", "OK");
                }
                else
                {
                    // Handle failure (error in saving or removing data)
                    await DisplayAlert("Error", "Failed to save selection", "OK");
                }
            }
            else
            {
                // Handle failure (error in retrieving existing dates)
                await DisplayAlert("Error", "Failed to retrieve existing dates", "OK");
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
        await Navigation.PushAsync(new SettingsPage());
        button.IsEnabled = true;
    }


}