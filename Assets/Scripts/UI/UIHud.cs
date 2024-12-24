using TMPro;
using UnityEngine;

public class UIHud : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private GameController _gameController;
    [SerializeField] private ShootingControl _shootingControl;

    private void OnEnable()
    {
        GameController.playerHP += UpdateHP;
        ShootingControl.Shooting += UpdateAmmo;
    }
    private void OnDisable()
    {
        GameController.playerHP -= UpdateHP;
        ShootingControl.Shooting -= UpdateAmmo;
    }
    public void UpdateHP()
    {

        _hp.text=_gameController.GetHP().ToString();
    }
    public void UpdateAmmo()
    {
        _ammo.text = _shootingControl.GetAmmo().ToString();
    }
}
