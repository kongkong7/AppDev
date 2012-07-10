#include <iostream>
#include <string>

using namespace std;

class BaseClass
{
public:
	BaseClass();
	~BaseClass();

	string GetString();
private:
	string __mStr;
};

class TestClass : public BaseClass
{
public:
	TestClass();
	~TestClass();

	int Sum(int pVal1, int pVal2);
	string GetString();

private:
	int __mSum;
	int __mVal1;
	int __mVal2;
	string __mStr;
};
