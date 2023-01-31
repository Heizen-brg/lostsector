using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    //detection point
    public Transform detectionPoint;
    //detection radius
    public const float detectionRadius = 0.2f;
    //detection layer
    public LayerMask detectionItemLayer;
    public LayerMask detectionConsumablesLayer;
    //cache trigger
    GameObject detectedObj;



    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObj.GetComponent<Item>().Interact();
            }
        }
        if (DetectConsume())
        {
            detectedObj.GetComponent<Consumables>().Consume();
        }
    }

    bool InteractInput()
    {
        return Input.GetKey(KeyCode.E);
    }


    bool DetectObject()
    {

        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionItemLayer);
        if (obj == null)
        {
            detectedObj = null;
            return false;
        }
        else
        {
            detectedObj = obj.gameObject;
            return true;
        }
    }

    bool DetectConsume()
    {

        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionConsumablesLayer);
        if (obj == null)
        {
            detectedObj = null;
            return false;
        }
        else
        {
            detectedObj = obj.gameObject;
            return true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    public void ExamineItem(Item item)
    {
        //item.GetComponent<Animator>().SetTrigger("Unlock");
    }

    public void PickupItem(Consumables consumable)
    {
        consumable.gameObject.SetActive(false);
    }
}
