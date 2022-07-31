using System.Linq;
using System.Reflection;
using Kalavarda.Primitives.Units.Buffs;
using Kalavarda.Primitives.WPF;

namespace OpenWorld
{
    internal class BuffsRepository: IReadonlyBuffsRepository
    {
        private static readonly Assembly _resourcesAssembly = typeof(BuffsRepository).Assembly;

        public static readonly BuffType Healing = new(1, "Исцеление", _resourcesAssembly.GetResourceUri("Resources/Buffs/Healing.png"));
        public static readonly BuffType Poisoning = new(2, "Отравление", _resourcesAssembly.GetResourceUri("Resources/Buffs/Poisoning.png"));

        private static readonly BuffType[] _types =
        {
            Healing,
            Poisoning
        };

        public BuffType GetById(uint id)
        {
            return _types.FirstOrDefault(t => t.Id == id);
        }
    }
}
