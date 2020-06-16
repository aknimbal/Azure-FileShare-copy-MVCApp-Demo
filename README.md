# Azure-FileShare-copy-MVCApp-Demo
ASP.NET MVC application to copy files to Azure File share

Follow the steps below:

1. Download the solution

2. Open Azure.Filestorage.MVCApp.Demo.sln using Visual Studio 2019

3. In Solution Explorer, right-click your project and choose Manage NuGet Packages.

4. In NuGet Package Manager, select Browse. Then search for and choose Microsoft.Azure.Storage.Blob, and then select Install.

This step installs the package and its dependencies.

5. Search for and install these packages:

    Microsoft.Azure.Storage.Common
    Microsoft.Azure.Storage.File
    Microsoft.Azure.ConfigurationManager

6. Next, save your credentials in your project's App.config file. In Solution Explorer, double-click App.config and edit the file so that it is similar to the following example. Replace myaccount with your storage account name and mykey with your storage account key.

7. Build and run

Note: Errors are written to Application Eventlog if any.
