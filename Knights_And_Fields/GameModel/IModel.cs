namespace GameModel
{
    public interface IModel
    {
        int CastleHP { get; set; }
        bool DeployKnight { get; set; }
        int Gold { get; set; }
        IUnit[][] Map { get; set; }
        int Score { get; set; }
        int Wave { get; set; }
        bool MoveUnit { get; set; }
        bool RemoveUnit { get; set; }
    }
}