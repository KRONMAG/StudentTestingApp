using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface IQuestionView : IView
    {
        int SelectedAnswerId { get; }
        int UnselectedAnswerId { get; }
        void SetSelectionMode(SelectionMode selectionMode);
        void SetQuestion(string text, byte[] image);
        void SetAnswers(IEnumerable<Tuple<int, string>> answers);
        event Action AnswerSelected;
        event Action AnswerUnselected;
    }
}