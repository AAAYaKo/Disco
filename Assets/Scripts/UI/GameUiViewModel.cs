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
    }
}
