using UnityEngine;

public class Entity : MonoBehaviour
{
    public int Level;

    private Collider _collider;

    private Vector3 _oldPosition;


    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void OnStartDrag() 
    {
        _oldPosition = transform.position;
        transform.localScale *= 1.15f;
        _collider.enabled = false;
    }

    public void OnStopDrag()
    {
        transform.localScale /= 1.15f;
        _collider.enabled = true;
    }

    public void ReturnToPosition()
    {
        transform.position = _oldPosition;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
