using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _enhancedExplosionForceBonus;
    [SerializeField] private float _enhancedExplosionRadiusBonus;
    [SerializeField] private ParticleSystem _effect;

    private float _enhancedExplosionForceBonusMultiplier = 2;
    private float _enhancedExplosionRadiusBonusMultiplier = 1.5f;

    public void Init()
    {
        _enhancedExplosionForceBonus *= _enhancedExplosionForceBonusMultiplier;
        _enhancedExplosionRadiusBonus *= _enhancedExplosionRadiusBonusMultiplier;
    }

    public void ExplodeHard()
    {
        float explosionForce = _explosionForce + _enhancedExplosionForceBonus;
        float explosionRadius = _explosionRadius + _enhancedExplosionRadiusBonus;
        List<ExplodingBlock> exploadableObjects = GetExplodableObjects(explosionRadius);

        Explode(exploadableObjects, explosionForce, explosionRadius);
    }

    public void ExplodeNormal(List<ExplodingBlock> explodableObjects)
    {
        Explode(explodableObjects, _explosionForce, _explosionRadius);
    }

    private void Explode(List<ExplodingBlock> explodableObjects, float explosionForce, float explosionRadius)
    {
        float upwardsModifier = 1;

        if (_effect != null)
            Instantiate(_effect, transform.position, Quaternion.identity);

        if (explodableObjects != null)
        {
            foreach (var block in explodableObjects)
            {
                if (block.TryGetComponent(out Rigidbody rigidbody))
                    rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
            }
        }

        Destroy(gameObject);
    }

    private List<ExplodingBlock> GetExplodableObjects(float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        List<ExplodingBlock> explodableObjects = new();

        foreach(Collider hit in hits)
        {
            if (hit.TryGetComponent(out ExplodingBlock block))
            {
                explodableObjects.Add(block);
            }

        }

        return explodableObjects;
    }
}
