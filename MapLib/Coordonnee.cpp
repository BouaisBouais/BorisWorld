#include "Coordonnee.h"

int Coordonnee::tailleCarte = 1;

Coordonnee::Coordonnee(){
	X=1;
	Y=1;
}
Coordonnee::Coordonnee(int x, int y){
	X = (x < 1) ? 1 : ((x > tailleCarte) ? tailleCarte : x);
	Y = (y < 1) ? 1 : ((y > tailleCarte) ? tailleCarte : y);
}

Coordonnee::Coordonnee(int* array){
	int x = array[0];
	int y = array[1];
	X = (x < 1) ? 1 : ((x > tailleCarte) ? tailleCarte : x);
	Y = (y < 1) ? 1 : ((y > tailleCarte) ? tailleCarte : y);
}

void Coordonnee::setX(int x){
	X = (x < 1) ? 1 : ((x > tailleCarte) ? tailleCarte : x);
}

void Coordonnee::setY(int y){
	Y = (y < 1) ? 1 : ((y > tailleCarte) ? tailleCarte : y);
}

Coordonnee Coordonnee::decaler(int x, int y){
	return Coordonnee(X+x, Y+y);
}

int* Coordonnee::toArray(){
	int* retour = new int[2];
	retour[0] = X;
	retour[1] = Y;
	return retour;
}