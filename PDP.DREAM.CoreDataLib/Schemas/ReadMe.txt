Nexus-PORTAL-DOORS System (c) 2006-2017 Carl Taswell and the PORTAL-DOORS Project Team.

The Nexus-PORTAL-DOORS System (NPDS) and PORTAL-DOORS Project (PDP) software are available per the MIT License for open source software.

NPDS has been described in the following manuscripts:

IEEE Transactions on Information Technology in Biomedicine 2008, 12(2):191-204 
"DOORS to the Semantic Web and Grid with a PORTAL for Biomedical Computing"
http://ieeexplore.ieee.org/document/4358907/

Future Internet 2010, 2(2):156-189 
"A Distributed Infrastructure for Metadata about Metadata: The HDMM Architectural Style and PORTAL-DOORS System"
http://www.mdpi.com/1999-5903/2/2/156 

USPTO Patent #7792836 filed 9/21/2007 issued 9/7/2010 
"PORTALs and DOORS for the Semantic Web and Grid"
http://pdfpiw.uspto.gov/.piw?Docid=07792836

USPTO Patent #8886628 filed 5/19/2011 issued 11/11/2014 
"Management of Multilevel Metadata in the PORTAL-DOORS System with Bootstrapping"
http://pdfpiw.uspto.gov/.piw?Docid=08886628

For information, updates and the latest version of software, please visit http://www.portaldoors.org.

This zip package should contain the XML Schema *.xsd files, and corresponding JSON Schema *.json files, for the current version 0.9.3 of NPDS:

1) npdsroot.xsd
2) nexusroot.xsd
3) portalroot.xsd
4) doorsroot.xsd
5) nexusroot.json
6) portalroot.json
7) doorsroot.json

JSON schema files have been generated from XML Schema files, and the latter should be considered authoritative. Interoperability with NPDS only requires compliance with the messaging interface specified in the XML Schema files. Resource representation metadata records may be stored in any desired database model for persistence. Thus, the current implementation in SQL Server with the Microsoft stack provides only a reference model for one possible implementation of an underlying data store with a SQL relational database system.  An alternative implementation with the MEAN stack and No-SQL document stores is under development.

All schemas in the 0.*.* versions of NPDS (including the current 0.9.3 version dated 2017-07-31 in this zip package) should be considered drafts subject to revision. Release versions beginning with the 1.*.* versions will be supported. Please consult the development roadmap for planned versions and contact me with any questions, suggestions, or critiques of the current design:

Carl Taswell
ctaswell@computer.org
8 Gilly Flower Street
Ladera Ranch, CA 92694
Tel: 949-481-3121