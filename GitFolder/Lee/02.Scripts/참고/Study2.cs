using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices; // 마샬링 관련
using System;   // IntPtr 관련

/*
 
    마샬링 : 다른 언어에서 만든 dll을 쓰기 위해서 마샬링이 필요. 즉, 다른 언어에서 읽을 수 있게 해주는 작업. 참고로 마샬링 방법은 많다.

            마샬링이란 어떤 언어(C#)로 작성된 프로그램의 출력 매개변수들(Object, Struct, Data 등)을 다른 언어(C++)로 작성된 프로그램의 입력으로 전달해야 하는 경우에 필요하다.

            언마샬링(Unmarshalling)은 이러한 마샬링 개념으로 묶어진 데이터, 
            즉. 마샬링을 통해 보내진 데이터들을 원래 구조(묶음 풀기)로 복원시키는 것이다.

            이러한 의미에서 개체 입출력을 위해 개체를 직렬화(Serialize)하고 복원(Deserialize)하는 과정과 비슷하다.
            마샬링과 언마샬링은 단순한 데이터의 직렬화가 아니라, 구조화된 대상들에 대해서 구조 해체/복원이 개입할 때 사용하는 개념이라는 점이 다르다.

            ex) C++ 로 작성된 프로그램에서 객체를 저장하거나 전송할 수 있는 형태로 묶어(마샬링) 다른 환경인 C#(닷넷)으로 전달하여
            해당 C# 프로그램에서 마샬링으로 전달된 데이터를 다시 복원(언마샬링)하여 사용하는것이다.

            즉 다시 복원할 수 있어야 마샬링의 의미가 적용되어진다.

            이러한 묶음/풀기의 큰 개념이  마샬링/언마샬링 이며
            C# 에서의 직렬화와 역직렬화가 마샬링 방법중 하나인 것이다.


    StructLayout Attribute를 사용하여 문자셋 설정 및 MarshalAsAttribute를 사용하여 동일한 구조체를 다양한 형식으로 정의.

    UnmanagedType 열거형 

    형식                              설명
    UnmanagedType.BStr	             고정 길이 및 유니코드 문자를 가진 COM 스타일 BSTR이다.
    UnmanagedType.LPStr(기본값)	     null로 끝나는 ANSI 문자 배열에 대한 포인터이다.
    UnmanagedType.LPTStr	         null로 끝나는 플랫폼 종속 문자 배열에 대한 포인터이다.
    UnmanagedType.LPUTF8Str	         null로 끝나는 UTF-8 인코딩 문자 배열에 대한 포인터이다.
    UnmanagedType.LPWStr	         null로 끝나는 유니코드 문자 배열에 대한 포인터이다.
    UnmanagedType.ByValTStr	         고정 길이 문자 배열이다. 배열 형식은 포함하는 구조체의 문자 집합에 의해 결정된다.
    
    (1)
    c++ :
    struct StructStrInfoA
    {
        char *  c1;
        char    c2[128];
    };

    c# :
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    struct StructStrInfoA
    {
        [MarshalAs(UnmanagedType.LPStr)] public String c1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] public String c2;
    }

    (2)
    c++ :
    struct StructStrInfoW
    {
        WCHAR * w1;
        WCHAR   w2[256];
        BSTR    b3;         (참고) Visual C++에서 유니코드 문자열 표시.
    };         
    
    c# :
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct StructStrInfoW
    {
        [MarshalAs(UnmanagedType.LPWStr)] public String w1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public String w2;
        [MarshalAs(UnmanagedType.BStr)] public String b3;
    }

    (3)
    c++ :
    struct StructStrInfoT
    {
        TCHAR * t1;
        TCHAR   t2[256];
    };

    c# :
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct StructStrInfoT
    {
        [MarshalAs(UnmanagedType.LPTStr)] public String t1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)] public String t2;
    }

    나머지는 아래 typestAll 구조체 참고
*/

// 인스펙터에 노출
[System.Serializable]
//[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // 한글 안됨
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)] // 한글 됨
public struct typestAll
{
    //문자열 포인터로 받기 아스키용
    //[MarshalAs(UnmanagedType.LPStr)]
    //public String strTest1;

    ////문자열 포인터로 받기 한글용
    [MarshalAs(UnmanagedType.LPWStr)]
    public String strTest1;

    //문자열 124 아스키용
    //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    //public String strTest2;

    //문자열 256 한글용
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public String strTest2;

    //숫자형
    public int intTest;

    //바이트 배열
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    public byte[] byteTest;

    //유인트형 배열
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public UInt32[] uintTest;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public Int32[] arrys;

    public int raw;
    public int col;
}

public class Study2 : MonoBehaviour
{
    int a;
    public int[] arr1;
    public int[,] arr2;
    public typestAll allTemp = new typestAll();

    //DllImport Attribute는 DLL에 정의된 함수를 호출하기 위해 사용된다.(System.Runtime.InteropServices.DllImport)
    [System.Runtime.InteropServices.DllImport("UnityDllCpp")]
    private static extern int GetNum1();

    [DllImport("UnityDllCpp")]
    private static extern int GetNum2(int a);

    [DllImport("UnityDllCpp")]
    private static extern int GetNum3();

    // 아래처럼...사용하자.
    //[DllImport("UnityDllCpp")]
    //private static extern int[] GetNum4();

    [DllImport("UnityDllCpp")]
    private static extern IntPtr GetNum4();

    [DllImport("UnityDllCpp")]
    // call by reference로 처리해야한 상황에서 포인터가 없으므로 ref 키워드를 사용하여
    // 참조를 받도록 처리
    extern public static void GetNum5(ref typestAll allTemp);

    private void Awake()
    {
        //arr1 = GetNum4(); // 못가져옴
        // 크기를 잡아주자.
        arr1 = new int[100];
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetNum1());
        Debug.Log(GetNum2(5));
        // 초기화 안하면 전역 변수 계속 올라간다...ㅎㅎㅎ 따라서 리셋 관련 함수 만들어 써야함...(유니티 껏다 키면 됨)
        Debug.Log(GetNum3());
        Debug.Log(GetNum3());
        GetNumArr1();
        GetNumArr2();

        // 이렇게 출력하면 에러 및 값을 전부 가져오지 못함.
        //for (int i = 0; i < 100; i++)
        //{
        //    Debug.Log(arr[i]);
        //}
    }

    /*
       플레이어 세팅에서 allow unsafe code 체크
     */
    unsafe void GetNumArr1()
    {
        // using System; 선언, 포인터란...ㅋㅋ
        IntPtr data = GetNum4();
        // 주소값
        data.ToString();
        // 주소값 출력
        Debug.Log(data.ToString());
        // void* => int* 변환... 포인터란...ㅋㅋ
        // 이 부분때문에 unsafe 필요...
        var temp = (int*)data.ToPointer();

        for (int i = 0; i < 100; i++)
        {
            arr1[i] = temp[i];
            Debug.Log(arr1[i]);
        }
        // 참고 
        //fixed (int* temp = &a)
        //{

        //    for (int i = 0; i < 100; i++)
        //    {
        //        arr1[i] = temp[i];
        //        Debug.Log(arr1[i]);
        //    }
        //}
    }

    // 여기선 unsafe 필요없다.
    void GetNumArr2()
    {
        GetNum5(ref allTemp);
        int temp = 0;

        arr2 = new int[allTemp.raw, allTemp.col];

        for (int i = 0; i < allTemp.raw; i++)
        {
            for (int j = 0; j < allTemp.col; j++)
            {
                arr2[i, j] = allTemp.arrys[temp++];
            }
        }

        Debug.Log(arr2[0, 1]);
    }
}

/*

    IntPtr : 
    포인터나 핸들은 이 구조체로 받을수 있다.
    (포인터나 핸들을 나타내는데 사용되는 플랫폼별(=32비트로, 64비트 하드웨어 및 운영 체제=) 형식.)
    상속 => Object => ValueType => IntPtr

    unsafe 한정자 :
    C# 에서도 C++ 처럼 type* 등 포인터를 사용 할 수 있다.
    C#에서는 이런걸 unsafe 코드라고 부른다.
    CLR에서 메모리 관리를 하는데 *을 사용함으로써 메모리를 직접 건드리는것은 매우 위험하다.
    *을 사용하는 코드를 작성하려면 컴파일 옵션으로 /unsafe 를 사용해야 한다.

    비주얼 스튜디오로 순수 C# 프로그래밍인 경우,
    프로젝트 속성 -> 빌드 -> unsafe code 에 체크 해줘야지만 빌드 에러없이 실행 할 수 있다.

    이렇게 unsafe 코드를 작성하면.. CLR에서 언제 메모리정리를 하면서 이동시킬지 모르기 때문에
    저 포인터 주소는 믿을수 없다.. 런타임 에러가 빵빵 터질수도 있다.
    그래서 unsafe 코드는 fixed 와 함께 사용하여야만 한다!
    fixed는 메모리를 고정 시켜주기 때문에 unsafe 와 함께 사용하면 안전하게 쓸 수 있다.
    fixed 는 unsafe 코드 내에서만 사용 가능하다
    fixed 스코프를 나오면 메모리 고정은 끝난다.

    EX) 참고 정도만...
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace TestD
    {
        class Program
        {
            class Point
            {
                public int X;
                public int Y;
            }
            unsafe static void CallByPoint(int* x)
            {
                *x = 10;
            }
            static void Main(string[] args)
            {
                Point pos = new Point();
                unsafe
                {
                    fixed(int* fixedX = &pos.X)
                    {
                        CallByPoint(fixedX);
                    }
                }

                Console.WriteLine("Call-By-Poing:{0}", pos.X);
            }
        }
    }   

    (참고) CLR:
          CLR(common language runtime) 공통 언어 런타임)
          마이크로소프트에서 제공하는 가상 머신의 하나의 요소라고 보면 된다.

          CLR은 마이크로소프트의 .NET 프레임웍의 일부로서, 지원되는 언어 중 어떤 하나로 작성된 프로그램이 공통의 
          객체지향형 클래스를 공유할 수 있도록 해주는 실행 관리 프로그램이다. 
          CLR은 자바 언어로 컴파일된 프로그램을 실행시키기 위해 썬 마이크로시스템즈가 제공하는 자바 가상머신에 
          어느 정도 비교될만하다. 
          마이크로소프트는 자사의 CLR을 하나의 "관리 실행 환경"이라고 지칭한다. 
          CLR용으로 컴파일된 프로그램은 반드시 해당 언어 고유의 실행 환경만을 고집하지 않으며, 
          윈도우7이나 윈도우10가 돌아가는 시스템이라면 어디서라도 실행될 수 있다.

          비주얼 베이직, 비주얼 C++ 또는 C# 언어를 쓰는 프로그래머들이 자신들의 프로그램을 이식 가능 실행 파일 내에서 
          CIL이라는 중간 형태의 코드로 컴파일하고 나면, 이를 CLR을 통해 관리하고 실행시킬 수 있다. 
          프로그래머가 컴파일할 때 그에 관한 서술적 정보를 지정하면, 이 정보는 컴파일된 프로그램과 함께 메타데이터로서 저장된다. 
          컴파일된 프로그램 내에 저장된 메타데이터는, 사용된 언어의 종류와 버전, 그리고 그 프로그램에 필요한 
          클래스 라이브러리는 무엇인지 등에 관한 정보를 CLR에게 알려 준다. 
          CLR은 한 언어로 작성된 클래스의 인스턴스가 다른 언어로 작성된 클래스의 메쏘드를 호출할 수 있게 해주며, 
          자투리 모으기와 예외 처리 및 디버깅 서비스 등도 제공한다.

          가비지콜랙터 :
          C#은 가비지콜랙터(Garbage Collector)를 가지고 있다.
          이 GC는 기본적으로 관리되는 모든 객체들의 참조 링크를 관리하며 더이상 참조되지 않는 객체들을
          자동으로 메모리에서 소거하는 작업을 수행함. 
          그러나 GC는 창 핸들, 열린 파일, 스트림과 같이 관리되지 않는 리소스들을 인식하지 못함.
 
*/

/*
 
        유니티가 지원하는 형 변환

        System.Convert.ToInt32(...);
        System.Convert.ToBoolean(...);
        int totalM = int.Parse( inputMoney.text ) * gameCount;
        totalMoney.text = totalM.ToString();
        returnMoney.text = returnMo.ToString();
 
*/

