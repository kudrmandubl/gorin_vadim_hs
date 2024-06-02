using UnityEngine;

namespace HS.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [SerializeField] private int _patrolPointCount = 10;
        [SerializeField] private float _minPointDistance = 3f;
        [SerializeField] private int _characterHP = 100;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _characterSpeed = 4f;

        public int PatrolPointCount => _patrolPointCount;
        public float MinPointDistance => _minPointDistance;
        public int CharacterHP => _characterHP;
        public int Damage => _damage;
        public float CharacterSpeed => _characterSpeed;
    }
}
