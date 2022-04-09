using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsLogic : MonoBehaviour
{

    public bool moveCards = false;
    private int _menuNumber;
    public int GetMenuNumber() {
        return _menuNumber;
    }


    private void Start()
    {
        _menuNumber = int.Parse(GetComponentInChildren<Text>().text);
    }

    private void OnMouseOver()
    {
        moveCards = true;
    }
    private void OnMouseDown()
    {
        moveCards = true;
    }
    private void OnMouseExit()
    {
        moveCards = false;
    }
    private void OnMouseUp()
    {
        moveCards = false;
    }



}
