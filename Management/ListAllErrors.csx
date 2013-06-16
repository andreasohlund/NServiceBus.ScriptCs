#load "Common.csx"

var errors = Management.Get<List<Error>>("errors");

foreach(var error in errors)
{
	Console.Out.WriteLine("Id:{0}, Type:{1}, Time:{2}, Reason:{3}",
		error.MessageId,
		error.MessageType,
		error.FailureDetails.TimeOfFailure,
		error.FailureDetails.Exception.Message);	
}

class Error
{
	public string Id {get;set;}
	public string MessageId {get;set;}
	public string MessageType {get;set;}
	public FailureDetails FailureDetails { get; set; }
}


public class FailureDetails
{
    public int NumberOfTimesFailed { get; set; }

    public string FailedInQueue { get; set; }

    public DateTime TimeOfFailure { get; set; }

    public ExceptionDetails Exception { get; set; }

    public DateTime ResolvedAt { get; set; }
}

public class ExceptionDetails
{
    public string ExceptionType { get; set; }

    public string Message { get; set; }

    public string Source { get; set; }

    public string StackTrace { get; set; }
}

