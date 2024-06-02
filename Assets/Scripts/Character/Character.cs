using HS.Common;
using UnityEngine;

namespace HS.Character
{
    public class Character : MonoBehaviour, ICharacter, IHasTransform
    {
        private ICharacterState _state;
        private ICharacterMovement _movement;
        private ICharacterHealth _health;

        public ICharacterMovement Movement => _movement;
        public ICharacterHealth Health => _health;
        public Transform Transform => transform;

        public void Init(ICharacterMovement movement, ICharacterHealth health)
        {
            _movement = movement;
            _health = health;
            _health.OnDeath += Stop;
        }

        public void SetState(ICharacterState state)
        {
            if (!_health.IsAlive)
            {
                return;
            }
            if(_state != null && _state.GetType() == state.GetType())
            {
                return;
            }
            Stop();
            _state = state;
            state.Start(this);
        }

        private void Stop()
        {
            if(_state == null)
            {
                return;
            }
            _state.Stop();
        }

        private void OnDestroy()
        {
            if(_health != null)
            {
                _health.OnDeath -= Stop;
            }
        }
    }
}