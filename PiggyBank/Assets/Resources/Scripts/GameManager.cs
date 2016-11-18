using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GAME_MODE
{
    //TITLE = 0,
    MENU=0,
    LOBBY,
    MAIN_GAME,
    RESULT,
}

public class GameManager : MonoBehaviour {

    // シングルトン
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    // 現在のモード
    public GAME_MODE _Mode;
    private static bool isStart = false;

    void Awake()
    {
        if(!isStart)
        {
            DontDestroyOnLoad(this.gameObject);
            isStart = true;
        }else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    public void OnLevelWasLoaded()
    {
        // 現在のモードを設定
        _Mode = (GAME_MODE)SceneManager.GetActiveScene().buildIndex;
    }
    
    /// <summary>
    /// ゲームモードを変更する。
    /// </summary>
    /// <param name="next">次のゲームモード</param>
    public void ChangeMode(GAME_MODE next)
    {
        SceneManager.LoadScene((int)next);
    }
}
