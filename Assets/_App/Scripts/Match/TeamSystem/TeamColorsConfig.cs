using UnityEngine;

namespace Tirlim.Match
{
    [CreateAssetMenu(fileName = "TeamColorsConfig", menuName = "ScriptableObjects/TeamColorsConfig")]
    public class TeamColorsConfig : ScriptableObject
    {
        public Color ColorA { get { return colorA; } }
        public Color ColorB { get { return colorB; } }
        public Color ColorNeutral { get { return colorNeutral; } }

        [SerializeField] private Color colorA;
        [SerializeField] private Color colorB;
        [SerializeField] private Color colorNeutral;
    }

}