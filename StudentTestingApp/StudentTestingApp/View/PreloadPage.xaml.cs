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
        /// Показ представления
        /// </summary>
        public void Show() =>
            Application.Current.MainPage = new NavigationPage(this);

        #endregion
    }
}