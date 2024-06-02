using UnityEngine;

namespace HS.UI
{
    public abstract class Screen : MonoBehaviour
    {
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}