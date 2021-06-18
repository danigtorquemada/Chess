using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.bishop)
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

        for (int z = -1; z <= 1; z += 2)
        {
            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = 1; j < 8; j++)
                {
                    x = z * j + column;
                    y = i * j + row;

                    //Table limits
                    if (x < 0 || y < 0 || x > 7 || y > 7) break;

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
                        { break; }
                    }
                }
            }
        }
    }
}
