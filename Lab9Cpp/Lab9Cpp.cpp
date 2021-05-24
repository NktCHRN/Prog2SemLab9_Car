#include "ConsoleProjectLib.h"
#include "StringFunctions.h"

using namespace std;

int main()
{
    ProgramInfo();
    cout << endl;
    string str;
    int (*ptrToFunc)(string, char) = FindLastSymbol;                            // pointer to function
    cout << "Enter your string: " << endl;
    getline(cin, str);
    bool run = false;
    do 
    {
        run = false;
        char symbol;
        cout << "\nEnter the symbol you want to find: " << endl;
        cin >> symbol;
        cin.clear();
        cin.ignore(std::numeric_limits<streamsize>::max(), '\n');
        int index = (*ptrToFunc)(str, symbol);
        if (index != -1)
            cout << "Index of your symbol (starting from 0): " << index << endl;
        else
            cout << "Your symbol was not found" << endl;
        cout << "\nDo you want to enter one more symbol? [Y/n]" << endl;
        cin >> symbol;
        while (tolower(symbol) != 'n' && tolower(symbol) != 'y') 
        {
            cin.clear();
            cin.ignore(std::numeric_limits<streamsize>::max(), '\n');
            cout << "You entered the wrong letter" << endl;
            cout << "Do you want to enter one more symbol? [Y/n]" << endl;
            cin >> symbol;
        }
        cin.clear();
        cin.ignore(std::numeric_limits<streamsize>::max(), '\n');
        if (tolower(symbol) == 'y')
            run = true;
    } while (run);
}