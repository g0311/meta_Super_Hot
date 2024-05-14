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

    // Update is called once per frame
    void Update()
    {
        _deltaTime = 0;
    }

    public void SetDeltaTime(float moveDistance)
    {
        if(_deltaTime <= moveDistance * 0.1f)
        {
            _deltaTime = moveDistance * 0.1f;
        }
    }

    public void PawnKilled(GameObject gameObject)
    {
        gameObject.GetComponent<Controller>().PawnDeath();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        
    }

    void GameOver(bool isWin)
    {

    }
}
