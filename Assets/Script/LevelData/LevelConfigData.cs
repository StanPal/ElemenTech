using UnityEngine;

[CreateAssetMenu(menuName ="LevelConfigData")]
public class LevelConfigData : ScriptableObject
{
    [SerializeField] private string _levelTheme = null;
    [SerializeField] private float _gravity = 0.0f;
    [SerializeField] private float _minJumpDist = 0.0f;

    public float Gravity { get { return _gravity; } }
    public float MinJumpDist { get { return _minJumpDist; } }
}
