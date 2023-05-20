using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using model;
using model.allies.turrets;
using model.enemies;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefabLv1;
    public GameObject enemyPrefabLv2;
    public GameObject enemyPrefabLv3;

    public Vector3 spawnPos;
    public GameObject waves;

    private Wave_wrapper[] _waves;
    public int WaveAmount { get; set; }

    public event EventHandler<EventArgs> EnemySpawned;
    public event EventHandler<EventArgs> EnemyDeath;
    public event EventHandler<EventArgs> WaveChanged;
    public event EventHandler<EventArgs> WaveStarted;
    public event EventHandler<EventArgs> GameEnded;
    
    private List<Enemy_Behavior> _enemies;

    private float _currentAmountLeft;

    private int _currentAmountKilled;

    private int _waveNumber = 1;
    public int WaveNumber
    {
        get => _waveNumber;
        set
        {
            _waveNumber = value;
            OnWaveChanged();
        }
    }

    private GameObject _tutorial;
    
    // Start is called before the first frame update
    void Start()
    {
        _tutorial = GameObject.Find("Tutorial");
        _enemies = new List<Enemy_Behavior>();
        _waves = waves.GetComponentsInChildren<Wave_wrapper>();
        WaveAmount = _waves[0].level1Amount + _waves[0].level2Amount + _waves[0].level3Amount;
    }

    public void StartWave()
    {
        if (_tutorial.activeSelf) return;
        WaveStarted?.Invoke(this, EventArgs.Empty);
        StartCoroutine(SpawnEnemy(_waves[WaveNumber - 1].Wave));
    }

    IEnumerator SpawnEnemy(Wave wave)
    {
        if (!(_currentAmountLeft < WaveAmount)) yield return null;
        
        for (int i = 0; i < wave.Level1Amount; i++)
        {
            var obj = gameObject.Instantiate(enemyPrefabLv1, spawnPos, Quaternion.identity, EnemyFactory.CreateLv1Enemy());
            var enemy = obj.GetComponent<Enemy_Behavior>();
            _enemies.Add(enemy);
            enemy.Death += EnemyOnDeath;    
            EnemySpawned?.Invoke(enemy, EventArgs.Empty);
            yield return new WaitForSeconds(wave.RoS);
        }
       
        for (int i = 0; i < wave.Level2Amount; i++)
        {
            var obj = gameObject.Instantiate(enemyPrefabLv2, spawnPos, Quaternion.identity, EnemyFactory.CreateLv2Enemy());
            var enemy = obj.GetComponent<Enemy_Behavior>();
            _enemies.Add(enemy);  
            enemy.Death += EnemyOnDeath;
            EnemySpawned?.Invoke(enemy, EventArgs.Empty);
            yield return new WaitForSeconds(wave.RoS);

        }
        for (int i = 0; i < wave.Level3Amount; i++)
        {
            var obj = gameObject.Instantiate(enemyPrefabLv3, spawnPos, Quaternion.identity, EnemyFactory.CreateLv3Enemy());
            var enemy = obj.GetComponent<Enemy_Behavior>();
            _enemies.Add(enemy);
            enemy.Death += EnemyOnDeath;
            EnemySpawned?.Invoke(enemy, EventArgs.Empty);
            yield return new WaitForSeconds(wave.RoS);

        }
        
    }

    private void EnemyOnDeath(object sender, EventArgs e)
    {
        EnemyDeath?.Invoke(sender, e);
        _enemies.Remove(sender as Enemy_Behavior);
        _currentAmountKilled++;
        if (_currentAmountKilled == WaveAmount)
        {
            WaveNumber++;
        }
    }

    protected virtual void OnWaveChanged()
    {
       
        if (WaveNumber >= _waves.Length)
        {
            GameEnded?.Invoke(this, EventArgs.Empty);
        }

        if (WaveNumber > _waves.Length)
        {
            _currentAmountKilled = 0;
            return;
        }
        _currentAmountKilled = 0;
        _currentAmountLeft = WaveAmount;
        WaveAmount = _waves[WaveNumber - 1].level1Amount + _waves[WaveNumber - 1].level2Amount +
                     _waves[WaveNumber - 1].level3Amount;
        WaveChanged?.Invoke(this, EventArgs.Empty);
        
    }
}
