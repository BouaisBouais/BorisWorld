#ifndef __WRAPPER__
#define __WRAPPER__

//#include "../MapLib/MapLib.h" // A changer
#include "../MapLib/MapLib.h"
//#pragma comment(lib, "../Debug/MapLib.lib") // A changer
#pragma comment(lib, "MapLib.lib") // A changer
#include <string>

#include <msclr\marshal_cppstd.h>

using namespace System;

namespace Wrapper {

	public ref class WrapperMap {
		private:
			MapLib* map;
		public:

			WrapperMap(){ map = MapLib_new(); }
			~WrapperMap(){ MapLib_delete(map); }
			int** genererMap(int taille, String^ chaineAleatoire)
			{ 
				msclr::interop::marshal_context context;
				std::string standardString = context.marshal_as<std::string>(chaineAleatoire);
				return MapLib_genererMap(map, taille, standardString);
			}
			int** posJoueurs() { return MapLib_posJoueurs(map);}
			int** suggestions(int peuple, int x, int y) { return MapLib_suggestions(map, peuple, x, y);}
	};
}
#endif