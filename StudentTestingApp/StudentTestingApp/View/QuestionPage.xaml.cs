﻿using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionPage : ContentPage, IQuestionView
    {
        private ObservableCollection<Answer> answers;
        private ObservableCollection<Answer> selectedAnswers;

        public QuestionPage()
        {
            InitializeComponent();
            answers = new ObservableCollection<Answer>();
            answersListView.ItemsSource = answers;
            selectedAnswers = new ObservableCollection<Answer>();
            selectedAnswersListView.ItemsSource = selectedAnswers;
        }

        private bool multipleChoiceAllowed()
        {
            var rightAnswerCount = 0;
            foreach (var answer in answers)
                if (answer.Right)
                    rightAnswerCount++;
            return rightAnswerCount > 1;
        }

        private void answerTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedAnswer = (Answer)answersListView.SelectedItem;
            if (multipleChoiceAllowed())
            {
                if (!selectedAnswers.Contains(selectedAnswer))
                    selectedAnswers.Add(selectedAnswer);
            }
            else
            {
                selectedAnswers.Clear();
                selectedAnswers.Add(selectedAnswer);
            }
        }

        private void selectedAnswerTapped(object sender, ItemTappedEventArgs e)
        {
            var unselectedAnswer = (Answer)selectedAnswersListView.SelectedItem;
            selectedAnswers.Remove(unselectedAnswer);
        }

        #region IQuestionView
        public IEnumerable<Answer> SelectedAnswers
        {
            get
            {
                return selectedAnswers.Select(x => x);
            }
        }

        public void SetAnswers(IEnumerable<Answer> answers)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.answers.Clear();
                answers.ToList().ForEach((answer) =>
                {
                    this.answers.Add(answer);
                });
            });
        }

        public void SetQuestion(Question question)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                textLabel.Text = question.Text;
                if (question.Image != null)
                    image.Source = ImageSource.FromStream(() => new MemoryStream(question.Image));
            });
        }
        #endregion
    }
}