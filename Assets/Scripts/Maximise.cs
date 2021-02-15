using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This MonoBehaviour class the controller for maximising the minimap
 */
public class Maximise : MonoBehaviour
{
    
    public GameObject gameObject;
    public Camera camera;
    bool mapIsOpened = false;

    Vector3 startScale, startTransform;

    /*
     * Sets the initial position and size of the minimap
     */
    void Start()
    {
        startScale = new Vector3(0.3f, 0.3f, 0.3f);
        startTransform = new Vector3(Screen.width - Screen.width/9.8f, Screen.height - Screen.height/6);

        // gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //gameObject.transform.position = new Vector3(Screen.width - 30f, Screen.height - 30f);
        gameObject.transform.localScale = startScale;
        gameObject.transform.position = startTransform;

        camera.fieldOfView = 6f;
    }

    /*
     * toggles the size of the minimap when key 'm' is pressed
     */
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
                camera.fieldOfView = 6f;

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