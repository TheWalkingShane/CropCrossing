using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    private void Update()
    {
        // Get the current position of the GameObject
        /*Vector3 currentPosition = transform.position;

        // Set the z component of the position to 0
        currentPosition.z = 0;

        // Assign the updated position back to the GameObject
        transform.position = currentPosition;*/
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        // these three are in charge of taking it out of the slots so it can hover over the slots and not be hiding
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        
        // this makes it so that you can drop the item to a slot, without this it will just teleport back
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        
        // records the movement of the mouse
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        //puts it back into the slot within the hierarchy
        transform.SetParent(parentAfterDrag);
        
        image.raycastTarget = true;
    }
}
