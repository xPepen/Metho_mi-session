using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MainBehaviour
{
    AbstractFactory factory;
    private List<AbstractFactory> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    [SerializeField] private float OffsetFromPlayer;
    [SerializeField] private int EnemyToSpawnCount;
    protected override void OnAwake()
    {
        base.OnAwake();
        ListOfFactory= new List<AbstractFactory>();
        
    }
    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(FactoryCoroutine());
        StartCoroutine(WaveCoroutine()); 
    }

    public void OnLevelup()
    {
        
    }

    IEnumerator FactoryCoroutine()
    {
        factory = EnemyFactoryPig.Instance;
        yield return new WaitForSeconds(TimeToSwitchFactory);
        factory = TreeFactory.Instance;
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
                enemy.transform.position = Player.Instance.transform.position + randomOffset.normalized * OffsetFromPlayer;
            }
            yield return new WaitForSeconds(10);
        }
    }
}
