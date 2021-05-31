using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    bool bFirstMovement = true;

    public override void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        Debug.Log("HOLA");
        base.Initialize(_team, _pieceType);
    }

    public override void ShowPosibleMovement(int row, int column)
    {
        base.ShowPosibleMovement(row, column);
        possibleMovements.Clear();

        int multiplierTeam = (myTeam == Team.black ? 1 : -1);
        BoxController _box = TableController.instance.GetBox(row, column + multiplierTeam);

        if (_box && !_box.HasPiece())
        {
            AddPosibleMovement(_box);
        }

        if (bFirstMovement && !_box.HasPiece())
        {
            _box = TableController.instance.GetBox(row, column + multiplierTeam * 2);
            if (_box && !_box.HasPiece())
            {
                AddPosibleMovement(_box);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            int x = i == 0 ? -1 : 1;

            _box = TableController.instance.GetBox(row + x, column + multiplierTeam);

            if (_box  && _box.HasPiece() && _box.GetPiece().GetTeam() != myTeam)
            {
                AddPosibleMovement(_box);
            }
        }
    }

    public override bool TryMove(BoxController _box)
    {
        if (base.TryMove(_box))
        {
            bFirstMovement = false;
            return true;
        }
        else
            return false;
    }
}
