using Unity.Mathematics;

namespace Disco.ObjectPooling
{
    public interface IPoolCoomponent
    {
        public void SetPool(BasePool pool);

        /// <summary>
        /// Translocate by postition and direction
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public void Translocate(float3 position, float2 direction);
        /// <summary>
        /// Translocate by postition and rotation
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void Translocate(float3 position, quaternion rotation);
    }
}