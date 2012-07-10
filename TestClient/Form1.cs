using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using TestWrap;

namespace TestClient
{
    public partial class Form1 : Form
    {
        public static Socket socket;
        public static byte[] getbyte = new byte[1024];
        public static byte[] setbyte = new byte[1024];

        public const int sPort = 11004;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TestClassWrap aWrap = new TestClassWrap();

            int aSum = aWrap.Sum(1, 2);

            //string aStr = aWrap.GetString();

            aSum = aSum + Convert.ToInt32(FnlFw.EConstBase.eSzUserId);

            MessageBox.Show( aSum.ToString() );
            

            /*
            string sendstring = null;
            string getstring = null;

            IPAddress serverIP = IPAddress.Parse( "10.95.1.129" );
            IPEndPoint serverEndPoint = new IPEndPoint(serverIP, sPort);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(serverEndPoint);

            if (socket.Connected)
            {
                MessageBox.Show("연결되었습니다.");

                socket.Receive(getbyte);
                int a;
            }
            */
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

namespace FnlFw
{
    enum EConstBase
    {
        eSzUserId = 20,
        eSzUserPswd = 20,
    };

    enum EFnlFwCTr
    {
        eCTrLoginMgrReq,
    };

    class CTrHead
    {
        private ushort __mTr;

        public CTrHead(ushort pTr) { __mTr = pTr; }
        public ushort GetTr() { return 0; }
        public void SetHead(ushort pTr) { return; }
        public void ResetHead() { __mTr = 0; }
        public bool IsValidHead() { return (0 != __mTr); }
    };

    class CTrEchoReq : CTrHead
    {
    public:
	    TRINFO(CTrEchoReq, true, false);
    public:
	    void					Set(char* pMsg);
	    unsigned int			GetSndLng(void);			// 실제 송신할 길이 -> Set() 한 후 호출되어야 함
	    unsigned int			CheckIntegrity(void);
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

    class CTrLoginMgrReq : CTrHead
    {
        public ushort Tr()
        {
            return ( (ushort)EFnlFwCTr.eCTrLoginMgrReq );
        }

        CTrLoginMgrReq() : base(10)
        {
            __mUserId = new byte[(int)EConstBase.eSzUserId + 1];
            __mUserPswd = new byte[(int)EConstBase.eSzUserPswd + 1];
        }

        //static bool IsCommon()
        //{
        //    return (pIsCommon);
        //}
        //static bool IsCommon(bool pIsCommon)
        //{
        //    return (pIsCommon);
        //}
        //static bool IsEncode()
        //{
        //    return (pIsEncode);
        //}
        static byte[] TrDesc()
        {
            return System.Text.Encoding.Default.GetBytes( "eCTrLoginMgrReq" );
        }

        public void Set(byte[] pUserId, byte[] pUserPswd)
        {
            base.SetHead(Tr());

            //System.Buffer.BlockCopy(  )
            // __mUserId
            //STRNCPYA(__mUserId,		pUserId,	SIZE_OF(__mUserId));
            //STRNCPYA(__mUserPswd,	pUserPswd,	SIZE_OF(__mUserPswd));
        }
        //public uint	GetSndLng()
        //{
        //    return(static_cast<uint>(sizeof(*this)));
        //}
        //public uint	CheckIntegrity(VOID) const
        //{
        //    MAKE_STRINGA(__mUserId);
        //    MAKE_STRINGA(__mUserPswd);
        //    return(0);
        //}
        //public byte[] GetUserId(VOID) const 
        //{
        //    return (__mUserId);
        //}
        //public byte[] GetUserPswd(VOID) const 
        //{
        //    return (__mUserPswd);
        //}

        private byte[] __mUserId;
        private byte[] __mUserPswd;

        //private byte[] __mUserId
        //{
        //    get { return __mUserId; }
        //    //set { __mUserId = value; }
        //}
        //private byte[] __mUserPswd
        //{
        //    get { return __mUserPswd; }
        //    //set { __mUserPswd = value; }
        //}
    };
};
