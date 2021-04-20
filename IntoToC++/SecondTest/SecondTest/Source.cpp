#include <iostream>

int main()
{
	//a lot of things have 2 ways to do things: the c way and the c++ way
	//c++ is backwards compatable with c

	printf("yo %i, %f\n", 100, 200.01f); //C-style (more error prone)
	//parameters here: https://www.cplusplus.com/reference/cstdio/printf/

	std::cout << "yo " << "again " << 100 << ", " << 200.01f << std::endl; //C++ style
	//parameters are scuffed, google it

	char string[5]; //size should be number of characters + 1 (one extra for null terminator)
	string[0] = 'C'; string[1] = 'a'; string[2] = 't'; //string[3] = '\0';
	//null terminated:
	//without the hidden 0 value it will keep going until it finds a \0
	strcpy_s(string, 5, "yoo");
	strcat_s(string, 5, "!"); //if length is more than 5 than you will overwrite ram 

	//CAVEOTS OF A NULL TERMINATED STRING:
	//has a set size
	// string == "hello!" does not return a correct value
	//have to use functions to change value
	//have to use functions to find length

	std::cout << string << std::endl;

	if (strcmp(string, "yoo!") == 0) //strcmp returns a non zero value if they are different
	{
		std::cout << "tru" << std::endl;
	}
	std::string myString = "Hello!"; //C# style
	//not null terminated

	//std is a namespace (standard)
	// std::endl is the same as '\n'
	// bitshift operator << is overloaded

	//could write 'using namespace std' at the top
		//this is viewed as dodgy because if two namespace function have the same name then it will not compile properly
	
	char input[64];
	std::cin >> input;
	std::cout << input;
	//dodgy way to stop program
	//basically types pause into an invisible console
	//system("pause");

	//less dodgy way
	getchar();

	return 0;
}