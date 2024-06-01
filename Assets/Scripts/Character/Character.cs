using UnityEngine;

namespace HS.Character
{
    public class Character : MonoBehaviour
    {
        private ICharacterState _state;
        private ICharacterMovement _movement;

        public ICharacterMovement Movement => _movement;

        public void Init(ICharacterMovement movement)
        {
            _movement = movement;
        }

        public void SetState(ICharacterState state)
        {
            if(_state != null)
            {
                _state.Stop();
            }
            _state = state;
            state.Start(this);
        }
    }
}