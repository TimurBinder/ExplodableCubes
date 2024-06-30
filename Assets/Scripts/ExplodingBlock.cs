using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Spawner))]
[RequireComponent (typeof(Exploder))]
public class ExplodingBlock : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _cloningChance;

    private readonly int _minCloningChance = 0;
    private readonly int _maxCloningChance = 100;
    private readonly int _cloningChanceDivider = 2;
    private readonly int _scaleDivider = 2;

    private Renderer _renderer;
    private Spawner _spawner;
    private Exploder _exploder;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _spawner = GetComponent<Spawner>();
        _exploder = GetComponent<Exploder>();
    }

    public void Enable()
    {
        _renderer.material.color = new(Random.value, Random.value, Random.value);
        transform.localScale /= _scaleDivider;
        _cloningChance /= _cloningChanceDivider;
        _exploder.Enable();
    }

    private void OnMouseUpAsButton()
    {
        if (Random.Range(_minCloningChance, _maxCloningChance) <= _cloningChance)
        {
            List<ExplodingBlock> explodableObjects = _spawner.TryCreateClones(this);
            _exploder.Explode(explodableObjects);
        }
        else
        {
            _exploder.EnhancedExplode();
        }
    }
}
