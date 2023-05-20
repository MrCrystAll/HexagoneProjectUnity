using System;
using System.Collections;
using System.Collections.Generic;
using model;
using model.allies.turrets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    [Header("Components")]
    public SpawnManager spawnManager;
    public Player player;
    public Base @base;
    public TurretSpawn turretSpawn;
    public Camera cam;
    public GameObject dragAndDropZone;
    public GameObject loseTransition;
    public GameObject winTransition;

    [Header("Text fields")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI baseHealthText;
    public TextMeshProUGUI waveNumberText;

    [Header("Buttons")] public Button startWaveButton;

    private DragAndDropTurret[] _dragAndDropTurrets;

    private int _enemiesLeft;
    private int _enemiesKilled;
    private bool _draggingTurret;
    private GameObject _draggedTurret;
    private TurretWrapper _draggedWrapper;
    private DragAndDropTurret _currentTurret;
    public TurretsAvailableAnim parentOfUI;
    private bool _gameOver;
    private bool _lastWave;

    private GameObject _snappedPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager.EnemySpawned += SpawnManagerOnEnemySpawned;
        spawnManager.EnemyDeath += SpawnManagerOnEnemyDeath;
        
        spawnManager.WaveStarted += SpawnManagerOnWaveStarted;
        spawnManager.WaveChanged += SpawnManagerOnWaveChanged;
        
        spawnManager.GameEnded += SpawnManagerOnGameEnded;
        
        player.MoneyChanged += PlayerOnMoneyChanged;
        moneyText.text = player.Money.ToString("F0");

        _dragAndDropTurrets = dragAndDropZone.GetComponentsInChildren<DragAndDropTurret>();
        foreach (var dragAndDropTurret in _dragAndDropTurrets)
        {
            dragAndDropTurret.MouseExited += DragAndDropTurretOnMouseExited;
            dragAndDropTurret.CanPay = player.Money >= dragAndDropTurret.Price;
        }

        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        
        @base.Damaged += BaseOnDamaged;

        print("Start of env ?");
        _enemiesLeft = spawnManager.WaveAmount;
        enemiesLeftText.text = _enemiesLeft.ToString();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManagerOnsceneLoaded;
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name != "Game")
        {
            Destroy(gameObject);
        }
    }

    private void SpawnManagerOnGameEnded(object sender, EventArgs e)
    {
        _lastWave = true;
    }

    private void PlayerOnMoneyChanged(object sender, EventArgs e)
    {
        if (_gameOver) return;
        moneyText.text = player.Money.ToString("F0");
        foreach (var dragAndDrop in _dragAndDropTurrets)
        {
            dragAndDrop.CanPay = player.Money >= dragAndDrop.Price;
        }
    }

    private void Update()
    {
        if (_gameOver) return;
        if (Input.GetMouseButton(0) && _draggingTurret)
        {
            var mouse = Input.mousePosition;
            _draggedTurret.gameObject.transform.position =
                cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, cam.transform.position.y));
            
            //Snap to closest turret place;
            _snappedPosition = turretSpawn.GetClosestObjectWithin(
                cam.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, cam.transform.position.y)),
                2);

            if (_snappedPosition)
            {
                _draggedTurret.gameObject.transform.position = _snappedPosition.transform.position;
                
            }

        }
        //Releasing turret when snapped
        else if (_snappedPosition is not null && !Input.GetMouseButton(0))
        {
            _draggingTurret = false;
            player.Money -= _currentTurret.Price;
            
            //Get position and set it
            var pos = _snappedPosition.GetComponent<TurretPosition>();
            _draggedWrapper.Canon.Active = true;
            _draggedWrapper.Position = pos;
            pos.IsFree = false;
            pos.turretInstance = _draggedTurret;
            
            _draggedTurret = null;
            _draggedWrapper = null;
            _snappedPosition = null;
            
            turretSpawn.EndGlowingAll();
            parentOfUI.gameObject.SetActive(true);
        }
        else if(_draggedTurret is not null)
        {
            Destroy(_draggedTurret);
            _draggedTurret = null;
            _snappedPosition = null;
            _draggingTurret = false;
            turretSpawn.EndGlowingAll();
            parentOfUI.gameObject.SetActive(true);
        }
    }

    private void DragAndDropTurretOnMouseExited(object sender, Vector2Args e)
    {
        if (_gameOver) return;
        var point = cam.ScreenToWorldPoint(new Vector3(e.Point.x, e.Point.y, cam.transform.position.z));

        DragAndDropTurret turret = sender as DragAndDropTurret;

        if (!turret.CanPay) return;
        _draggedTurret = Instantiate(turret.turret, point, Quaternion.identity);
        
        parentOfUI.gameObject.SetActive(false);
        _draggedWrapper = _draggedTurret.GetComponent<TurretWrapper>();
        _draggedWrapper.Canon = _draggedTurret.GetComponentInChildren<TurretBehaviour>();
        
        _currentTurret = turret;
        _draggingTurret = true;
        turretSpawn.StartGlowingAll();

    }

    private void SpawnManagerOnWaveStarted(object sender, EventArgs e)
    {
        startWaveButton.gameObject.SetActive(false);
    }

    private void SpawnManagerOnWaveChanged(object sender, EventArgs e)
    {
        if (_gameOver) return;
        _enemiesLeft = spawnManager.WaveAmount;
        _enemiesKilled = 0;
        startWaveButton.gameObject.SetActive(true);
        waveNumberText.text = spawnManager.WaveNumber.ToString();
    }

    private void BaseOnDamaged(object sender, EventArgs e)
    {
        if (_gameOver) return;
        baseHealthText.text = @base.Health.ToString("F0");
        if (@base.Health <= 0)
        {
            _gameOver = true;
            loseTransition.SetActive(true);
        } 
    }

    private void SpawnManagerOnEnemySpawned(object sender, EventArgs e)
    {
        _enemiesLeft--;
        enemiesLeftText.text = _enemiesLeft.ToString();
    }

    private void SpawnManagerOnEnemyDeath(object sender, EventArgs e)
    {
        _enemiesKilled++;
        var enemy = sender as Enemy_Behavior;
        if (_enemiesKilled >= spawnManager.WaveAmount && _lastWave)
        {
            winTransition.SetActive(true);
            return;
        }
        if (enemy.Enemy.Health > 0) return;
        player.Money += enemy.Enemy.MoneyGain;
    }
}
