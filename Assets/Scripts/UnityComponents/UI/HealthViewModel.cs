using UnityEngine;
using UnityMVVM.ViewModel;

namespace Disco
{
    public class HealthViewModel : ViewModelBase
    {
        [SerializeField] private int _count;

        private UiRepository _repository;


        public int Count
        {
            get { return _count; }
            set
            {
                if(value != _count)
                {
                    _count = value;
                    NotifyPropertyChanged(nameof(Count));
                }
            }
        }
        private void Awake()
        {
            _repository = ScriptableObject.CreateInstance<UiRepository>();
            _repository.PropertyChanged += (sender, argument) =>
            {
                if (argument.PropertyName == nameof(_repository.PlayerHealth))
                    Count = _repository.PlayerHealth;
            };
        }
    }
}
