using System.Collections;

using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public Transform _spawnPoint;
    public GameObject _botPrefab;

    private void Start()
    {
        StartCoroutine(BotSpawnCoroutine());
    }
    private IEnumerator BotSpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Instantiate(_botPrefab, _spawnPoint.position, Quaternion.identity);
            yield return null;
        }
    }
}