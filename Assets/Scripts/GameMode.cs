using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode _instance;

    public GameMode Instance()
    {
        if (_instance == null)
        {
            _instance = new GameMode();
        }
        return _instance;
    }


    public float _deltaTime = 0; //�÷��̾� ��Ʈ�ѷ����� ����

    // Update is called once per frame
    void Update()
    {
        //�¸� ���� �ľ�
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
