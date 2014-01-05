#ifndef __WRAPPER__
#define __WRAPPER__

//#include "../MapLib/MapLib.h" // A changer
#include "../MapLib/MapLib.h"
//#pragma comment(lib, "../Debug/MapLib.lib") // A changer
#pragma comment(lib, "MapLib.lib") // A changer

using namespace System;

namespace Wrapper {

	public ref class WrapperMap {
		private:
			MapLib* map;
		public:

			WrapperMap(){ map = MapLib_new(); }
			~WrapperMap(){ MapLib_delete(map); }
			int** genererMap(int taille) { return MapLib_genererMap(map,taille);}
			int** posJoueurs() { return MapLib_posJoueurs(map);}
			int** suggestions(int peuple, int x, int y) { return MapLib_suggestions(map, peuple, x, y);}
	};
}
#endif