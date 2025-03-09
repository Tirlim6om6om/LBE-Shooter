using System;
using UnityEngine;
using Zenject;

namespace Tirlim.Match
{
    public class TeamSkin : MonoBehaviour
    {
        [SerializeField] private Renderer[] renderers;

        private TeamColorsConfig _teamColorsConfig;
        private PlayerTeam _playerTeam;

        [Inject]
        public void Construct(PlayerTeam playerTeam, TeamColorsConfig teamColorsConfig)
        {
            _playerTeam = playerTeam;
            _teamColorsConfig = teamColorsConfig;
            SetTeamColor(playerTeam.CurrentTeam);
            _playerTeam.OnSwitchTeam += SetTeamColor;
        }

        private void OnDestroy()
        {
            _playerTeam.OnSwitchTeam -= SetTeamColor;
        }

        private void SetTeamColor(Team team)
        {
            Color color = GetTeamColor(team);

            foreach (var renderer in renderers)
            {
                renderer.material.SetColor("_TeamColor", color);
            }
        }

        private Color GetTeamColor(Team team)
        {
            switch (team)
            {
                case Team.teamA:
                    return _teamColorsConfig.ColorA;
                case Team.teamB:
                    return _teamColorsConfig.ColorB;
                case Team.neutral:
                    return _teamColorsConfig.ColorNeutral;
            }
            return Color.white;
        }
    }
}
