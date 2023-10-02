using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        // these three are in charge of taking it out of the slots so it can hover over the slots and not be hiding
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        
        // this makes it so that you can drop the item to a slot, without this it will just teleport back
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("Dragging");
        
        // records the movement of the mouse
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
       // Debug.Log("End Drag");
        //puts it back into the slot within the hierarchy
        transform.SetParent(parentAfterDrag);
        
        image.raycastTarget = true;
    }
}
