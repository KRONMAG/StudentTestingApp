using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ITestResultsView : IView
    {
        event Action RemoveTestResult;
        event Action RemoveAllTestResults;
        int TestResultToRemoveId { get; }
        void SetTestResults(IReadOnlyList<Tuple<int, string, string, DateTime, int, double>> results);
    }
}