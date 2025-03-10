using Tirlim.Match;
using Tirlim.Player;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    [SerializeField] private Team team;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            playerTrigger.SetImmortal(true, team);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            playerTrigger.SetImmortal(false, team);
        }
    }
}
