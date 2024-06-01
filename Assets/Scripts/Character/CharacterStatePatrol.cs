using HS.Location;
using System.Collections.Generic;
using UnityEngine;

namespace HS.Character
{
    public class CharacterStatePatrol : ICharacterState
    {
        private List<Point> _patrolPoints;
        private Point _currentPoint;
        private ICharacterMovement _characterMovement;
        public CharacterStatePatrol(List<Point> patrolPoints)
        {
            _patrolPoints = patrolPoints;
        }

        public void Start(Character character)
        {
            _characterMovement = character.Movement;
            MoveToNextPoint();
        }

        public void Stop()
        {
            _characterMovement.Stop();
        }

        private void MoveToNextPoint()
        {
            _currentPoint = GetRandomPoint(_patrolPoints, _currentPoint);
            _characterMovement.SetDestination(_currentPoint.Position, MoveToNextPoint);
        }

        private Point GetRandomPoint(List<Point> points, Point currentPoint)
        {
            List<Point> possiblePoints = new List<Point>(points);
            possiblePoints.Remove(currentPoint);

            return possiblePoints[Random.Range(0, possiblePoints.Count)];
        }
    }
}
