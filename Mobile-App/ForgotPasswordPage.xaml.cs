using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_App;

public partial class ForgotPasswordPage : ContentPage
{
    public ForgotPasswordPage()
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void ImageButton_OnClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}