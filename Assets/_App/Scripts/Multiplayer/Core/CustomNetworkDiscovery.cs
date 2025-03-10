using System;
using System.Collections.Generic;
using Mirror.Discovery;

namespace Game.Network
{
    public class CustomNetworkDiscovery : NetworkDiscovery
    {
        public Action OnServerListUpdated;
        
        // Total потому что OnServerFound уже есть
        public Action<ServerResponse> OnServerTotalRemoved;
        public Action<ServerResponse> OnServerTotalFound;

        public Dictionary<long, ServerResponse> DiscoveredServers { get; private set; } =
            new Dictionary<long, ServerResponse>();

        private void OnEnable()
        {
            OnServerFound.AddListener(OnServerFoundHandler);
        }

        private void OnDisable()
        {
            OnServerFound.RemoveListener(OnServerFoundHandler);
        }
        public new void StartDiscovery()
        {
            foreach (ServerResponse info in DiscoveredServers.Values)
            {
                OnServerTotalRemoved?.Invoke(info);
            }
            DiscoveredServers.Clear();
            OnServerListUpdated?.Invoke();
            base.StartDiscovery();
        }
        
        private void OnServerFoundHandler(ServerResponse info)
        {
            if (DiscoveredServers.TryAdd(info.serverId, info))
            {
                OnServerListUpdated?.Invoke();
                OnServerTotalFound?.Invoke(info);
            }
        }
    }
}