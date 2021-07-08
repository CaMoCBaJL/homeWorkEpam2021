namespace BLInterfaces
{
    public interface IBLController
    {
        ILogicLayer AwardLogic { get; }

        ILogicLayer UserLogic { get; }
    }
}
