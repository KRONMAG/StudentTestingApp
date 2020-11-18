using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    /// <summary>
    /// Страница отображения вопроса и варианов ответа на него
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage, IQuestionView
    {
        /// <summary>
        /// Выбранные варианты ответа
        /// </summary>
        private readonly ObservableCollection<Tuple<int, string>> _selectedAnswers;

        /// <summary>
        /// Режим выбора вариантов ответа
        /// </summary>
        private SelectionMode _selectionMode;

        /// <summary>
        /// Инициализация страницы
        /// </summary>
        public QuestionPage()
        {
            InitializeComponent();
            _selectedAnswers = new ObservableCollection<Tuple<int, string>>();
            SelectedAnswersListView.ItemsSource = _selectedAnswers;
            _selectionMode = SelectionMode.Single;
        }

        /// <summary>
        /// Обработчик нажатия на элемент списка вариантов ответов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void AnswerTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedAnswer = (Tuple<int, string>)AnswersListView.SelectedItem;
            if (!_selectedAnswers.Contains(selectedAnswer))
            {
                if (_selectionMode == SelectionMode.Single)
                    _selectedAnswers.Clear();
                _selectedAnswers.Add(selectedAnswer);
                SelectAnswer?.Invoke(selectedAnswer.Item1);
            }
        }

        /// <summary>
        /// Обработчик нажатия на элемент списка выбранных вариантов ответов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void SelectedAnswerTapped(object sender, ItemTappedEventArgs e)
        {
            var unselectedAnswer = (Tuple<int, string>)SelectedAnswersListView.SelectedItem;
            _selectedAnswers.Remove(unselectedAnswer);
            UnselectAnswer?.Invoke(unselectedAnswer.Item1);
        }

        #region IQuestionView

        /// <summary>
        /// Событие выбора варианта ответа
        /// Параметр события - идентификатор выбранного варианта ответа
        /// </summary>
        public event Action<int> SelectAnswer;

        /// <summary>
        /// Событие отмены выбора варианта ответа
        /// Параметр события - идентификатор вариант ответа для отмены выбора
        /// </summary>
        public event Action<int> UnselectAnswer;

        /// <summary>
        /// Установка режима выбора вариантов ответов
        /// </summary>
        /// <param name="selectionMode">Режим выбора ответов</param>
        public void SetSelectionMode(SelectionMode selectionMode) =>
            _selectionMode = selectionMode;

        /// <summary>
        /// Показ вопроса
        /// </summary>
        /// <param name="text">Текст вопроса</param>
        /// <param name="image">Изображение, поясняющее текст вопроса</param>
        public void ShowQuestion(string text, byte[] image = null)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                TextLabel.Text = text;
                if (image != null)
                    Image.Source = ImageSource.FromStream(() => new MemoryStream(image));
            });
        }

        /// <summary>
        /// Показ вариантов ответа на вопрос
        /// </summary>
        /// <param name="answers">
        /// Варианты ответа на вопрос:
        /// - первый элемент кортежа - идентификатор варианта ответа;
        /// - второй элемент кортежа - текст варианта ответа</param>
        public void ShowAnswers(IReadOnlyList<Tuple<int, string>> answers)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _selectedAnswers.Clear();
                AnswersListView.ItemsSource = answers;
            });
        }

        /// <summary>
        /// Страница не заносится в стек навигации, так как она является элементом
        /// страницы с вкладками вопросов TestNavigationPage и отображается внутри нее
        /// </summary>
        public void Show()
        {

        }

        #endregion
    }
}