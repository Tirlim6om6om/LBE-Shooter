using Tirlim.Player;
using UnityEngine;

public class SaveZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            playerTrigger.PlayerHealthSystem.SetImmortal(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerTrigger playerTrigger))
        {
            playerTrigger.PlayerHealthSystem.SetImmortal(false);
        }
    }
}
