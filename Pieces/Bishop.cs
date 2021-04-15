using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.bishop)
    {
        base.Initialize(_team, _pieceType);
    }
}
