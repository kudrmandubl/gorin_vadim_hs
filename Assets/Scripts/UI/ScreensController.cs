using UnityEngine;

namespace HS.UI
{
    public class ScreensController : MonoBehaviour
    {

        private Screen[] _screens;
        private Screen _currentScreen;

        public void Init()
        {
            _screens = GetComponentsInChildren<Screen>(true);
            HideAllScreens();
        }

        public T ShowScreen<T>(bool insertToPrev = true) where T : Screen
        {
            if (_currentScreen)
            {
                _currentScreen.SetActive(false);
            }
            _currentScreen = GetScreen<T>();
            _currentScreen.SetActive(true);
            return _currentScreen as T;
        }


        public T GetScreen<T>() where T : Screen
        {
            for (int i = 0; i < _screens.Length; i++)
            {
                if (_screens[i] is T targetScreen)
                {
                    return targetScreen;
                }
            }
            return null;
        }

        private void HideAllScreens()
        {
            for (int i = 0; i < _screens.Length; i++)
            {
                _screens[i].SetActive(false);
            }
        }
    }
}