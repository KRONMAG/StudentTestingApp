using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    public class MessageDialog : IMessageDialog
    {
        public void ShowMessage(string message)
        {
            var navigationStack = App.Current.MainPage.Navigation.NavigationStack;
            var page = navigationStack[navigationStack.Count - 1];
            page.Dispatcher.BeginInvokeOnMainThread(() => page.DisplayAlert(string.Empty, message, "Назад"));
        }
    }
}