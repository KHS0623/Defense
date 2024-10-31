using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    public GameObject carriage;
    private Vector2 targetPosition;

    public GameObject thisWheel;  
    public float rotationSpeed = 1.0f; 
    public float bobbingSpeed = 1.0f;  
    public float bobbingAmount = 0.1f;  
    private float randomOffset;  
    private Vector3 initialPosition;

    public GameObject[] BackGroundPortal;
    public GameObject[] StageJoin;

    public SceneLoader SceneLoader;
    private void Start()
    {
        if (carriage != null)
        {
            initialPosition = carriage.transform.position;
            randomOffset = Random.Range(0f, 100f);  // 0~100 사이의 무작위 값을 시작 오프셋으로 설정
        }
    }

    void Update()
    {
        // PC 환경에서 마우스 왼쪽 클릭 감지
        /*if (Input.GetMouseButtonDown(0))
        {
            HandleTouchOrClick(Input.mousePosition);
        }*/

        // 모바일 환경에서 터치 감지
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 터치 시작(한 번만) 이벤트 감지
            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchOrClick(touch.position);
            }
        }
    }

    void HandleTouchOrClick(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position); // 터치 또는 클릭 위치에서 레이 발사
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // 충돌한 오브젝트가 있을 때만 실행
        if (hit.collider != null)
        {
            Collider2D hitCollider = hit.collider;


            if (hitCollider != null)
            {
                Vector2 pos = hitCollider.transform.position;
                pos.x += 1;
                pos.y = -3;

                // 이동할 위치가 특정 포털일 경우에만 이동 시작
                if (hitCollider.name.Equals("Stage1Portal") || hitCollider.name.Equals("Stage2Portal") || hitCollider.name.Equals("Stage3Portal"))
                {
                    targetPosition = pos; // 목표 위치 설정
                    GameManager.instance.isMoving = true; // 이동 시작
                }

                switch (hitCollider.name)
                {
                    case "Stage1Portal":
                        if(BackGroundPortal == null && StageJoin == null)
                        {
                            break;
                        }
                        for (int i = 0; BackGroundPortal.Length > i; i++){
                            BackGroundPortal[i].SetActive(false);
                            StageJoin[i].SetActive(false);
                        }
                        BackGroundPortal[0].SetActive(true);
                        StageJoin[0].SetActive(true);
                        break;

                    case "Stage2Portal":
                        if (BackGroundPortal == null && StageJoin == null)
                        {
                            break;
                        }
                        for (int i = 0; BackGroundPortal.Length > i; i++)
                        {
                            BackGroundPortal[i].SetActive(false);
                            StageJoin[i].SetActive(false);
                        }
                        BackGroundPortal[1].SetActive(true);
                        StageJoin[1].SetActive(true);
                        break;

                    case "Stage3Portal":
                        if (BackGroundPortal == null && StageJoin == null)
                        {
                            break;
                        }
                        for (int i = 0; BackGroundPortal.Length > i; i++)
                        {
                            BackGroundPortal[i].SetActive(false);
                            StageJoin[i].SetActive(false);
                        }
                        BackGroundPortal[2].SetActive(true);
                        StageJoin[2].SetActive(true);
                        break;

                    case "Stage1Join":
                        SceneLoader.LoadScene("Stage1Scene");
                        break;

                    default:
                        if (BackGroundPortal == null && StageJoin == null)
                        {
                            break;
                        }
                        for (int i = 0; BackGroundPortal.Length > i; i++)
                        {
                            BackGroundPortal[i].SetActive(false);
                            StageJoin[i].SetActive(false);
                        }
                        BackGroundPortal[0].SetActive(true);
                        StageJoin[0].SetActive(true);
                        break;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isMoving)
        {

            carriage.transform.position = Vector2.Lerp(carriage.transform.position, targetPosition, 0.08f);
            if (thisWheel != null)
            {
                float Speed = rotationSpeed * (carriage.transform.position.x - targetPosition.x);
                thisWheel.transform.Rotate(0, 0, Speed * Time.fixedDeltaTime * 100);

            }
            else
            {
                Debug.LogWarning("thisWheel이 할당되지 않았습니다. Inspector에서 오브젝트를 할당해");
            }

            if (carriage != null)
            {
                float perlinValue = Mathf.PerlinNoise(Time.time * bobbingSpeed + randomOffset, 0f);
                float newY = initialPosition.y + (perlinValue - 0.5f) * bobbingAmount * 2f;  
                carriage.transform.position = new Vector3(carriage.transform.position.x, newY, initialPosition.z);
            }
            else
            {
                Debug.LogWarning("bobbingObject가 할당되지 않았습니다. Inspector에서 오브젝트를 할당해");
            }
            // 현재 위치와 목표 위치가 거의 같으면 이동 중지
            if (Vector2.Distance(carriage.transform.position, targetPosition) < 0.01f)
            {
                carriage.transform.position = targetPosition; // 최종 위치 설정
                GameManager.instance.isMoving = false; // 이동 중지
            }
        }
    }
}
