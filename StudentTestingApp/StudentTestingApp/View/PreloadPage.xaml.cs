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

        public void SetProcessName(string processName) =>
            Device.BeginInvokeOnMainThread(() => ProcessNameLabel.Text = processName);

        public void Show()
        {
            Application.Current.MainPage = new NavigationPage(this);
            Device.BeginInvokeOnMainThread(() =>
            {
                new Timer(async state =>
                {
                    await AnimatedImage.RotateTo(-360, 500);
                    AnimatedImage.Rotation = 0;
                    await AnimatedImage.RotateYTo(360, 500);
                    AnimatedImage.RotationY = 0;
                }, null, 0, 1500);
            });
        }

        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        #endregion
    }
}