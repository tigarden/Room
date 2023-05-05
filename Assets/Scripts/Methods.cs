using UnityEngine;

public static class Methods
{
    public static T GetSelectedInteractableItem<T>() where T : MonoBehaviour
    {
        var mousePosition = Input.mousePosition;
        var ray = Camera.main!.ScreenPointToRay(mousePosition);
        if (!Physics.Raycast(ray, out var hitInfo)) return null;
        var hitObject = hitInfo.collider.gameObject.GetComponent<T>();
        return hitObject;
    }
}