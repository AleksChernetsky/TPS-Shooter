using UnityEngine;

using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public GameObject PlayerPrefab;

    public override void InstallBindings()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Container.InstantiatePrefab(PlayerPrefab, StartPoint);
    }
}