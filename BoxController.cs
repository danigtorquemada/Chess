using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    int row;
    int column;

    [SerializeField] Piece piece;

    [SerializeField] Color baseColor;
    [SerializeField] Color selectedColor;
    [SerializeField] Color possibleMovementColor;

    MaterialPropertyBlock propertyBlock;
    Renderer renderer;
    public void Initialize(Color _color, int _row, int _column)
    {
        bool b = TryGetComponent(out renderer);
        propertyBlock = new MaterialPropertyBlock();

        baseColor = _color;
        possibleMovementColor *= _color;
        selectedColor *= _color;


        ChangeColor(baseColor);
        SetPosition(_row, _column);
    }

    void ChangeColor(Color color)
    {
        propertyBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(propertyBlock);
    }

    public void SetPosition(int _row, int _column)
    {
        row = _row;
        column = _column;
    }

    public void SetPiece(Piece _piece)
    {
        piece = _piece;
        if (piece != null)
            piece.transform.position = transform.position;
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
            piece.ShowPosibleMovement(row, column);
    }

    public void DeselectBox(bool firstSelected = true)
    {
        ChangeColor(baseColor);
        if (!firstSelected && piece != null)
            piece.HidePosibleMovement();
    }
}
