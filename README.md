# AMC2GIFT

**Ce projet a été fait dans le cadre des projets individuels de DUT informatique de 2ème année**. 
 
Le but de ce projet est de créer un outil permettant de convertir des fichiers XMLMOODLE, AMC et GIFT vers un autre de ces formats. 
 
-----------------------------------------

# Avancement du projet

Pour l'instant toutes les commandes sont implémentées et fonctionnelles sauf le verbose.

# Commandes disponibles

Il est possible d'exécuter 3 commandes : 

## Conversion

Commande permettant de convertir un fichier source d'un format spécifique vers un chemin de destination sous un autre format.
(Actuellement pas implémentée !)


``-c (--convert) -v(--verbose) <1-2-3> <formatsource> <cheminsource> <formatdest> <chemindest>``
  
Remplacer ``<formatsource>`` et ``<formatdest>`` avec le format de source et celui de destination, les formats possibles sont AMC, XMLMOODLE et GIFT.
Remplacer ``<cheminsource>`` ``<chemindest>`` avec les chemins de la source et celui de la destination, le chemin de destination ne doit pas forcement exister. Attention, on ne supporte que des chemins relatifs pour l’instant !

La partie ``-v(--verbose) <1-2-3>`` est optionnelle et peut ne pas paraitre comme argument de la commande. Le verbose indique le niveau de log qu'on veut avoir pendant l'exécution. Par défaut la valeur est à 1 :
1 = logs minimums, indique uniquement début et fin de chaque étape de conversion.
2 = logs moyens, indique aussi le nombre de questions détectés et leur type.
3 = logs maximums, indique aussi des éventuels erreurs de syntaxe trouvés pendant la lecture du fichier source.

## Help

Affiche le manuel du logiciel
``-h (--help)``

## Analyse

Permet de faire uniquement une analyse de la syntaxe d'un fichier source en un format spécifique.

``-a (--analyze) <formatsource> <cheminsource>``

Remplacer ``<formatsource>``  avec le format, les formats possibles sont AMC, XMLMOODLE et GIFT.
Remplacer ``<cheminsource>`` avec les chemins de la source. Attention, on ne supporte que des chemins relatifs pour l’instant !
Nous n'avons pas de niveau de verbosité pour cette commande, on suppose que le niveau est toujours au maximum (sinon on n'aurait aucun intérêt en ne montrant pas les erreurs à fin d'analyse.

-----------------------------------------

# Auteur

* Ahmed Saad El Din

# Tuteur

* Gaetan Rey
-----------------------------------------

## Licence

Ce projet est licencié sous la licence Apache 2.0.
