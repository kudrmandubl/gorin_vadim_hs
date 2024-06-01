using UnityEngine;

namespace HS.Location
{
    public class Point : MonoBehaviour
    {
        private Vector3 _position;

        public Vector3 Position => _position;

        public void Init(Vector3 position)
        {
            _position = position;

            transform.position = position;
        }
    }
}
