using HS.Character;
using UnityEngine;

namespace HS.Damager
{
    public class Damager : IDamager
    {
        private const float MaxRayDistance = 100f;

        UnityEngine.Camera _camera;
        private int _damage;

        public Damager(UnityEngine.Camera camera, int damage)
        {
            _camera = camera;
            _damage = damage;
        }

        public void TryDamage()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance))
            {
                return;
            }
            ICharacter characterHealth = hit.collider.GetComponentInParent<ICharacter>();
            if (characterHealth == null || characterHealth.Health == null)
            {
                return;
            }

            characterHealth.Health.AddHealth(-_damage);
        }
    }
}
