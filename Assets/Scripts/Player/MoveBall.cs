using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    [SerializeField] private int _startForce;
    [SerializeField] private float _bounceForce ;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _bounceCount;
    [SerializeField] private int _maxBounces;
    public void ShotBall(Vector2 normal)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(new Vector2(normal.x * _startForce, normal.y * _startForce), ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Bounce();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Bounce();
        }
        if(collision.gameObject.layer == 8)
        {
            DeadBall();
        }
    }
    private void Bounce()
    {
        if (_bounceCount < _maxBounces)
        {
           
            _bounceCount++;
        }
        else
        {
            DeadBall();
        }
    }
    private void DeadBall()
    {
        _bounceCount = 0;
        _rigidbody.linearVelocity = Vector2.zero;
        _self.SetActive(false);
    }
}
