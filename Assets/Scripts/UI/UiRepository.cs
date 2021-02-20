using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Disco.Ui
{
    public class UiRepository : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static UiRepository Instance
        {
            get
            {
                return _instance ??= new UiRepository();
            }
        }
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

        private static UiRepository _instance;
        private int _playerLives;
        private bool _isDead;


        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
