
using UnityEngine;

public class ChildBehavior : MonoBehaviour
{
    protected virtual void ResetValue()
    {
        
    }

    protected virtual void LoadComponentInParent()
    {
        
    }

    protected virtual void LoadComponentInIt()
    {
        
    }

    protected virtual void LoadComponentInChild()
    {
        
    }

    protected virtual void LoadComponent()
    {
        LoadComponentInParent();
        LoadComponentInIt();
        LoadComponentInChild();
    }

    protected void Reset()
    {
        LoadComponent();
        ResetValue();
    }

    protected virtual void Start()
    {
        LoadComponent();
        ResetValue();
    }

    protected GameObject AddChildTransform(string transformName)
    {
        GameObject childObject = new GameObject(transformName)
        {
            transform = 
            {
                parent = this.transform,
                localPosition = Vector3.zero,
                localRotation = Quaternion.identity
            }
        };
        return childObject;
    }

    protected T LoadComponent<T>(T comp, string childName = null, bool searchInParent = false) where T : Component
    {
        if (comp == null)
        {
            comp = searchInParent && transform.parent != null 
                ? transform.parent.GetComponent<T>() ?? transform.parent.gameObject.AddComponent<T>() 
                : childName != null 
                    ? transform.GetComponentInChildren<T>() ?? AddChildTransform(childName).AddComponent<T>() 
                    : transform.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }
        return comp;
    }


}
