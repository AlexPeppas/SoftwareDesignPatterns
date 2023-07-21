First go to Program.cs setup your Serilog configuration and then use the custom loggerProvider by leveraging UseSerilog() in services configuration.
Then right click on Workers project and Publish to local folder (same as logging folder). This will generate the publishProfiles.
Then you will need Microsoft.Extensions.Hosting.WindowsServices and UseWindowsService() to register this as a windows service.
Run as admin the registerAsWindowsService.ps1 to register this as a background windows service and observe its status at windows services app, along with the logging file.
Stop the service and run as admin the unregisterAsWindowsService.ps1 to delete this background service.