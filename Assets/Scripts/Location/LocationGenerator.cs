using System.Collections.Generic;
using UnityEngine;

namespace HS.Location
{
    public class LocationGenerator : ILocationGenerator
    {
        private const int TriesCount = 1000;

        private Point _basePointPrefab;
        private Point _patrolPointPrefab;

        private Transform _pointsContainer;
        private Collider _groundCollider;

        private int _patrolPointCount = 10;
        private float _minPointDistance = 3f;

        private List<Point> _allPoints = new List<Point>();

        public LocationGenerator WithPointPrefabs(Point basePointPrefab, Point patrolPointPrefab)
        {
            _basePointPrefab = basePointPrefab;
            _patrolPointPrefab = patrolPointPrefab;
            return this;
        }

        public LocationGenerator WithLocation(ILocation location)
        {
            _pointsContainer = location.PointsContainer;
            _groundCollider = location.GroundCollider;
            return this;
        }

        public LocationGenerator WithSettings(int patrolPointCount, float minDistance)
        {
            _patrolPointCount = patrolPointCount;
            _minPointDistance = minDistance;
            return this;
        }


        public void Generate(out Point basePoint, out List<Point> points)
        {
            Clear();

            Bounds groundBounds = _groundCollider.bounds;
            basePoint = GenerateBasePoint(groundBounds.center, groundBounds.extents, _basePointPrefab);
            points = GeneratePatrolPoints(groundBounds.center, groundBounds.extents, _patrolPointPrefab);
        }

        private Point GenerateBasePoint(Vector3 groundCenter, Vector3 groundSize, Point pointPrefab)
        {
            Point spawnedPoint = SpawnPoint(groundCenter, groundSize, pointPrefab);
            return spawnedPoint;
        }

        private List<Point> GeneratePatrolPoints(Vector3 groundCenter, Vector3 groundSize, Point pointPrefab)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < TriesCount; i++)
            {
                Point spawnedPoint = SpawnPoint(groundCenter, groundSize, pointPrefab);
                if (!spawnedPoint)
                {
                    continue;
                }
                points.Add(spawnedPoint);

                if (points.Count >= _patrolPointCount)
                    break;
            }
            return points;
        }

        public void Clear()
        {
            for (int i = _allPoints.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(_allPoints[i].gameObject);
            }
            _allPoints.Clear();
        }

        private Point SpawnPoint(Vector3 groundCenter, Vector3 groundSize, Point pointPrefab)
        {
            Vector3 position = GetRandomPosition(groundCenter, groundSize);
            if (!CheckDitanceToPoints(position, _minPointDistance, _allPoints))
                return null;

            Point spawnedPoint = GameObject.Instantiate(pointPrefab, position, Quaternion.identity, _pointsContainer);
            spawnedPoint.Init(position);

            _allPoints.Add(spawnedPoint);
            return spawnedPoint;
        }

        private Vector3 GetRandomPosition(Vector3 groundCenter, Vector3 groundSize)
        {
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);

            return groundCenter + new Vector3(groundSize.x * x, groundSize.y, groundSize.z * z);
        }

        private bool CheckDitanceToPoints(Vector3 newPosition, float minDistance, List<Point> allPoints)
        {
            bool isNormalPlace = true;

            foreach (Point point in allPoints)
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
