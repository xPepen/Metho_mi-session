using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemySpawner : MainBehaviour
{
    AbstractFactory<Enemy> m_BossFactory;
    private List<AbstractFactory<Enemy>> ListOfFactory;
    [SerializeField] private float TimeToSwitchFactory;
    [SerializeField] private float OffsetFromPlayer;
    [SerializeField] private int EnemyToSpawnCount;
    [SerializeField] private float TimeBetweenWave;

    [SerializeField] private float TimeToSwitchLevel;

    private bool m_GamehasBeenInit;

    [SerializeField] private UnityEngine.UI.Button nextLevelBtn;

    //timer to spawn boss
    private float m_timwWatch;
    [SerializeField] private float m_timeToSpawnBoss = 100f;

    [SerializeField] private UnityEvent m_OnChangeLevel;

    private List<Enemy> m_ListOfEnemy;
    [SerializeField] private SceneAssetLoader loadLevel;

    [SerializeField] private int CurrentLevel = 1;
    [SerializeField] private int MaxCurrentLevel = 3;


    private Timer m_timer;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_ListOfEnemy = new List<Enemy>();
        ListOfFactory = new List<AbstractFactory<Enemy>>();
        m_BossFactory = TreeFactory.Instance;
        m_timer = new Timer(null, TimeToSwitchLevel, TimeType.Delta, true);
        m_timer.PauseTime();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        m_timer.OnUpdate();
        print(m_timer.CurrentTime);
        if (m_timer.IsTimerFinish())
        {
            EnemySpawnGameplayloop();
        }


        if (m_BossFactory != null)
        {
            m_timwWatch += Time.deltaTime;
            print("Nomral watch = " + m_timwWatch);
        }
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

    private void EnemySpawnGameplayloop()
    {
        CurrentLevel++;
        var newDuration = m_timer.GetCurrentDuration * 1.15f;
        m_timer.ChangeDuration(newDuration);
        switch (CurrentLevel)
        {
            case 2:
                baseMediumFactory();
                baseEazyFactory();
                SetNewLevelButton();
                break;
            case 3:
                RestEnemyList();
                baseEazyFactory();
                baseMediumFactory();
                baseHardFactory();
                break;
        }

        if (CurrentLevel <= 3)
        {
            m_OnChangeLevel?.Invoke();
            m_timer.PauseTime();
        }
    }

    private void SetNewLevelButton()
    {
        nextLevelBtn.onClick.AddListener(() =>
        {
           // loadLevel.LoadAdressable("Level" + CurrentLevel);
            loadLevel.LoadScene("Level" + CurrentLevel);
            m_timer.StartTimer();
            nextLevelBtn.onClick.RemoveListener(() =>
            {
               //loadLevel.LoadAdressable("Level" + CurrentLevel);
                loadLevel.LoadScene("Level" + CurrentLevel);
                m_timer.StartTimer();
            });
        });
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
        m_timer.StartTimer();
        if (!m_GamehasBeenInit)
        {
            m_GamehasBeenInit = true;
            baseEazyFactory();
        }

        StopAllCoroutines();
        // StartCoroutine(FactoryCoroutine());
        StartCoroutine(WaveCoroutine());
    }

    public void OnGameplayEnd()
    {
        m_timer.PauseTime();
        if (!m_GamehasBeenInit) return;
        if (m_ListOfEnemy.Count <= 0) return;

        StopAllCoroutines();
        RestEnemyList();
    }

    private void RestEnemyList()
    {
        if (m_ListOfEnemy.Count == 0) return;
        for (int i = 0; i < m_ListOfEnemy.Count; i++)
        {
            m_ListOfEnemy[i].RePoolItem.Invoke(m_ListOfEnemy[i]);
        }

        m_ListOfEnemy.Clear();
    }


    public void OnLevelUp(int _enemyIncrement)
    {
        if (Player.Instance.Level % 2 >= 0)
        {
            EnemyToSpawnCount += _enemyIncrement;
        }

        if (Player.Instance.Level % 5 == 0)
        {
            var _boss = m_BossFactory.CreateEnemy();
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
                entityToCreate = ListOfFactory[index].CreateEnemy();
                m_ListOfEnemy.Add(ListOfFactory[index].Entity);
                entityToCreate.transform.position = Player.Instance.transform.position +
                                                    (Vector3)Random.insideUnitCircle.normalized * OffsetFromPlayer;
                if (m_timwWatch >= m_timeToSpawnBoss)
                {
                    entityToCreate = m_BossFactory.CreateEnemy();
                    entityToCreate.transform.position = transform.position;
                    m_timwWatch = 0;
                }
            }

            yield return new WaitForSeconds(TimeBetweenWave);
        }
    }
}