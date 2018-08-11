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
            Device.BeginInvokeOnMainThread(() =>
            {
                new Timer(new TimerCallback(async (o) =>
                {
                    await AnimatedImage.RotateTo(-360, 500);
                    AnimatedImage.Rotation = 0;
                    await AnimatedImage.RotateYTo(360, 500);
                    AnimatedImage.RotationY = 0;
                }), null, 0, 1500);
            });
        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Ошибка", message, "Назад");
            });
        }
        #endregion
    }
}