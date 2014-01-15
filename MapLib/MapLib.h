#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#ifndef MAPLIB
#define MAPLIB
#include "Coordonnee.h"
#include <string>

class DLL MapLib {
private :
	int nbTypes;
	bool init;
	int tailleCarte;
	int** carte;
	bool bordEau(Coordonnee coords);
	// utilisé pour charger une carte
	int positionX;
	int positionY;
	int tailleCourante;

public:
	static enum typeCases{
		DESERT = 0,
		EAU,
		FORET,
		MONTAGNE,
		PLAINE
	};
	static enum typeUnite{
		VIKING = 0,
		GAULOIS,
		NAIN
	};

	MapLib() {};
	~MapLib() {};
	int** genererMap(int taille, std::string chaine);
	int** genererMapTest();
	int** posJoueurs(); 
	int** suggestions(int peuple, int x, int y);
	int getCase(Coordonnee coords);
};

// A ne pas implémenter dans le .h !
EXTERNC DLL MapLib* MapLib_new();
EXTERNC DLL void MapLib_delete(MapLib* maplib);
EXTERNC DLL int** MapLib_genererMap(MapLib* maplib, int taille, std::string chaine);
EXTERNC DLL int** MapLib_posJoueurs(MapLib* maplib);
EXTERNC DLL int** MapLib_suggestions(MapLib* maplib, int peuple, int x, int y);

#endif