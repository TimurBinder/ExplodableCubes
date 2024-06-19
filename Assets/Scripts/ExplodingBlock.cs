using System.Collections.Generic;
using UnityEngine;

public class ExplodingBlock : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _cloningChance;
    [SerializeField] private int _explosionForce;
    [SerializeField] private int _explosionRadius;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private ExplodingBlock _prefab;

    private int _minClonesCount = 2;
    private int _maxClonesCount = 6;
    private int _minCloningChance = 0;
    private int _maxCloningChance = 100;

    private int _cloningChanceDivider = 2;
    private int _scaleDivider = 2;

    public void Enable()
    {
        gameObject.GetComponent<Renderer>().material.color = new(Random.value, Random.value, Random.value);
        gameObject.transform.localScale /= _scaleDivider;
        _cloningChance /= _cloningChanceDivider;
    }

    private void OnMouseUpAsButton()
    {
        List<ExplodingBlock> explodableObjects = null;

        if (Random.Range(_minCloningChance, _maxCloningChance) < _cloningChance)
            explodableObjects = GetClones();

        Explode(explodableObjects);
    }

    private void Explode(List<ExplodingBlock> explodableObjects)
    {
        if (_effect != null)
            Instantiate(_effect, gameObject.transform.position, Quaternion.identity);

        if (explodableObjects != null)
        {
            foreach (var block in explodableObjects)
            {
                if (block.GetComponent<Rigidbody>() != null)
                    block.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, block.transform.position, _explosionRadius);
            }
        }

        Destroy(gameObject);
    }

    private List<ExplodingBlock> GetClones()
    {
        List<ExplodingBlock> clones = new();
        int clonesCount = Random.Range(_minClonesCount, _maxClonesCount + 1);

        for (int i = 0; i < clonesCount; i++)
        {
            ExplodingBlock clone = Instantiate(_prefab);
            clone.Enable();
            clones.Add(clone);
        }

        return clones;
    }
}
