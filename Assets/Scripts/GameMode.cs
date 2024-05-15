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

    public GameObject _player;

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

    // Update is called once per frame
    void Update()
    {
        _deltaTime = 0.01f;
    }

    public void SetDeltaTime(float moveDistance)
    {
        if(_deltaTime <= moveDistance * 10)
        {
            _deltaTime = moveDistance * 10;
        }
    }

    public void PawnKilled(GameObject gameObject)
    {
        gameObject.GetComponent<Controller>().PawnDeath();
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        
    }

    private void GameOver(bool isWin)
    {

    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
