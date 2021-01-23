using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Schild : MonoBehaviour
{
    public Signal signal;
    public Animator animator;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private bool playerInRange;

    void Start()
    {
        dialogBox.SetActive(false);
        animator.SetBool("IsOpen", false);
    }

    void Update()
    {
        
    }
}
