using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Disco.Ui
{
    public class UiRepository : INotifyPropertyChanged
    {
        /// <summary>
        /// Invoked at the time of property changing
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Static instance of UiRepository
        /// </summary>
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


        private UiRepository() { }

        /// <summary>
        /// Invoke Property changed event
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
