using System;
using UnityEngine;

namespace Disco
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _max = 12;
        [SerializeField] private int _current;

        public event Action OnDeath;
        public int Current
        {
            get => _current;
            set
            {
                if (value > _max || value < 0)
                    throw new ArgumentOutOfRangeException("Current health can't be more then Max Health or lower than Zero");
                else
                {
                    if (value == 0)
                        OnDeath?.Invoke();
                    _loader.Repository.PlayerHealth = value;
                    _current = value;
                }
            }
        }

        private UiLoader _loader;


        private void Awake()
        {
            _loader = FindObjectOfType<UiLoader>();
            _loader.OnLoadComplete += () =>
            {
                Current = _max;
            };

            OnDeath += () =>
            {
                _loader.Repository.IsDead = true;
                Debug.Log("Are you Dead");
            };
        }

        public void TryAplyDamagage(int damage)
        {
            if (damage <= 0) return;
            if (Current == 0) return;
            int next = Current - damage;
            if (next <= 0)
            {
                Current = 0;
                return;
            }
            Current = next;
        }
    }
}
