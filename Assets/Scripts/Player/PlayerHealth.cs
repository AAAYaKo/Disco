using Disco.Ui;
using System;
using UnityEngine;

namespace Disco.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _max = 12;
        [SerializeField] private int _current;

        public event Action Death;
        public int Current
        {
            get => _current;
            set
            {
                //Verify value 
                if (value > _max || value < 0)
                    throw new ArgumentOutOfRangeException("Current health can't be more then Max Health or lower than Zero");
                else
                {
                    _repository.PlayerHealth = value;
                    _current = value;
                    //Die
                    if (value == 0)
                        Death?.Invoke();
                }
            }
        }
        private readonly UiRepository _repository = UiRepository.Instance;


        private void OnEnable()
        {
            Current = _max;
            _repository.IsDead = false;
            Death += OnDie;
        }

        private void OnDisable()
        {
            Death -= OnDie;
        }

        private void OnDie()
        {
            _repository.IsDead = true;
            Time.timeScale = 0;
        }

        public void TryAplyDamagage(int damage)
        {
            if (damage <= 0 || Current == 0) return;
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
