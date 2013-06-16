#load "Common.csx"

var endpoints = Management.Get<List<Endpoint>>("endpoints");

foreach(var endpoint in endpoints)
{
	Console.Out.WriteLine(endpoint.Name + " runs at " + endpoint.Machine);	
}

class Endpoint
{
	public string Name{get;set;}
	public string Machine{get;set;}
}