using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    public override void Initialize(Team _team, PieceType _pieceType = PieceType.king)
    {
        base.Initialize(_team, _pieceType);
    }

    public override void ShowPosibleMovement(int row, int column)
    {
        base.ShowPosibleMovement(row, column);
        possibleMovements.Clear();

        BoxController _box;
        int x;
        int y;

        for (int z = -1; z <= 1; z++)
        {
            for (int i = -1; i <= 1; i++)
            {
                x = z  + row;
                y = i  + column;

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
