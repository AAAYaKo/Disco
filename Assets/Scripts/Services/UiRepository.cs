using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Disco
{
    public class UiRepository : ScriptableObject, INotifyPropertyChanged
    {
        private int _playerLives = 0;
        private bool _isDead;

        public bool IsDead
        {
            get => _isDead;
            set
            {
                if(value != _isDead)
                {
                    _isDead = value;
                    OnPropertyChanged();
                }
            }
        }


        public int PlayerHealth
        {
            get => _playerLives;
            set
            {
                if (value != _playerLives)
                {
                    _playerLives = value;
                    OnPropertyChanged();
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
