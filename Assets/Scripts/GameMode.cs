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
    public Transform[] _spawnPoint;

    private void Update()
    {
        //random pos enemy spawn
        //spawn corutine
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
        //print UI
        gameObject.GetComponent<PlayerController>().GameOver(_killedCount);
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
