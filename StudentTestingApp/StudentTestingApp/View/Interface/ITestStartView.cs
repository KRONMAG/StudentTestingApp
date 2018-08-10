using System;

namespace StudentTestingApp.View.Interface
{
    interface ITestStartView : IDerivedView
    {
        void ShowTestInfo(string testName, int questionCount, int? duration);
    }
}