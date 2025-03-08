using System;
using Mirror.Discovery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionServerItem : MonoBehaviour
{
    public event Action<ServerResponse> ConnectRequested;
        
    [SerializeField] private TextMeshProUGUI _nameServer;
    [SerializeField] private Button _connectButton;
    private ServerResponse _info;

    public void Init(ServerResponse info)
    {
        _info = info;
        _nameServer.text = $"{_info.uri.Host} : {_info.uri.Port}";
    }
        
    private void OnEnable()
    {
        _connectButton.onClick.AddListener(OnConnectButtonClick);
    }

    private void OnDisable()
    {
        _connectButton.onClick.RemoveListener(OnConnectButtonClick);
    }

    private void OnConnectButtonClick()
    {
        ConnectRequested?.Invoke(_info);
    }
}