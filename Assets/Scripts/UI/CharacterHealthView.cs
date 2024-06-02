using UnityEngine;
using UnityEngine.UI;

namespace HS.UI
{
    public class CharacterHealthView : MonoBehaviour
    {
        [SerializeField] private Transform _healthPercentsImageTransform;

        public void SetPercents(float value)
        {
            value = Mathf.Clamp01(value);
            _healthPercentsImageTransform.localScale = new Vector3(value, 1, 1);
        }
    }
}