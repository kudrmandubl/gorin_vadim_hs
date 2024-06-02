
namespace HS.Character
{
    public interface ICharacter
    {
        ICharacterMovement Movement { get; }
        ICharacterHealth Health { get; }
        void Init(ICharacterMovement movement, ICharacterHealth health);
        void SetState(ICharacterState state);
    }
}
