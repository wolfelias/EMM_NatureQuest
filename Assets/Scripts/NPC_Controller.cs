using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour, Interactable_Interface
{
    [SerializeField] Quiz_TextList quizText;

    public void Interact()
    {
        DialogManager.Instance.ShowDialog(quizText);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
