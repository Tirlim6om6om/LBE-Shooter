using System;
using Mirror;
using Tirlim.Gun;
using UnityEngine;

namespace Tirlim.Player
{
    public enum PlayerState
    {
        Alive,
        Dead,
        Immortal,
    }
    
    public class PlayerHealthSystem : MonoBehaviour
    {
        public int Health { get { return _health; } }
        public event Action<PlayerState> OnSwitchState;
        public event Action<int> OnChangeHealth;
    
        private PlayerState _currentState;
        private int _health;

        private void Start()
        {
            Regeneration();
        }

        public void SetDamage(int damage)
        {
            if (_currentState == PlayerState.Dead | _currentState == PlayerState.Immortal)
                return;
            
            if (_health - damage <= 0)
            {
                Dead();
            }
            else
            {
                SetHealth(_health - damage);
            }
        }
    
        public void SetImmortal(bool active)
        {
            if (active)
            {
                SwitchState(PlayerState.Immortal);
            }
            else
            {
                Regeneration();
            }
        }
    
        public void Regeneration()
        {
            SetHealth(100);
            SwitchState(PlayerState.Alive);
        }
        
        public void Dead()
        {
            SetHealth(0);
            SwitchState(PlayerState.Dead);
        }
    
        private void SwitchState(PlayerState state)
        {
            Debug.Log("Switch state: " + state);
            _currentState = state;
            OnSwitchState?.Invoke(state);
        }
    
        public void SetHealth(int health)
        {
            _health = health;
            OnChangeHealth?.Invoke(health);
            Debug.Log("Health = " + _health);
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Dead();
            }
        }
#endif
    }
}
