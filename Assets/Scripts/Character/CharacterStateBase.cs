using HS.Location;
using System;

namespace HS.Character
{
    public class CharacterStateBase : ICharacterState
    {
        private Point _basePoint;
        private Action _onCompleteAction;
        private ICharacterMovement _characterMovement;

        public CharacterStateBase(Point basePoint, Action onCompleteAction)
        {
            _basePoint = basePoint;
            _onCompleteAction = onCompleteAction;
        }

        public void Start(Character character)
        {
            _characterMovement = character.Movement;
            _characterMovement.SetDestination(_basePoint.Position, _onCompleteAction);
        }

        public void Stop()
        {
            _characterMovement.Stop();
        }
    }
}
