using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MainBehaviour
{
    AbstractFactory m_BossFactory;
    // private List<AbstractFactory> m_BossFactory;
    private List<AbstractFactory> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    [SerializeField] private float OffsetFromPlayer;
    [SerializeField] private int EnemyToSpawnCount;
    //timer to spawn boss
    private float m_timwWatch;
    [SerializeField] private float m_timeToSpawnBoss = 100f;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        ListOfFactory= new List<AbstractFactory>();
        m_BossFactory = TreeFactory.Instance;
    }

    private void baseEazyFactory()
    {
        ListOfFactory.Add(EnemyFactoryPig.Instance);
        ListOfFactory.Add(EnemyChikenFactory.Instance);
        ListOfFactory.Add(EnemyFactorySnail.Instance);
    }
    private void baseMediumFactory()
    {
        ListOfFactory.Add(EnemyPigFactory2.Instance);
        ListOfFactory.Add(EnemyChikenFactory2.Instance);
        ListOfFactory.Add(EnemyPigFactory2.Instance);
    }
    private void baseHardFactory()
    {
        ListOfFactory.Add(EnemyPigFactory3.Instance);
        ListOfFactory.Add(EnemyChikenFactory3.Instance);
        ListOfFactory.Add(EnemyFactorySnail3.Instance);
    }
  
    private void OnUpgradeFactory()
    {
        if (ListOfFactory.Count > 0)
        {
            ListOfFactory.Clear();

        }
    }

    public void SpawnBoss()
    {
        //check level spawn x boss 
        //call this fuction inside an event
    }
    protected override void OnStart()
    {
        base.OnStart();
        StartCoroutine(FactoryCoroutine());
        StartCoroutine(WaveCoroutine()); 
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (m_BossFactory != null)
        {
            m_timwWatch += Time.deltaTime;
        }
    }

    public void OnLevelUp(int _enemyIncrement)
    {
        if (Player.Instance.Level % 2 >= 0)
        {
            EnemyToSpawnCount += _enemyIncrement;
        }
        if (Player.Instance.Level % 5 == 0)
        {
           var _boss =  m_BossFactory.CreateEnemy();
           _boss.transform.position = Player.Instance.transform.position;
        }
    } 
    IEnumerator FactoryCoroutine()
    {
        baseEazyFactory();
        yield return new WaitForSeconds(TimeToSwitchFactory);
        
        baseMediumFactory();
        yield return new WaitForSeconds(TimeToSwitchFactory);
        
        baseHardFactory();
        yield return new WaitForSeconds(TimeToSwitchFactory);
    }

    IEnumerator WaveCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < EnemyToSpawnCount; i++)
            {
                var index = Random.Range(0, ListOfFactory.Count);
                var enemy =  ListOfFactory[index].CreateEnemy();
                enemy.transform.position = Player.Instance.transform.position + (Vector3)Random.insideUnitCircle.normalized * OffsetFromPlayer;
                if (m_timwWatch >= m_timeToSpawnBoss)
                {
                    var _boss = m_BossFactory.CreateEnemy();
                    _boss.transform.position = transform.position;
                    m_timwWatch = 0;
                }
            }
            yield return new WaitForSeconds(10);
        }
    }
}
