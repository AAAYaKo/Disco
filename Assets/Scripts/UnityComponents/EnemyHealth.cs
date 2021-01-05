using System;
using UnityEngine;

namespace Disco
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _max = 3;
        [SerializeField] private int _curret;

        public int Current
        {
            get => _curret;
            set
            {
                if (value > _max || value < 0)
                    throw new ArgumentOutOfRangeException("Health can't be more than Max and less than Zero");
                else
                {
                    if (value == 0)
                        OnDeath.Invoke();
                    _curret = value;
                }
            }
        }

        public event Action OnDeath;


        private void Awake()
        {
            Current = _max;
            OnDeath += () =>
            {
                gameObject.SetActive(false);
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
