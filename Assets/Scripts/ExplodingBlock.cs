using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class ExplodingBlock : MonoBehaviour
{
    [SerializeField] private int _explosionForce;
    [SerializeField] private int _explosionRadius;
    [SerializeField] private ParticleSystem _effect;

    private void OnMouseUpAsButton()
    {
        Spawner spawner = GetComponent<Spawner>();
        spawner.TryCreateClones(out List<Spawner> explodableObjects);
        Explode(explodableObjects);
    }

    private void Explode(List<Spawner> explodableObjects)
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
