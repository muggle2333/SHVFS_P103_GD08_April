using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectComponent : MonoBehaviour
{
    private GameObject objectContainer;
    private void Start()
    {
        objectContainer = GameObject.FindObjectOfType<ObjectContainer>().gameObject;
    }
    public void DestroyObject()
    {
         GameObject.Destroy(gameObject);
    }
    public void ChangeOwner(Transform owner)
    {
        //transform.parent = owner;
        //transform.localPosition = new Vector3(0,0, 0);
        transform.SetParent(owner,false);
    }
    //put the object near the owner but on the ground
    public void PutDownObject(Transform owner)
    {
        transform.SetParent(objectContainer.transform, true);
        Vector3 tempPos = new Vector3(owner.position.x, 0, owner.position.z) - owner.forward;
        transform.position = tempPos;
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
    }
}
