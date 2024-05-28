using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    private Vector3 _prevPos;
    public GameObject _centerEye;
    public GameObject[] _fracturePool;
    public GameObject[] _hands;
    public GameObject _gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        _prevPos = _centerEye.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = Vector3.Distance(_prevPos, _centerEye.transform.position);
        GameMode.Instance.SetDeltaTime(moveDistance);
        _prevPos = _centerEye.transform.position;
    }

    public override void PawnDeath()
    {
        _isAlive = false;
    }

    public void GameOver(int killCount)
    {
        //killed anim + UI
        for (int i = 0; i < 14; i++)
        {
            _fracturePool[i].SetActive(true);
        }
        GetComponentInChildren<BoxCollider>().enabled = false;

        //hands -> dead -> mesh render off -> layser pointer on
        _hands[0].GetComponentInChildren<Hand>().Dead();
        _hands[1].GetComponentInChildren<Hand>().Dead();

        Instantiate(_gameOverUI, _prevPos, Quaternion.identity);
        _gameOverUI.GetComponentInChildren<Text>().text = "Kill Count = " + killCount;
    }
}
