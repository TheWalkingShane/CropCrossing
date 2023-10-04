using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFix : MonoBehaviour
{
    private void LateUpdate()
    {
        // Get the parent transform
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            // Set the size of the current object to match the parent's local scale
            Vector3 parentScale = parentTransform.localScale;
            transform.localScale = new Vector3(parentScale.x, parentScale.y, parentScale.z);
        }
        else
        {
            Debug.LogWarning("No parent found.");
        }
    }
}
