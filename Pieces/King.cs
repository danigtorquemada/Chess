using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.king)
    {
        base.Initialize(_team, _pieceType);
    }
}
