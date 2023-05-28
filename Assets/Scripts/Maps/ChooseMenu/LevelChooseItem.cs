using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class LevelChooseItem : MonoBehaviour
{
    [SerializeField] private Sprite focusedImage;
    [SerializeField] private string description;
    [SerializeField] private int raceID;
    [SerializeField] private MenuUIController menuUIController;
    [SerializeField] private ChooseRaceController raceController;

    private void OnMouseDown() {
        raceController.SetLoadScene(raceID);
        raceController.MakeTransitionTo(1);
    }

    private void OnMouseEnter() {
        menuUIController.SetMenuItem(focusedImage, description);
        Debug.Log("Enter");
    }
    private void OnMouseExit() {
        menuUIController.HideMenu();
        Debug.Log("Exit");
    }
}
