using System;

namespace StudentTestingApp.View.Interface
{
    public interface IMainView : IView
    {
        event Action SubjectsViewSelected;
        event Action TestResultsViewSelected;
        event Action UpdateTestsSelected;
    }
}