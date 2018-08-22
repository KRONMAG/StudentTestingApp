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
            Device.BeginInvokeOnMainThread(() =>
            {
                App.Current.MainPage = new NavigationPage(this) { BarBackgroundColor = Color.FromHex("212121") };
                new Timer(new TimerCallback(async (state) =>
                {
                    await animatedImage.RotateTo(-360, 500);
                    animatedImage.Rotation = 0;
                    await animatedImage.RotateYTo(360, 500);
                    animatedImage.RotationY = 0;
                }), null, 0, 1500);
            });
        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DisplayAlert("Ошибка", message, "Назад");
            });
        }
        #endregion
    }
}