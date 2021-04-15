using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.queen)
    {
        base.Initialize(_team, _pieceType);
    }
}
