using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance;

    public SpriteRenderer[,] _backgrounds = new SpriteRenderer[3, 3];

    [SerializeField] private List<SpriteRenderer> _backgroundsList = new List<SpriteRenderer>();

    [SerializeField] private List<SpriteRenderer> _availableBackgrounds = new List<SpriteRenderer>();

    private SpriteRenderer _piece;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SetCenter(SpriteRenderer sprite)
    {
        _piece = sprite;
        Invoke("Set", 0.3f);
    }
    private void Set()
    {
        _backgrounds = new SpriteRenderer[3, 3];
        _backgrounds[1, 1] = _piece;

        _availableBackgrounds.Clear();

        foreach (SpriteRenderer sprite in _backgroundsList)
        {
            if (sprite != _piece)
                _availableBackgrounds.Add(sprite);
        }

        for (int row = 0; row < _backgrounds.GetLength(0); row++)
        {
            for (int col = 0; col < _backgrounds.GetLength(1); col++)
            {
                if (row == 1 && col == 1)
                {

                }
                else
                {
                    _backgrounds[row, col] = GetRandomAvailableSprite();
                    _availableBackgrounds.Remove(_backgrounds[row, col]);

                    SetBackgroundPosition(row, col);
                }
            }
        }
    }
    private void SetBackgroundPosition(int row, int col)
    {
        Vector2 position = _backgrounds[1,1].transform.position;

        switch (row)
        {
            case 0:
                position += new Vector2(0, _backgrounds[1, 1].bounds.size.y);
                break;
            case 2:
                position -= new Vector2(0, _backgrounds[1, 1].bounds.size.y);
                break;
        }
        switch (col)
        {
            case 0:
                position -= new Vector2(_backgrounds[1, 1].bounds.size.x, 0);
                break;
            case 2:
                position += new Vector2(_backgrounds[1, 1].bounds.size.x, 0);
                break;
        }
        _backgrounds[row, col].transform.position = position;
    }
    private SpriteRenderer GetRandomAvailableSprite()
    {
        return _availableBackgrounds[Random.Range(0, _availableBackgrounds.Count)];
    }
}
