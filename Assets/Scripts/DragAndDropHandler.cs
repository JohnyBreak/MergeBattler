using System;
using System.Xml;
using UnityEngine;

public class DragAndDropHandler : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    [SerializeField] private MergeSystem _mergeSystem;

    private Camera _camera;
    private bool _isSwiping;
    private Vector3 _previousPosition;
    private Entity _selectedEntity;
    private Entity _mergeEntity;
    private Plane _plane;

    private void Awake()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        _camera = Camera.main;
    }

    
    private void Update()
    {
        ProcessClickDown();
        ProcessClickUp();
        ProcessSwipe();
    }

    private void ProcessClickDown()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton) == false)
        {
            return;
        }

        _isSwiping = true;
        _previousPosition = Input.mousePosition;

        if (Physics.Raycast(GetRay(_previousPosition), out RaycastHit hit)) 
        {
            if (hit.collider.TryGetComponent(out Entity entity)) 
            {
                _selectedEntity = entity;
                _selectedEntity.OnStartDrag();
            }
        }

    }

    private void ProcessClickUp()
    {
        if (Input.GetMouseButtonUp(LeftMouseButton) == false) 
        {
            return;
        }

        if (_selectedEntity == null) 
        {
            return;
        }

        _selectedEntity.OnStopDrag();

        if (_mergeEntity != null)
        {
            _mergeSystem.Merge(_selectedEntity, _mergeEntity);
        }
        else 
        {
            Physics.Raycast(_selectedEntity.transform.position, -Vector3.up, out RaycastHit hit);

            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent(out FormationPoint point)) 
                {
                    _selectedEntity.SetPosition(point.GetPosition());

                    _mergeEntity = null;
                    _selectedEntity = null;
                    _isSwiping = false;

                    return;
                }
            }

            _selectedEntity.ReturnToPosition();
        }

        _mergeEntity = null;
        _selectedEntity = null;
        _isSwiping = false;
    }

    private void ProcessSwipe()
    {
        if (_isSwiping == false) 
        {
            return;
        }

        if (_previousPosition == Input.mousePosition) 
        {
            return;
        }

        if (_selectedEntity == null) 
        {
            return;
        }

        _previousPosition = Input.mousePosition;

        Ray touchRay = GetRay(_previousPosition);

        if (_plane.Raycast(touchRay, out float hitDistance)) 
        {
            Vector3 point = touchRay.GetPoint(hitDistance);
            _selectedEntity.transform.position = point;
        }

        if (Physics.Raycast(touchRay, out RaycastHit hit)) 
        {
            if (hit.collider.TryGetComponent(out Entity entity))
            {
                if (entity != _selectedEntity && entity != _mergeEntity) 
                {
                    _mergeEntity = entity;
                }
            }
            else 
            {
                _mergeEntity = null;
            }
        }
    }

    private Ray GetRay(Vector3 position) 
    {
        return _camera.ScreenPointToRay(position);
    }
}
