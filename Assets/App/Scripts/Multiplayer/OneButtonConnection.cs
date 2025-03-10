using System.Collections;
using Game.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Mirror;
using Mirror.Discovery;

public class OneButtonConnection : MonoBehaviour
{
    [SerializeField] private Button connectButton;
    [SerializeField] private TextMeshProUGUI statusText;
    // Таймаут поиска сервера (в секундах)
    [SerializeField] private float connectionTimeout = 2.0f;

    private CustomNetworkManager _networkManager;
    private CustomNetworkDiscovery _networkDiscovery;
    private bool _isConnecting = false;
    private bool _serverFound = false;

    [Inject]
    public void Construct(CustomNetworkManager networkManager, CustomNetworkDiscovery networkDiscovery)
    {
        _networkManager = networkManager;
        _networkDiscovery = networkDiscovery;
        NetworkClient.OnConnectedEvent += OnClientConnected;
        NetworkClient.OnDisconnectedEvent += OnClientDisconnected;
    }

    private void Start()
    {
        if (connectButton != null)
            connectButton.onClick.AddListener(OnConnectButtonClick);
        else
            Debug.LogError("Не назначена кнопка подключения!");
    }

    private void OnDestroy()
    {
        NetworkClient.OnConnectedEvent -= OnClientConnected;
        NetworkClient.OnDisconnectedEvent -= OnClientDisconnected;

        if (_networkDiscovery != null)
        {
            _networkDiscovery.OnServerTotalFound -= OnDiscoveredServer;
        }
    }

    public void OnConnectButtonClick()
    {
        if (_isConnecting)
            return;

        _isConnecting = true;
        _serverFound = false;
        if (statusText != null)
            statusText.text = "Поиск сервера...";

        if (_networkDiscovery != null)
        {
            _networkDiscovery.OnServerTotalFound += OnDiscoveredServer;
            _networkDiscovery.StartDiscovery();
            StartCoroutine(DiscoverOrHost());
        }
        else
        {
            Debug.LogError("Компонент NetworkDiscovery не назначен!");
            _isConnecting = false;
        }
    }

    private IEnumerator DiscoverOrHost()
    {
        float timer = 0f;
        // Ждем, пока не обнаружим сервер или не истечёт timeout
        while (timer < connectionTimeout && !_serverFound)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        
        if (!_serverFound & _networkDiscovery.DiscoveredServers.Count == 0)
        {
            if (statusText != null)
                statusText.text = "Сервер не найден, создаю хост...";
            Debug.Log("Сервер не найден. Запускаю хост...");
            _networkDiscovery.StopDiscovery();
            _networkDiscovery.AdvertiseServer();
            _networkManager.StartHost();
            if (statusText != null)
                statusText.text = "Хост запущен!";
        }
        _isConnecting = false;
    }

    // Метод должен иметь именно такую сигнатуру, чтобы корректно приниматься UnityEvent<ServerResponse>.
    private void OnDiscoveredServer(ServerResponse response)
    {
        if (_serverFound)
            return;

        _serverFound = true;
        StartCoroutine(ConnectToServer(response));
    }
    
    private IEnumerator ConnectToServer(ServerResponse response)
    {
        // Ждем один кадр, чтобы безопасно завершить обнаружение
        yield return null;
        _networkDiscovery.StopDiscovery();
        // Используем адрес, полученный от обнаруженного сервера
        _networkManager.networkAddress = response.uri.Host;
        if (statusText != null)
            statusText.text = "Найден сервер, подключаюсь...";
        Debug.Log("Найден сервер: " + response.uri);
        _networkManager.StartClient();
    }

    private void OnClientConnected()
    {
        Debug.Log("Подключен к серверу");
        if (statusText != null)
            statusText.text = "Подключен к серверу";
    }

    private void OnClientDisconnected()
    {
        Debug.Log("Отключен от сервера");
        if (statusText != null)
            statusText.text = "Отключен от сервера";
    }
}
