Feature: RechercheLivreTitre

@mytag
Scenario: Recherche Livre par titre
	Given je saisis la valeur "Germinal"
	Then le nombre de colonne est 1