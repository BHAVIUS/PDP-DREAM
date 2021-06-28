# PDP-DREAM

### PORTAL-DOORS Project PDP-DREAM Repository for C# Software

Current Version: Aoraki 9.3.2  
Current DotNet Target: 6.0.0-preview5 with Visual Studio 17 preview 1  
Planned DotNet Target: 6.0 maintain in 6.0 for Microsoft lifetime of 6.0  
Dev/Test Focus: web apps with Telerik UI for ASP.Net Core  
Public Release: 24 June 2021 in repository github.com/BHAVIUS/PDP-DREAM  

Solution Stack of Projects with Assemblies for NexusWebApp  

* PDP.DREAM.NexusWebApp (PDP.Aoraki.NexusWebApp)
* -> PDP.DREAM.NexusRestApi (PDP.Aoraki.NexusRestApi)
* --> PDP.DREAM.NexusDataLib (PDP.Aoraki.NexusDataLib)
* ---> PDP.DREAM.NpdsCoreLib (PDP.Aoraki.NpdsCoreLib)

Solution Stack of Projects with Assemblies for ScribeWebApp  

* PDP.DREAM.ScribeWebApp (PDP.Aoraki.ScribeWebApp)
* -> PDP.DREAM.ScribeRestApi (PDP.Aoraki.ScribeRestApi)
* --> PDP.DREAM.SiaaDataLib (PDP.Aoraki.SiaaDataLib)
* ---> PDP.DREAM.ScribeDataLib (PDP.Aoraki.ScribeDataLib)
* ----> PDP.DREAM.NexusDataLib (PDP.Aoraki.NexusDataLib)
* -----> PDP.DREAM.NpdsCoreLib (PDP.Aoraki.NpdsCoreLib)