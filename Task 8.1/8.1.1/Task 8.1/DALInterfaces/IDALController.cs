namespace DALInterfaces
{
    public interface IDALController
    {
        IDataLayer UserDAL { get; }

        IDataLayer AwardDAL { get; }
    }
}
