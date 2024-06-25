using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private int _explosionForce;
    [SerializeField] private int _explosionRadius;
    [SerializeField] private ParticleSystem _effect;

    public void Explode(List<ExplodingBlock> explodableObjects)
    {
        if (_effect != null)
            Instantiate(_effect, gameObject.transform.position, Quaternion.identity);

        if (explodableObjects != null)
        {
            foreach (var block in explodableObjects)
            {
                if (block.TryGetComponent<Rigidbody>(out Rigidbody rigidbody) == true)
                    rigidbody.AddExplosionForce(_explosionForce, block.transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }
}
