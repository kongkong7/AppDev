#include "TestClass.h"

BaseClass::BaseClass()
{
	cout << "======= BaseClass ���� =======" << endl;
}

BaseClass::~BaseClass()
{
	cout << "======= BaseClass �Ҹ� =======" << endl;
}

string BaseClass::GetString()
{
	__mStr = "BaseClass";
	return __mStr;
}

TestClass::TestClass()
{
	cout << "======= TestClass ���� =======" << endl;
	__mSum	= 0;
	__mVal1	= 0;
	__mVal2	= 0;
}

TestClass::~TestClass()
{
	cout << "======= TestClass �Ҹ� =======" << endl;
	__mSum	= 0;
	__mVal1	= 0;
	__mVal2	= 0;
}

int TestClass::Sum( int pVal1, int pVal2 )
{
	__mSum = pVal1 + pVal2;
	return __mSum;
}

string TestClass::GetString()
{
	__mStr = "TestClass";
	return __mStr;
}