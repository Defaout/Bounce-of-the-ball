using UnityEngine;

public class UIStart : MonoBehaviour
{
    [SerializeField] private LoadJson _loadJson;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _hudPanel;
    public void StartGame()
    {
        _loadJson.StartLoad();
        _startPanel.SetActive(false);
        _hudPanel.SetActive(_startPanel);
    }
}
