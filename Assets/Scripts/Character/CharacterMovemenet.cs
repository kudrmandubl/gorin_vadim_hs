using DG.Tweening;
using System;
using UnityEngine;

namespace HS.Character
{
    public class CharacterMovemenet : MonoBehaviour, ICharacterMovement
    {
        private Transform _characterTransform;
        private float _speed;

        private Tween _moveTween;
        private bool _isActive;

        public CharacterMovemenet(Transform characterTransform, float speed)
        {
            _characterTransform = characterTransform;
            _speed = speed;
        }

        public void SetDestination(Vector3 target, Action onReachTarget)
        {
            float duration = (_characterTransform.position - target).magnitude / _speed;
            if (_moveTween.IsActive())
            {
                _moveTween.Kill();
            }
            _moveTween = _characterTransform.DOMove(target, duration)
                .SetEase(Ease.Linear)
                .OnComplete(onReachTarget.Invoke);
        }

        public void Stop()
        {
            if (_moveTween.IsActive())
            {
                _moveTween.Kill();
            }
        }
    }
}
