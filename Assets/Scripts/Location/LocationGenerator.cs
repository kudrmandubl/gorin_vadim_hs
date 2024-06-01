using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HS.Location
{
    public class LocationGenerator : MonoBehaviour
    {
        private const int TriesCount = 1000;

        [SerializeField] private Point _basePointPrefab;
        [SerializeField] private Point _patrolPointPrefab;
        [SerializeField] private Transform _pointsContainer;

        [SerializeField] private int _patrolPointCount = 10;
        [SerializeField] private float _minDistance = 3f;
        [SerializeField] private Collider _groundCollider;

        private List<Point> _points = new List<Point>();
        private int _totalPointsCount;

        private void Start()
        {
            Init();
        }
        
        private void Init()
        {
            _totalPointsCount = _patrolPointCount + 1;

            Clear();
            Generate();
        }

        private void Clear()
        {
            for (int i = _points.Count - 1; i >= 0; i--)
            {
                Destroy(_points[i].gameObject);
            }
            _points.Clear();
        }

        private void Generate()
        {
            Bounds groundBounds = _groundCollider.bounds;
            for (int i = 0; i < TriesCount; i++)
            {
                if (!TrySpawnPoint(groundBounds.center, groundBounds.extents))
                {
                    continue;
                }

                if (_points.Count >= _totalPointsCount)
                    break;
            }

        }

        private bool TrySpawnPoint(Vector3 groundCenter, Vector3 groundSize)
        {
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);

            Vector3 position = groundCenter + new Vector3(groundSize.x * x, groundSize.y, groundSize.z * z);

            if (!CheckDitanceToPoints(position, _minDistance))
                return false;

            Point spawnedPoint = Instantiate(GetNextPointPrefab(), position, Quaternion.identity, _pointsContainer);
            spawnedPoint.Init(position);
            _points.Add(spawnedPoint);
            return true;
        }

        private Point GetNextPointPrefab()
        {
            return _points.Count > 0 ? _patrolPointPrefab : _basePointPrefab;
        }

        private bool CheckDitanceToPoints(Vector3 newPosition, float minDistance)
        {
            bool isNormalPlace = true;

            foreach (Point point in _points)
            {
                if ((point.Position - newPosition).sqrMagnitude < minDistance * minDistance)
                {
                    isNormalPlace = false;
                    break;
                }
            }

            return isNormalPlace;
        }
    }
}
