using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.WPF.Skills;
using OpenWorld.Models;
using OpenWorld.Models.Skills;

namespace OpenWorld
{
    internal class HeroSkillBinds : ISkillBinds
    {
        public Hero Hero { get; set; }

        public IReadOnlyCollection<SkillBind> SkillBinds { get; } = new[]
        {
            new SkillBind(nameof(SimpleStrike), Key.D1)
        };

        public ISkill GetSkill(string key)
        {
            return (ISkill)Hero.Skills.OfType<IHasKey>().First(sk => sk.Key == key);
        }

        public SkillBind GetBind(string key)
        {
            return SkillBinds.FirstOrDefault(sb => sb.SkillKey == key);
        }
    }
}
