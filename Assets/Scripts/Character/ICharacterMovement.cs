using System;
using UnityEngine;

namespace HS.Character
{
    public interface ICharacterMovement
    {
        void SetDestination(Vector3 target, Action onReachTarget);
        void Stop();
    }
}
