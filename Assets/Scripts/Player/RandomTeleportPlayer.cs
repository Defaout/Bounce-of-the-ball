using UnityEngine;

public class RandomTeleportPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _positionForShot;
    [SerializeField] private int _playerPositon;

    public void StartRandomTeleport()
    {
        int newPosition = Random.Range(0, _positionForShot.Length);
        while (true)
        {
            if (newPosition == _playerPositon)
            {
                newPosition = Random.Range(0, _positionForShot.Length);
            }
            else
            {
                _playerPositon = newPosition;
                break;
            }
        }
        TeleportPlayer(newPosition);
    }
    private void TeleportPlayer(int index)
    {
        _player.transform.SetParent(_positionForShot[index].transform, false);
        _player.transform.position = _positionForShot[index].transform.position;

    }
}
