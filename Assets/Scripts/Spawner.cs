using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Spawner : MonoBehaviour
{
    private readonly int _minClonesCount = 2;
    private readonly int _maxClonesCount = 6;

    public List<Rigidbody> TryCreateClones(ExplodingBlock prefab)
    {
        List<Rigidbody> clones = new();
        int clonesCount = Random.Range(_minClonesCount, _maxClonesCount + 1);

        for (int i = 0; i < clonesCount; i++)
        {
            ExplodingBlock clone = Instantiate(prefab);
            clone.Enable();
            clone.TryGetComponent<Rigidbody>(out Rigidbody cloneRigidbody);
            clones.Add(cloneRigidbody);
        }

        return clones;
    }
}
