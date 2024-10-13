using System.Collections;

using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public Transform _spawnPoint;
    public GameObject _botPrefab;
    public float _spawnTime;

    private void Start()
    {
        StartCoroutine(BotSpawnCoroutine());
    }
    private IEnumerator BotSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);
            Instantiate(_botPrefab, _spawnPoint.position, Quaternion.identity);
            yield return null;
        }
    }
}