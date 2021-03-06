using System;
using UnityEngine;

namespace Disco.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDamageble
    {
        public event Action OnDeath;

        [SerializeField] private int _max = 3;
        [SerializeField] private int _curret;

        public int Current
        {
            get => _curret;
            set
            {
                //Verify
                if (value > _max || value < 0)
                    throw new ArgumentOutOfRangeException("Health can't be more than Max and less than Zero");
                else
                {
                    _curret = value;
                    //Die
                    if (value == 0)
                        OnDeath?.Invoke();
                }
            }
        }


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
            if (Current == 0 && damage <= 0) return;
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
