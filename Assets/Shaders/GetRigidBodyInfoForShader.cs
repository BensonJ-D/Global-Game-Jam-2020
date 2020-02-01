using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles object 'compression' on impacts by getting data from Rigidbody2D on collision (ignores triggers) and sends data
// to material via property block for updating per-object

public class GetRigidBodyInfoForShader : MonoBehaviour {

    public Rigidbody2D _rb;
    private MaterialPropertyBlock _matPropBlock;
    private Renderer _renderer;
    private Vector4 _collisionSurfaceNormal; // normal of surface that collider has impacted
    private ContactPoint2D[] _contactPoint2D; // stores information of collisions
    private float _resetAmount = 0.05f; // how much to bring vector gained from surface normal impact back to zero each step
    private float _collisionImpulseNormal; // represents force of impact on surface, used to scale effect

	void Start () {
        //_rb = GetComponent<Rigidbody2D>();
        _matPropBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
        _collisionSurfaceNormal = new Vector4(0, 0, 0, 0);
        _contactPoint2D = new ContactPoint2D[10];
    }
	
	void LateUpdate ()
    {
        if (IsMaterialUpdateRequired(_collisionSurfaceNormal.x, _collisionSurfaceNormal.y))
        {
            // update material with new data
            UpdateMaterialPropertyBlock();

            // gradually reset the shape if it's been deformed
            DecreaseVector();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetCollisionInformation();
    }

    private void UpdateMaterialPropertyBlock()
    {
        _renderer.GetPropertyBlock(_matPropBlock);
        _matPropBlock.SetVector("_CollisionNormal", _collisionSurfaceNormal);
        _matPropBlock.SetFloat("_CollisionNormalImpulse", _collisionImpulseNormal);
        _renderer.SetPropertyBlock(_matPropBlock);
    }

    // get direction and impulse force of first impact this frame
    private void GetCollisionInformation()
    {
        _rb.GetContacts(_contactPoint2D);
       
        _collisionSurfaceNormal = _contactPoint2D[0].normal;
        _collisionImpulseNormal = _contactPoint2D[0].normalImpulse;
    }

    // checks whether the collision normal is at 0 or not 
    // (i.e. there has just been a collision, or there was recently one and shape hasn't finished bouncing back to normal)
    private bool IsMaterialUpdateRequired(float x, float y)
    {
        bool xNot0 = !FastApproximately(x, 0f, 0.02f);
        bool yNot0 = !FastApproximately(y, 0f, 0.02f);

        return xNot0 || yNot0;
    }

    // bring amount back towards zero (whether above or below). If values are zero, does nothing
    private void DecreaseVector()
    {
        if (_collisionSurfaceNormal.x > 0f)
        {
            _collisionSurfaceNormal.x -= _resetAmount;
        }
        else
        {
            _collisionSurfaceNormal.x += _resetAmount;
        }

        if (_collisionSurfaceNormal.y > 0f)
        {
            _collisionSurfaceNormal.y -= _resetAmount;
        }
        else
        {
            _collisionSurfaceNormal.y += _resetAmount;
        }
    }

    // from the interwebs, like Mathf.Approximately but with a threshold
    public static bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }
}
