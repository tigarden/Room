using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private int _highlightIntensity = 4;    
    private Outline _outline;
    private Transform _itemOriginalParent;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform inventory)
    {
        _rigidbody.isKinematic = true;
        var currentTransform = transform;
        _itemOriginalParent = currentTransform.parent;
        currentTransform.SetParent(inventory);
        currentTransform.localPosition = Vector3.zero;
        currentTransform.localRotation = Quaternion.identity;
    }

    public void ThrowAway(Vector3 direction)
    {
        transform.SetParent(_itemOriginalParent);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction*500);
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }
}