using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathPiece : MonoBehaviour
{
    int id;
    int count = 0;
    [SerializeField] Text amountText;

    public void InitializeData(int _id)
    {
        id = _id;
        GetComponent<Image>().sprite = SpriteManager.singleton.GetSprite(id);
        AddPiece();
    }

    public void AddPiece()
    {
        count++;
        amountText.text = count.ToString();
    }

    public int GetId() { return id; }
}
