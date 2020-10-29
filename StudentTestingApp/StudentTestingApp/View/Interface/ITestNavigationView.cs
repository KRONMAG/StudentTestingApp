using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IView
    {
        event Action FinishTestEarlySelected;
        void SetQuestionViews(IEnumerable<IQuestionView> questionViews);
        void SetRemainingTime(int seconds);
    }
}