using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ITestsView : IView
    {
        event Action TestSelected;
        int SelectedTestId { get; }
        void SetTests(IReadOnlyList<Tuple<int, string>> tests);
    }
}