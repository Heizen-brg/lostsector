using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour
{

    public enum InteractionType {NONE, PickUp, Examine }
    public InteractionType type;

    public UnityEvent customEvent;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Interact ()
    {
        switch(type) {
            case InteractionType.PickUp:
                //add obj
                


                Debug.Log("pick up");
                break;

            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this);

                break;
            default:
                Debug.Log("null");
                break;
        }
        //Invoke Event

        customEvent.Invoke();

    }
}
