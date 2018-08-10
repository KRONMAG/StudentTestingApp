using System;

namespace StudentTestingApp.View.Interface
{
    interface IDerivedView
    {
        void Show(IParentView parentView);
    }
}
