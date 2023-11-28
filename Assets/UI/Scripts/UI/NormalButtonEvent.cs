using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class NormalButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Audio.AudioManager.Instance?.PlayButtonHighlightedSFX();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Audio.AudioManager.Instance?.PlayButtonPressedSFX();
        }
    }
}