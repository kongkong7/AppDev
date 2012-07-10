// 기본 DLL 파일입니다.

#include "stdafx.h"

#include "TestWrap.h"

namespace TestWrap
{
	TestClassWrap::TestClassWrap() : _mTestLib( new TestClass )
	{}

	TestClassWrap::~TestClassWrap()
	{
		if( _mTestLib )
		{
			delete _mTestLib;
			_mTestLib = 0;
		}
	}

	int TestClassWrap::Sum( int pVal1, int pVal2 )
	{
		return _mTestLib->Sum( pVal1, pVal2 );
	}

	string TestClassWrap::GetString()
	{
		return _mTestLib->GetString();
	}
}