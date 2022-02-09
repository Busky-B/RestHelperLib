# RestHelperLib
Helperklass som tar emot en modell för att sedan göra ett abstrakt api-call och utföra operationer mot api (användare behöver alltså inte hantera deserialization, httpclients och responses etc).
Stödjer full CRUD.

# Övriga funktioner
Klassen innehåller även 1 överladdad funktion, PrintObjProps. Denna funktion tar emot en Generisk klassmodell och skriver ut / returnerar en sträng med alla dess properties och values. 

# TODO
PrintObjProps behöver vidare utveckling så att den kan användas på razorpages. Baktanken är att man skall slippa skriva ut alla displayfor/editorfor statements som behöver göras för varje property på sin modell.
