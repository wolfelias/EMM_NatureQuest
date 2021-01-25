using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximise : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject miniMap;
    public Camera camera;
    bool mapIsOpened = false;

    void Start(){
        miniMap.transform.localScale = new Vector3 (1f, 1f, 1f);
        miniMap.transform.position = new Vector3(Screen.width-200, Screen.height-200);
        camera.fieldOfView = 10f;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")){
            if(mapIsOpened){
                miniMap.transform.localScale = new Vector3 (1f, 1f, 1f);
                miniMap.transform.position = new Vector3(Screen.width-200, Screen.height-200);
                camera.fieldOfView = 10f;

            }else{
                miniMap.transform.localScale = new Vector3 (2f, 2f, 2f);
                miniMap.transform.position = new Vector3(Screen.width/2, Screen.height/2);
                camera.fieldOfView = 16f;
            }
            mapIsOpened = !mapIsOpened;
        }
    }
}
