using Xamarin.Forms;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    public class WaitingAnimation : IWaitingAnimation
    {
        private static readonly WaitingAnimationPage _waitingAnimationPage;

        static WaitingAnimation()
        {
            _waitingAnimationPage = new WaitingAnimationPage();
        }

        public void StartAnimation(string message) =>
            Device.BeginInvokeOnMainThread
            (
                () =>
                {
                    _waitingAnimationPage.Message = message;
                    App.Current.MainPage.Navigation.PushModalAsync(_waitingAnimationPage);
                }
            );

        public void StopAnimation() =>
            Device.BeginInvokeOnMainThread
            (
                () =>
                {
                    App.Current.MainPage.Navigation.PopModalAsync();
                }
            );
    }
}