using System;
using System.ComponentModel;

namespace StudentTestingApp.Presenter.Common
{
    /// <summary>
    /// Средство запуска операций в фоновом режиме
    /// </summary>
    public static class Worker
    {
        /// <summary>
        /// Запуск задачи в фоновом режиме
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого операцией значения</typeparam>
        /// <param name="view">Представление, для которого вызывается операция</param>
        /// <param name="task">Делегает, представляющий операцию</param>
        /// <param name="onSuccess">Делегат, вызываемый в случае успешного завершения операции</param>
        /// <param name="onError">Делегат, вызываемый при возникновении исключения в ходе выполнения операции</param>
        public static void Run<T>(Func<T> task, Action<T> onSuccess, Action<Exception> onError)
        {
            var worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += (_, e) =>
                e.Result = task();

            worker.RunWorkerCompleted += (_, e) =>
            {
                if (e.Error != null)
                    onError?.Invoke(e.Error);
                else if (!e.Cancelled)
                    onSuccess((T)e.Result);
            };

            worker.RunWorkerAsync();
        }
    }
}