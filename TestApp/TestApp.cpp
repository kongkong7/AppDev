// TestApp.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.

#include "stdafx.h"
#include <iostream>
#include <string>

//#include "TestClass.h"

using namespace std;

class BaseClass
{
public:
	BaseClass();
	BaseClass(int pVal);
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

BaseClass::BaseClass()
{
	cout << "======= BaseClass 생성 =======" << endl;
}

BaseClass::BaseClass(int pVal)
{
	cout << "======= BaseClass 복사 생성 =======" << endl;
	cout << pVal << endl;
}

BaseClass::~BaseClass()
{
	cout << "======= BaseClass 소멸 =======" << endl;
}

TestClass::TestClass() : BaseClass( 10 )
{
	cout << "======= TestClass 생성 =======" << endl;
	__mSum	= 0;
	__mVal1	= 0;
	__mVal2	= 0;
}

TestClass::~TestClass()
{
	cout << "======= TestClass 소멸 =======" << endl;
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

enum EFnlFwCTr
{
	eFnlFwCTrStx=1000,
	// 가상TR. => Troc가 아닌 Primary가 바로 처리함.
	eCTrFireRevolverReq,

	eCTrEchoReq=eFnlFwCTrStx+100,	//Echo.
};

#define	DISABLE_COPY_ASSIGN(pClass)															\
		private:																			\
			pClass(const pClass##&)															\
			{																				\
			}																				\
			pClass##& operator=(const pClass##&)											\
			{																				\
				return(*this);																\
			}
#define	DISABLE_COPY_ASSIGN_DEF(pClass)														\
			pClass()																		\
			{																				\
			}																				\
		private:																			\
			pClass(const pClass##&)															\
			{																				\
			}																				\
			pClass##& operator=(const pClass##&)											\
			{																				\
				return(*this);																\
			}

class CTrHead
{
public:
	explicit CTrHead(unsigned short pTr=0);
	//DISABLE_COPY_ASSIGN(CTrHead);
public:
	unsigned short			GetTr(void) const;
	void					SetHead(unsigned short pTr);
#ifdef	_DEBUG
	void					ResetHead(void)		{__mTr = 0;}
	bool					IsValidHead(void)	{return(0 != __mTr);}
#endif
private:
	unsigned short			__mTr;
};

#define	FNL_ASSERT(pExp, pMsg)																	\
{																								\
	bool	___aExp = static_cast<bool>(pExp);													\
	if(false == ___aExp)																		\
	{																							\
	}																							\
}
#define	SIZE_OF(x)		(sizeof(x)/sizeof(x[0]))
#define	STRCPYA(pDest, pStr, pDestSz)		FNL_ASSERT(0 == ::strcpy_s(pDest, pDestSz, pStr),				"Invalid!")
#define	FNL_STRINGA(pMsg, pSz)	FNL_ASSERT((NULL != pMsg) && ('\0'  != pMsg[0]) && (0 < pSz),				"Invalid string!");
#define	MAKE_STRINGA(x)	(x[SIZE_OF(x)-1] = '\0')

#define	TRINFOEX(pCTr,pIsCommon,pIsEncode)		static	unsigned short	Tr(void)		\
												{										\
													return(e##pCTr);					\
												}										\
												static bool			IsCommon(void)		\
												{										\
													return(pIsCommon);					\
												}										\
												static bool			IsEncode(void)		\
												{										\
													return(pIsEncode);					\
												}										\
												static const char*	TrDesc(void)		\
												{										\
													return("e"#pCTr);					\
												}										\
												pCTr() : CTrHead(Tr()) {NULL;}

#define	TRINFO(pCTr,pIsCommon,pIsEncode)		TRINFOEX(pCTr,pIsCommon,pIsEncode)		\
												DISABLE_COPY_ASSIGN(pCTr);

CTrHead::CTrHead(unsigned short pTr) : __mTr(pTr)
{
	FNL_ASSERT(0 != pTr,	"Invalid parameter!");
}
unsigned short CTrHead::GetTr(void) const
{
	FNL_ASSERT(0 != __mTr,	"Invalid!");
	return (__mTr);
}
void	CTrHead::SetHead(unsigned short pTr)
{
	FNL_ASSERT(0 != pTr,	"Invalid parameter!");
	__mTr = pTr;
}

class CTrEchoReq : public CTrHead
{
public:
	TRINFO(CTrEchoReq, true, false);
public:
	void					Set(const char* pMsg);
	unsigned int			GetSndLng(void) const;			// 실제 송신할 길이 -> Set() 한 후 호출되어야 함
	unsigned int			CheckIntegrity(void) const;
	const char*				GetMsg(void) const;
private:
	mutable char			__mMsg[255+1];					// NULL을 반드시 포함해야 한다.
};

void CTrEchoReq::Set(const char* pMsg) 
{
	FNL_STRINGA(pMsg, SIZE_OF(__mMsg));
	SetHead(Tr());
	STRCPYA(__mMsg, pMsg, SIZE_OF(__mMsg));
}
unsigned int CTrEchoReq::GetSndLng(void) const
{
	return(static_cast<unsigned int>(sizeof(*this)));
}
unsigned int CTrEchoReq::CheckIntegrity(void) const
{
	MAKE_STRINGA(__mMsg);
	return(0);
}

const char* CTrEchoReq::GetMsg(void) const 
{
	return (__mMsg);
}

class kong
{
public:
	static	unsigned short	Tr(void)
	{
		return(17);
	}
	static bool			IsCommon(void)
	{
		return(true);
	}
	static bool			IsEncode(void)
	{
		return(false);
	}
	static const char*	TrDesc(void)
	{
		return("eTrkong");
	}	
	
private:
	char a;
};

int _tmain(int argc, _TCHAR* argv[])
{
	CTrEchoReq aTr;

	cout << sizeof(aTr) << endl;
	cout << aTr.IsCommon() << endl;
	cout << aTr.IsEncode() << endl;

	kong aKong;
	cout << sizeof(aKong) << endl;
	cout << aKong.IsCommon() << endl;
	cout << aKong.IsEncode() << endl;
	cout << aKong.TrDesc() << endl;
	//cout << "======= TestApp Start =======" << endl;

	//TestClass aClass;

	//int aSum = aClass.Sum(10, 20);
	//string aStr = aClass.GetString();

	//cout << "Sum : " << aSum << endl;
	//cout << "String : " << aStr << endl;

	//cout << "======= TestApp End =======" << endl;

	return 0;
}

