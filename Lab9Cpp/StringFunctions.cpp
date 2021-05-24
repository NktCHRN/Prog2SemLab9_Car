#include "StringFunctions.h"

using namespace std;

int FindLastSymbol(string str, char symbol)			// finds the last entry of symbol, return index or -1 if not found
{
	int size = str.size();
	const int notFound = -1;
	for (int i = size - 1; i >= 0; i--)
		if (str[i] == symbol)
			return i;
	return notFound;
}
