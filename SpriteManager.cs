using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager singleton;
    [SerializeField] Sprite[] sprites;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
    }

    public Sprite GetSprite(int id)
    {
        return sprites[id];
    }
}
