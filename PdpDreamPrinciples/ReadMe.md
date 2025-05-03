# PDP-DREAM Ontology

The PDP-DREAM ontology consists of axioms representing the design principles that have guided the PORTAL-DOORS Project over its 16-year history along with classes, named individuals, and properties needed to represent those axioms.
We drew from papers across that publication history for PDP, but chiefly from the two journal articles that detail the design principles and original architecture:

 - Carl Taswell, 2008, DOORS to the Semantic Web and Grid With a PORTAL for Biomedical Computing also available at DOI 10.1109/TITB.2007.905861 submitted 31 October 2006, published online 3 August 2007, published in print 2008 IEEE Transactions on Information Technology in Biomedicine 12(2):191-204. (https://www.portaldoors.net/pub/docs/IEEETITB2008v12n2p191-204Taswell.pdf)
 - Carl Taswell, 2010, A Distributed Infrastructure for Metadata about Metadata: The HDMM Architectural Style and PORTAL-DOORS System also available at DOI 10.3390/fi2020156 submitted 30 December 2009, published online 1 June 2010 Future Internet 2(2):156-189. (https://www.portaldoors.net/pub/docs/FutInt2010v2n2p156-189Taswell.pdf)

A more complete list of PORTAL-DOORS Project papers can be found at https://www.portaldoors.net/PDP/Site/Papers.

This version of PDP-DREAM represents the third major iteration on the design process for the ontology.
A paper describing that process in more detail is currently under peer review.
For more about the previous versions of PDP-DREAM, see the following publications:
 - Shiladitya Dutta, Kelechi Uhegbu, Sathvik Nori, Sohyb Mashkoor, S. Koby Taswell, and Carl Taswell, 2020, DREAM Principles from the PORTAL-DOORS Project and NPDS Cyberinfrastructure also available at DOI 10.1109/ICSC.2020.00044 presented February 2020 at the 14th IEEE International Conference on Semantic Computing in San Diego, California. (https://www.portaldoors.net/pub/docs/ICSC2020PDPDREAM191222.pdf)
 - S. Koby Taswell, Christopher Triggle, June Vayo, Shiladitya Dutta, and Carl Taswell, 2020, The Hitchhiker's Guide to Scholarly Research Integrity also available at DOI 10.1002/pra2.223 presented with hyperlinked version and slides October 2020 at the 83rd ASIS&T Annual Meeting of the Association for Information Science and Technology. (https://www.asist.org/am20/)

We have made the current version of the ontology available in three formats:
 - PdpDream.npdsq represents the ontology as a set of N-quads (https://www.w3.org/TR/n-quads/) with comments in the NpdsQuad name-value pair format.
 - PdpDream.rdf represents the ontology in RDF 1.1 / XML (https://www.w3.org/TR/rdf-syntax-grammar/).
 - PdpDream.rdf represents the ontology in OWL 2 / XML (https://www.w3.org/TR/2012/REC-owl2-xml-serialization-20121211/).
PdpDream.npdsq is the authoritative version due to the greater expressiveness of quads over triples.
In particular, while the principles exist as named individuals in all three versions, only the N-quad representation makes the URI of each principle the graph label of the N-quad that expresses it.
The NpdsQuad-style comments also provide useful information, specifically a natural language name and the quote from the source literature on which we based each axiom.
All three formats should be parsable for most ontology editors.
If you believe an ontology editor did not load any of the files correctly, please contact Carl Taswell (mailto:ctaswell@bhavi.us).

The NpdsQuads format provides additional information in the form of name-value pairs.
Each name corresponds to an infosubset in the Nexus-PORTAL-DOORS-Scribe (NPDS) Cyberinfrastructure.
A block of comment lines and N-quad lines with no blank lines between them corresponds to the lexical metadata and semantic description of an entity in an NPDS record.
This approach enables creation of records from blocks in an NpdsQuads document and export of information from an NPDS service to an NpdsQuads document.

 
