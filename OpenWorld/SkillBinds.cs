using System.Collections.Generic;
using System.Collections.ObjectModel;
using Kalavarda.Primitives.WPF.Binds;
using OpenWorld.Models.Hero.Skills;

namespace OpenWorld
{
    internal class SkillBinds: SkillBindsBase
    {
        public override IReadOnlyDictionary<string, string> Binds { get; } = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            { KeyBinds.Code_Skill_1, nameof(SimpleStrike) }
        });
    }
}
