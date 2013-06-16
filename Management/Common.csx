//todo: Make all this a script pack
using RestSharp;
using Microsoft.Win32;

public ManagementService Management = new ManagementService();

public class ManagementService
{
	public ManagementService()
	{
		int port = (int)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\ParticularSoftware\ServiceBus\Management", "Port", 33333);

		client = new RestClient(string.Format("http://localhost:{0}/api",port));
	}

	public T Get<T>(string method) where T: class, new()
	{
		var request = new RestRequest(method, Method.GET);
		var response = client.Execute<T>(request);
		
        if(response.ResponseStatus == ResponseStatus.Error)
        {
        	throw new Exception(response.ErrorMessage);
        }
		return response.Data;
	}

	RestClient client;
}