# Example CSOD Create Event/Session API code

## Introduction

This project provides sample code for authenticating and consuming Cornerstone OnDemand's (CSOD) [Event/Session API](https://apiexplorer.csod.com/apiconnectorweb/apiexplorer#/apidoc/59aa5211-b2c9-45af-97b1-0c0902dc4060).

It utilizes CSOD's OAuth 2.0 authentication to obtain an access token via provided ClientId and ClientSecret.

## Provisioning Access and Configuring the Project

1. Follow directions in the API documentation to [register an application with OAuth 2.0](https://apiexplorer.csod.com/apiconnectorweb/apiexplorer#/info).
    1. You will be provided with a *ClientId* and *ClientSecret*.
1. Next, modify `Sample/App.config` file and replace:
    1. *ClientId* value with the value from step 1
    1. *ClientSecret* value with the value from Step 1
    1. The `[portal]` section of *ServiceURL* value with your client's portal URL

As an example, your modified config file would appear as follows:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
      <!--Enter the client id from the portal.-->
      <add key="ClientId" value="test" />
      <!--Enter the client secret from the portal.-->
      <add key="ClientSecret" value="asasdf3241234va" />
      <!--Default grant type is client_credentials-->
      <add key="GrantType" value="client_credentials" />
      <!--Default scope is all-->
      <add key="Scope" value="all" />
      <!--Enter the URL for the Service-->
      <add key="ServiceURL" value="https://acme.csod.com/services/api/x/users/v1/employees" />
    </appSettings>
</configuration>
```

## Building the Project

Open `Sample.sln` with your C# IDE of choice and build the project.  `Program.cs` is the entry point of the solution, and the solution is configured to generate a simple console application.

Running the project will then authenticate with CSOD's OAuth framework, call the Event/Session API, and print out the access token.

## Source file descriptions

* `App.config` - This configuration file contains the values of the keys ClientId, ClientSecret, GrantType , Scope and ServiceURL. Users has to set the values according to the portal they are looking for.
* `Program.cs` - This is the execution class to call the OAuth2 Get Access Token and return the token and corresponding details.
* `OAuth2.cs` - This class wraps the OAuth methods to exchange *ClientId* and *ClientSecret* for an *access_token*
* `EdgeApi.cs` - This class wraps HTTP calls with necessary header information to set the context for subsequent API calls
* `Util.cs` - Using this class to Build Request Parameters.
* `Portal.cs` - This class contains the properties like *ClientId*, *ClientSecret*, *GrantType*, *Scope*, and *ServiceURL*. These are retrieved from the `App.config` file.
* `HttpClientHelper.cs` - This is a helper class which sending HTTP requests and receiving HTTP responses from a resource identified by a URI. Extension of the HTTPClient class.
* `HttpClientParameters.cs` - Defined the properties used in HttpClientHelper class.
* `CommonExtensions.cs` - Extension class for strings and common methods like Split, ContainsAny, Join, ConvertToCommaDelimitedString and ConvertToDelimitedString etc.
