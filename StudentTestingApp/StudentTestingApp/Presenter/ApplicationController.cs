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

        private ApplicationController() =>
            _container = new UnityContainer();

        public void RegisterModelAsSingleton<TFrom, TTo>() where TTo : class, TFrom =>
            _container.RegisterSingleton<TFrom, TTo>();

        public void RegisterModel<TFrom, TTo>() where TTo : class, TFrom =>
            _container.RegisterType<TFrom, TTo>();

        public void RegisterView<TFrom, TTo>() where TTo : class, TFrom where TFrom : IView =>
            _container.RegisterType<TFrom, TTo>();

        public T CreatePresenter<T>() where T : class, IPresenter
        {
            if (!_container.IsRegistered<T>())
                _container.RegisterType<T>();
            return _container.Resolve<T>();
        }

        public T CreatePresenter<T, TU>() where T : class, IPresenter<TU>
        {
            if (!_container.IsRegistered<T>())
                _container.RegisterType<T>();
            return _container.Resolve<T>();
        }
    }
}