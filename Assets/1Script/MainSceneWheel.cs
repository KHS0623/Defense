using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneWheel : MonoBehaviour
{
    public GameObject thisWheel;  // ȸ���� ���� ������Ʈ
    public GameObject bobbingObject;  // ���Ʒ��� ���ȰŸ� ������Ʈ
    public float rotationSpeed = 1.0f;  // ���� ȸ�� �ӵ�
    public float bobbingSpeed = 1.0f;   // ���ȰŸ� �ӵ�
    public float bobbingAmount = 0.1f;  // ���ȰŸ��� ũ��
    private float randomOffset;  // ������ ���� ��ġ
    private Vector3 initialPosition;  // bobbingObject�� �ʱ� ��ġ

    private void Start()
    {
        if (bobbingObject != null)
        {
            initialPosition = bobbingObject.transform.position;
            randomOffset = Random.Range(0f, 100f);  // 0~100 ������ ������ ���� ���� ���������� ����
        }
    }

    private void FixedUpdate()
    {
        if (thisWheel != null)
        {
            thisWheel.transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime * 100);
        }
        else
        {
            Debug.LogWarning("thisWheel�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� ������Ʈ�� �Ҵ����ּ���.");
        }

        if (bobbingObject != null)
        {
            // PerlinNoise�� ����Ͽ� ���ȰŸ��� �� �����ϰ� ����
            float perlinValue = Mathf.PerlinNoise(Time.time * bobbingSpeed + randomOffset, 0f);
            float newY = initialPosition.y + (perlinValue - 0.5f) * bobbingAmount * 2f;  // -0.5 ~ +0.5�� �߽ɿ��� ���Ʒ��� ���ȰŸ�
            bobbingObject.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
        }
        else
        {
            Debug.LogWarning("bobbingObject�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� ������Ʈ�� �Ҵ����ּ���.");
        }
    }


}
