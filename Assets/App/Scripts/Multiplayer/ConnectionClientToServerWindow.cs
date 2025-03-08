using System.Collections;
using System.Collections.Generic;
using Game.Network;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConnectionClientToServerWindow : MonoBehaviour
{
        [Header("View")] 
        [SerializeField] private ConnectionServerItem _prefab;
        [SerializeField] private GameObject _rootItems;
        [SerializeField] private Button _refreshServersButton;
        
        private Dictionary<ServerResponse, ConnectionServerItem> _itemByServerResponse = new ();
        private NetworkManager _networkManager;
        private CustomNetworkDiscovery _networkDiscovery;
        
        [Inject]
        public void Construct(NetworkManager networkManager, CustomNetworkDiscovery networkDiscovery)
        {
            _networkManager = networkManager;
            _networkDiscovery = networkDiscovery;
        }
        

        protected virtual void OnEnable()
        {
            _networkDiscovery.StartDiscovery();
            OnServerListUpdated();
            _networkDiscovery.OnServerTotalFound += OnServerTotalFound;
            _networkDiscovery.OnServerTotalRemoved += OnServerTotalRemoved;
            _networkDiscovery.OnServerListUpdated += OnServerListUpdated;
            _refreshServersButton.onClick.AddListener(OnRefreshServerButtonClicked);
        }
        
        protected virtual void OnDisable()
        {
            _networkDiscovery.OnServerTotalFound -= OnServerTotalFound;
            _networkDiscovery.OnServerTotalRemoved -= OnServerTotalRemoved;
            _networkDiscovery.OnServerListUpdated -= OnServerListUpdated;
            _refreshServersButton.onClick.RemoveListener(OnRefreshServerButtonClicked);
        }

        private void OnServerTotalFound(ServerResponse info)
        {
            ConnectionServerItem connectionServerItem = Instantiate(_prefab, _rootItems.transform);
            connectionServerItem.Init(info);
            _itemByServerResponse.Add(info, connectionServerItem);
            connectionServerItem.ConnectRequested += OnConnectRequested;
        }

        private void OnServerTotalRemoved(ServerResponse info)
        {
            ConnectionServerItem connectionServerItem = _itemByServerResponse[info];
            connectionServerItem.ConnectRequested -= OnConnectRequested;
            Destroy(connectionServerItem.gameObject);
            _itemByServerResponse.Remove(info);
        }

        private void OnServerListUpdated()
        {
            bool isNoServers = _networkDiscovery.DiscoveredServers.Count == 0;
        }

        private void OnConnectRequested(ServerResponse info)
        {
            _networkDiscovery.StopDiscovery();

            _networkManager.StartClient(info.uri);
        }
        
        private void OnRefreshServerButtonClicked()
        {
            _networkDiscovery.StartDiscovery();
        }
}
