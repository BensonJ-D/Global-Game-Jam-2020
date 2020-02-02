using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbsHandler : MonoBehaviour
{
    Transform weapon;

    private void Start()
    {
        weapon = transform.GetChild(0);
    }

    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        Transform otherTransform = other.transform;

        if (other.tag == "Weapon" && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Attach!");

            otherTransform.SetParent(transform);
            other.GetComponent<activateArm>().enabled = true;
            otherTransform.localPosition = new Vector2(0.5f, 0);

            HingeJoint2D hinge = transform.GetComponent<HingeJoint2D>();
            hinge.connectedBody = other.GetComponent<Rigidbody2D>();
            hinge.autoConfigureConnectedAnchor = true;
            hinge.enabled = true;

            transform.GetComponent<activateArm>().enabled = false;

            weapon = transform.GetChild(0);
        }
    }

    public void DropWeapon()
    {
        if (weapon != null)
        {
            transform.GetComponent<HingeJoint2D>().enabled = false;

            weapon.SetParent(null);

            int layer = LayerMask.NameToLayer("Default");
            weapon.gameObject.layer = layer;
            SetLayerRecursively(weapon.gameObject, layer);

            weapon = null;
        }
    }

    public bool HasWeapon()
    {
        return weapon != null;
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
