using Mirror;
using UnityEngine;

namespace Tirlim.Multiplayer
{
    public class HideFromOtherClients : NetworkBehaviour
    {
        public override void OnStartClient()
        {
            if (!isOwned)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
