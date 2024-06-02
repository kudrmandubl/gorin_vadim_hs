using HS.Character;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace HS.UI
{
    public class GameScreen : Screen
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _idleButton;
        [SerializeField] private Button _patrolButton;
        [SerializeField] private Button _baseButton;
        [SerializeField] private CharacterHealthView _characterHealthView;

        private ICharacterHealth _characterHealth;

        public Action OnMenuButtonClick;
        public Action OnIdleButtonClick;
        public Action OnPatrolButtonClick;
        public Action OnBaseButtonClick;

        public void Init(ICharacterHealth characterHealth)
        {
            _characterHealth = characterHealth;
            characterHealth.OnChangeHealth += _characterHealthView.SetPercents;
            _characterHealthView.SetPercents(1);
        }

        private void Start()
        {
            _menuButton.onClick.AddListener(MenuButtonClick);
            _idleButton.onClick.AddListener(IdleButtonClick);
            _patrolButton.onClick.AddListener(PatrolButtonClick);
            _baseButton.onClick.AddListener(BaseButtonClick);
        }

        private void MenuButtonClick()
        {
            OnMenuButtonClick?.Invoke();
        }

        private void IdleButtonClick()
        {
            OnIdleButtonClick?.Invoke();
        }

        private void PatrolButtonClick()
        {
            OnPatrolButtonClick?.Invoke();
        }

        private void BaseButtonClick()
        {
            OnBaseButtonClick?.Invoke();
        }

        private void OnDestroy()
        {
            if(_characterHealthView != null && _characterHealth != null)
            {
                _characterHealth.OnChangeHealth -= _characterHealthView.SetPercents;
            }   
        }
    }
}
