using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface IQuestionView : IView
    {
        event Action AnswerSelected;
        event Action AnswerUnselected;
        int SelectedAnswerId { get; }
        int UnselectedAnswerId { get; }
        void SetSelectionMode(SelectionMode selectionMode);
        void SetQuestion(string text, byte[] image);
        void SetAnswers(IReadOnlyList<Tuple<int, string>> answers);
    }
}