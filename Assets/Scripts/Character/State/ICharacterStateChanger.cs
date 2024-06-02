
namespace HS.Character
{
    public interface ICharacterStateChanger 
    {
        void Init();
        void SetIdle();
        void SetPatrol();
        void SetBase();
    }
}