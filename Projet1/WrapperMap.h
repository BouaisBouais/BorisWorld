#ifndef __WRAPPER__
#define __WRAPPER__

#include "../MapLib/MapLib.h" // A changer
#pragma comment(lib, "../Debug/MapLib.lib") // A changer

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
			int** suggestions(int peuple, int* coords) { return MapLib_suggestions(map, peuple, coords);}
	};
}
#endif