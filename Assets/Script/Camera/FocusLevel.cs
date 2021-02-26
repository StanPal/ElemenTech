using UnityEngine;

public class FocusLevel : MonoBehaviour
{
    [SerializeField] private float _halfXBounds = 20f;
    [SerializeField] private float _halfYBounds = 15f;
    [SerializeField] private float  _halfZBounds = 15f;
    private Bounds _focusBounds;

    public float HalfXBounds { get => _halfXBounds; }
    public float HalfYBounds { get => _halfYBounds; }
    public float HalfZBounds { get => _halfZBounds; }
    public Bounds FocusBounds { get => _focusBounds; }

    private void Update()
    {
        Vector3 position = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(position.x - _halfXBounds, position.y - _halfYBounds, position.z - _halfZBounds));
        bounds.Encapsulate(new Vector3(position.x - _halfXBounds, position.y + _halfYBounds, position.z - _halfZBounds));
        _focusBounds = bounds;
    }
}
