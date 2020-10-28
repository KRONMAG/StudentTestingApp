using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.View.Interface;
using SelectionMode = StudentTestingApp.View.SelectionMode;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage, IQuestionView
    {
        private readonly ObservableCollection<Tuple<int, string>> _selectedAnswers;

        private SelectionMode _selectionMode;

        public QuestionPage()
        {
            InitializeComponent();
            _selectedAnswers = new ObservableCollection<Tuple<int, string>>();
            SelectedAnswersListView.ItemsSource = _selectedAnswers;
            _selectionMode = SelectionMode.Single;
        }

        private void AnswerTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedAnswer = (Tuple<int, string>)AnswersListView.SelectedItem;
            if (!_selectedAnswers.Contains(selectedAnswer))
            {
                if (_selectionMode == SelectionMode.Single)
                    _selectedAnswers.Clear();
                _selectedAnswers.Add(selectedAnswer);
                AnswerSelected?.Invoke();
            }
        }

        private void SelectedAnswerTapped(object sender, ItemTappedEventArgs e)
        {
            var unselectedAnswer = (Tuple<int, string>)SelectedAnswersListView.SelectedItem;
            _selectedAnswers.Remove(unselectedAnswer);
            AnswerUnselected?.Invoke();
        }

        #region IQuestionView

        public event Action AnswerSelected;

        public event Action AnswerUnselected;

        public int SelectedAnswerId => ((Tuple<int, string>)AnswersListView.SelectedItem).Item1;

        public int UnselectedAnswerId => ((Tuple<int, string>)SelectedAnswersListView.SelectedItem).Item1;

        public void Show()
        {

        }

        public void SetSelectionMode(SelectionMode selectionMode) =>
            _selectionMode = selectionMode;

        public void SetQuestion(string text, byte[] image)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                TextLabel.Text = text;
                if (image != null)
                    Image.Source = ImageSource.FromStream(() => new MemoryStream(image));
            });
        }

        public void SetAnswers(IEnumerable<Tuple<int, string>> answers)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _selectedAnswers.Clear();
                AnswersListView.ItemsSource = answers;
            });
        }

        #endregion
    }
}