var control = Require<NServiceBusControl>();
var client = control.CreateClient(control.GetLocalUri());

var endpoints = client.GetEndpoints();
Console.WriteLine("{0} endpoint(s) found.", endpoints.Count);
foreach(var endpoint in endpoints)
{
    Console.WriteLine("{0} runs at {1}", endpoint.Name, endpoint.Machine);
}

var errors = client.GetErrors();
Console.WriteLine("{0} error(s) found.", errors.Count);
foreach(var error in errors)
{
    Console.WriteLine(
        "Error Id:{0}, Type:{1}, Time:{2}, Reason:{3}",
        error.MessageId,
        error.MessageType,
        error.FailureDetails.TimeOfFailure,
        error.FailureDetails.Exception.Message);
}
