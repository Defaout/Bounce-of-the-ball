using TMPro;
using UnityEngine;

public class TuchBlock : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private EnemyTurret _enemyTurret;
    [SerializeField] private GameObject _self;

    [SerializeField] private int _idBlock;
    [SerializeField] private int _armor;
    [SerializeField] private int _helth;
    [SerializeField] private int _damageToArmor;

    [SerializeField] private TMP_Text _uiHP;
    public void OnSpawn(Block blockSelf,GameController gameController, EnemyTurret enemyTurret)
    {
        _idBlock = blockSelf._id;
        _armor = blockSelf._armor;
        _helth = blockSelf._hp;
        _gameController= gameController;
        _enemyTurret=enemyTurret;
        UpdateUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Damage(_gameController.GetDamage());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {

            Damage(_gameController.GetDamage());
        }
    }
    private void Damage(int damage)
    {
        if (_armor > 0)
        {
            DamageArrmor();
        }
        else
        {
            DamageHeath(damage);
        }
    }
    private void DamageHeath(int damage)
    {
        if (_helth - damage > 0)
        {
            _helth -= damage;
            UpdateUI();
        }
        else
        {
            if (_enemyTurret != null)
            {
                _enemyTurret.Dead();
            }
            else
            {
                Destroy(_self);
            }
        }
    }
    private void DamageArrmor()
    {
        _armor -= _damageToArmor;
        UpdateUI();
    }
    private void UpdateUI()
    {
        string uiHP;
        if (_armor > 0)
        {
            uiHP = _armor.ToString() + "(" + _helth.ToString() + ")";
        }
        else
        {
            uiHP = "(" + _helth.ToString() + ")";
        }
        _uiHP.text = uiHP;
    }
}
