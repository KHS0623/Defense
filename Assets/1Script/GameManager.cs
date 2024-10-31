using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public FadeEffect fade;
    public bool isLeftMax = false;
    public bool isRightMax = false;
    public bool isMoving = false; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            fade = GetComponent<FadeEffect>(); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitializeFadeEffect());
    }

    private IEnumerator InitializeFadeEffect()
    {
        yield return new WaitForSeconds(0.1f);  // �� �ε� �� �ణ�� ����

        if (fade != null)
        {
            fade.FadeInOutSetting();
        }
        else
        {
            Debug.LogError("FadeEffect ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }
}
