using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    //for selectLevel Button in MainMenu
    public class MenuLevelButtonEvent : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            //if (RbUIManager.Instance && transform.Find(Common.Constant.RB_TEXT_NAME) && transform.Find(Common.Constant.RB_TEXT_NAME).TryGetComponent<Rigidbody2D>(out var rb)) //RbUIManager存在，且有RbText子物体，并且这个子物体有刚体组件。 RbUIManager exist , && Have a child Object named "RbText", && "RbText" have rb2D.
            if (RbUIManager.Instance) RbUIManager.EventHappened(name);
        }
    }
}