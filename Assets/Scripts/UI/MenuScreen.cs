using System;
using UnityEngine;
using UnityEngine.UI;

namespace HS.UI
{
    public class MenuScreen : Screen
    {
        [SerializeField] private Button _newGameButton;

        public Action OnNewGameButtonClick;

        private void Start()
        {
            _newGameButton.onClick.AddListener(NewGameButtonClick);
        }

        private void NewGameButtonClick()
        {
            OnNewGameButtonClick?.Invoke();
        }
    }
}
