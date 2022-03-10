using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.horse)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void PossibleMovements()
    {
        base.PossibleMovements();

        BoxController _box;
        int x;
        int y;
        bool doubleColumn;

        for (int leftIncrementer = -1; leftIncrementer <= 1; leftIncrementer += 2)
        {
            for (int upIncrementer = -1; upIncrementer <= 1; upIncrementer += 2)
            {
                for (int j = 0; j <= 1; j++)
                {
                    doubleColumn = j == 0;

                    if (doubleColumn)
                    {
                        x = leftIncrementer + position.Column;
                        y = upIncrementer * 2 + position.Row;
                    }
                    else
                    {

                        x = leftIncrementer * 2 + position.Column;
                        y = upIncrementer + position.Row;
                    }

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
                            break;
                        }
                        else
                        { continue; }
                    }
                }
            }
        }
    }
}
