# PDP-DREAM

### PORTAL-DOORS Project PDP-DREAM Repository for C# Software

This Version: Gangkhar 10.4.4  
Required Package: Telerik.UI.for.AspNet.Core.2023.1.314.nupkg  
Required Database: Microsoft SQL Server 2022 v16.0.4003.1  
Current DotNet Target: .NET Runtime 8.0.pre2 with SDK 8.0.pre2 and Visual Studio 17.6.pre2  
Planned DotNet Target: maintain in most recent preview of Net 7, 8, 9  
Dev/Test Focus Net 6 Aoraki: web apps with Telerik UI for ASP.Net Core MVC Views     
Dev/Test Focus Net 7 Cervin: web apps with Telerik UI for ASP.Net Core Razor Pages  
Dev/Test Focus Net 8 Gangkhar: web apps with Telerik UI for ASP.Net Core Blazor Components and desktop apps with Telerik UI for MAUI  
Public Release: Initial 24 June 2021 in repository github.com/BHAVIUS/PDP-DREAM  

Visual Studio Solution of DotNet Projects and Assemblies  
(stack dependencies configured as chain from top to bottom)  

* PDP.DREAM.ScribeWebLib (PDP.Gangkhar.ScribeWebLib) for ACGT
* PDP.DREAM.ScribeDataLib (PDP.Gangkhar.ScribeDataLib) for ACGT
* PDP.DREAM.NexusWebLib (PDP.Gangkhar.NexusWebLib) for ACGT
* PDP.DREAM.NexusDataLib (PDP.Gangkhar.NexusDataLib) for ACGT
* PDP.DREAM.CoreWebLib (PDP.Gangkhar.CoreWebLib) for ACGT
* PDP.DREAM.CoreDataLib (PDP.Gangkhar.CoreDataLib) for ACGT

where  
A is Aoraki on Net 6 with MVC Views, fall 2022,  
C is Cervin on Net 7 with Razor Pages, spring 2023,  
G is Gangkhar on Net 8 with Blazor Components, fall 2023,  
T is Tahtali on current Net preview.  

Related Github Projects  

* <a href="https://github.com/BHAVIUS/PORTALDOORS">BHAVIUS/PORTALDOORS</a>  
* <a href="https://github.com/BHAVIUS/NPDSLINKS">BHAVIUS/NPDSLINKS</a>  
* <a href="https://github.com/BHAVIUS/PDP-DREAM">BHAVIUS/PDP-DREAM</a>  

<h4>NPDS Cyberinfrastructure</h4>

<p>
  NPDS is a <em>"who what where"</em> diristry-registry-directory system for identifying,
  describing, locating and linking things on the internet, web and grid.
  PORTAL registries identify resources with unique labels and lexical tags
  in a manner compatible with the lexical web.
  DOORS directories specify locations and semantic descriptions for these identified resources in a manner compatible with the semantic web.
  PORTAL registries and DOORS directories were designed to be analogous to IRIS registries and DNS directories.
  This original design has been enhanced with
  Nexus diristries to provide integrated services combining the functions of both PORTAL registries and DOORS directories.
</p>

<p>
  For comprehensive reports on the NPDS cyberinfrastructure, refer to the original design proposed in 
  <a href="https://www.portaldoors.org/pub/docs/IEEETITB2008v12n2p191-204Taswell.pdf" target="_blank">
  DOORS to the Semantic Web and Grid With a PORTAL for Biomedical Computing</a>
  published online 3 August 2007 in the journal IEEE Transactions on Information Technology in Biomedicine
  and the revised design described in
  <a href="https://www.portaldoors.org/pub/docs/FutInt2010v2n2p156-189Taswell.pdf" target="_blank">
  A Distributed Infrastructure for Metadata about Metadata: The HDMM Architectural Style and PORTAL-DOORS System</a>
  published online 1 June 2010 in the journal Future Internet.
  More recent information can be found in the documents available at
  <a href="https://www.portaldoors.org/PDP/Site/Papers" target="_blank">PDP Site Papers</a>.
  These reports contain explanations of the principles, concepts, models, schemas,
  URL and querystring designs used by PDP for NPDS.
</p>

<h4>PDP-DREAM Principles</h4>

<p>
 The principles for the PORTAL-DOORS Project (PDP) were first proposed and described by Taswell in 2006 
 as the foundation for work on PDP and the Nexus-PORTAL-DOORS-Scribe (NPDS) cyberinfrastructure.
 This work on PDP and NPDS has been continuously available since 2007 from a publicly accessible web site at 
 <a href="https://www.portaldoors.org/" target="_blank">www.PORTALDOORS.org</a>.
 The 2006 PDP principles were renamed the 2019 DREAM principles in 
   <a href="https://www.portaldoors.org/pub/docs/ECAI2019DREAMFAIR0618.pdf" target="_blank">
  DREAM Principles and FAIR Metrics from the PORTAL-DOORS Project for the Semantic Web</a>
 with the acronym DREAM for 
 <em>"Discoverable Data with Reproducible Results for Equivalent Entities with Accessible Attributes and Manageable Metadata"</em>.
 These PDP-DREAM principles have been discussed further in the 2019 
 <a href="https://www.portaldoors.org/pub/docs/BCDC2019PdpDemo0817.pdf" target="_blank">
 Managing Scientific Literature with Software from the PORTAL-DOORS Project</a>, the 2020
 <a href="https://www.portaldoors.org/pub/docs/ICSC2020PDPDREAM191222.pdf" target="_blank">
 DREAM Principles from the PORTAL-DOORS Project and NPDS Cyberinfrastructure</a>, and the 2020
 <a href="https://www.portaldoors.org/pub/docs/ASIST2020HHGuide1022.pdf" target="_blank">
 Hitchhiker's Guide to Scholarly Research Integrity</a>.
</p>

<h4>PDP-DREAM Software</h4>

<p>
  PDP-DREAM software provides anonymous user read access to resource metadata in the Nexus diristries,
  PORTAL registries and DOORS directories  as well as
  information on the continuing design, development, and implementation of NPDS as a cyberinfrastructure
  for semantic computing on the internet, web, and grid.
  Scribe registrar services are maintained at the following sites for resource metadata and data management at
  Nexus diristries,  PORTAL registries and  DOORS directories:
</p>
<ul>
  <li>
    <a href="https://www.brainhealthalliance.net/" target="_blank">BHA Registrar</a> for BrainWatch, Eywa,
    Gaia, HELPME, and SOLOMON diristries supported by Brain Health Alliance
  </li>
  <li>
    <a href="https://www.telegenetics.net/" target="_blank">GTG Registrar</a> for BioPORT, CTGaming, GeneScene,
    ManRay, and Osler diristries supported by Global TeleGenetics
  </li>
  <li>
    <a href="https://www.portaldoors.net/" target="_blank">PDP Registrar</a> for Avicenna, Beacon, DaVinci, Knuth, and Socrates diristries
     supported by the PORTAL-DOORS Project
  </li>
  <li>
    <a href="https://www.npdslinks.net/" target="_blank">NPDS Registrar</a> for the NPDS network system root supported by the PORTAL-DOORS Project
  </li>
</ul>
<p>
  all of which require secure https connection for authorized agent write access. Resource metadata record
  agents with Author and/or Editor roles may access write privileges at the BHA, GTG and PDP Scribe Registrars, but
  not for management of NPDS components where write access is restricted to agents with Admin roles.
</p>


<h4>Nexus Sites (anonymous)</h4>

<ul>
  <li><a href="http://www.bhaconservation.net/" target="_blank">www.BHAConservation.net</a></li>
  <li><a href="http://www.bhahealth.net/" target="_blank">www.BHAHealth.net</a></li>
  <li><a href="http://www.biomedicalcomputing.net/" target="_blank">www.BioMedicalComputing.net</a></li>
  <li><a href="http://www.brainwatch.net/" target="_blank">www.BrainWatch.net</a></li>
  <li><a href="http://www.clinicaltelegaming.net/" target="_blank">www.ClinicalTeleGaming.net</a></li>
  <li><a href="http://www.genescene.net/" target="_blank">www.GeneScene.net</a></li>
  <li><a href="http://www.nucmedlib.net/" target="_blank">www.NucMedLib.net</a></li>
</ul>

<h4>Scribe Sites (authenticated)</h4>

<ul>
  <li><a href="https://www.brainhealthalliance.net/" target="_blank">www.BrainHealthAlliance.net</a></li>
  <li><a href="https://www.npdslinks.net/" target="_blank">www.NPDSLINKS.net</a></li>
  <li><a href="https://www.portaldoors.net/" target="_blank">www.PORTALDOORS.net</a></li>
  <li><a href="https://www.telegenetics.net/" target="_blank">www.TeleGenetics.net</a></li>
</ul>
