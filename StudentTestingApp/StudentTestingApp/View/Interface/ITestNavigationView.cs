using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ITestNavigationView : IView
    {
        void SetQuestionViews(IEnumerable<IQuestionView> questionViews);
        void StartTimer(int testDuration);
        event Action OnTestEnd;
    }
}