using System.Collections.Generic;
using System.Windows.Input;
using Kalavarda.Primitives.WPF.Binds;

namespace OpenWorld
{
    internal class KeyBinds: KeyBindsBase
    {
        public const string Code_SelectTarget = "SelectTarget";
        public const string Code_SelectSelf = "SelectSelf";
        public const string Code_SelectNone = "SelectNone";
        public const string Code_Bag = "Bag";
        public const string Code_Alchemy = "Alchemy";
        public const string Code_Equipment = "Equipment";

        public const string Code_Skill_1 = "Skill_1";
        public const string Code_Skill_2 = "Skill_2";

        private static readonly KeyBind[] _binds = {
            new(Code_SelectTarget, "Выбрать цель", Key.Tab),
            new(Code_SelectSelf, "Выбрать своего персонажа", Key.F1),
            new(Code_SelectNone, "Сбросить цель", Key.Escape),
            new(Code_Bag, "Сумка", Key.B),
            new(Code_Alchemy, "Алхимия", Key.A),
            new(Code_Equipment, "Экипировка", Key.I),
            
            new(Code_Skill_1, "Умение 1", Key.D1),
            new(Code_Skill_1, "Умение 2", Key.D2)
        };

        public override IReadOnlyCollection<KeyBind> Binds => _binds;
    }
}
