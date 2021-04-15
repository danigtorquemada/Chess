using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.tower)
    {
        base.Initialize(_team, _pieceType);
    }
}
