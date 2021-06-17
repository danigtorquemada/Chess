using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text textTurn;
    [SerializeField] string[] text;
    public void ChangeTurn(int tx)
    {
        textTurn.text = text[tx];
    }
}
