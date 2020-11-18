using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Расширение навигации
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Добавление страницы на вершину стека
        /// Метод устраняет эффект множественного открытия одной той же страницы
        /// при многократном нажатии кнопки (выборе элемента списка) перехода к этой странице
        /// Если на вершине стека навигации уже есть страница данного типа, то она не заносится в стек
        /// </summary>
        /// <param name="navigation">Средсто навигации</param>
        /// <param name="page">Страница, которую требуется показать</param>
        /// <param name="animated">Следует ли использовать анимацию при переходе к странице</param>
        /// <returns></returns>
        public static async Task PushAsyncSingle(this INavigation navigation, Page page, bool animated = false)
        {
            if (navigation.NavigationStack.Last().GetType() != page.GetType())
                await navigation.PushAsync(page, animated);
        }
    }
}
