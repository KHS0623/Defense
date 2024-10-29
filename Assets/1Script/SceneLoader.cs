using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public FadeEffect fadeEffect; // FadeEffect 스크립트에 대한 참조
    public GameObject loadingScreen;
    public Image loadingSlider;
    public Text loadingText;

    public float loadSpeed = 0.5f;  // 로딩 속도를 제어할 변수

    public void LoadScene(string sceneName)
    {
        fadeEffect = GameManager.instance.fade;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        fadeEffect.FadeOut();
        yield return new WaitForSeconds(2f);
        fadeEffect.FadeIn();
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(loadSpeed);
        

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float progress = 0f;
        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);

            progress = Mathf.MoveTowards(progress, targetProgress, loadSpeed * Time.deltaTime);

            loadingSlider.fillAmount = progress;
            loadingText.text = (progress * 100f).ToString("F0") + "%";

            if (progress >= 1f)
            {
                loadingText.text = "Press any key to continue";
                if (Input.anyKeyDown)
                {
                    fadeEffect.FadeOut();
                    yield return new WaitForSeconds(2f);
                    operation.allowSceneActivation = true;
                    
                }
            }
            
            yield return null;
        }

        
    }
}
