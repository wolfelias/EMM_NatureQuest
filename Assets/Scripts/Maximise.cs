using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximise : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public Camera camera;
    bool mapIsOpened = false;


    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")){
            if(mapIsOpened){
                gameObject.transform.localScale = new Vector3 (1, 1f, 1);
                gameObject.transform.position = new Vector3(Screen.width/1.15f, Screen.height/1.2f);
                camera.fieldOfView = 16f;

            }else{
                gameObject.transform.localScale = new Vector3 (3, 3f, 3);
                gameObject.transform.position = new Vector3(Screen.width/2, Screen.height/2);
                camera.fieldOfView = 16f;
            }
            mapIsOpened = !mapIsOpened;
        }
    }
}
