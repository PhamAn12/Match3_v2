using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SizeNum : MonoBehaviour
{
    private Button _okButton;
    private InputField _inputCols;
    private InputField _inputRows;
    public void GetInputRows(string rows)
    {

        if(Convert.ToInt32(rows) < 3)
        {
//            EditorUtility.DisplayDialog("row invalid","row must be > 3","OK");
            Debug.Log("kd");
            _okButton = GameObject.Find("Canvas/OptionMenu/OkButton").GetComponent<Button>();
            _inputCols = GameObject.Find("Canvas/OptionMenu/InputRow").GetComponent<InputField>();
            _inputCols.text = "";
        }
        
        else
        {
            PlayerPrefs.SetString("SizeRows", rows);
            Debug.Log("rows :" + rows);
        }
    } 
    public void GetInputColumns(string cols)
    {
        
        if(Convert.ToInt32(cols) < 3 || cols == null)
        {
//            EditorUtility.DisplayDialog("col invalid","col must be > 3","OK");
            Debug.Log("kd");
            _okButton = GameObject.Find("Canvas/OptionMenu/OkButton").GetComponent<Button>();
            _inputRows = GameObject.Find("Canvas/OptionMenu/InputCol").GetComponent<InputField>();
            _inputRows.text = "";
        } 
        else
        {
            PlayerPrefs.SetString("SizeCols", cols);
            Debug.Log("cols :" + cols);
        }
    }

    public void GetInputMove(string numMove)
    {
        PlayerPrefs.SetString("NumOfMoves",numMove);
    }
}
