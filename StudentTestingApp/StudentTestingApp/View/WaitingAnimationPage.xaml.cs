using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница с анимацией ожидания
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitingAnimationPage : PopupPage
    {
        /// <summary>
        /// Текст, отображаемый рядом с анимацией ожидания
        /// </summary>
        public string Message
        {
            get => MessageLabel.Text;
            set => MessageLabel.Text = value;
        }

        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public WaitingAnimationPage()
        {
            CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed() =>
            true;
    }
}