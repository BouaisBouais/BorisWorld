#ifndef COORDONNEE
#define COORDONNEE
class Coordonnee{
private:
	int X;
	int Y;
	static int tailleCarte;
public:
	Coordonnee();
	Coordonnee(int x, int y);
	Coordonnee(int* array);
	static void initialise(int taille){ tailleCarte = taille; };
	int getX(){ return X; };
	int setX(int x);
	int getY(){ return Y; };
	int setY(int y);
	Coordonnee decaler(int x, int y);
	int* toArray();
};
#endif