// TestWrap.h

#pragma once

#include <iostream>
#include <string>

#include "TestClass.h"

using namespace System;

namespace TestWrap
{
	public ref class TestClassWrap
	{
	protected :
		TestClass* _mTestLib;

	public :
		TestClassWrap();
		virtual ~TestClassWrap();

		int Sum( int pVal1, int pVal2 );
		string GetString();
	};
}
