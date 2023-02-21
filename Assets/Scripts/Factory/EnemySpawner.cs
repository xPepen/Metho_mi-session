using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MainBehaviour
{
    GameObject player;
    AbstractFactory factory;
    private List<AbstractFactory> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    [SerializeField] private float OffsetFromPlayer;
    [SerializeField] private int EnemyToSpawnCount;
    protected override void OnAwake()
    {
        base.OnAwake();
        ListOfFactory= new List<AbstractFactory>();
        factory = EnemyFactoryPig.Instance;
    }
    protected override void OnStart()
    {
        base.OnStart();
        player = GameObject.Find("Player");
        StartCoroutine(FactoryCoroutine());
        StartCoroutine(WaveCoroutine()); 
    }


    IEnumerator FactoryCoroutine()
    {
        factory = EnemyFactoryPig.Instance;
        yield return new WaitForSeconds(TimeToSwitchFactory);
        //factory = MediumEnemyFactory.Instance;
        //yield return new WaitForSeconds(TimeToSwitchFactory);
        //factory = HardEnemyFactory.Instance;
    }

    IEnumerator WaveCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < EnemyToSpawnCount; i++)
            {
                var enemy =  factory.CreateEnemy();
                Vector3 randomOffset = Random.insideUnitCircle;
                enemy.transform.position = player.transform.position + randomOffset.normalized * OffsetFromPlayer;
            }
            yield return new WaitForSeconds(10);
        }
    }
}
