using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    int row;
    int column;

    Piece piece;

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


        ChangeColor(selectedColor);
        SetPosition(_row, _column);
    }

    public void ChangeColor(Color color)
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
    }

    public bool HasPiece()
    {
        return piece != null;
    }
}
