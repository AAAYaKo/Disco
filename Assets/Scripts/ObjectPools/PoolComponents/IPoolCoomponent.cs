using Unity.Mathematics;

namespace Disco.ObjectPooling
{
    public interface IPoolCoomponent
    {
        public void Translocate(float3 position, float2 direction);
        public void Translocate(float3 position, quaternion rotation);
    }
}