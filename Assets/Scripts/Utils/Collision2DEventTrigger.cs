using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers events when this Collider collides with another colliders. 
/// </summary>
public class Collision2DEventTrigger : MonoBehaviour
{
    [Space()]
    [Header( "You'll probably need a Rigidbody!" )]
    [Header( "Also, remember to check if the collider is trigger or not" )]
    [Space()]
    [Space()]

    [SerializeField] LayerMask _colisionLayers;
    [SerializeField] bool _isTriggerCollider = true;

    public UnityEvent<Vector3> _onCollisionEnter;
    public UnityEvent _onCollisionExit;
    public UnityEvent _onFirstCollisionEnter;
    public UnityEvent _onLastCollisionExit;
    public UnityEvent<int> _onCollisionCountChanged;

    List<Collider2D> _colliders;

    private void Awake()
    {
        _colliders = new List<Collider2D>();
    }

    void OnTriggerEnter2D( Collider2D collider2D )
    {
        if( !_isTriggerCollider )
            return;

        if( _colisionLayers == ( _colisionLayers | ( 1 << collider2D.gameObject.layer ) ) )
        {
            _onCollisionEnter.Invoke( collider2D.ClosestPoint( transform.position ) );

            _colliders.Add( collider2D );
            _onCollisionCountChanged.Invoke(_colliders.Count);
            
            if (_colliders.Count == 1)
                _onFirstCollisionEnter.Invoke();
        }
    }

    void OnTriggerExit2D( Collider2D collider2D )
    {
        if( !_isTriggerCollider )
            return;

        if( _colisionLayers == ( _colisionLayers | ( 1 << collider2D.gameObject.layer ) ) )
        {
            _onCollisionExit.Invoke();

            _colliders.Remove(collider2D);
            _onCollisionCountChanged.Invoke(_colliders.Count);

            if (_colliders.Count == 0)
                _onLastCollisionExit.Invoke();
        }
    }

    private void OnCollisionEnter2D( Collision2D collision )
    {
        if( _isTriggerCollider )
            return;

        if( _colisionLayers == ( _colisionLayers | ( 1 << collision.gameObject.layer ) ) )
        {
            _onCollisionEnter.Invoke( collision.GetContact( 0 ).point );

            _colliders.Add(collision.collider);
            _onCollisionCountChanged.Invoke(_colliders.Count);

            if (_colliders.Count == 1)
                _onFirstCollisionEnter.Invoke();
        }
    }

    private void OnCollisionExit2D( Collision2D collision )
    {
        if( _isTriggerCollider )
            return;

        if( _colisionLayers == ( _colisionLayers | ( 1 << collision.gameObject.layer ) ) )
        {
            _onCollisionExit.Invoke();

            _colliders.Remove(collision.collider);
            _onCollisionCountChanged.Invoke(_colliders.Count);

            if (_colliders.Count == 0)
                _onLastCollisionExit.Invoke();
        }
    }
}
