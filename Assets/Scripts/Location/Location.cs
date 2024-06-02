using UnityEngine;

namespace HS.Location
{
    public class Location : MonoBehaviour, ILocation
    {
        [SerializeField] private Transform _pointsContainer;
        [SerializeField] private Collider _groundCollider;

        public Transform PointsContainer => _pointsContainer;
        public Collider GroundCollider => _groundCollider;
    }
}