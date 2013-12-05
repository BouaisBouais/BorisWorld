#include "MapLib.h"
#include <time.h>
#include <stdlib.h>
#include <cmath>
#include "Coordonnee.h"

/**
* Génère la carte de taille taille et la retourne sous tableau d'int
* @param int taille La taille de la carte en tiles
* @return int** Le tableau représentant la carte
* @todo Donner des valeurs d'apparitions aux cases, sinon les vikngs sont meilleurs
*/
int** MapLib::genererMap(int taille){
	int nbTypes = MapLib::typeCases::PLAINE;
	srand (time(NULL));
	Coordonnee::initialise(taille);
	
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

	bool posJoueur1 = false;
	while(!posJoueur1){
		//Le joueur peut être au nord, est, sud ou ouest
		int pos = rand()*4;
		Coordonnee coords;
		switch(pos)
		{
		case 0:
		default:
			coords.setY(1 + rand() * deltaRand);
			coords.setX(rand() * tailleCarte + 1);
			break;
		case 1:
			coords.setX(tailleCarte - rand() * deltaRand);
			coords.setY(rand() * tailleCarte + 1);
			break;
		case 2:
			coords.setY(tailleCarte - rand() * deltaRand);
			coords.setX(rand() * tailleCarte + 1);
			break;
		case 3:
			coords.setX(1 + rand() * deltaRand);
			coords.setY(rand() * tailleCarte + 1);
			break;
		}
		if(getCase(coords) != MapLib::typeCases::EAU){
			joueur1 = coords.toArray();
			posJoueur1 = true;
		}
	}

	bool posJoueur2 = false;
	//TODO : Risque peut-être d'être trop proche du centre parfois.
	while(!posJoueur2){
		int deltaPlusMoins = rand() * 2 - 1; //On ajoute ou on retrait
		Coordonnee coords;
		coords.setX(tailleCarte - joueur1[0] + 1 + rand() * deltaRand * deltaPlusMoins);
		coords.setY(tailleCarte - joueur1[1] + 1 + rand() * deltaRand * deltaPlusMoins);

		if(getCase(coords) != MapLib::typeCases::EAU){
			joueur2 = coords.toArray();
			posJoueur2 = true;
		}
	}
	//Fin définition des positions

	positions[0] = joueur1;
	positions[1] = joueur2;

	return positions;
}

//En fonction du peuple de l'unité et de sa position actuelle, voir si elle ne serait pas mieux à proximité
int** MapLib::suggestions(int peuple, int* coords){
	int** suggestions = NULL;
	int precision = 1; //A une case de la case actuelle
	suggestions = new int* [3]; //Jusqu'à 3 propositions
	for(int i=0; i<3; i++){
		suggestions[i] = new int[2]; //X,Y
		for(int j=0; j<2; j++)
			suggestions[i][j] = 0;
	}
	
	int x = coords[0];
	int y = coords[1];

	Coordonnee coordonnees = Coordonnee(coords);

	//On commence par vérifier que l'on n'est pas sur une case au max de nos points
	switch(peuple){
	case MapLib::typeUnite::GAULOIS :
		if(getCase(coordonnees) == MapLib::typeCases::PLAINE)
			return suggestions;
		break;
	case MapLib::typeUnite::NAIN :
		if(getCase(coordonnees) == MapLib::typeCases::FORET)
			return suggestions;
		break;
	case MapLib::typeUnite::VIKING :
		if(bordEau(coordonnees))
			return suggestions;
		break;
	}

	//TODO : Coder la recherche de suggestions !
	//On parcoure les cases à proximité de notre case
	for(int i=-precision; i<precision+1; i++){
		for(int j=-precision; j<precision+1; j++){
			Coordonnee testCoords = coordonnees.decaler(i,j);
			bool interesting = false;

			switch(peuple){
			case MapLib::typeUnite::GAULOIS :
				if(getCase(testCoords) == MapLib::typeCases::PLAINE)
					interesting = true;
				break;
			case MapLib::typeUnite::NAIN :
				if(getCase(testCoords) == MapLib::typeCases::FORET)
					interesting = true;
				break;
			case MapLib::typeUnite::VIKING :
				if(bordEau(testCoords))
					interesting = true;
				break;
			}

			//Si la case est intéressante et moins loin que les autres, on la prend !
			if(interesting){
				int testDistance = std::abs(i) + std::abs(j);
				for(int i=0; i<3; i++){
					int tempX = suggestions[i][0];
					int tempY = suggestions[i][1];
					int distance = std::abs(tempX - x) + std::abs(tempY - y);
					if(testDistance < distance){
						for(int j=2; j>i; j--){ //On décale les suggestions
							suggestions[j] = suggestions[j-1];
						}
						//On mets celle-ci à la place
						suggestions[i] = testCoords.toArray();
					}
				}
			} //endif(interesting)
		}
	}
	return suggestions;
}

int MapLib::getCase(Coordonnee coords){
	return carte[coords.getX()-1][coords.getY()-1];
}

bool MapLib::bordEau(Coordonnee coords){
	return ( (coords.getX > 1 && getCase(coords.decaler(-1,0)) == MapLib::typeCases::EAU) ||
		(coords.getY > 1 && getCase(coords.decaler(0,-1)) == MapLib::typeCases::EAU) ||
		(coords.getX < tailleCarte && getCase(coords.decaler(1,0)) == MapLib::typeCases::EAU) ||
		(coords.getY < tailleCarte && getCase(coords.decaler(0,1)) == MapLib::typeCases::EAU));
}

MapLib* MapLib_new() { return new MapLib(); }
void MapLib_delete(MapLib* maplib) { delete maplib; }

int** MapLib_genererMap(MapLib* maplib, int taille){ return maplib->genererMap(taille); }
int** MapLib_posJoueurs(MapLib* maplib){ return maplib->posJoueurs(); }
int** MapLib_suggestions(MapLib* maplib, int peuple, int* coords){ return maplib->suggestions(peuple, coords); }