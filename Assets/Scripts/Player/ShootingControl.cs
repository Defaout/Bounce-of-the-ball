using System.Collections;
using UnityEngine;

public class ShootingControl : MonoBehaviour
{
    [SerializeField] private Transform _shotPosition;// Точка стрельбы
    [SerializeField] private GameObject[] _magazine;// Массив для хранения патронов
    [SerializeField] private GameObject _ball;// Префаб патрона

    [SerializeField] private int _maxBullets;// Максимальное количество патронов
    [SerializeField] private float shootingDelay = 0.5f; // Задержка между выстрелами
    [SerializeField] private bool isShooting = false; // Флаг стрельбы

    [SerializeField] private RandomTeleportPlayer _RandomTeleportPlayer;

    private int _numberRoundsFired = 0;

    public delegate void EndAmmoInMagazineStartTeleport();
    public static EndAmmoInMagazineStartTeleport attackPlayer;

    public delegate void StartUpdateHUbAmmo();
    public static StartUpdateHUbAmmo Shooting;
    void Start()
    {
        // Инициализация массива патронов
        _magazine = new GameObject[_maxBullets];
        for (int i = 0; i < _maxBullets ; i++)
        {
            _magazine[i] = Instantiate(_ball);
            _magazine[i].SetActive(false); // Деактивируем патроны
        }
    }
    void Update()
    {
        // Проверяем, зажата ли левая кнопка мыши
        if (Input.GetMouseButton(0)&& _numberRoundsFired == 0 && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (!isShooting)
            {
                StartCoroutine(Shoot());
            }
        }
    }
    public int GetAmmo()
    {
        int ammo = _maxBullets - _numberRoundsFired;
        return ammo;
    }
    private IEnumerator Shoot()
    {
        isShooting = true;

        while (_numberRoundsFired<= _maxBullets)
        {
            // Находим первый неактивный патрон
            GameObject bullet = GetInactiveBullet();
            if (bullet != null&& _numberRoundsFired< _maxBullets)
            {
                // Позиция мыши в мировых координатах
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.y += 1;
                // Вычисляем вектор направления
                Vector2 direction = (mousePosition - _shotPosition.position).normalized;

                // Активируем патрон и устанавливаем его позицию и направление
                bullet.transform.position = _shotPosition.position;
                bullet.SetActive(true);
                bullet.GetComponent<MoveBall>().ShotBall(direction);

                _numberRoundsFired++;
                Shooting?.Invoke();
                // Ждем перед следующим выстрелом
                yield return new WaitForSeconds(shootingDelay);
            }
            else
            {
                if (AllPotholesWorthless())
                {
                    _RandomTeleportPlayer.StartRandomTeleport();
                    _numberRoundsFired = 0;
                    isShooting = false;
                    Shooting?.Invoke();
                    attackPlayer?.Invoke();
                    break;
                }
                yield return new WaitForSeconds(shootingDelay);
            }
        }
    }

    private GameObject GetInactiveBullet()
    {
        // Ищем неактивный патрон
        foreach (GameObject bullet in _magazine)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        return null; // Если все патроны активны
    }
    private bool AllPotholesWorthless()
    {
        // Ищем неактивный патрон
        foreach (GameObject bullet in _magazine)
        {
            if (bullet.activeSelf)
            {
                return false;
            }
        }
        return true; // Если все патроны активны
    }

}
