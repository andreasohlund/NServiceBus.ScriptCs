namespace NServiceBus.ScriptCs.ServiceControl
{
    using System;
    using System.Globalization;
    using Microsoft.Win32;
    using global::ScriptCs.Contracts;

    public class NServiceBusControl : IScriptPackContext
    {
        public Client CreateClient(Uri baseUri)
        {
            return new Client(baseUri);
        }

        public Uri GetLocalUri()
        {
            var port = Registry.GetValue(
                @"HKEY_LOCAL_MACHINE\SOFTWARE\ParticularSoftware\ServiceControl", "Port", 33333) ?? 33333;

            return new Uri(
                string.Format("http://localhost:{0}/api", ((int)port).ToString(CultureInfo.InvariantCulture)));
        }
    }
}
