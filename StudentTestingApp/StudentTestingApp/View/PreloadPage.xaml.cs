using System;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreloadPage : ContentPage, IPreloadView
    {
        public PreloadPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
 
        #region IPreloadPage
        public void Show()
        {
            App.Current.MainPage = this;
            new Timer(new TimerCallback(async (state) =>
            {
                await animatedImage.RotateTo(-360, 500);
                animatedImage.Rotation = 0;
                await animatedImage.RotateYTo(360, 500);
                animatedImage.RotationY = 0;
            }), null, 0, 1500);
        }

        public void ShowError(string message)
        {
            DisplayAlert("Ошибка", message, "Назад");
        }
        #endregion
    }
}