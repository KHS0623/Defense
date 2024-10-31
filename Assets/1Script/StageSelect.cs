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
            randomOffset = Random.Range(0f, 100f);  // 0~100 ������ ������ ���� ���� ���������� ����
        }
    }

    void Update()
    {
        // PC ȯ�濡�� ���콺 ���� Ŭ�� ����
        /*if (Input.GetMouseButtonDown(0))
        {
            HandleTouchOrClick(Input.mousePosition);
        }*/

        // ����� ȯ�濡�� ��ġ ����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ��ġ ����(�� ����) �̺�Ʈ ����
            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchOrClick(touch.position);
            }
        }
    }

    void HandleTouchOrClick(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position); // ��ġ �Ǵ� Ŭ�� ��ġ���� ���� �߻�
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // �浹�� ������Ʈ�� ���� ���� ����
        if (hit.collider != null)
        {
            Collider2D hitCollider = hit.collider;


            if (hitCollider != null)
            {
                Vector2 pos = hitCollider.transform.position;
                pos.x += 1;
                pos.y = -3;

                // �̵��� ��ġ�� Ư�� ������ ��쿡�� �̵� ����
                if (hitCollider.name.Equals("Stage1Portal") || hitCollider.name.Equals("Stage2Portal") || hitCollider.name.Equals("Stage3Portal"))
                {
                    targetPosition = pos; // ��ǥ ��ġ ����
                    GameManager.instance.isMoving = true; // �̵� ����
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
                Debug.LogWarning("thisWheel�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� ������Ʈ�� �Ҵ���");
            }

            if (carriage != null)
            {
                float perlinValue = Mathf.PerlinNoise(Time.time * bobbingSpeed + randomOffset, 0f);
                float newY = initialPosition.y + (perlinValue - 0.5f) * bobbingAmount * 2f;  
                carriage.transform.position = new Vector3(carriage.transform.position.x, newY, initialPosition.z);
            }
            else
            {
                Debug.LogWarning("bobbingObject�� �Ҵ���� �ʾҽ��ϴ�. Inspector���� ������Ʈ�� �Ҵ���");
            }
            // ���� ��ġ�� ��ǥ ��ġ�� ���� ������ �̵� ����
            if (Vector2.Distance(carriage.transform.position, targetPosition) < 0.01f)
            {
                carriage.transform.position = targetPosition; // ���� ��ġ ����
                GameManager.instance.isMoving = false; // �̵� ����
            }
        }
    }
}
