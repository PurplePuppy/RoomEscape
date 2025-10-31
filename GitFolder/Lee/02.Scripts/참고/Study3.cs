using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  MonoBehaviour가 아닌 클래스에 대해 Inspector에 나타내기.
[System.Serializable]
public class MonsterPower
{
    public int[] power;
}

public class Study3 : MonoBehaviour
{
    public int[] arr1;
    // 기본적으로 인스펙터에 노출 안됨. 물론 나중에 커스텀으로 노출 시킬수 있지만 아직은 참자...
    public int[,] arr2 = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
    // 1차원
    public MonsterPower monsPower1;
    // 2차원 : 이런식으로 다차원 배열을 표현하면 편하다. 실지로도 많이들 이렇게 쓰고 인스펙터에 노출도 됨
    public MonsterPower[] monsPower2;

    // Start is called before the first frame update
    void Start()
    {
        //int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        //Debug.Log(array2D[2,1]);
        //int[,] array1 = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        //Debug.Log(array1[3, 0]);
        //int[,] array2; // 선언과 동시에 초기화가 아니면 반드시 아래와 같이!!!
        //array2 = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
        //Debug.Log(array2[3, 1]);

        // 1차원배열 (numbers).
        int[] n1 = new int[3] { 2, 4, 6 };
        int[] n2 = new int[] { 2, 4, 6 };
        int[] n3 = { 2, 4, 6 };
        ArrDisplay(n1, n2, n3);
        // 1차원배열 (strings).
        string[] s1 = new string[3] { "onj1", "oraclejava1", "onjoracle1" };
        string[] s2 = new string[] { "onj2", "oraclejava2", "onjoracle2" };
        string[] s3 = { "onj3", "oraclejava3", "onjoracle3" };
        ArrDisplay(s1, s2, s3);
        // 다차원배열
        int[,] n4 = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
        int[,] n5 = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
        int[,] n6 = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
        ArrDisplay(n4);
        ArrDisplay(n5);
        ArrDisplay(n6);
        // 가변길이 배열
        int[][] n7 = new int[2][] { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
        int[][] n8 = new int[][] { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
        int[][] n9 = { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
        ArrDisplay(n7, n8, n9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void ArrDisplay(params int[][] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            foreach (int j in arr[i])
            {
                Debug.Log(j);
            }
            Debug.Log("----------");
        }
    }

    static void ArrDisplay(int[,] arr)
    {

        foreach (int j in arr)
        {
            Debug.Log(j);
        }

    }
    static void ArrDisplay(params int[][][] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr[i].Length; j++)
            {
                foreach (int k in arr[i][j])
                {
                    Debug.Log(k);
                }
            }
            Debug.Log("----------");
        }

    }
    static void ArrDisplay(params string[][] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            foreach (string j in arr[i])
            {
                Debug.Log(j);
            }
            Debug.Log("----------");
        }
    }
}


/*
 
        1. 1차원배열

        선언 

        int[] arr = new int[3]

        이 배열에는 array[0]에서 array[2]까지의 요소가 있고 배열을 생성하고 배열 요소를 기본값으로 
        초기화하려면 new 연산자를 사용한다. 위 예제에서는 모든 배열 요소를 0으로 초기화 한다.
        같은 방법으로 문자열 요소를 저장하는 배열을 선언할 수 있는데…
        string[] strArr = new string[5];

        초기화

        선언 시 배열을 초기화할 수 있으며, 이런 경우 차수는 초기화 목록의 요소 수로 지정되므로 별도로 지정할 필요가 없다. 
        int[] array1 = new int[] { 1, 3, 5 };

        문자열 배열도 초기화할 수 있다. 
        string[] week = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

        선언 시 배열을 초기화할 경우
        int[] array2 = { 1, 3, 5};
        string[] week = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

        배열 변수를 초기화하지 않고 선언할 수 있지만 이러한 변수에 배열을 할당하려면 new 연산자를 사용해야 한다.
        int[] array3;
        array3 = new int[] { 1, 3, 5, 7, 9 };   // OK, 반드시 new 할 것
        //array3 = {1, 3, 5, 7, 9};   // Error

        값 형식 및 참조 형식 배열
        SomeType[] array4 = new SomeType[10];
        이 선언의 결과는 SomeType이 값 형식인지 또는 참조 형식인지에 따라 달라진다. 
        값 형식인 경우 문에는 요소의 배열 10개가 생성되는데, 각 배열에는 SomeType 형식이 있다. 
        SomeType이 참조 형식인 경우 선언의 결과로 10개의 요소로 구성된 배열이 생성되며 각 요소는 null 참조로 초기화된다.



        2. 다차원 배열

        배열은 차원을 하나 이상 가질 수 있다.
        int[,] arr = new int[4, 2];   //4행2열의 2차원배열 (cf [깊이, 넓이]로 생각하면 쉬움)

        다음 선언은 4 x 2 x 3의 3차원 배열이다.(z, y, x)
        int[, ,] arr = new int[4, 2, 3];


        배열 초기화

        다음 예제처럼 선언 시에 배열을 초기화할 수 있다.
        // Two-dimensional array.
        int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

        // The same array with dimensions specified.
        int[,] array2Da = new int[4, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

        // A similar array with string elements.
        string[,] array2Db = new string[3, 2] { { "one", "two" }, { "three", "four" },
                                                { "five", "six" } };

        // Three-dimensional array.
        int[, ,] array3D = new int[,,] { { { 1, 2, 3 }, { 4, 5, 6 } }, 
                                         { { 7, 8, 9 }, { 10, 11, 12 } } };
        // The same array with dimensions specified.
        int[, ,] array3Da = new int[2, 2, 3] { { { 1, 2, 3 }, { 4, 5, 6 } }, 
                                               { { 7, 8, 9 }, { 10, 11, 12 } } };

        // Accessing array elements. (c# 콘솔 프로젝트) (우린 Debug.Log() )
        System.Console.WriteLine(array2D[0, 0]);
        System.Console.WriteLine(array2D[0, 1]);
        System.Console.WriteLine(array2D[1, 0]);
        System.Console.WriteLine(array2D[1, 1]);
        System.Console.WriteLine(array2D[3, 0]);
        System.Console.WriteLine(array2Db[1, 0]);
        System.Console.WriteLine(array3Da[1, 0, 1]);
        System.Console.WriteLine(array3D[1, 1, 2]);

        // Output:
        // 1
        // 2
        // 3
        // 4
        // 7
        // three
        // 8
        // 12


        또한 다음 예 처럼 차수를 지정하지 않고 배열을 초기화할 수 있다.
        int[,] array4 = { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };

        배열 변수를 초기화 없이 선언한 경우, 배열 변수에 배열을 할당하려면 new 연산자를 사용해야 한다. 
        int[,] array5;
        array5 = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };   // OK, 반드시 new할 것
        //array5 = {{1,2}, {3,4}, {5,6}, {7,8}};   // Error

        다음 예제는 특정 배열 요소에 값을 할당한다.
        array5[2, 1] = 25;

        3. 가변길이 배열

        가변 배열의 요소에도 배열이 사용된다. 가변 배열의 요소는 다양한 차원과 크기를 가질 수 있는데 
        이러한 가변 배열을 "배열의 배열"이라고도 한다. 

        아래는 3개의 요소를 가진 1차원 배열의 선언이며 이 배열의 각 요소는 1차원 정수 배열이다.

        int[][] onj = new int[3][];


        onj를 사용하려면 먼저 요소를 초기화해야 한다. 다음과 같이 요소를 초기화할 수 있다.

        onj [0] = new int[3];
        onj [1] = new int[4];
        onj [2] = new int[2];


        초기 값을 사용하여 배열 요소를 값으로 채울 수도 있는데 이 경우 배열 크기를 지정할 필요가 없다. 

        onj [0] = new int[] { 1, 2, 5 };
        onj [1] = new int[] { 1, 2, 4, 6 };
        onj [2] = new int[] { 11, 22 };


        아래처럼  선언 시 배열을 초기화할 수 있다.

        int[][] onj = new int[][] 
        {
            new int[] {1,3,5,7,9},
            new int[] {0,2,4,6},
            new int[] {11,22}
        };


        아래와 같이 약식 표기를 사용할 수도 있다. 요소에 대한 기본 초기화가 없으므로 요소 초기화에는 new를 해야 한다.

        int[][] onj = 
        {
            new int[] {1,3,5,7,9},
            new int[] {0,2,4,6},
            new int[] {11,22}
        };


        가변 배열은 배열의 배열이므로, 각 요소는 참조 형식이고 null로 초기화 된다.아래 예제처럼 개별 배열 요소에 액세스할 수 있다.

        onj [0][1] = 77;
        onj [2][1] = 88;


        가변 배열과 다차원 배열을 함께 사용할 수 있다. 아래는 서로 다른 크기의 세 개의 2차원 배열 요소를 갖는 1차원 가변 배열의 선언 및 초기화다. 

        int[][,] onj = new int[3][,] 
        {
            new int[,] { {1,3}, {5,7} },
            new int[,] { {0,2}, {4,6}, {8,10} },
            new int[,] { {11,22}, {99,88}, {0,9} } 
        };



        Length 메서드는 가변 배열에 포함된 배열의 수를 반환한다. 

        System.Console.WriteLine(onj.Length); (c# 콘솔 프로젝트) (우린 Debug.Log(onj.Length))
        [결과] 3
 
 
 
        [배열 초기화 및 출력 예제]
 
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using System.Text;
        namespace ConsoleApplication3
        {
            class Program
            {
                static void ArrDisplay(params int[][] arr)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        foreach (int j in arr[i])
                        {
                            Console.WriteLine(j);
                        }
                        Console.WriteLine("----------");
                    }            
                }
                static void ArrDisplay(int[,] arr)
                {
            
                        foreach (int j in arr)
                        {
                            Console.WriteLine(j);
                        }
                              
                }
                static void ArrDisplay(params int[][][] arr)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        for (int j = 0; j < arr[i].Length; j++)
                        {
                            foreach (int k in arr[i][j])
                            {
                                Console.WriteLine(k);
                            }
                        }
                        Console.WriteLine("----------");
                    }    
            
                }
                static void ArrDisplay(params string[][] arr)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        foreach (string j in arr[i])
                        {
                            Console.WriteLine(j);
                        }
                        Console.WriteLine("----------");
                    }    
                }      
                static void Main(string[] args)
                {
                    // 1차원배열 (numbers).
                    // static void ArrDisplay(params int[][] arr)
                    int[] n1 = new int[3] { 2, 4, 6 };
                    int[] n2 = new int[] { 2, 4, 6 };
                    int[] n3 = { 2, 4, 6 };
                    ArrDisplay(n1, n2, n3);
                    // 1차원배열 (strings).
                    // static void ArrDisplay(params string[][] arr)
                    string[] s1 = new string[3] { "onj1", "oraclejava1", "onjoracle1" };
                    string[] s2 = new string[] { "onj2", "oraclejava2", "onjoracle2" };
                    string[] s3 = { "onj3", "oraclejava3", "onjoracle3" };
                    ArrDisplay(s1, s2, s3);
                    // 다차원배열
                    //  static void ArrDisplay(int[,] arr)
                    int[,] n4 = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
                    int[,] n5 = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
                    int[,] n6 = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
                    ArrDisplay(n4);
                    ArrDisplay(n5);
                    ArrDisplay(n6);
                    // 가변길이 배열 
                    // 가변 길이 배열 3개 받는 함수 호출  static void ArrDisplay(params int[][][] arr)
                    int[][] n7 = new int[2][] { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
                    int[][] n8 = new int[][] { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
                    int[][] n9 = { new int[] { 2, 4, 6 }, new int[] { 1, 3, 5 } };
                    ArrDisplay(n7, n8, n9);
                }
            }
        }
 
        [결과]

                2       4       6
                2       4       6
                2       4       6
                onj1    oraclejava1     onjoracle1
                onj2    oraclejava2     onjoracle2
                onj3    oraclejava3     onjoracle3
                1       2       3       4       5       6
                1       2       3       4       5       6
                1       2       3       4       5       6
                2       4       6       1       3       5
                2       4       6       1       3       5
                2       4       6       1       3       5


        (추가 문법 공부)

         params 키워드 :
         일반적으로 1차원 배열의 매개변수를 가지고 있으면 아래처럼 설정 및 호출하게 된다.

            public void PrintParams (int[] items) {
                foreach (var item in items) {
                    Console.WriteLine(item);
                }
            }
 
            public Params(int[] items) {
                // 호출 방법
                PrintParams(items);
                PrintParams(new int[] { 1, 2, 3, 4, 5 });
            }

        PrintParams(new int[] { 1, 2, 3, 4, 5 }); 처럼, 
        new 를 이용해서 배열을 생성하고 값 설정해주고 조금 복잡스럽다.
        그래서 params 키워드를 이용하면 조금 더 편리하게 호출할 수 있다.

            // 즉, params==파라메터로 받겠다란 선언
            public void PrintParams(params int[] items) {
                foreach (var item in items) {
                    Console.WriteLine(item);
                }
            }
 
            public Params(int[] items) {
                // 호출방법
                PrintParams(items);
                PrintParams(new int[] { 1, 2, 3, 4, 5 });
    
                // params라 가능한 호출 방법
                PrintParams(1, 2, 3, 4, 5);
            }

        기존에 호출방법도 가능하지만, PrintParams(1, 2, 3, 4, 5);처럼 호출하는 것도 가능하다.
        즉, n개의 매개변수를 호출할 수 있다는 장점이 있다.
        (가변 개수의 인수를 사용하는 메서드 매개 변수를 지정할 수 있다. 매개 변수 배열은 1차원 배열이어야 한다.)
        하지만 유의해야할 사항도 있다.
        params 키워드는 1차원 배열만 가능하고, params 매개변수는 매개변수 마지막에 있어야 한다. 
        (아래와 같은 오류가 발생할 수 있으니, 마지막에 있어야 한다.)

        ex) 
            public void PrintParams(params int[] items, int item){}



       
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        헷갈리지 말자!!!

        // 가변 배열
        int[][] jaggedArray = { new int[] {1,2,3,4},
                                new int[] {5,6,7},
                                new int[] {8},
                                new int[] {9}
                              };
        
        // 다차원 배열
        int [,] multiDimArray = {{1,2,3,4},
                                 {5,6,7,0},
                                 {8,0,0,0},
                                 {9,0,0,0}
                                };

*/