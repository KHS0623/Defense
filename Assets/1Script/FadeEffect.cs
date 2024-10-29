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
            Debug.LogError("�±װ� 'FadeInOut'�� ���� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f, false)); // ���̵� ��
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f, true)); // ���̵� �ƿ�
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
            elapsed += Time.deltaTime; // ��� �ð� ����

            // ���������� ���� ���� ��ȭ��Ŵ
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;

            yield return null; // �� ������ ���
        }

        color.a = endAlpha;
        fadeImage.color = color;

        // �˺��� ���� 0�� �Ǿ��� �� ��Ȱ��ȭ
        if (color.a <= 0f)
        {
            targetObject.SetActive(false);
        }
        else
        {
            // �ʿ信 ���� ��Ȱ��ȭ���� ����
            targetObject.SetActive(true);
        }
    }
}
