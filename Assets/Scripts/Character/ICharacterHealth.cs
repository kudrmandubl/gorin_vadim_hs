
using System;

namespace HS.Character
{
    public interface ICharacterHealth
    {
        bool IsAlive { get; }
        Action<float> OnChangeHealth { get; set; }
        Action OnDeath { get; set; }
        void AddHealth(int value);
    }
}
