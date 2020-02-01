using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimbsHandler : MonoBehaviour
{
    Transform weapon;

    public Vector2 oldVelocity;


    void Update()
    {
        weapon = transform.Find("Weapon");

        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.GetComponent<HingeJoint2D>().enabled = false;
            transform.GetComponent<activateArm>().enabled = true;

            weapon.SetParent(null);
            weapon.GetComponent<activateArm>().enabled = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        Transform otherTransform = other.transform;
        Debug.Log("Contact with Weapon!");

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
        }
    }
}
