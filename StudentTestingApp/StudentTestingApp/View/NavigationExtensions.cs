using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentTestingApp.View
{
    public static class NavigationExtensions
    {
        public static async Task PushAsyncSingle(this INavigation navigation, Page page, bool animated = false)
        {
            if (navigation.NavigationStack.Last().GetType() != page.GetType())
                await navigation.PushAsync(page, animated);
        }
    }
}
