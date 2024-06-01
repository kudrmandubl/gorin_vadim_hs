using DG.Tweening;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace HS.Character
{
    public class CharacterStateIdle : ICharacterState
    {
        private const float TargetScale = 1.5f;
        private const float ScaleDuration = 1f;

        private Character _character;
        private Tween _scaleTween;
        private float _startScale;

        public void Start(Character character)
        {
            _character = character;
            _startScale = character.transform.localScale.x;
            _scaleTween = character.transform.DOScale(TargetScale, ScaleDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .OnKill(ResetScale);
        }

        public void Stop()
        {
            if (_scaleTween.IsActive())
            {
                _scaleTween.Kill();
            }
        }

        private void ResetScale()
        {
            _character.transform.localScale = Vector3.one * _startScale;
        }
    }
}
