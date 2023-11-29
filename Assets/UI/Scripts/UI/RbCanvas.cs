using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbCanvas : MonoBehaviour
{
    public void SetLevelButton(string name)
    {
        var child = transform.Find(Common.Constant.RB_CANVAS_BUTTON_FATHER_NAME + name);
        if (child != null && child.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.bodyType = RigidbodyType2D.Static; //set the selectButton
        }
        Debug.Log("EventHappened: name = " + name + ", child = " + child);
    }
}
