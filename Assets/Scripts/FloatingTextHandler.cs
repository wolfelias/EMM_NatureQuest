using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandler : MonoBehaviour
{
    public float x = 0;
    public float y = 25;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        transform.localPosition += new Vector3(x, y, 0);
    }
}
