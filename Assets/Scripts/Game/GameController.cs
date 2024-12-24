using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    [SerializeField] private int _playerHP;
    [SerializeField] private int _damageEnemy;

    [SerializeField] private GameObject _lose;
    [SerializeField] private GameObject _win;

    [SerializeField] private int _numEnemy;

    public delegate void Health();
    public static Health playerHP;


    private void OnEnable()
    {
        EnemyTurret.attack += DamagePlayer;
        EnemyTurret.DieEnemy += EnemiDead;
    }
    private void OnDisable()
    {
        EnemyTurret.attack -= DamagePlayer;
        EnemyTurret.DieEnemy -= EnemiDead;
    }
    public void SetNumEnimy(int num)
    {
        _numEnemy=num;
    }
    public void EnemiDead()
    {
        _numEnemy--;
        if( _numEnemy == 0 )
        {
            _win.SetActive(true);
        }
    }
    public int GetHP()
    {
        return _playerHP;
    }
    public int GetDamage()
    {
        return _damageEnemy;
    }
    public void DamagePlayer(int damage)
    {
        if(_playerHP-damage > 0)
        {
            _playerHP-=damage;
            playerHP?.Invoke();
        }
        else
        {
            _lose.SetActive(true);
        }
    }
}
