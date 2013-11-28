#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL MapLib {
private :
	bool init;
	int tailleCarte;
	int** carte;

public:
	static enum typeCases{
		DESERT = 0,
		EAU,
		FORET,
		MONTAGNE,
		PLAINE
	};

	MapLib() {};
	~MapLib() {};
	int** genererMap(int taille);
	int** posJoueurs(); 
	int** suggestions(int peuple, int* coords);
};

// A ne pas implémenter dans le .h !
EXTERNC DLL MapLib* MapLib_new();
EXTERNC DLL void MapLib_delete(MapLib* maplib);
EXTERNC DLL int** MapLib_genererMap(MapLib* maplib, int taille);
EXTERNC DLL int** MapLib_posJoueurs(MapLib* maplib);
EXTERNC DLL int** MapLib_suggestions(MapLib* maplib, int peuple, int* coords);