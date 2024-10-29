using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ÇÊ¿ä


public class MainBackGround : MonoBehaviour
{

    public GameObject[] BackGround;
    public float BackGroundSpeed = 0.1f;


    void Start()
    {

    }
    private void FixedUpdate()
    {

        for(int i = 0; i < BackGround.Length; i++)
        {

            if(BackGround[i] != null)
            {
                
                if (BackGround[i].transform.position.x <= -18.5f)
                {
                    Vector3 pos = BackGround[i].transform.position;
                    BackGround[i].transform.position = new Vector3(18.6f, pos.y, pos.z);
                }
                else
                {
                    BackGround[i].transform.position += Vector3.left * BackGroundSpeed;
                }
            }
            
        }
    }


}
