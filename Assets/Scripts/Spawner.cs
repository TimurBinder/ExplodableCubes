using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Spawner : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _cloningChance;
    [SerializeField] private Spawner _prefab;

    private readonly int _minClonesCount = 2;
    private readonly int _maxClonesCount = 6;
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

    public void TryCreateClones(out List<Spawner> clones)
    {
        if (Random.Range(_minCloningChance, _maxCloningChance) > _cloningChance)
        {
            clones = null;
            return;
        }

        clones = new();
        int clonesCount = Random.Range(_minClonesCount, _maxClonesCount + 1);

        for (int i = 0; i < clonesCount; i++)
        {
            Spawner clone = Instantiate(_prefab);
            clone.Enable();
            clones.Add(clone);
        }
    }
}
