using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour, IInitializable, ISubscriber
{
    [SerializeField] private List<GameObject> _levelPrefabs = new List<GameObject>();
    private GameObject _level;
    public void Initialize()
    {
        BuildLevel();
    }

    public void SubscribeAll()
    {
        GameState.Instance.GameStarted += BuildLevel;
        GameState.Instance.ScoreAdded += RebuildLevel;
    }

    public void UnsubscribeAll()
    {
        GameState.Instance.GameStarted -= BuildLevel;
        GameState.Instance.ScoreAdded -= RebuildLevel;
    }

    private void BuildLevel()
    {
        if (_level != null)
            ClearLevel();
        _level = Instantiate(_levelPrefabs[GameState.Instance.LevelId]);
    }
    private void RebuildLevel()
    {
        ClearLevel();
        Invoke("BuildLevel", 1.5f);
    }
    private void ClearLevel()
    {
        Destroy(_level);
        _level = null;
    }
}
