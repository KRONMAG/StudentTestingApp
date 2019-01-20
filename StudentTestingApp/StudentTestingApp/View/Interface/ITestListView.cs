﻿using System;
using System.Collections.Generic;

namespace StudentTestingApp.View.Interface
{
    public interface ITestListView : IView
    {
        int SelectedTestId { get; }
        void SetTests(IEnumerable<Tuple<int, string>> tests);
        event Action TestSelected;
    }
}