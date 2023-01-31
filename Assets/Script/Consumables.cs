using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]

public class Consumables : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ConsumeType { NONE, Health, Energy, Armor }

    public ConsumeType type;

    public UnityEvent customEvent;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 10;
    }

    public void Consume()
    {
        switch (type)
        {
            case ConsumeType.Health:
                //add obj
                FindObjectOfType<InteractionSystem>().PickupItem(this);
                Debug.Log("health");
                break;

            case ConsumeType.Energy:
                FindObjectOfType<InteractionSystem>().PickupItem(this);
                Debug.Log("Energy");

                break;
            case ConsumeType.Armor:
                FindObjectOfType<InteractionSystem>().PickupItem(this);
                Debug.Log("Armor");
                break;
            default:
                Debug.Log("null");
                break;
        }
        //Invoke Event

        customEvent.Invoke();

    }
}
