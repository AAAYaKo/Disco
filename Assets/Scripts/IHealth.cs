using System;

namespace Disco
{
    internal interface IHealth : IDamageble
    {
        public event Action Death;
        public int Current { get; set; }
    }
}