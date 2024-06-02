
namespace HS.Config
{
    public interface IGameConfig
    {
        int PatrolPointCount { get; }
        float MinPointDistance { get; }
        int CharacterHP { get; }
        int Damage { get; }
        float CharacterSpeed { get; }
    }
}
