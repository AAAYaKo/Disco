using UnityEngine;
using UnityMVVM.ViewModel;

namespace Disco
{
    public class GameUiViewModel : ViewModelBase
    {
        [SerializeField] private int _count;
        [SerializeField] private bool _isDead;


        private UiRepository _repository;


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

        private void Awake()
        {
            _repository = ScriptableObject.CreateInstance<UiRepository>();
            _repository.PropertyChanged += (sender, argument) =>
            {
                switch (argument.PropertyName)
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
            };
        }
    }
}
