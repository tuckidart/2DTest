using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    protected bool _hovering = false;
    public bool IsHovering => _hovering;

    public virtual void OnMouseEnter()
    {
        _hovering = true;
    }

    public virtual void OnMouseExit()
    {
        _hovering = false;
    }

    public virtual void OnMouseDown() { }
    public virtual void OnMouseUp() { }
}
