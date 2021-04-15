using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] Team myTeam;
    [SerializeField] PieceType pieceType;

    public enum Team { black, white}

    public enum PieceType { pawn, bishop, horse, tower, queen, king}

    public virtual void Initialize(Team _team, PieceType _pieceType = PieceType.pawn)
    {
        myTeam = _team;
        pieceType = _pieceType;

        SetImage();
    }

    void SetImage()
    {
        if(TryGetComponent(out SpriteRenderer renderer))
        {
            renderer.sprite = SpriteManager.singleton.GetSprite(((int)myTeam * 6) + (int)pieceType); 
        }
    }
}
