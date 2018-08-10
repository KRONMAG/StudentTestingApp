using System;

namespace StudentTestingApp.View.Interface
{
    interface ITestListView : IDerivedView
    {
        void ShowTest(string testName);
        string SelectedTestName { get; }
        event Action OnSelectTest;
    }
}
