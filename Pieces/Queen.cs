using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.queen)
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
                for (int j = 1; j < 8; j++)
                {
                    x = leftIncrementer * j + position.Column;
                    y = upIncrementer * j + position.Row;

                    //Table limits
                    if (x < 0 || y < 0 || x > 7 || y > 7) break;

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
                            break;
                        }
                        else
                        { break; }
                    }
                }
            }
        }
    }
}
