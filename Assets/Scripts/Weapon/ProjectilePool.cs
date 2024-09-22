using System.Collections.Generic;

using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Transform _projectileContainer;
    [SerializeField] private Projectile _projectilePrefab;

    [SerializeField] private int _amountToPool;

    private List<GameObject> _pooledProjectiles = new List<GameObject>();
    public static ProjectilePool instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            CreateProjectile();
        }
    }

    private GameObject CreateProjectile(bool isActiveByDefault = false)
    {
        GameObject projectile = Instantiate(_projectilePrefab.gameObject, _projectileContainer);
        projectile.SetActive(isActiveByDefault);
        _pooledProjectiles.Add(projectile);
        return projectile;
    }
    public GameObject GetProjectile()
    {
        for (int i = 0; i < _pooledProjectiles.Count; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
                return _pooledProjectiles[i];
        }
        return CreateProjectile(true);
    }
}