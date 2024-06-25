using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ExplodingBlock _prefab;

    private readonly int _minClonesCount = 2;
    private readonly int _maxClonesCount = 6;

    public List<ExplodingBlock> TryCreateClones()
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
