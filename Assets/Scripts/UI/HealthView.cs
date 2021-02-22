using UnityEngine;
using UnityEngine.UI;
using UnityMVVM.View;

namespace Disco.Ui
{
    public class HealthView : ViewBase
    {
        [SerializeField] private Text _lable;

        public int Count
        {
            set
            {
                _lable.text = $"Lives: {value}";
            }
        }
    }
}
