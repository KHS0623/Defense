using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    public Image fadeImage;
    public GameObject targetObject;
    public float fadeDuration = 1f;

    public void FadeInOutSetting()
    {
        targetObject = GameObject.FindWithTag("FadeInOut");

        if (targetObject != null)
        {
            fadeImage = targetObject.GetComponent<Image>();
            FadeIn();
        }
        else
        {
            Debug.LogError("태그가 'FadeInOut'인 게임 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f, false)); // 페이드 인
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f, true)); // 페이드 아웃
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, bool deactivateObject)
    {
        targetObject.SetActive(true);
        float elapsed = 0f;

        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // 경과 시간 증가

            // 점진적으로 알파 값을 변화시킴
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;

            yield return null; // 한 프레임 대기
        }

        color.a = endAlpha;
        fadeImage.color = color;

        // 알베도 값이 0이 되었을 때 비활성화
        if (color.a <= 0f)
        {
            targetObject.SetActive(false);
        }
        else
        {
            // 필요에 따라 비활성화하지 않음
            targetObject.SetActive(true);
        }
    }
}
