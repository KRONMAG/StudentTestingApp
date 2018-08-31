using Unity;
using StudentTestingApp.Presenter.Interface;
using StudentTestingApp.View.Interface;

namespace StudentTestingApp.Presenter
{
    public class ApplicationController
    {
        private static ApplicationController _instance;

        public static ApplicationController Instance => _instance ?? (_instance = new ApplicationController());

        private readonly IUnityContainer _container;

        private ApplicationController()
        {
            _container = new UnityContainer();
        }

        public void RegisterModel<TFrom, TTo>() where TTo : class, TFrom
        {
            _container.RegisterType<TFrom, TTo>();
        }

        public void RegisterView<TFrom, TTo>() where TTo : class, TFrom where TFrom : IView
        {
            _container.RegisterType<TFrom, TTo>();
        }

        public void Run<T>() where T : class, IPresenter
        {
            if (!_container.IsRegistered<T>())
            {
                _container.RegisterType<T>();
            }

            _container.Resolve<T>().Run();
        }

        public void Run<T, TU>(TU parameter) where T : class, IPresenter<TU>
        {
            if (!_container.IsRegistered<T>())
            {
                _container.RegisterType<T>();
            }

            _container.Resolve<T>().Run(parameter);
        }
    }
}