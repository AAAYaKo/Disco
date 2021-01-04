using System;

namespace Disco
{
    internal interface IHealth
    {
        public event Action OnDeath;
        public int Current { get; set; }
        public void TryAplyDamagage(int damage);
    }
}