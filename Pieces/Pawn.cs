using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        Debug.Log("HOLA");
        base.Initialize(_team, _pieceType);
    }
}
