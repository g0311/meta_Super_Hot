using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Button _playBtn;
    public Button _exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        _playBtn.onClick.AddListener(PlayBtn);
        _exitBtn.onClick.AddListener(ExitBtn);
    }

    void PlayBtn()
    {
        SceneManager.LoadScene(1);
        return;
    }
    void ExitBtn()
    {
        SceneManager.LoadScene(0);
        return;
    }
}
