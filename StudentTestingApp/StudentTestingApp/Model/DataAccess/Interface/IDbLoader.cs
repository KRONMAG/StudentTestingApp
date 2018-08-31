namespace StudentTestingApp.Model.DataAccess.Interface
{
    public interface IDbLoader
    {
        bool DbExist { get; }
        void CreateEmptyDb();
        bool LoadDb();
    }
}