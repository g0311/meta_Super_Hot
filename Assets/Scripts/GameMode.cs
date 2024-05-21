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
    //UI


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
        if(gameObject == _player)
            gameObject.GetComponent<Controller>().PawnDeath();
        else
            gameObject.GetComponentInParent<Controller>().PawnDeath();
        CheckGameOver();
    }

    private void CheckGameOver()
    {
        //check All Enemies dead or Player deads
    }

    private void GameOver(bool isWin)
    {
        //print UI
        if (isWin)
            return;
        else
            return;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
}
