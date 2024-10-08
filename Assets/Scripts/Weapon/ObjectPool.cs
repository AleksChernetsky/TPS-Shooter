using System.Collections.Generic;

using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private GameObject _projectile;
    private List<GameObject> _pooledProjectiles = new List<GameObject>();

    [Header("HitEffect")]
    [SerializeField] private Transform _hitEffectContainer;
    [SerializeField] private GameObject _hitEffect;
    private List<GameObject> _pooledHitEffects = new List<GameObject>();

    [SerializeField] private int _amountToPool;

    public static ObjectPool Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        CreatePool();
    }

    public GameObject GetProjectile()
    {
        for (int i = 0; i < _pooledProjectiles.Count; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
                return _pooledProjectiles[i];
        }
        return CreateObject(_projectile, _projectileContainer, _pooledProjectiles);
    }
    public GameObject GetHitEffect()
    {
        for (int i = 0; i < _pooledHitEffects.Count; i++)
        {
            if (!_pooledHitEffects[i].activeInHierarchy)
                return _pooledHitEffects[i];
        }
        return CreateObject(_hitEffect, _hitEffectContainer, _pooledHitEffects);
    }
    private void CreatePool()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            CreateObject(_projectile, _projectileContainer, _pooledProjectiles);
            CreateObject(_hitEffect, _hitEffectContainer, _pooledHitEffects);
        }
    }
    private GameObject CreateObject(GameObject objectToPool, Transform container, List<GameObject> objectList)
    {
        GameObject pooledObject = Instantiate(objectToPool, container);
        pooledObject.SetActive(false);
        objectList.Add(pooledObject);
        return pooledObject;
    }
}