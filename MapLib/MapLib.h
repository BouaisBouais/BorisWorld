#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

class DLL MapLib {
public:
	static enum typeCases{
		DESERT,
		EAU,
		FORET,
		MONTAGNE,
		PLAINE
	};

	MapLib() {};
	~MapLib() {};
	int** genererMap(int taille);
};

// A ne pas implémenter dans le .h !
EXTERNC DLL MapLib* MapLib_new();
EXTERNC DLL void MapLib_delete(MapLib* maplib);
EXTERNC DLL int** MapLib_genererMap(MapLib* maplib, int taille);