#include "MapLib.h"
#include <time.h>
#include <stdlib.h>

int** MapLib::genererMap(int taille){
	int nbTypes = MapLib::typeCases::PLAINE;
	srand (time(NULL));
	
	int** retour = NULL;
	retour = new int* [taille];
	for(int x=0; x<taille; x++){
		retour[x] = new int [taille];
		for(int y=0; y<taille; y++){
			int random = rand() % nbTypes;
			retour[x][y] = random;
		}
	}

	return retour;
}

MapLib* MapLib_new() { return new MapLib(); }
void MapLib_delete(MapLib* maplib) { delete maplib; }

int** MapLib_genererMap(MapLib* maplib, int taille){ return maplib->genererMap(taille); }