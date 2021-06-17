using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] protected Team myTeam;
    [SerializeField] PieceType pieceType;

    protected List<BoxController> possibleMovements = new List<BoxController>();

    public enum Team { White , Black }

    public enum PieceType { pawn, bishop, horse, tower, queen, king }

    public virtual void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        myTeam = _team;
        pieceType = _pieceType;

        SetImage();

        GameManager.singleton.AddPiece(this);
    }

    void SetImage()
    {
        if (TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.sprite = SpriteManager.singleton.GetSprite(((int)myTeam * 6) + (int)pieceType);
        }
    }

    public Team GetTeam()
    {
        return myTeam;
    }

    public virtual void ShowPosibleMovement(int row, int column) { }

    protected void AddPosibleMovement(BoxController _box)
    {
        possibleMovements.Add(_box);
        _box.ChangePossibleMovement();
    }

    public virtual void HidePosibleMovement()
    {
        for (int i = 0; i < possibleMovements.Count; i++)
        {
            possibleMovements[i].DeselectBox();
        }
    }

    public virtual bool TryMove(BoxController _box)
    {
        if (possibleMovements.Contains(_box))
        {
            possibleMovements.Clear();
            return true;
        }
        else
            return false;
    }
}
