using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WaitingAnimationPage : PopupPage
    {
        public string Message
        {
            get => MessageLabel.Text;
            set => MessageLabel.Text = value;
        }

        public WaitingAnimationPage()
        {
            InitializeComponent();
        }
    }
}