using System;
using System.Linq;
using StudentTestingApp.Model.DataAccess.Interface;
using StudentTestingApp.Model.Entity;
using StudentTestingApp.Presenter.Common;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    /// <summary>
    /// Представитель представления списка учебных предметов
    /// </summary>
    public class SubjectsPresenter : BasePresenter<ISubjectsView>
    {
        /// <summary>
        /// Хранилище учебных предметов
        /// </summary>
        private readonly IReadOnlyRepository<Subject> _repository;

        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="controller">Контроллер приложения</param>
        /// <param name="view">Представление списка учебных предметов</param>
        /// <param name="repository">Хранилище учебных предметов</param>
        public SubjectsPresenter
            (ApplicationController controller,
            ISubjectsView view,
            IReadOnlyRepository<Subject> repository) :
            base(controller, view)
        {
            _repository = repository;
            view.SelectSubject += SelectSubject;
        }

        /// <summary>
        /// Обработчик события выбора учебного предмета из списка
        /// </summary>
        private void SelectSubject() =>
            controller.CreatePresenter<TestsPresenter, Subject>().Run
            (
                _repository
                    .Get()
                    .First(subject => subject.Id == view.SelectedSubjectId)
            );

        /// <summary>
        /// Показ списка учебных предметов, представления
        /// </summary>
        public override void Run()
        {
            view.ShowSubjects
            (
                _repository
                    .Get()
                    .Select(subject => new Tuple<int, string>(subject.Id, subject.Name))
                    .ToList()
            );
            base.Run();
        }
    }
}