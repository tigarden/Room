using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Camera _mainCamera;
    private InteractableItem _lastHighlightedItem;

    private Transform _lastItemOriginParent;
    private InteractableItem _lastPickedItem;
    private bool _hasPickedItem;

    //private bool _isFocusOnItem = false;
    [SerializeField] private GameObject _playerInventory;

    // Start is called before the first frame update
    private void Awake()
    {
        //_mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        HighlightInteractableItem();

        if (Input.GetKeyDown(GlobalConstants.INTERACTION_KEY))
        {
            var door = Methods.GetSelectedInteractableItem<Door>();
            var interactableObject = Methods.GetSelectedInteractableItem<InteractableItem>();
            
            if (door)
            {
                door.SwitchDoorState();
            }

            if (!_hasPickedItem && interactableObject)
            {
                if (_lastPickedItem != interactableObject)
                {   
                    interactableObject.PickUp(_playerInventory.transform);
                    _lastPickedItem = interactableObject;
                    _hasPickedItem = true;
                    return;
                }
            }

            if (_hasPickedItem)
            {
                _lastPickedItem!.ThrowAway(transform.forward);
                _lastPickedItem = null;
                _hasPickedItem = false;
            }
        }
        // var ray = new Ray(_mainCamera.transform.position, transform.forward);
        // if (Physics.Raycast(ray, out var raycastHit,2f))
        // {
        //     if (raycastHit.collider.gameObject.TryGetComponent<Door>(out var door))
        //     {
        //         if (Input.GetKeyDown(GlobalConstants.INTERACTION_KEY))
        //         {door.SwitchDoorState();}
        //     }
        //     
        //     if (raycastHit.collider.gameObject.TryGetComponent<InteractableItem>(out var item))
        //     {
        //         _highlightedItem = item;
        //         _itemOriginParent = _highlightedItem.gameObject.transform.parent;
        //         _highlightedItem.SetFocus();
        //         _isFocusOnItem = true;
        //         if (Input.GetKeyDown(GlobalConstants.INTERACTION_KEY))
        //         {
        //             GameObject o;
        //             _highlightedItem.gameObject.transform.SetParent(_inventory.transform);
        //             _highlightedItem.gameObject.transform.position = _inventory.transform.position;
        //             _highlightedItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //             _highlightedItem.RemoveFocus();
        //         }
        //     }
        //
        //     
        // }
        // else
        // {
        //     if (_isFocusOnItem)
        //     {
        //         _highlightedItem.RemoveFocus();
        //         _isFocusOnItem = false;
        //     }
        //     
        // }
        // if (_inventory.transform.childCount > 0)
        // {
        //     if (Input.GetKeyDown(KeyCode.R))
        //     {
        //         _highlightedItem.GetComponent<Rigidbody>().isKinematic = false;
        //         _highlightedItem.GetComponent<Rigidbody>().AddForce(0,0,1);
        //         _highlightedItem.transform.SetParent(_itemOriginParent);
        //     }
        // }
    }

    private void HighlightInteractableItem()
    {
        var interactableObject = Methods.GetSelectedInteractableItem<InteractableItem>();

        if (_lastHighlightedItem != interactableObject)
        {
            if (_lastHighlightedItem != null)
            {
                _lastHighlightedItem.RemoveFocus();
            }

            if (interactableObject != null)
            {
                interactableObject.SetFocus();
            }
        }

        _lastHighlightedItem = interactableObject;
    }
}