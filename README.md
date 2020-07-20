<p align="center">
    <img src="https://assets.pingone.com/ux/end-user/0.14.0/images/ping-logo.svg" height="150" width="150" />
</p>

# PingFederate Agentless Integration Kit Sample Applications for .NET

## Overview

These sample applications let you test an integration with the Agentless Integration Kit. PingFederate acts as both the identity provider (IdP) and service provider (SP), showing the complete end-to-end configuration and user experience.

The package includes two independent ASP.NET Core web applications, one for each of the IdP and SP roles. You can see the source code and deploy the applications easily using the development servers included with your .NET deployment.
The included PingFederate configuration archive allows a single instance of PingFederate to run both sample applications.

<p align="center">
    <img src="/images/example.gif"/>
</p>

## System requirements and dependencies

* PingFederate 8.x or later
* PingFederate Agentless Integration Kit 1.5 or later
* .NET Core 3.1 or later

### Browser compatibility

The samples work with all major browsers, including Chrome, Firefox, and Microsoft Edge.

## Setup

### Deploying the PingFederate configuration archive
The included configuration archive creates the adapter instances and connections needed to run the sample applications.

To deploy the configuration archive, import the `configuration-archive/data.zip` file through the administrator console or copy it to the drop-in-deployer directory. For instructions, see [Configuration archive](https://docs.pingidentity.com/csh?Product=pf-latest&topicname=oor1564002974031.html) in the PingFederate documentation.

**Caution:** Deploying the configuration archive will destroy your existing PingFederate configuration. We recommend that you deploy it on a fresh installation of PingFederate. Otherwise, back up your current configuration as shown in [Exporting an archive](https://docs.pingidentity.com/csh?Product=pf-latest&topicname=amd1564002974196.html) in the PingFederate documentation.

### Running the applications
You can run the applications with the .NET-provided development server.

1. If you want to use a hostname and port other than `https://localhost:5001` for the IdP sample application and `https://localhost:6001`
for the SP sample application, make the following changes.
   1. Modify the `appsetting.json` file in `sample-applications/AgentlessIdpSample` for the IdP application, and the equivalent file for the SP application.
   2. Modify the adapter and connection configurations in PingFederate.
2. Go to the `sample-applications/AgentlessIdpSample` directory and enter the following command: `dotnet run`
3. Go to the `sample-applications/AgentlessSpSample` directory and enter the following command: `dotnet run`
4. In your browser, go to the following URL to start IdP single sign-on flow:
```https://localhost:9031/sp/startSSO.ping?PartnerIdpId=PF-DEMO```

## Modifying your application
When you are ready to make changes to your own application, see the examples in the `example-code` directory to help you get started.

## Documentation

For the latest documentation, see [Agentless Integration Kit](https://docs.pingidentity.com/bundle/integrations/page/ygj1563994984859.html) in the Ping Identity [Support Home](https://support.pingidentity.com/s/).

## Known Limitations

To keep the app simple and focused on interactions with PF, it does not support browser page refreshes.

## Reporting bugs

Please report issues using this project's issue tracker.

## License

This project is licensed under the Apache license. See the [LICENSE](LICENSE) file for more information.
