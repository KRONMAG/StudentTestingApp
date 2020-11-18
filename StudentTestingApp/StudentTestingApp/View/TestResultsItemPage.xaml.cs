using System;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница отображения элемента списка результатов тестирования
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestResultsItemPage : PopupPage
    {
        /// <summary>
        /// Событие запроса публикации результата тестирования в системе Дневник
        /// </summary>
        public event Action ShareTestResult;

        /// <summary>
        /// Событие запроса удаления результата тестирования
        /// </summary>
        public event Action RemoveTestResult;

        /// <summary>
        /// Инициализация элемент управления
        /// </summary>
        /// <param name="result">
        /// Результат тестирования:
        /// - первый элемент кортежа - идентификатор результата тестирования;
        /// - второй элемент кортежа - наименование учебного предмета пройденного теста;
        /// - третий элемент кортежа - наименование пройденного теста;
        /// - четвертый элемент кортежа - дата и время начала тестирования;
        /// - пятый элемент кортежа - время в секундах, затраченное на прохождения теста;
        /// - шестой элемент кортежа - процент правильных ответов
        /// </param>
        public TestResultsItemPage(Tuple<int, string, string, DateTime, int, decimal> result)
        {
            BindingContext = result;
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки публикации результата тестирования в системе Дневник
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private async void ShareTestResultClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            ShareTestResult?.Invoke();
        }

        /// <summary>
        /// Обработчик нажатия кнопки удаления результата тестирования
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private async void RemoveTestResultClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
            RemoveTestResult?.Invoke();
        }

        /// <summary>
        /// Обработчик нажатия кнопки выхода с текущей страницы
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private async void BackClicked(object sender, EventArgs e) =>
            await PopupNavigation.Instance.PopAllAsync();
    }
}