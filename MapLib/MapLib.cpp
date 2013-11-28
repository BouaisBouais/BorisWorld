#include "MapLib.h"
#include <time.h>
#include <stdlib.h>

int** MapLib::genererMap(int taille){
	int nbTypes = MapLib::typeCases::PLAINE;
	srand (time(NULL));
	
	carte = NULL;
	carte = new int* [taille];
	for(int x=0; x<taille; x++){
		carte[x] = new int [taille];
		for(int y=0; y<taille; y++){
			int random = rand() % nbTypes;
			carte[x][y] = random;
		}
	}

	tailleCarte = taille;
	init = true;

	return carte;
}

int** MapLib::posJoueurs(){
	int* joueur1 = new int[2];
	int* joueur2 = new int[2];
	int** positions = NULL;
	positions = new int* [2];

	//TODO: Définir positions de départ des joueurs. Ne pas commencer dans de l'eau.
	joueur1[0] = 1;
	joueur1[1] = 1;
	joueur2[0] = tailleCarte;
	joueur2[1] = tailleCarte;
	//Fin définition des positions

	positions[0] = joueur1;
	positions[1] = joueur2;

	return positions;
}

//En fonction du peuple de l'unité et de sa position actuelle, voir si elle ne serait pas mieux à proximité
//Retour : int[0][0] : NB de propositions, int[1][0] : X, int[1][1] : Y, int[2][0] : X, int[2][1] : Y, etc...
int** MapLib::suggestions(int peuple, int* coords){
	int** suggestions = NULL;

	//TODO : Coder la recherche de suggestions !
	int* suggestion1 = new int[2];
	suggestion1[0] = 1;
	suggestion1[1] = 1;

	int nbSuggestions = 1;
	suggestions = new int*[nbSuggestions+1];

	suggestions[0] = new int[1];
	suggestions[0][0] = nbSuggestions;

	suggestions[1] = suggestion1;

	return suggestions;
}

MapLib* MapLib_new() { return new MapLib(); }
void MapLib_delete(MapLib* maplib) { delete maplib; }

int** MapLib_genererMap(MapLib* maplib, int taille){ return maplib->genererMap(taille); }
int** MapLib_posJoueurs(MapLib* maplib){ return maplib->posJoueurs(); }
int** MapLib_suggestions(MapLib* maplib, int peuple, int* coords){ return maplib->suggestions(peuple, coords); }