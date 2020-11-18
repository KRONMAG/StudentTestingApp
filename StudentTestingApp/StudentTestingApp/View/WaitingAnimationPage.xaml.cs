using Xamarin.Forms.Xaml;
using Xamarin.Forms;
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
        /// Инициализация страницы
        /// </summary>
        public WaitingAnimationPage()
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Назад":
        /// отменяет действие выхода со страницы анимации ожидания
        /// </summary>
        /// <returns>Ложь, если нужно уйти со страницы, иначе - истина</returns>
        protected override bool OnBackButtonPressed() =>
            true;

        /// <summary>
        /// Показ сообщения рядом с анимацией ожидания
        /// </summary>
        /// <param name="message">Сообщение для показа</param>
        public void ShowMessage(string message) =>
            Device.BeginInvokeOnMainThread(() =>
                MessageLabel.Text = message);
    }
}