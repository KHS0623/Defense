using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneWheel : MonoBehaviour
{
    public GameObject thisWheel;  // 회전할 바퀴 오브젝트
    public GameObject bobbingObject;  // 위아래로 덜컹거릴 오브젝트
    public float rotationSpeed = 1.0f;  // 바퀴 회전 속도
    public float bobbingSpeed = 1.0f;   // 덜컹거림 속도
    public float bobbingAmount = 0.1f;  // 덜컹거림의 크기
    private float randomOffset;  // 랜덤한 시작 위치
    private Vector3 initialPosition;  // bobbingObject의 초기 위치

    private void Start()
    {
        if (bobbingObject != null)
        {
            initialPosition = bobbingObject.transform.position;
            randomOffset = Random.Range(0f, 100f);  // 0~100 사이의 무작위 값을 시작 오프셋으로 설정
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
            Debug.LogWarning("thisWheel이 할당되지 않았습니다. Inspector에서 오브젝트를 할당해주세요.");
        }

        if (bobbingObject != null)
        {
            // PerlinNoise를 사용하여 덜컹거림을 더 랜덤하게 만듦
            float perlinValue = Mathf.PerlinNoise(Time.time * bobbingSpeed + randomOffset, 0f);
            float newY = initialPosition.y + (perlinValue - 0.5f) * bobbingAmount * 2f;  // -0.5 ~ +0.5로 중심에서 위아래로 덜컹거림
            bobbingObject.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
        }
        else
        {
            Debug.LogWarning("bobbingObject가 할당되지 않았습니다. Inspector에서 오브젝트를 할당해주세요.");
        }
    }


}
