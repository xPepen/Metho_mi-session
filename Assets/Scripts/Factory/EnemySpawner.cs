using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MainBehaviour
{
    AbstractFactory<Enemy> m_BossFactory;
    private List<AbstractFactory<Enemy>> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    [SerializeField] private float OffsetFromPlayer;
    [SerializeField] private int EnemyToSpawnCount;

    private bool m_GamehasBeenInit = false;
    //timer to spawn boss
    private float m_timwWatch;
    [SerializeField] private float m_timeToSpawnBoss = 100f;
    
    private  List<Enemy> m_ListOfEnemy;
    
    protected override void OnAwake()
    {
        base.OnAwake();
        m_ListOfEnemy = new List<Enemy>();
        ListOfFactory= new List<AbstractFactory<Enemy>>();
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
   

    public void OnGameplayStart()
    {
        if (!m_GamehasBeenInit)
        {
            m_GamehasBeenInit = true;
        }
        StopAllCoroutines();
        StartCoroutine(FactoryCoroutine());
        StartCoroutine(WaveCoroutine()); 
    }

    public void OnGameplayEnd()
    {
        if (!m_GamehasBeenInit) return;
        if (m_ListOfEnemy.Count <= 0) return;
        
        StopAllCoroutines();

        for (int i = 0; i < m_ListOfEnemy.Count; i++)
        {
            m_ListOfEnemy[i].m_RePool();
        }
        m_ListOfEnemy.Clear();
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
        GameObject entityToCreate = null;
        while (true)
        {
            for (int i = 0; i < EnemyToSpawnCount; i++)
            {
                var index = Random.Range(0, ListOfFactory.Count);
                entityToCreate =  ListOfFactory[index].CreateEnemy();
                m_ListOfEnemy.Add( ListOfFactory[index].Entity);
                entityToCreate.transform.position = Player.Instance.transform.position + (Vector3)Random.insideUnitCircle.normalized * OffsetFromPlayer;
                if (m_timwWatch >= m_timeToSpawnBoss)
                {
                    entityToCreate = m_BossFactory.CreateEnemy();
                    entityToCreate.transform.position = transform.position;
                    m_timwWatch = 0;
                }
            }
            yield return new WaitForSeconds(10);
        }
    }
}
