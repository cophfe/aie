
int main() 
{
	//Integer type (signed)
	bool myBool = 18446744073709551615; //1 byte guaranteed, any non zero number will return zero
	char myChar = 53; //you get the idea
	short myShort = 32767; //2 bytes
	int myInt = 2147483647; //4 bytes
	long myLong = 2147483647; //4 bytes (old and unused in new software)
	long long myLongLong = 18446744073709551615 / 2; //8 bytes
	
	//Floating point types (signed)
	float myFloat = 0.5f; //4 bytes
	double myDouble = 0.5; //8 bytes

	//unsigned
	unsigned int myUnsignedInt = 4;
	//all primative types except float, double and bool can be unsigned

	//Arrays
	int myArray[3] = { 1,2,3 };
	int myArray2D[3][2] = { {0,1}, {2,3} };

	//'string'
	const char myString[] = "string";
	return 0; //Program exited correctly 
				//(non zero num indicates error, although windows doesn't use it anyway so it does not matter)
}