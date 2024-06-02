
using System;

namespace HS.Character
{
    public class CharacterHealth : ICharacterHealth
    {
        private int _startHealth;
        private int _health;

        public bool IsAlive => _health > 0;
        public Action<float> OnChangeHealth { get; set; }
        public Action OnDeath { get; set; }

        public CharacterHealth(int health)
        {
            _startHealth = health;
            _health = health;   
        }

        public void AddHealth(int value)
        {
            if(!IsAlive)
            {
                return;
            }

            _health += value;
            OnChangeHealth?.Invoke((float)_health / _startHealth);

            if (_health <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}
