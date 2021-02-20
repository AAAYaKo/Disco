using System;

namespace Disco
{
    internal interface IHealth : IDamageble
    {
        /// <summary>
        /// Invoked at the time of death
        /// </summary>
        public event Action Death;
        /// <summary>
        /// Current Value of Heath
        /// </summary>
        public int Current { get; set; }
    }
}