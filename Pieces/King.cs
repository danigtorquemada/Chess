using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.king)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void PossibleMovements()
    {
        base.PossibleMovements();

        BoxController _box;
        int x;
        int y;

        for (int leftIncrementer = -1; leftIncrementer <= 1; leftIncrementer++)
        {
            for (int upIncrementer = -1; upIncrementer <= 1; upIncrementer++)
            {
                x = leftIncrementer  + position.Column;
                y = upIncrementer  + position.Row;

                //Table limits
                if (x < 0 || y < 0 || x > 7 || y > 7) continue;

                _box = TableController.instance.GetBox(x, y);

                if (_box)
                {
                    if (!_box.HasPiece())
                    {
                        AddPossibleMovement(_box);
                    }
                    else if (_box.GetPiece().GetTeam() != myTeam)
                    {
                        AddPossibleMovement(_box);
                    }
                    else
                    { continue; }
                }

            }
        }
    }
}
