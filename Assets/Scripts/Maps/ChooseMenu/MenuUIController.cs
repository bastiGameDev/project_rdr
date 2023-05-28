using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image iconImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject menu;
    public void SetMenuItem(Sprite sprite, string description) {
        ShowMenu();
        iconImage.sprite = sprite;
        descriptionText.text = description;
    }
    

    public void ShowMenu() {
        menu.SetActive(true);
    }
    public void HideMenu() {
        menu.SetActive(false);
    }
}
