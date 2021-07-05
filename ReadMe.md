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

<h4>PDP Software</h4>

<p>
  PDP software provides anonymous user read access to resource metadata in the Nexus diristries,
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
<p>
  For comprehensive reports on NPDS, please refer to
  <a href="https://www.portaldoors.org/pub/docs/IEEETITB2008v12n2p191-204Taswell.pdf" target="_blank">
  DOORS to the Semantic Web and Grid With a PORTAL for Biomedical Computing</a>
  published online 3 August 2007 in the journal IEEE Transactions on Information Technology in Biomedicine
  and
  <a href="https://www.portaldoors.org/pub/docs/FutInt2010v2n2p156-189Taswell.pdf" target="_blank">
  A Distributed Infrastructure for Metadata about Metadata: The HDMM Architectural Style and PORTAL-DOORS System</a>
  published online 1 June 2010 in the journal Future Internet.
  More recent information can be found in the documents available at
  <a href="https://www.portaldoors.org/PDP/Site/Papers" target="_blank">https://www.portaldoors.org/PDP/Site/Papers</a>.
  These reports contain explanations of the principles, concepts, models, schemas,
  URL and querystring designs used by PDP for NPDS.
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
