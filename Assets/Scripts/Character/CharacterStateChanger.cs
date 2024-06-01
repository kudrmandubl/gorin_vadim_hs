using System.Collections.Generic;
using UnityEngine;
using HS.Location;

namespace HS.Character
{
    public class CharacterStateChanger : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private Point _basePoint;
        private List<Point> _patrolPoints;

        public void Init(Point basePoint, List<Point> patrolPoints)
        {
            _basePoint = basePoint;
            _patrolPoints = patrolPoints;
            SetPatrol();
        }

        [ContextMenu("SetIdle")]
        public void SetIdle()
        {
            SetState(new CharacterStateIdle());
        }

        [ContextMenu("SetPatrol")]
        public void SetPatrol()
        {
            SetState(new CharacterStatePatrol(_patrolPoints));
        }

        [ContextMenu("SetBase")]
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
