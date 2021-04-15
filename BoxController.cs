using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    int row;
    int column;

    Piece piece;

    public void SetPosition(int _row, int _column)
    {
        row = _row;
        column = _column;
    }

    public void SetPiece(Piece _piece)
    {
        piece = _piece;
    }

    public bool HasPiece()
    {
        return piece != null;
    }
}
