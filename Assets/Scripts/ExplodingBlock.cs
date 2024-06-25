using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Spawner))]
[RequireComponent (typeof(Exploder))]
public class ExplodingBlock : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _cloningChance;
    [SerializeField] private ExplodingBlock _prefab;

    private readonly int _minCloningChance = 0;
    private readonly int _maxCloningChance = 100;
    private readonly int _cloningChanceDivider = 2;
    private readonly int _scaleDivider = 2;

    public void Enable()
    {
        GetComponent<Renderer>().material.color = new(Random.value, Random.value, Random.value);
        transform.localScale /= _scaleDivider;
        _cloningChance /= _cloningChanceDivider;
    }

    private void OnMouseUpAsButton()
    {
        List<ExplodingBlock> explodableObjects = null;

        if (Random.Range(_minCloningChance, _maxCloningChance) <= _cloningChance)
        {
            Spawner spawner = GetComponent<Spawner>();
            explodableObjects = spawner.TryCreateClones(_prefab);
        }

        Exploder exploder = GetComponent<Exploder>();
        exploder.Explode(explodableObjects);
    }
}
