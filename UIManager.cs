using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text textTurn;
    [SerializeField] string[] text;

    [SerializeField] Transform whiteDeaths;
    [SerializeField] Transform blackDeaths;
    [SerializeField] DeathPiece deathPiece;

    Dictionary<int, DeathPiece> deathsPieces = new Dictionary<int, DeathPiece>();
    public void ChangeTurn(int tx)
    {
        textTurn.text = text[tx];
    }

    public void DeathPiece(int id)
    {
        if(deathsPieces.ContainsKey(id))
        {
            deathsPieces[id].AddPiece();
        }
        else
        {
            DeathPiece piece = Instantiate(deathPiece, id > 5 ? blackDeaths : whiteDeaths);
            piece.InitializeData(id);
            deathsPieces.Add(id, piece);
            OrderPieces(id > 5 ? blackDeaths : whiteDeaths);
        }
    }

    public void OrderPieces(Transform transform)
    {
        DeathPiece[] pieces = transform.GetComponentsInChildren<DeathPiece>();
        Array.Sort(pieces, delegate (DeathPiece x, DeathPiece y) { return x.GetId().CompareTo(y.GetId()); });

        Vector3 pos;
        RectTransform rect;
        for (int i = 0; i < pieces.Length; i++)
        {
            rect = pieces[i].GetComponent<RectTransform>();
            pos = rect.localPosition;
            pos.x = (i * 75);
            rect.localPosition = pos;
        }
    }
}
