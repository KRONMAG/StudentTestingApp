﻿using System;
using System.Threading.Tasks;
using StudentTestingApp.View.Interface;
using StudentTestingApp.Model;
using Unity;

namespace StudentTestingApp.Presenter
{
    public class PreloadPresenter
    {
        private IPreloadView preloadView;

        public PreloadPresenter(IPreloadView preloadView)
        {
            this.preloadView = preloadView;
            preloadView.Show();
            new Task(() =>
            {
                if (!DB.Instance.InitializeDB()) preloadView.ShowError("Не удалось загрузить тесты, проверьте наличие интернет-соединения");
                new SubjectPresenter(App.Container.Resolve<ISubjectListView>());
            }).Start();
        }
    }
}