using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode _instance;
    public static GameMode Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameMode>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameMode");
                    _instance = obj.AddComponent<GameMode>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    public float _deltaTime = 0;
    public GameObject _player;
    public int _killedCount = 0;
    public GameObject _enemyFactory;
    public Transform[] _spawnPoint;

    private void Start()
    {
        StartCoroutine(SpawnPlayer());
    }

    private void LateUpdate()
    {
        _deltaTime = 0.01f;
    }

    public void SetDeltaTime(float moveDistance)
    {
        if(_deltaTime <= moveDistance * 5)
        {
            _deltaTime = moveDistance * 5;
        }
    }

    public void PawnKilled(GameObject gameObject)
    {
        if (gameObject == _player)
        {
            gameObject.GetComponent<Controller>().PawnDeath();
            GameOver(gameObject);
        }
        else
        {
            gameObject.GetComponentInParent<Controller>().PawnDeath();
            _killedCount++;
        }
    }

    private void GameOver(GameObject gameObject)
    {
        //stop interact
        gameObject.GetComponent<PlayerController>().GameOver(_killedCount);
        StopAllCoroutines();
        foreach (Transform t in _spawnPoint)
        {
            if (t.childCount != 0)
            {
                t.GetChild(0).GetComponent<Controller>().PawnDeath();
            }
        }

    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    IEnumerator SpawnPlayer()
    {
        while (true)
        {
            foreach (Transform t in _spawnPoint)
            {
                if (t.childCount == 0)
                {
                    int x = Random.Range(1, 6);
                    if (x == 5)
                    {
                        Instantiate(_enemyFactory, t);
                    }
                }
            }
            yield return new WaitForSeconds(5);
        }
    }
}
