using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница предварительной настройки приложения
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreloadPage : ContentPage, IPreloadView
    {
        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public PreloadPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        #region IPreloadPage

        /// <summary>
        /// Показ наименования текущего шага процесса предварительной настройки приложения
        /// </summary>
        /// <param name="header">Наименование шага</param>
        public void ShowStepName(string processName) =>
            Device.BeginInvokeOnMainThread(() => ProcessNameLabel.Text = processName);

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                DisplayAlert(string.Empty, message, "Назад"));

        /// <summary>
        /// Показ представления, анимации ожидания
        /// </summary>
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

        #endregion
    }
}