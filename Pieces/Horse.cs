using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.horse)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void ShowPosibleMovement(int column, int row)
    {
        base.ShowPosibleMovement(column, row);
        possibleMovements.Clear();

        BoxController _box;
        int x;
        int y;
        bool doubleColumn;

        for (int z = -1; z <= 1; z += 2)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = 0; j <= 1; j++)
                {
                    doubleColumn = j == 0;

                    if (doubleColumn)
                    {
                        x = z + column;
                        y = i * 2 + row;
                    }
                    else
                    {

                        x = z * 2 + column;
                        y = i + row;
                    }

                    //Table limits
                    if (x < 0 || y < 0 || x > 7 || y > 7) continue;

                    _box = TableController.instance.GetBox(x, y);

                    if (_box)
                    {
                        if (!_box.HasPiece())
                        {
                            AddPosibleMovement(_box);
                        }
                        else if (_box.GetPiece().GetTeam() != myTeam)
                        {
                            AddPosibleMovement(_box);
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
