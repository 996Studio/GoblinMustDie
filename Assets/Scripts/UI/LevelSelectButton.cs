using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class LevelSelectButton : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler{

    public TMP_Text text;
    public Image rect;
    public Image circle;

    public Color textColorWhenSelected;
    public Color rectColorMouseOver;

    public int levelIndex;
    public bool isUnlocked;

    private void Start()
    {
        if (isUnlocked)
        {
            rect.color = Color.clear;
            text.color = Color.white;
            circle.color = Color.white;
        }
        else
        {
            rect.color = Color.clear;
            text.color = Color.grey;
            circle.color = Color.grey;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (isUnlocked)
        {
            rect.DOColor(Color.clear, .1f);
            text.DOColor(Color.white, .1f);
            circle.DOColor(Color.white, .1f);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (isUnlocked)
        {
            rect.DOColor(Color.white, .1f);
            text.DOColor(Color.black, .1f);
            circle.DOColor(Color.red, .1f);

            rect.transform.DOComplete();
            rect.transform.DOPunchScale(Vector3.one / 3, .2f, 20, 1);

            LevelSelect.instance.currentLevelIndex = levelIndex;
        }
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isUnlocked)
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                rect.DOColor(rectColorMouseOver, .2f);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isUnlocked)
        {
            if (EventSystem.current.currentSelectedGameObject != gameObject)
            {
                rect.DOColor(Color.clear, .2f);
            }
        }
    }
}