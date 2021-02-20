using System.ComponentModel;
using UnityEngine;
using UnityMVVM.ViewModel;

namespace Disco.Ui
{
    public class GameUiViewModel : ViewModelBase
    {
        [SerializeField] private int _count;
        [SerializeField] private bool _isDead;

        public int Count
        {
            get => _count;
            set
            {
                if (value != _count)
                {
                    _count = value;
                    NotifyPropertyChanged(nameof(Count));
                }
            }
        }
        public bool IsDead
        {
            get => _isDead;
            set
            {
                if (value != _isDead)
                {
                    _isDead = value;
                    NotifyPropertyChanged(nameof(IsDead));
                }
            }
        }

        private readonly UiRepository _repository = UiRepository.Instance;


        private void OnEnable()
        {
            _repository.PropertyChanged += OnPropertyChanged;
        }
        private void OnDisable()
        {
            _repository.PropertyChanged -= OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_repository.PlayerHealth):
                    Count = _repository.PlayerHealth;
                    break;

                case nameof(_repository.IsDead):
                    IsDead = _repository.IsDead;
                    break;

                default:
                    break;
            }
        }
    }
}
