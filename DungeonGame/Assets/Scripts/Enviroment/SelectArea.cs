using System;
using UnityEngine;

public class SelectArea : MonoBehaviour
{
    public PlayerControllerSO controller;
    
    public bool rememberPrevious;
    public bool revertOnExit; 

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
       if(!other.CompareTag("Player")) return;
       
       var pc = other.GetComponent<PlayerController>();
       if (pc && controller)
       {
           pc.SetController(controller, rememberPrevious);
       }
    }

    void OnTriggerExit(Collider other)
    {
        if (!revertOnExit) return;
        
        var pc = other.GetComponent<PlayerController>();

        if (pc)
        {
            pc.RevertToPreviousController();
        }
    }
}
