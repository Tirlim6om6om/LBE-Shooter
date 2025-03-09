using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Zenject;

namespace Tirlim.Match
{
    public struct PlayerInfo
    {
        public NetworkConnectionToClient Connection;
        public PlayerTeam PlayerTeam;

        public PlayerInfo(NetworkConnectionToClient connection, PlayerTeam playerTeam)
        {
            Connection = connection;
            PlayerTeam = playerTeam;
        }
    }
    
    public class TeamSystem : MonoBehaviour
    {
        private CustomNetworkManager _networkManager;
        private List<PlayerInfo> _playersTeamA = new List<PlayerInfo>();
        private List<PlayerInfo> _playersTeamB = new List<PlayerInfo>();

        [Inject]
        public void Construct(CustomNetworkManager networkManager)
        {
            _networkManager = networkManager;
            _networkManager.OnPlayerJoin += AddPlayerToTeam;
        }
        
        public void AddPlayerToTeam(NetworkConnectionToClient conn)
        {
            PlayerTeam player = conn.identity.gameObject.GetComponent<PlayerTeam>();
            
            if (_playersTeamA.Count <= _playersTeamB.Count)
            {
                _playersTeamA.Add(new PlayerInfo(conn, player));
                player.SetTeam(Team.teamA);
                Debug.Log("Add " + conn.connectionId + " to A");
            }
            else
            {
                _playersTeamB.Add(new PlayerInfo(conn, player));
                player.SetTeam(Team.teamB);
                Debug.Log("Add " + conn.connectionId + " to B");
            }
        }
    }

}