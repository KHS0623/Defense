using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ÇÊ¿ä


public class MainBackGround : MonoBehaviour
{

    public GameObject BackGround;
    public float BackGroundSpeed = 0.1f;


    void Start()
    {
    }
    private void FixedUpdate()
    {

                
                if (transform.position.x <= -18.5f)
                {
                    Vector3 pos = transform.position;
                    transform.position = new Vector3(18.6f, pos.y, pos.z);
                }
                else
                {
                    transform.position += Vector3.left * BackGroundSpeed;
                }
            
            
        
    }


}
