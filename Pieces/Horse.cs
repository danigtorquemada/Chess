using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.horse)
    {
        base.Initialize(_team, _pieceType);
    }
}
