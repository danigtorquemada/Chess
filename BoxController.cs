using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public Position position;

    [SerializeField] Piece piece;

    [SerializeField] Color baseColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Color possibleMovementColor;

    MaterialPropertyBlock propertyBlock;
    Renderer renderer;
    public void Initialize(Color _color, int _column, int _row)
    {
        bool b = TryGetComponent(out renderer);
        propertyBlock = new MaterialPropertyBlock();

        baseColor = _color;
        possibleMovementColor *= _color;
        selectedColor *= _color;


        ChangeColor(baseColor);
        SetPosition(_column, _row);
    }

    void ChangeColor(Color color)
    {
        propertyBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetPosition(int _column, int _row)
    {
        position = new Position(_column, _row);
    }

    public void SetPiece(Piece _piece)
    {
        piece = _piece;
        if (piece != null)
        {
            piece.transform.position = transform.position;
            piece.SetPosition(position.Column, position.Row);
        }
    }

    public bool HasPiece(out Piece currentPiece)
    {
        currentPiece = piece;

        return piece != null;
    }
    public bool HasPiece()
    {
        return piece != null;
    }

    public Piece GetPiece()
    {
        return piece;
    }

    public void ChangePossibleMovement()
    {
        ChangeColor(possibleMovementColor);
    }

    public void SelectBox(int turn)
    {
        ChangeColor(selectedColor);

        if (piece != null && turn == (int)piece.GetTeam())
            piece.SelectPiece(this);
    }

    public void DeselectBox(bool firstSelected = true)
    {
        ChangeColor(baseColor);
        if (!firstSelected && piece != null)
            piece.HidePosibleMovement();
    }
}
