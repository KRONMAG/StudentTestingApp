using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления списка тестов
    /// </summary>
    public class TestsPresenter : BasePresenter<ITestsView, Subject>
    {
        /// <summary>
        /// Хранилище тестов
        /// </summary>
        private readonly IReadOnlyRepository<Test> _repository;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представлене списка тестов</param>
        /// <param name="repository">Хранилище тестов</param>
        public TestsPresenter(ApplicationController controller, ITestsView view, IReadOnlyRepository<Test> repository) :
            base(controller, view)
        {
            _repository = repository;
            view.SelectTest += SelectTest;
        }

        /// <summary>
        /// Обработчик выбора теста из списка
        /// </summary>
        private void SelectTest() =>
            controller.CreatePresenter<TestStartPresenter, Test>().Run
            (
                _repository.Get(view.SelectedTestId)
            );

        /// <summary>
        /// Показ списка тестов выбранного учебного предмета, представления
        /// </summary>
        /// <param name="subject">Выбранный учебный предмет</param>
        public override void Run(Subject subject)
        {
            view.ShowTests
            (
                _repository
                    .Get(test => test.SubjectId == subject.Id)
                    .Select(test => new Tuple<int, string>(test.Id, test.Name))
                    .ToList()
            );
            base.Run(subject);
        }
    }
}