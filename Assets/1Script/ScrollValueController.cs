using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollValueController : MonoBehaviour
{
    public float minPositionX = -10f; // 최소 X 위치
    public float maxPositionX = 10f;   // 최대 X 위치
    public float scrollSensitivity = 0.1f; // 드래그 민감도

    private Vector2 previousInputPosition; // 이전 입력 위치
    private bool isDragging = false;        // 드래그 중인지 확인하는 변수

    private void Update()
    {
        // 클릭 또는 터치 시작
        if (Input.GetMouseButtonDown(0))
        {
            previousInputPosition = Input.mousePosition;
            isDragging = true; // 드래그 시작
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            previousInputPosition = Input.GetTouch(0).position;
            isDragging = true; // 드래그 시작
        }

        // 드래그 중일 때
        if (isDragging)
        {
            Vector2 currentInputPosition;

            // 현재 입력 위치 업데이트
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
                isDragging = false; // 드래그가 끝난 경우
                return;
            }

            // 이전 위치와 현재 위치의 차이를 계산
            float deltaX = currentInputPosition.x - previousInputPosition.x;

            // 오브젝트 이동
            Vector3 newPosition = transform.position + new Vector3(deltaX * scrollSensitivity * Time.deltaTime, 0, 0);

            // 위치를 클램프하여 범위를 제한
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

            // 이전 입력 위치 업데이트
            previousInputPosition = currentInputPosition;
        }

        // 드래그가 끝난 경우 상태 초기화
        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            isDragging = false; // 드래그 상태 해제
        }
    }
}



