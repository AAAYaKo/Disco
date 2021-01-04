using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Disco
{
    public class UiRepository : ScriptableObject, INotifyPropertyChanged
    {
        private int _playerLives = 0;

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
