using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TouchGrabber : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SceneLogic _sceneLogic;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_sceneLogic)
        {
            _sceneLogic.CurrentKnife.Throw();
        }
    }
}
