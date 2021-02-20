namespace Disco
{
    internal interface IDamageble
    {
        /// <summary>
        /// Aply damage. Not work if damage less then zero or if current heath is zero
        /// </summary>
        /// <param name="damage"></param>
        public void TryAplyDamagage(int damage);
    }
}