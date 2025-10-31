using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices; // 마샬링 관련
using System;   // IntPtr 관련

public class Study1 : MonoBehaviour
{
    //UnityDllCpp의 전역변수를 사용하기 때문에 Study1의 내부변수 a는
    //함수 호출과 무관하다
    //int a;
    public int[] arr1;

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
        // 초기화 안하면 전역 변수 계속 올라간다...ㅎㅎㅎ 따라서 리셋 관련 함수 만들어 써야함...
        Debug.Log(GetNum3());
        Debug.Log(GetNum3());
        GetNumArr1();

        // 이렇게 출력하면 에러 및 값을 전부 가져오지 못함.
        //for (int i = 0; i < 100; i++)
        //{
        //    Debug.Log(arr1[i]);
        //}
    }

    /*
       플레이어 세팅에서 allow unsafe code 체크
     */
    unsafe void GetNumArr1()
    {
        // IntPtr은 포인터를 저정할 수 있는 구조체이지 포인터는 아니다.
        // using System; 선언, 포인터란...ㅋㅋ
        IntPtr data = GetNum4();
        // 주소값
        data.ToString();
        // 주소값 출력
        Debug.Log(data.ToString());
        // IntPtr 구조체에 포인터로 변환해주는 메소드 사용
        // 리턴 타입이 void*이므로 형변환
        // 유니티에는 포인터개념이 없으므로 unsafe 키워드를 통해 허용
        // 유니티 에디터 project setting에서 unsafe 코드 허용하도록 설정 체크
        // C#에서는 포인터가 없으므로 int*를 var로 처리
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