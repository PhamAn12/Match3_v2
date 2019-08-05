using System;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    //Attach this script to a Dropdown GameObject
    Dropdown m_Dropdown;
    //This is the string that stores the current selection m_Text of the Dropdown
    string m_Message;
    //This Text outputs the current selection to the screen
    public Text m_Text;
    //This is the index value of the Dropdown
    int m_DropdownValue;
    //private GameContext gameContext;
    void Start()
    {
        //Fetch the DropDown component from the GameObject
        m_Dropdown = GameObject.Find("Canvas/Dropdown").GetComponent<Dropdown>();
        //Output the first Dropdown index value
        Debug.Log("Starting Dropdown Value : " + m_Dropdown.itemText);
    }

    public void EventClick()
    {
        Debug.Log("Dropdown value : " + m_Dropdown.value);
        GameContext gameContext = new GameContext();
        var entity = gameContext.CreateEntity();
        
        Debug.Log("dkdkdkdk  : " + entity);

    }
}