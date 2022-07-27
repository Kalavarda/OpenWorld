using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units.Interfaces;

namespace OpenWorld.Controllers
{
    internal class UseItemController : IUseItemController
    {
        private readonly IProcessor _processor;

        public UseItemController(IProcessor processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public void Use(IUsable usable)
        {
            var process = usable.Use();
            if (process != null)
                _processor.Add(process);
        }
    }
}
