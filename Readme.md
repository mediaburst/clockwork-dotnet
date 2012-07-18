Clockwork SMS API Wrapper for .NET
==================================
This wrapper lets you interact with Clockwork without the hassle of having to create any XML or make HTTP calls.

It's compiled as a .NET 2 library so should work with version 2 or above.

The examples below are for C#, you can find a sample VB.NET app in the Sample VB folder.

What's Clockwork?
-----------------
The mediaburst SMS API is being re-branded as Clockwork. At the same time we'll be launching some exciting new features
and these shiny new wrappers.

The terms Clockwork and "mediaburst SMS API" refer to exactly the same thing.

Installation
------------
We've packaged Clockwork as a [NuGet package][1], simply search for Clockwork in the NuGet package manager.

Alternatively, download this project and add Clockwork.dll as a project reference in Visual Studio.


Prerequisites
-------------
* Microsoft .NET Framework 2 or higher
* A [Clockwork][2] account


Usage
-----
For convenience you probably want to import the Clockwork namespace in to your code file

	using Clockwork;

### Sending a message
	
	Clockwork.API api = new API(key);
	SMSResult result = api.Send( new SMS { To = "441234567890", Message = "Hello World" } );   

### Sending multiple messages

We recomment you use batch sizes of 500 messages or fewer. By limiting the batch size it prevents any timeouts when sending.

	Clockwork.API api = new API(key);
	List<SMS> smsList = new List<SMS>();
	smsList.Add(new SMS { To = "441234567891", Message = "Hello Bill" });
	smsList.Add(new SMS { To = "441234567892", Message = "Hello Ben" });
	List<SMSResult> results = api.Send(smsList);

### Handling the resposne

The responses come back in SMSResult objects, these contain the unique Clockwork Message ID, whether the message worked, and the original SMS so you can update your database.

	Clockwork.API api = new API(key);
	SMSResult result = api.Send( new SMS { To = "441234567890", Message = "Hello World" } );   

	if(result.Success) 
	{
		Console.WriteLine("SMS Sent to {0}, Clockwork ID: {1}", result.SMS.To, result.ID);
	}
	else
	{
		Console.WriteLine("SMS to {0} failed, Clockwork Error: {1} {2}", result.SMS.To, result.ErrorCode, result.ErrorMessage);
	}

If you send multiple SMS messages in a single send, you'll get back a List of SMSResult objects, one per SMS object.

The SMSResult object will look something like this:

	SMSResult {  
		SMS          = SMS {
								To      = "441234567890",
								Message = "Hello World"
						},
		ID           = "clockwork_message_id",
		Success      = true,
		ErrorCode    = 0,
		ErrorMessage = null		
	}; 


If a message fails, Success will be false, and the reason for failure will be set in ErrorCode and ErrorMessage.  

For example, if you send to invalid phone number "abc":

	SMSResult { 
		SMS          = SMS {
								To      = "abc",
								Message = "Hello World"
						}, 
		ID           = null,
		Success      = false,
		ErrorCode    = 10,
		ErrorMessage = "Invalid 'To' Parameter"
	}; 

### Checking your credit

Check how many SMS credits you currently have available, the value will be returned as a long

	Clockwork.API api = new API(key);
	long credit = api.CheckCredit();
    
### Handling Errors

The Clockwork wrapper will throw exceptions if the entire call failed.

Exceptions of the following types are expected:

	* APIException (Clockwork.APIException)
	
	* WebException (System.Net.WebException)
	
	* ArgumentException (System.ArgumentException)

For example sending the wrong API Key will throw an APIException whereas, trying to send without an internet connection would throw a WebException.

A basic structure for Exception handling would look like this:

	try
	{
		Clockwork.API api = new API(not_your_key);
		SMSResult result = api.Send( new SMS { To = "441234567890", Message = "Hello World" } );   
		// Process the result here
	}
	catch (APIException ex)
	{
		// You'll get an API exception for errors such as wrong API Key
	}
	catch (WebException ex)
	{
		// Web exceptions mean you couldn't reach the Clockwork server
	}
	catch (ArgumentException ex)
	{
		// Argument exceptions are thrown for missing parameters, such as forgetting to set the API Key
	}
	catch (Exception ex)
	{
		// Something else went wrong, the error message should help here
	}


Advanced Usage
--------------
This class has a few additional features that some users may find useful, if these are not set your account defaults will be used.

### Optional Parameters

*   From [string]

    The from address displayed on a phone when they receive a message

*   Long [nullable boolean]  

    Enable long SMS. A standard text can contain 160 characters, a long SMS supports up to 459.

*   Truncate [nullable boolean]  

    Truncate the message payload if it is too long, if this is set to false, the message will fail if it is too long.

*	InvalidCharacterAction [enum]

	What to do if the message contains an invalid character. Possible values are
	* AccountDefault - Use the setting from your Clockwork account
	* None			 - Fail the message
	* Remove		 - Remove the invalid characters then send
	* Replace		 - Replace some common invalid characters such as replacing curved quotes with straight quotes

### Setting Options

#### Global options
Options set on the API object will apply to all SMS messages unless specifically overridden.

In this example both message will be sent from Clockwork

	Clockwork.API api = new API(key);
	api.From = "Clockwork";
	List<SMS> smsList = new List<SMS>();
	smsList.Add(new SMS { To = "441234567891", Message = "Hello Bill" });
	smsList.Add(new SMS { To = "441234567892", Message = "Hello Ben" });
	List<SMSResult> results = api.Send(smsList); 

#### Per-message Optons
Set option values individually on each message

In this example, one message will be from Clockwork and the other from 84433

	Clockwork.API api = new API(key);
	List<SMS> smsList = new List<SMS>();
	smsList.Add(new SMS { To = "441234567891", Message = "Hello Bill", From="Clockwork" });
	smsList.Add(new SMS { To = "441234567892", Message = "Hello Ben", From="84433" });
	List<SMSResult> results = api.Send(smsList);

### Proxy Server Support
If you need to override the system proxy settings you can optionally pass a System.Net.WebProxy object to the SMS class.  
See [WebProxy on MSDN][3] for full details.

	// Create the Clockwork API object as normal
	Clockwork.API api = new API(key);

	// Set your proxy settings
	api.Proxy = new System.Net.WebProxy(proxyHost, proxyPort);

	// Send the message
	SMSResult result = api.Send( new SMS { To = "441234567890", Message = "Hello World" } );  
		 

License
-------
This project is licensed under the ISC open-source license.

A copy of this license can be found in License.txt.

Contributing
------------
If you have any feedback on this wrapper drop us an email to hello@clockworksms.com.

The project is hosted on GitHub at https://github.com/mediaburst/clockwork-dotnet.
If you would like to contribute a bug fix or improvement please fork the project 
and submit a pull request.

[1]: https://nuget.org/packages/Clockwork/
[2]: http://www.clockworksms.com/
[3]: http://msdn.microsoft.com/en-us/library/system.net.webproxy.aspx	"MSDN"