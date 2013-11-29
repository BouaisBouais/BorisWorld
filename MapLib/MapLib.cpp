#include "MapLib.h"
#include <time.h>
#include <stdlib.h>

/**
* Génère la carte de taille taille et la retourne sous tableau d'int
* @param int taille La taille de la carte en tiles
* @return int** Le tableau représentant la carte
*/
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

/**
* Rend la positions de départ des deux joueurs, en tiles.
* Le retour est ordonné de la manière suivante : 
   - int[0][0] : joueur1.x
   - int[0][1] : joueur1.y
   - int[1][0] : joueur2.x
   - int[1][1] : joueur2.y
   @return int** La position de départ en tiles
*/
int** MapLib::posJoueurs(){
	int* joueur1 = new int[2];
	int* joueur2 = new int[2];
	int** positions = NULL;
	positions = new int* [2];
	srand(time(NULL));

	int deltaRand = 2;

	//TODO: Définir positions de départ des joueurs. Ne pas commencer dans de l'eau.
	bool posJoueur1 = false;
	while(!posJoueur1){
		//Le joueur peut être au nord, est, sud ou ouest
		int pos = rand()*4;
		int x;
		int y;
		switch(pos)
		{
		case 0:
		default:
			y = 1 + rand() * deltaRand;
			x = rand() * tailleCarte + 1;
			break;
		case 1:
			x = tailleCarte - rand() * deltaRand;
			y = rand() * tailleCarte + 1;
			break;
		case 2:
			y = tailleCarte - rand() * deltaRand;
			x = rand() * tailleCarte + 1;
			break;
		case 3:
			x = 1 + rand() * deltaRand;
			y = rand() * tailleCarte + 1;
			break;
		}
		if(carte[x-1][y-1] != MapLib::typeCases::EAU){
			joueur1[0] = x;
			joueur1[1] = y;
			posJoueur1 = true;
		}
	}

	bool posJoueur2 = false;
	//TODO : Risque peut-être d'être trop proche du centre parfois.
	while(!posJoueur2){
		int deltaPlusMoins = rand() * 2 - 1; //On ajoute ou on retrait
		int x = tailleCarte - joueur1[0] + 1 + deltaRand * deltaPlusMoins;
		int y = tailleCarte - joueur1[1] + 1 + deltaRand * deltaPlusMoins;

		if(carte[x-1][y-1] != MapLib::typeCases::EAU){
			joueur2[0] = x;
			joueur2[1] = y;
			posJoueur2 = true;
		}
	}
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