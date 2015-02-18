namespace NServiceBus.ScriptCs.ServiceControl
{
    using System;

    using global::ScriptCs.Contracts;

    public class ScriptPack : IScriptPack
    {
        [CLSCompliant(false)]
        public void Initialize(IScriptPackSession session)
        {
        }

        [CLSCompliant(false)]
        public IScriptPackContext GetContext()
        {
            return new NServiceBusControl();
        }

        public void Terminate()
        {
        }
    }
}
