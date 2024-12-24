using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _fallDamage;

    [SerializeField] private Rigidbody2D _rigidbody;

    public delegate void ShotPlayer(int _damage);
    public static ShotPlayer attack;
    public delegate void DestroyEnemy();
    public static DestroyEnemy DieEnemy;

    private void OnEnable()
    {
        ShootingControl.attackPlayer += ShotInPlayer;
    }
    private void OnDisable()
    {
        ShootingControl.attackPlayer -= ShotInPlayer;
    }
    public void SetParametrs(int damage, int fallDamage)
    {
        _damage=damage;
        _fallDamage=fallDamage;
        _rigidbody= GetComponent<Rigidbody2D>();
        FallEnemy();
    }
    public void ShotInPlayer()
    {
        attack?.Invoke(_damage);
    }
    public void FallEnemy()
    {
        _rigidbody.gravityScale=1.0f;
    }
    public void StopFall() 
    {
        _rigidbody.simulated = false;
        _rigidbody.linearVelocity = Vector2.zero;
        _rigidbody.gravityScale = 0.0f;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            StopFall();
            _rigidbody.simulated = true;
        }
        else if (collision.gameObject.layer == 8)
        {
            attack?.Invoke(_fallDamage);
            Dead();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            FallEnemy();
        }
    }

    public void Dead()
    {
        DieEnemy?.Invoke();
        Destroy(gameObject);
    }
}
