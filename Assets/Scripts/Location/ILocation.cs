
using UnityEngine;

namespace HS.Location
{
    public interface ILocation
    {
        Transform PointsContainer { get; }
        Collider GroundCollider { get; }
    }
}
