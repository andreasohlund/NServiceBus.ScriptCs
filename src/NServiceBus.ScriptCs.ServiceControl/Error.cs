namespace NServiceBus.ScriptCs.ServiceControl
{
    public class Error
    {
        public string Id { get; set; }
        
        public string MessageId { get; set; }
        
        public string MessageType { get; set; }
        
        public FailureDetails FailureDetails { get; set; }
    }
}
