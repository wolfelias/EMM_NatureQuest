/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximise : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public Camera camera;
    bool mapIsOpened = false;
    private Vector2 startPosition;


    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("m")){
            if(mapIsOpened){
                gameObject.transform.localScale = new Vector3 (1, 1f, 1);
               // gameObject.transform.position = new Vector3(Screen.width/1.15f, Screen.height/1.2f);
                gameObject.transform.position = startPosition;
                camera.fieldOfView = 8.3f;

            }else{
                gameObject.transform.localScale = new Vector3 (3, 3f, 3);
                gameObject.transform.position = new Vector3(Screen.width/2, Screen.height/2);
                camera.fieldOfView = 16f;
            }
            mapIsOpened = !mapIsOpened;
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maximise : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public Camera camera;
    bool mapIsOpened = false;

    Vector3 startScale, startTransform;

    void Start()
    {
        startScale = new Vector3(0.5f, 0.5f, 0.5f);
        startTransform = new Vector3(Screen.width - Screen.width/6, Screen.height - Screen.height/4);

        // gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //gameObject.transform.position = new Vector3(Screen.width - 30f, Screen.height - 30f);
        gameObject.transform.localScale = startScale;
        gameObject.transform.position = startTransform;

        camera.fieldOfView = 8f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (mapIsOpened)
            {
                //gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //gameObject.transform.position = new Vector3(Screen.width - 300f, Screen.height - 300f);
                gameObject.transform.localScale = startScale;
                gameObject.transform.position = startTransform;
                camera.fieldOfView = 8f;

            }
            else
            {
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                gameObject.transform.position = new Vector3(Screen.width / 2, Screen.height / 2);
                camera.fieldOfView = 16f;
            }
            mapIsOpened = !mapIsOpened;
        }
    }
}