using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MainBehaviour
{
    GameObject player;
    AbstractFactory factory;
    private List<AbstractFactory> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    protected override void OnAwake()
    {
        base.OnAwake();
        ListOfFactory= new List<AbstractFactory>();
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
            for (int i = 0; i < 10; i++)
            {
                var enemy =  factory.CreateEnemy();
                Vector3 randomOffset = Random.insideUnitCircle;
                enemy.transform.position = player.transform.position + randomOffset.normalized * 5;
            }
            yield return new WaitForSeconds(10);
        }
    }
}
