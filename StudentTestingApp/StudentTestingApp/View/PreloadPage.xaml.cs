using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreloadPage : ContentPage, IPreloadView
    {
        private Timer _timer;

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
                Application.Current.MainPage = new NavigationPage(this) {BarBackgroundColor = Color.FromHex("212121")};
                _timer = new Timer(async state =>
                {
                    await AnimatedImage.RotateTo(-360, 500);
                    AnimatedImage.Rotation = 0;
                    await AnimatedImage.RotateYTo(360, 500);
                    AnimatedImage.RotationY = 0;
                }, null, 0, 1500);
            });
        }

        public void Close()
        {
        }

        public void ShowError(string message)
        {
            Device.BeginInvokeOnMainThread(() => { DisplayAlert("Ошибка", message, "Назад"); });
        }

        #endregion
    }
}