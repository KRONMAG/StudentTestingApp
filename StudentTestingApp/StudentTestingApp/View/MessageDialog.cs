using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Средство показа диалоговых окон
    /// </summary>
    public class MessageDialog : IMessageDialog
    {
        /// <summary>
        /// Отображение диалогового окна с заданным сообщением
        /// </summary>
        /// <param name="message">Сообщение для показа</param>
        public void ShowMessage(string message)
        {
            var navigationStack = App.Current.MainPage.Navigation.NavigationStack;
            var page = navigationStack[navigationStack.Count - 1];
            page.Dispatcher.BeginInvokeOnMainThread(() => page.DisplayAlert(string.Empty, message, "Назад"));
        }
    }
}