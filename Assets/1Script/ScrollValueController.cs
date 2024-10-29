using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollValueController : MonoBehaviour
{
    public float minPositionX = -10f; // �ּ� X ��ġ
    public float maxPositionX = 10f;   // �ִ� X ��ġ
    public float scrollSensitivity = 0.1f; // �巡�� �ΰ���

    private Vector2 previousInputPosition; // ���� �Է� ��ġ
    private bool isDragging = false;        // �巡�� ������ Ȯ���ϴ� ����

    private void Update()
    {
        // Ŭ�� �Ǵ� ��ġ ����
        if (Input.GetMouseButtonDown(0))
        {
            previousInputPosition = Input.mousePosition;
            isDragging = true; // �巡�� ����
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            previousInputPosition = Input.GetTouch(0).position;
            isDragging = true; // �巡�� ����
        }

        // �巡�� ���� ��
        if (isDragging)
        {
            Vector2 currentInputPosition;

            // ���� �Է� ��ġ ������Ʈ
            if (Input.GetMouseButton(0))
            {
                currentInputPosition = Input.mousePosition;
            }
            else if (Input.touchCount > 0)
            {
                currentInputPosition = Input.GetTouch(0).position;
            }
            else
            {
                isDragging = false; // �巡�װ� ���� ���
                return;
            }

            // ���� ��ġ�� ���� ��ġ�� ���̸� ���
            float deltaX = currentInputPosition.x - previousInputPosition.x;

            // ������Ʈ �̵�
            Vector3 newPosition = transform.position + new Vector3(deltaX * scrollSensitivity * Time.deltaTime, 0, 0);

            // ��ġ�� Ŭ�����Ͽ� ������ ����
            newPosition.x = Mathf.Clamp(newPosition.x, minPositionX, maxPositionX);

            transform.position = newPosition;
            if (newPosition.x == maxPositionX)
            {
                GameManager.instance.isLeftMax = true;
            }
            else if (newPosition.x == minPositionX)
            {
                GameManager.instance.isRightMax = true;
            }
            else
            {
                GameManager.instance.isRightMax = false;
                GameManager.instance.isLeftMax = false;
            }

            // ���� �Է� ��ġ ������Ʈ
            previousInputPosition = currentInputPosition;
        }

        // �巡�װ� ���� ��� ���� �ʱ�ȭ
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false; // �巡�� ���� ����
        }
    }
}



