using System.Collections.Generic;
using UnityEngine;

public class ExplodingBlock : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float _cloningChance;
    [SerializeField] private int _explodeForce;
    [SerializeField] private int _explodeRadius;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private ExplodingBlock _prefab;

    private int _minClonesCount = 2;
    private int _maxClonesCount = 6;
    private int _minCloningChance = 0;
    private int _maxCloningChance = 100;

    private int _cloningChanceDivider = 2;
    private int _scaleDivider = 2;

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
                    block.GetComponent<Rigidbody>().AddExplosionForce(_explodeForce, block.transform.position, _explodeRadius);
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

            float cloneScaleX = gameObject.transform.localScale.x / _scaleDivider;
            float cloneScaleY = gameObject.transform.localScale.y / _scaleDivider;
            float cloneScaleZ = gameObject.transform.localScale.z / _scaleDivider;
            
            Color cloneColor = new (Random.value, Random.value, Random.value);

            Debug.Log(cloneColor);

            clone.transform.localScale = new Vector3(cloneScaleX, cloneScaleY, cloneScaleZ);
            clone.GetComponent<Renderer>().material.color = cloneColor;
            clone._cloningChance = _cloningChance / _cloningChanceDivider;

            clones.Add(clone);
        }

        return clones;
    }
}
