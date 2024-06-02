using System.Collections.Generic;
using HS.Location;

namespace HS.Character
{
    public class CharacterStateChanger : ICharacterStateChanger
    {
        private ICharacter _character;
        private Point _basePoint;
        private List<Point> _patrolPoints;

        public CharacterStateChanger(ICharacter character, Point basePoint, List<Point> patrolPoints)
        {
            _character = character;
            _basePoint = basePoint;
            _patrolPoints = patrolPoints;
        }

        public void Init()
        {
            SetIdle();
        }

        public void SetIdle()
        {
            SetState(new CharacterStateIdle());
        }

        public void SetPatrol()
        {
            SetState(new CharacterStatePatrol(_patrolPoints));
        }

        public void SetBase()
        {
            SetState(new CharacterStateBase(_basePoint, SetIdle));
        }

        private void SetState(ICharacterState state)
        {
            _character.SetState(state);
        }

    }
}
