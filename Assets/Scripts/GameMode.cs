using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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

    public float _moveDistance = 0;
    public GameObject _player;
    public int _killedCount = 0;
    public GameObject _enemyFactory;
    public Transform[] _spawnPoint;

    private void Start()
    {
        if(_spawnPoint != null)
            StartCoroutine(SpawnPlayer());
    }

    private void LateUpdate()
    {
        _moveDistance = 0.001f;
    }

    public void SetDeltaTime(float moveDistance)
    {
        if(_moveDistance <= moveDistance * 10 && moveDistance * 10 < 0.2)
        {// 시간이 목적 이상으로 빠르게 흐르지 않기 위해 일정 범위를 지정
            _moveDistance = moveDistance * 10;
        }
    }

    public void GameOver()
    {
        StopAllCoroutines();
        foreach (Transform t in _spawnPoint)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                if (t.GetChild(i).GetComponent<Controller>()._isAlive)
                {
                    t.GetChild(i).GetComponent<Controller>().PawnDeath();
                }
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
                int x = Random.Range(1, 6);
                if (x == 5)
                {
                    Instantiate(_enemyFactory, t);
                }
            }
            yield return new WaitForSeconds(3);
        }
    }
}
