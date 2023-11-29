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
            if (RbUIManager.Instance) RbUIManager.EventHappened(name);
        }
    }
}