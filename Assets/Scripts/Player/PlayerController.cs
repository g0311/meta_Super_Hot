using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerController : Controller
{
    private Vector3 _prevPos;
    public GameObject _centerEye;
    public GameObject[] _fracturePool;
    public GameObject[] _hands;
    public GameObject _gameOverUI;
    public GameObject _playerPos;
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
        _playerDeathSound.Play();

        for (int i = 0; i < 14; i++)
        {
            _fracturePool[i].SetActive(true);
            _fracturePool[i].transform.SetParent(null);
        }

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponentInParent<CharacterController>().enabled = false;
        _hands[0].GetComponentInChildren<Hand>().Dead();
        _hands[1].GetComponentInChildren<Hand>().Dead();

        //killed anim + UI
        Vector3 localp = transform.localPosition;
        localp.z -= 3;
        StartCoroutine(GameOverCameraMove(transform.TransformPoint(localp)));
        _gameOverUI.GetComponentInChildren<Text>().text = "Kill Count = " + GameMode.Instance._killedCount;
        _gameOverUI.SetActive(true);

        GameMode.Instance.GameOver();
    }

    IEnumerator GameOverCameraMove(Vector3 pos)
    {
        float step = 0.1f;
        while (Vector3.Distance(transform.position, pos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, pos, step);
            step += 0.02f;
            Debug.Log(transform.position);
            yield return null;
        }
        transform.position = pos;
    }    
}
