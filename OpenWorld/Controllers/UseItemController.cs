using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units.Interfaces;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class UseItemController : IUseItemController
    {
        private readonly IProcessor _processor;
        private readonly Hero _hero;

        public UseItemController(IProcessor processor, Hero hero)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
        }

        public void Use(IUsable usable)
        {
            var process = usable.Use(_hero);
            if (process != null)
                _processor.Add(process);
        }
    }
}
